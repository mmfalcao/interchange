using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Data.OleDb;
using System.Data.SqlServerCe;
using InterchangeWFA.Objects;
using DigiconFrameworkServer.Objects.CommandObjects;

namespace InterchangeWFA
{
    class DataBase
    {
        private String ConnString;

        OleDbConnection OleDbconn;
        OleDbDataAdapter OleDbAdapter;
        OleDbDataReader OleDbReader;
        OleDbCommand OleDbcmd;
        OleDbCommandBuilder OldDbcmdBuilder;
        DataTable dtTab = new DataTable();
        DataSet datasetBD = new DataSet();
        DataSet dtsetUpdate = new DataSet();
        ConnectWS Configs = new ConnectWS();
        Service servico = new Service();
        GenericObject Utils;

        public DataBase()
        {
            this.ConnString = Configs.Connectionstring;
        }

        public DataBase( ConnectWS Configs )
        {
            if (String.Empty != Configs.Ip && String.Empty != Configs.Instancia &&
                String.Empty != Configs.Userdb && String.Empty != Configs.Senhadb )
            {
                this.ConnString = "Provider=SQLOLEDB;Data Source=" + Configs.Ip + ";Initial Catalog=" + Configs.Instancia + ";Persist Security Info=True;User ID=" + Configs.Userdb + "; Password=" + Configs.Senhadb;
            }
            else
            {
                this.ConnString = Configs.Connectionstring;
            }
        }

        public bool TestConn()
        {
            OleDbconn = new OleDbConnection(ConnString);
            try
            {
                OleDbconn.Open();
                if (OleDbconn.State == ConnectionState.Open)
                {
                    OleDbconn.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //método que consulta o bd e me retorna
        //um list de estados
        public List<ConnectWS> ObterLista()
        {
            
            //instância do list que será retornado
            var lista = new List<ConnectWS>();

            //instância da conexão
            OleDbconn = new OleDbConnection(ConnString);

            //comando sql ou seja a query a ser executada
            var sql = "SELECT * FROM load_employe_dclock WHERE TIPTEM IS NOT NULL AND nomfun <> '' ORDER BY nomfun, TIPTEM";

            //instância do comando onde passo a conexão e a string com sql
            OleDbcmd = new OleDbCommand(sql, OleDbconn);
            //abro conexão 
            OleDbconn.Open();
            //instância do leitor
            var leitor = OleDbcmd.ExecuteReader();

            OleDbAdapter = new OleDbDataAdapter(OleDbcmd);


            int count = 0;
            
            //enquanto o leitor lê...
            while (leitor.Read())
            {
                //vinculo os valores lidos do bd ao objeto
                Configs.Nome[count] = leitor["nomfun"].ToString();
                //Console.WriteLine("Configs {0} Nome: {1}", count, Configs.Nome);
                Configs.NumPis[count] = leitor["numpis"].ToString();
                Configs.NumMifare[count] = leitor["numfis"].ToString();
                Configs.Matricula[count] = leitor["idtpes"].ToString();

                //adiciono o objeto a lista
                lista.Add(Configs);
                count +=1;
            }
            //fecho conexão 
            OleDbconn.Close();
            //retorno a lista
            return lista;
        }

        //método que consulta o bd e me retorna
        //um list de estados
        public List<ConnectWS> ObterListaBio()
        {
            //instância do list que será retornado
            var lista = new List<ConnectWS>();
            //var listaOp = new List<Operation>();

            //instância da conexão
            OleDbconn = new OleDbConnection(ConnString);

            //comando sql ou seja a query a ser executada
            var sql = "SELECT numpis, TIPTEM, TEMBIO FROM load_employe_dclock WHERE TIPTEM IS NOT NULL ORDER BY numpis";
            //var sql = "SELECT numpis, TIPTEM, TEMBIO FROM load_employe_dclock WHERE numpis = 20493391007";

            //instância do comando onde passo a conexão e a string com sql
            OleDbcmd = new OleDbCommand(sql, OleDbconn);

            OleDbAdapter = new OleDbDataAdapter(OleDbcmd);

            //abro conexão 
            OleDbconn.Open();

            //instância do OleDBDataReader
            //var leitor = OleDbcmd.ExecuteReader();

            OleDbAdapter.Fill(dtTab);

            int count = 0;

            //int bufferSize = 170;                   // Size of the BLOB buffer.
            //byte[] outbyte = new byte[bufferSize];  // The BLOB byte[] buffer to be filled by GetBytes.
            
            /*string pasta = AppDomain.CurrentDomain.BaseDirectory; 
            MemoryStream ms;*/

            //enquanto o leitor lê...
            //while (leitor.Read())
            while (dtTab.Rows.Count > count)
            {
                //Console.WriteLine("contador {0}", count);
                //vinculo os valores lidos do bd ao objeto
                Configs.NumPis[count] = dtTab.Rows[count]["numpis"].ToString();
                //Console.WriteLine("PIS: {0}", Configs.NumPis);

                Configs.Tbio2[count] = dtTab.Rows[count]["TIPTEM"].ToString();
                //Console.WriteLine("Tipo Template: {0}", Configs.Tbio2);

                //ms = new MemoryStream((byte[])dtTab.Rows[count]["TEMBIO"]);
                //Console.WriteLine("Template ms: {0}", ms );

                Configs.Tembio = (byte[])dtTab.Rows[count]["TEMBIO"];
                //Console.WriteLine("Template List: {0}", Configs.Tembio );

                Configs.Tbio1[count] = BitConverter.ToString(Configs.Tembio).Replace("-", string.Empty);
                //Console.WriteLine("Template Hex: {0}", Configs.Tbio1[count]);


                /*pasta += count;
                pasta += ".txt";
                Console.WriteLine("pasta: {0}", pasta);

                 Grava o retorno do WS em um arquivo
                StreamWriter wr = new StreamWriter(pasta, true);
                wr.WriteLine("PIS: " + Configs.NumPis[count].ToString());
                wr.WriteLine("Tipo Template: " + Configs.Tbio2[count].ToString());
                wr.WriteLine("Template ms: " + ms);
                wr.WriteLine("Tamanho Template List: " + Configs.Tembio.Length);
                wr.WriteLine("Tamanho Template List: " + Configs.Tembio);
                wr.Close();*/

                //adiciono o objeto a lista
                lista.Add(Configs);
                count += 1;
            }
            //fecho conexão 
            OleDbconn.Close();
            //retorno a lista
            return lista;
        }


        public DataTable ExecuteQuery(string sql)
        {
            OleDbconn = new OleDbConnection(ConnString);
            Console.WriteLine("Connection String: " + ConnString);
            try
            {
                OleDbconn.Open();
                Console.WriteLine("Query: " + sql);
                OleDbAdapter = new OleDbDataAdapter(sql, OleDbconn);
                
                OleDbAdapter.Fill(datasetBD);

                if (datasetBD.Tables[0].Rows.Count > 0)
                    return datasetBD.Tables[0];
                else
                    return new DataTable("Empty");
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new ArgumentException(" Parameter Invalid " + ex.Message, "ExecuteQuery");
            }
            finally
            {
                OleDbconn.Close();
            }
        }

        /// <summary>
        /// Method to query non executable as operation of DELETE and INSERT.
        /// </summary>
        /// <param name="sql">Receive parameter type string "sql".</param>
        public void ExecuteNonQuery(string sql)
        {
            OleDbconn = new OleDbConnection(ConnString);
            try
            {
                OleDbconn.Open();
                OleDbcmd = new OleDbCommand(sql, OleDbconn);
                OleDbcmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OleDbconn.Close();
            }
        }
    }
}
