using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace InterchangeWFA.Objects
{
    //protected byte[] bytes = new byte[1024];

    public enum ListType
    {
        PERMISSION = 1,
        HOLIDAY = 2,
        ACCESS = 3,
        TEMPLATE = 4,
        BLOCK = 5,
        EMPLOYEE = 6,
        SNACK_TIME = 7,
        PWD = 8,
        EMPLOYEECLOCK = 9,
        PLATE = 10,
        FACIAL = 11
    }

    [Serializable]
    public abstract class GenericObject : IGenericObject
    {
        protected byte[] bytes = new byte[1024];
        public abstract byte[] Bytes { get; }
        protected static readonly String INVALID_VALUE_MSG = "Invalid value";
         
        private int deviceID;
        public int DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }

        protected int pos = 0;

        protected void Reset()
        {
            pos = 0;
        }
        
        protected IPAddress ConvertStringToIPAddress(String value, String param)
        {
            String[] ip = value.Split('.');
            if (ip.Length != 4)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }
            return new IPAddress(new byte[] { (byte)Convert.ToInt32(ip[0]), (byte)Convert.ToInt32(ip[1]), (byte)Convert.ToInt32(ip[2]), (byte)Convert.ToInt32(ip[3]) });
        }

		protected short ConvertStringToDays(String value, String param)
        {
            String[] date = value.Split('/');
            short spanned = 0;
            if (date.Length != 3)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }
            DateTime beginningDate = new DateTime(1970, 1, 1);
            DateTime actualDate = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]), 0, 0, 0);
            long elapsedTicks = actualDate.Ticks - beginningDate.Ticks;
            spanned = (short)new TimeSpan(elapsedTicks).Days;
            return spanned;
        }

        protected DateTime ConvertStringToDateTime(String value, String param)
        {
            String[] datetime = value.Split(' ');
            if (datetime.Length != 2)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }

            String[] date = datetime[0].Split('/');
            if (date.Length != 3)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }

            String[] time = datetime[1].Split(':');
            if (date.Length != 3)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }

            return new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]), Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
        }

        /// <summary>
        /// ValidateString, verify limit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected String ValidateString(String value, String param)
        {
            //if (value == null || value.Length > 16)
            if (value.Length > 16)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }
            return value;
        }
        
        protected byte ConvertIntBoolToByteBool(int value, String param)
        {
            if (value != 0 && value != 1)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }
            if (value == 1)
                return 0x01;
            else
                return 0x00;
        }

        protected int ConvertByteBoolToIntBool(byte value)
        {
            if (value == 0x01)
                return 1;
            else
                return 0;
        }

        protected byte[] ConvertStringToBytes(String value, int size, String param)
        {
            String[] aux = value.Split(';');
            byte[] buffer = new byte[size];

            if (aux.Length != size)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }

            for (int p = 0; p < aux.Length; p++)
            {
                try
                {
                    buffer[p] = Convert.ToByte(aux[p]) ;                  
                }
                catch
                {
                    throw new ArgumentException(INVALID_VALUE_MSG + " in position " + p.ToString() + " . ", param);
                }
            }           
            return buffer;
        }

        protected String ConvertBytesToString(byte[] values)
        {
            String s = "";
            for (int i = 0; i < values.Length - 1; i++)
            {
                s += values[i] + ";";
            }
            s += values[values.Length - 1];
            return s;
        }

        #region Masks
        protected byte[] ConvertStringToBitmask(String value, int size, String param)
        {
            if (value == null)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }
            
            int totBytes = size/8;
            if(totBytes == 0){
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }

            String[] aux = value.Split(';');
            if (aux.Length < (totBytes * 8))
            {
                Array.Resize<String>(ref aux, size);
                for (int x = value.Split(';').Length; x < size; x++) aux[x] = "0";                
            }
                    if (aux.Length != size || (totBytes * 8) != size)
                    {
                        throw new ArgumentException(INVALID_VALUE_MSG, param);
                    }

            byte[] ret = new byte[size/8];

            for (int p = 0; p < aux.Length; p++) 
            {
                if (!aux[p].Equals("1") && !aux[p].Equals("0"))
                {
                    throw new ArgumentException(INVALID_VALUE_MSG, param);
                    
                }
            }

            int i = 0;
            for (int q = ret.Length - 1; q >= 0 ; q--)
            {
                if (aux[i++].Equals("1"))
                {
                    ret[q] += 0x01;
                }
                if (aux[i++].Equals("1"))
                {
                    ret[q] += 0x02;
                }
                if (aux[i++].Equals("1"))
                {
                    ret[q] += 0x04;
                }
                if (aux[i++].Equals("1"))
                {
                    ret[q] += 0x08;
                }
                if (aux[i++].Equals("1"))
                {
                    ret[q] += 0x10;
                }
                if (aux[i++].Equals("1"))
                {
                    ret[q] += 0x20;
                }
                if (aux[i++].Equals("1"))
                {
                    ret[q] += 0x40;
                }
                if (aux[i++].Equals("1"))
                {
                    ret[q] += 0x80;
                }
            }
            return ret;
        }

        protected String ConvertBitmaskToString(byte[] values)
        {
            String ret = "";
            foreach (byte b in values)
            {
                if ((b & (1 << 7)) != 0)
                {
                    ret += "1;";
                }
                else
                {
                    ret += "0;";
                }

                if ((b & (1 << 6)) != 0)
                {
                    ret += "1;";
                }
                else
                {
                    ret += "0;";
                }

                if ((b & (1 << 5)) != 0)
                {
                    ret += "1;";
                }
                else
                {
                    ret += "0;";
                }

                if ((b & (1 << 4)) != 0)
                {
                    ret += "1;";
                }
                else
                {
                    ret += "0;";
                }

                if ((b & (1 << 3)) != 0)
                {
                    ret += "1;";
                }
                else
                {
                    ret += "0;";
                }

                if ((b & (1 << 2)) != 0)
                {
                    ret += "1;";
                }
                else
                {
                    ret += "0;";
                }

                if ((b & (1 << 1)) != 0)
                {
                    ret += "1;";
                }
                else
                {
                    ret += "0;";
                }

                if ((b & (1 << 0)) != 0)
                {
                    ret += "1;";
                }
                else
                {
                    ret += "0;";
                }

            }
            ret = ret.Substring(0, ret.Length - 1);
            return new string(ret.ToCharArray().Reverse().ToArray());
        }

        //set bits 
        protected String ConvertBitmaskToStringBit(byte value)
        {
            String ret = "";

            //if ((value & (1 << 7)) != 0)
            //{
            //    ret += "1;";
            //}
            //else
            //{
            //    ret += "0;";
            //}

            //if ((value & (1 << 6)) != 0)
            //{
            //    ret += "1;";
            //}
            //else
            //{
            //    ret += "0;";
            //}

            //if ((value & (1 << 5)) != 0)
            //{
            //    ret += "1;";
            //}
            //else
            //{
            //    ret += "0;";
            //}

            //if ((value & (1 << 4)) != 0)
            //{
            //    ret += "1;";
            //}
            //else
            //{
            //    ret += "0;";
            //}

            //if ((value & (1 << 3)) != 0)
            //{
            //    ret += "1;";
            //}
            //else
            //{
            //    ret += "0;";
            //}
            if ((value & (1 << 2)) != 0)
            {
                ret += "1;";
            }
            else
            {
                ret += "0;";
            }

            if ((value & (1 << 1)) != 0)
            {
                ret += "1;";
            }
            else
            {
                ret += "0;";
            }

            if ((value & (1 << 0)) != 0)
            {
                ret += "1;";
            }
            else
            {
                ret += "0;";
            }

            ret = ret.Substring(0, ret.Length - 1);
            return new string(ret.ToCharArray().Reverse().ToArray());    
        }
        //get bits
        protected byte ConvertStringToBitmaskBit(String value, int size, String param)
        {
            if (value == null)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }

            String[] aux = value.Split(';');
            int len = aux.Length;

            if (size != aux.Length || size > 8)
            {
                throw new ArgumentException(INVALID_VALUE_MSG, param);
            }

            if (aux.Length < 7)
            {
                Array.Resize<String>(ref aux, 8);
                for (int x = 7; x >= len; x -- ) aux[x] += "0";
            }

            byte ret = new byte();

            if (aux[0].Equals("1"))
            {
                ret = (byte)(ret | 1);
            }
            if (aux[1].Equals("1"))
            {
                ret = (byte)(ret | 2);
            }
            if (aux[2].Equals("1"))
            {
                ret = (byte)(ret | 4);
            }
            if (aux[3].Equals("1"))
            {
                ret = (byte)(ret | 8);
            }
            if (aux[4].Equals("1"))
            {
                ret = (byte)(ret | 1);
            }
            if (aux[5].Equals("1"))
            {
                ret = (byte)(ret | 2);
            }
            if (aux[6].Equals("1"))
            {
                ret = (byte)(ret | 4);
            }
            if (aux[7].Equals("1"))
            {
                ret = (byte)(ret | 8);
            }              

            return ret;
        }
        #endregion  Masks       

        protected void Add(object value)
        {
            try
            {
                if (value is int)
                {
                    addInt((int)value);
                }
                if (value is long)
                {
                    addLong((long)value);
                }
                else if (value is short)
                {
                    addShort((short)value);
                }
                else if (value is String)
                {
                    addString((String)value);
                }
                else if (value is byte[])
                {
                    addBytes((byte[])value);
                }
                else if (value is byte)
                {
                    addByte((byte)value); ;
                }
                else if (value is DateTime)
                {
                    addDateTime((DateTime)value);
                }
                else if (value is IPAddress) {
                    addIPAddress((IPAddress)value);
                }
                else if (value is bool) {
                    addBool((bool)value);
                }
                else if (value is ushort)
                {
                    addShort((ushort)value);
                }
                if (value is uint)
                {
                    addInt((uint)value);
                }

                //else if (value == null)
                //{
                //    Console.WriteLine(Console.WriteLineType.WARN, "null");
                //}
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                //Console.WriteLine(Console.WriteLineType.WARN, "Tentando adicionar parametro inválido "+value+" - " + deviceID+" - "+e.Message);
            }
        }

        protected short convertShortDate(DateTime date)
        {
            return Get1970DateTimeShort(date);
        }

        private void addBool(bool p)
        {
            if (p)
            {
                addByte(0x01);
            }
            else 
            {
                addByte(0x00);
            }
        }

        protected long ConvertBytesToLong(byte[] longByte)
        {
            if (longByte.Length > 8 || longByte == null)
            {
                Console.WriteLine("Error ConvertBytesToLong...");
                //Console.WriteLine(Console.WriteLineType.ERROR, "Error ConvertBytesToLong...");
                throw new ArgumentException(INVALID_VALUE_MSG, "ConvertBytesToLong");
            }

            byte[] longBt = new byte[8];
            Array.Copy(longByte,0,longBt,(longBt.Length - longByte.Length), longByte.Length);
            Array.Reverse(longBt);
            return BitConverter.ToInt64(longBt, 0);

        }
        /// <summary>
        /// Convert long to byte array
        /// </summary>
        /// <param name="byteLong">byte</param>
        /// <returns>byte</returns>
        protected byte[] ConvertLongToBytes(long byteLong)
        {
            byte[] longBt = BitConverter.GetBytes(byteLong);
            Array.Reverse(longBt);
            return longBt;
        }

        /// <summary>
        /// Add ints bytes to message
        /// </summary>
        /// <param name="value">int</param>
        private void addInt(int value)
        {
            byte[] intBytes = BitConverter.GetBytes(value);
            Array.Reverse(intBytes);
            checkLength(4);
            Buffer.BlockCopy(intBytes, 0, bytes, pos, 4);
            pos += 4;
        }

        private void addInt(uint value)
        {
            byte[] intBytes = BitConverter.GetBytes(value);
            Array.Reverse(intBytes);
            checkLength(4);
            Buffer.BlockCopy(intBytes, 0, bytes, pos, 4);
            pos += 4;
        }

        private void addLong(long value)
        {
            byte[] longBytes = BitConverter.GetBytes(value);
            Array.Reverse(longBytes);
            checkLength(8);
            Buffer.BlockCopy(longBytes, 0, bytes, pos, 8);
            pos += 8;
        }

        private void addShort(short value)
        {
            byte[] shortBytes = BitConverter.GetBytes(value);
            Array.Reverse(shortBytes);
            checkLength(2);
            Buffer.BlockCopy(shortBytes, 0, bytes, pos, 2);
            pos += 2;
        }

        private void addShort(ushort value)
        {
            byte[] shortBytes = BitConverter.GetBytes(value);
            Array.Reverse(shortBytes);
            checkLength(2);
            Buffer.BlockCopy(shortBytes, 0, bytes, pos, 2);
            pos += 2;
        }

        private void addString(String value)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] strBuff = ascii.GetBytes(value);
            Array.Resize<byte>(ref strBuff, 16);
            checkLength(16);
            Buffer.BlockCopy(strBuff, 0, bytes, pos, 16);
            pos += 16;
        }

        private void addIPAddress(IPAddress iPAddress)
        {
            byte[] ip = iPAddress.GetAddressBytes();
            Array.Resize<byte>(ref ip, 4);
            addBytes(ip);
        }

        private void addBytes(byte[] value)
        {
            checkLength(value.Length);
            Buffer.BlockCopy(value, 0, bytes, pos, value.Length);
            pos += value.Length;
        }

        private void addByte(byte value)
        {
            checkLength(1);
            bytes[pos++] = value;
        }

        private void addDateTime(DateTime dateTime)
        {
            int sendTime = Get1970DateTimeInt(dateTime);
            addInt(sendTime);
        }

        private void checkLength(int size)
        {
            if ((pos + size) >= bytes.Length)
            {
                Array.Resize<byte>(ref bytes, (bytes.Length+size) * 2);
            }
        }

        protected void Resize()
        {
            Array.Resize<byte>(ref bytes, pos);
        }

        protected int GetInt()
        {
            int retValue = 0;
            try
            {
                retValue += ((bytes[pos++] & 0xff) << 24);
                retValue += ((bytes[pos++] & 0xff) << 16);
                retValue += ((bytes[pos++] & 0xff) << 8);
                retValue += (bytes[pos++] & 0xff);
            }
            catch (Exception) { }
            return retValue;
        }

        //please optmize
        protected long GetLong()
        {
            long retlong = 0;
            byte[] longBt = new byte[8];    
            try
            {
               longBt = GetBytes(8);
               retlong = ConvertBytesToLong(longBt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retlong;
        }

        protected short GetShort()
        {
            short retValue = 0;
            try
            {
                retValue += (short)((bytes[pos++] & 0xff) << 8);
                retValue += (short)(bytes[pos++] & 0xff);
            }
            catch (Exception) { }
            return retValue;
        }

        protected byte[] GetBytes(int length)
        {
            byte[] retBytes = null;

            if(length > 0){
                retBytes = new byte[length];
                Buffer.BlockCopy(bytes, pos, retBytes, 0, length);
                pos += length;
            }

            return retBytes;
        }

        protected byte GetByte()
        {
            byte b = 0x00;
            try
            {
                b = bytes[pos++];
            }
            catch (Exception) { }
            return b;
        }

        protected DateTime Get1970DateTime()
        {
            long data = GetInt();
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(data);
        }

        protected int Get1970DateTimeInt(DateTime dateTime)
        {
            DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan span = (dateTime.ToUniversalTime() - Epoch);
            return (int)span.TotalSeconds;
        }

        protected short Get1970DateTimeShort(DateTime dateTime)
        {
            DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan span = (dateTime.ToUniversalTime() - Epoch);
            return (short)span.Days;
        }

        protected short Get1970DateTimeByte(DateTime dateTime)
        {
            DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan span = (dateTime.ToUniversalTime() - Epoch);
            return (short)span.Days;
        }

        public byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;
            try
            {
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }
            catch (Exception _Exception)
            {
                Console.WriteLine(_Exception.Message);
            }
            return _Buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="days">short days since 1970</param>
        /// <returns>String date dd/mm/aaaa</returns>
        public static String ConvertDaysToString(short days)
        {
            return new DateTime(1970, 1, 1).AddDays((long)days).ToString("d");
        }

        public enum ProtocolVersion
        {
            FW_0000 = 0000, //Unknown
            FW_2175 = 2175,
            FW_2180 = 2180,
            FW_5175 = 5175,
            FW_5180 = 5180,
            FW_5190 = 5190,
            FW_5191 = 5191,
            FW_6135 = 6135,
            FW_6136 = 6136,
            FW_6137 = 6137,
            FW_9100 = 9100,
            FW_9110 = 9110,
            FW_9120 = 9120,
            FW_9200 = 9200,
            FW_9210 = 9210,
            FW_9220 = 9220,
            FW_9300 = 9300,
            FW_9310 = 9310,
            FW_9321 = 9321,
            FW_9322 = 9322,
            FW_9323 = 9323,
            FW_9400 = 9400,
            FW_9401 = 9401,
            FW_9404 = 9404,
            FW_9410 = 9410,
            FW_9500 = 9500,
            FW_9600 = 9600
        };

        //Implementations to Rep support
        /// <summary>
        /// Converts a hexadecimal string to an equivalent byte[] with its value
        /// </summary>
        /// <param name="bin">String containing the hexadecimal value to be converted</param>
        /// <returns>value equivalent byte[]</returns>
        public static byte[] ConvertHexString2Value(string hex)
        {
            // If the hex string size is not multiple of 2, it's invalid!
            if (hex.Length % 2 != 0)
            {
                Console.WriteLine("Invalid hex string");
                return null;
            }

            // Creates the byte[] that will receive the result.
            byte[] result = new byte[hex.Length / 2];

            // For the loop...
            int j = 0;

            // Converts each 2 bytes of the input on your equivalent hex value and stores it in the byte[]
            for (int i = 0; i <= hex.Length - 2; i += 2, j++)
            {
                try
                {
                    result[j] = (byte)Convert.ToChar(Int32.Parse(hex.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return result;
        }

        private string LerNumero(BinaryReader r)
        {
            byte[] bytesNum = new byte[23];

            r.Read(bytesNum, 0, 23);

            return Encoding.ASCII.GetString(bytesNum);
        }

        private byte[] LerTemplate(BinaryReader r)
        {
            byte tamanho = r.ReadByte();
            byte[] template = new byte[tamanho];

            r.Read(template, 0, tamanho);

            return template;
        }

        private byte[] RetornarBytesCartao(long cartao)
        {
            var bytes = new byte[5];

            for (int i = 0; i < 5; i++)
            {
                bytes[i] = (byte)(cartao / (Math.Pow(256, 4 - i)) % 256);
            }

            return bytes;
        }

        public bool VersionIsAlowed(int firmwareVersion)
        {
            try
            {
                //remove release and build 
                firmwareVersion = (firmwareVersion / 100) * 100;
                return Enum.IsDefined(typeof(ProtocolVersion), firmwareVersion);
            }
            catch
            {
                return false;
            }
        }
    }
}
