using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.IO;
using System.Data;
using System.Linq;
using System.Data.OleDb;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Microsoft.Web.Services2.Messaging;
using Microsoft.Web.Services3;
using InterchangeWFA.Objects;
using InterchangeWFA.DiginetWebServiceRef;
using DigiconFrameworkServer.Objects.CommandObjects;

namespace InterchangeWFA
{
    public partial class frmMain : Form
    {
        string query;
        string path;
        string path_res;
        string path_dump;
        OleDbConnection OleDbconn;
        OleDbDataAdapter OleDbAdapter;
        OleDbDataReader OleDbReader;
        OleDbCommand OleDbcmd = new OleDbCommand();
        OleDbCommandBuilder OldDbcmdBuilder;       
        DataSet datasetBD = new DataSet();
        DataSet dtsetUpdate = new DataSet();
        DataTable dataTableBD = new DataTable();
        ConnectWS Configs = new ConnectWS();
        Service servico = new Service();
        DataBase dbProp;

        List<ConnectWS> userRecords = new List<ConnectWS>();

        //Timer and associated variables
        //private static System.Timers.Timer timerClock;
        //private int clockTime = 0;
        //private int alarmTime = 0;

        DateTime dt_inicio;
        DateTime dt_fim;
        TimeSpan ts_diff;

        DiginetWebServiceRef.DigiNetWSService consumirWS = new DiginetWebServiceRef.DigiNetWSService();

        public frmMain()
        {
            InitializeComponent();
        }
       
        private void btnStop_Click(object sender, EventArgs e)
        {
            MessageBox.Show("STOP", "Interchange", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            // Fecha o BD
            OleDbconn.Close();           
        }      

        private void rTBDisplayLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void dGVDiginetWS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Localiza Diretorio contém os configs
            path = AppDomain.CurrentDomain.BaseDirectory;
            path += "interchange.ini";

            // Importa as configurações           
            servico.ImportConfig(Configs, path);
            
            this.load_employe_dclockTableAdapter.Fill(this.rondaGravatai.load_employe_dclock);

        }

        //private static void SetTimer()
        //{
        //    
        //    timerClock = new System.Timers.Timer(2000);
        //     
        //    timerClock.Elapsed += OnTimedEvent;
        //    timerClock.AutoReset = true;
        //    timerClock.Enabled = true;
        //}

        //private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        //{
        //    Console.WriteLine("Tempo decorrido {0:HH:mm:ss.fff}",
        //                      e.SignalTime);
        //}

        private void btnStart_Click(object sender, EventArgs e)
        {
            //SetTimer();
            dt_inicio = DateTime.Now;
            Console.WriteLine("Inicio: {0}", dt_inicio);

            // Pasta Local
            path = AppDomain.CurrentDomain.BaseDirectory;
            // Arquivo de Config
            path += "interchange.ini";

            // Pasta Local result
            path_res = AppDomain.CurrentDomain.BaseDirectory;

            MessageBox.Show("Inicio: " + dt_inicio, "Inserir Colaborador", MessageBoxButtons.OK, MessageBoxIcon.Information );
            
            // Importa as configurações
            servico.ImportConfig(Configs, path);
            
            // DB-Inicio                        
            try
            {
                query = "SELECT * FROM load_employe_dclock WHERE TIPTEM IS NOT NULL AND nomfun <> '' ORDER BY nomfun, TIPTEM";
                
                dbProp = new DataBase(Configs);
                dataTableBD = dbProp.ExecuteQuery(query);
                                
                int max = dataTableBD.Rows.Count;

                MessageBox.Show("Total de Registros: " + max.ToString(), "Ronda BD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Atribui na GRID a Tabela               
                dGViewSenior.DataSource = dataTableBD;

                userRecords = dbProp.ObterLista();
                
                string respostaXML;
                //   WSDL-Begin
                try
                {
                    if (servico.Autenticando(Configs) == 0)
                    {
                        respostaXML = servico.ReqSoap_Diginet(Configs, max, userRecords, "insereColaborador");
                        //Console.WriteLine(respostaXML);
                                               
                        dt_fim = DateTime.Now;
                        Console.WriteLine("Processado: {0}", dt_fim);

                        string icpath = dt_fim.ToString();
                        icpath = icpath.Trim();
                        icpath = icpath.Replace("/", "");
                        icpath = icpath.Replace(":", "");
                        icpath = icpath.Replace(" ", "-");

                        // Pasta Local + result
                        path_res += "RetornoCarga_IC_" + icpath + ".txt";

                        // Grava o retorno do WS em um arquivo
                        StreamWriter wr = new StreamWriter(path_res, true);
                        wr.Write(respostaXML);
                        wr.Close();
                        
                        //File.WriteAllText(path_res, respostaXML);

                        // Diferença entre inicio e fim
                        ts_diff = dt_fim.Subtract(dt_inicio);

                        MessageBox.Show("Processado Carga - Tempo: " + ts_diff.TotalMinutes.ToString() + " minutos", "Inserir Colaborador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //timerClock.Stop();
                    //timerClock.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Isto é um erro?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //   WSDL-END

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Isto é um erro?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // DB-End
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            path = AppDomain.CurrentDomain.BaseDirectory;

            path += "interchange.ini";

            try
            {
                servico.ImportConfig(Configs, path);
                MessageBox.Show("Atualizando Configuração", "Interchange", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                OldDbcmdBuilder = new OleDbCommandBuilder(OleDbAdapter);
                dtsetUpdate = datasetBD.GetChanges();
                if (dtsetUpdate != null)
                {
                    OleDbAdapter.Update(datasetBD.Tables[0]);
                }
                datasetBD.AcceptChanges();
                MessageBox.Show("Atualizando Banco de Dados", "Interchange - BD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Isto é um erro?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.load_employe_dclockTableAdapter.FillBy(this.rondaGravatai.load_employe_dclock);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        protected String PathApp
        {
            get
            {
                return Path.GetDirectoryName(Application.ExecutablePath);
            }
        }

        private void btnGravaBio_Click_1(object sender, EventArgs e)
        {
            dt_inicio = DateTime.Now;
            // Pasta Local
            path = AppDomain.CurrentDomain.BaseDirectory;
            path += "interchange.ini";

            // Pasta Local result
            path_res = AppDomain.CurrentDomain.BaseDirectory;

            string msgBox = "Inicio: " + dt_inicio;
            Console.WriteLine(msgBox);
            MessageBox.Show(msgBox, "Gravar Biometria", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Importa as configurações
            servico.ImportConfig(Configs, path.ToString());

            // DB-Inicio                        
            try
            {
                query = "SELECT numpis, TIPTEM, TEMBIO FROM load_employe_dclock WHERE TIPTEM IS NOT NULL ORDER BY numpis, TIPTEM";
                // query = "SELECT * FROM load_employe_dclock WHERE numpis = 20493391007";

                dbProp = new DataBase(Configs);
                dataTableBD = dbProp.ExecuteQuery(query);

                int max = dataTableBD.Rows.Count;
                //int max = 4;

                MessageBox.Show("Total de Registros: " + max.ToString(), "Ronda BD", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Atribui na GRID a Tabela               
                dGViewSenior.DataSource = dataTableBD;

                userRecords = dbProp.ObterListaBio();

                string respostaXML;
                //   WSDL-Begin
                try
                {
                    if (servico.Autenticando(Configs) == 0)
                    {
                        respostaXML = servico.ReqSoap_Diginet(Configs, max, userRecords, "gravarBiometriaHex");
                        //Console.WriteLine(respostaXML);

                        dt_fim = DateTime.Now;
                        Console.WriteLine("Processado: {0}", dt_fim);
                        string gbpath = dt_fim.ToString();
                        gbpath = gbpath.Trim();
                        gbpath = gbpath.Replace("/", "");
                        gbpath = gbpath.Replace(":", "");
                        gbpath = gbpath.Replace(" ", "-");

                        // Pasta Local + result
                        path_res += "RetornoCarga_GB_" + gbpath + ".txt";
                        // Grava o retorno do WS em um arquivo
                        StreamWriter wr = new StreamWriter(path_res, true);
                        wr.Write(respostaXML);
                        wr.Close();

                        // Diferença entre inicio e fim
                        ts_diff = dt_fim.Subtract(dt_inicio);

                        MessageBox.Show("Cargouuuu Tempo: " + ts_diff.TotalMinutes.ToString() + " minutos", "Gravar Biometria", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Isto é um erro?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } //   WSDL-END

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Isto é um erro?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }// DB-End
                            
        }

        public List<Operation> ListbyFile() 
        {
            // Origem do arquivo
            string path_bin = AppDomain.CurrentDomain.BaseDirectory;
            path_bin += "digitais.bin";

            //instância do list que será retornado
            var lista = new List<ConnectWS>();
            var listaOp = new List<Operation>();

            int cont = 0;
            try
            {
                using (var f = new BinaryReader(new FileStream(path_bin, FileMode.Open)))
                {
                    int i = 0;
                    while (f.BaseStream.Position < f.BaseStream.Length && cont > 0)
                    {
                        i++;

                        cont--;
                        var numero = LerNumero(f);
                        var template1 = LerTemplate(f);
                        var template2 = LerTemplate(f);

                        var op = new Operation();
                        var tmp1 = new TemplateListPersonTemplate();
                        var tmp2 = new TemplateListPersonTemplate();
                        var cartao = new TemplateListPersonCard();

                        byte[] cardB = RetornarBytesCartao(long.Parse(numero.Trim()));


                        //op.PersonId.PersonID = numero;

                        tmp1.TamplateFactory = 1;
                        tmp1.Template = template1;

                        tmp2.TamplateFactory = 1;
                        tmp2.Template = template2;

                        cartao.CardID = cardB;
                        cartao.CardTec = 3;

                        op.TemplateListPerson.PersonID = numero;
                        op.TemplateListPerson.Template = new[] { tmp1, tmp2 };
                        op.TemplateListPerson.BioConfLevel = 50;
                        op.TemplateListPerson.Card = new[] { cartao };

                        //-------------------------
                        /*var card = new EmployeeListPersonCard();
                        card.CardID = cardB;
                        card.CardTec = 3;

                        op.EmployeeListPerson.PersonID = numero;
                        op.EmployeeListPerson.Card = new[] { card };

                        //----------------------------------
                        op.PersonId.PersonID = numero;*/

                        //-------------------------------

                        //op.Type = (byte)Convert.ToInt32(comboBox1.SelectedIndex);

                        listaOp.Add(op); ;
                    }
                }

                string respostaXML;
                try
                {
                    if (servico.Autenticando(Configs) == 0)
                    {
                        respostaXML = servico.ReqSoap_Diginet(Configs, 3000, userRecords, "gravarBiometria");
                        //Console.WriteLine(respostaXML);

                        dt_fim = DateTime.Now;
                        Console.WriteLine("Processado: {0}", dt_fim);
                        string gbpath = dt_fim.ToString();
                        gbpath = gbpath.Trim();
                        gbpath = gbpath.Replace("/", "");
                        gbpath = gbpath.Replace(":", "");
                        gbpath = gbpath.Replace(" ", "-");

                        // Pasta Local + result
                        path_res += "RetornoCarga_GB_" + gbpath + ".txt";
                        // Grava o retorno do WS em um arquivo
                        StreamWriter wr = new StreamWriter(path_res, true);
                        wr.Write(respostaXML);
                        wr.Close();

                        // Diferença entre inicio e fim
                        ts_diff = dt_fim.Subtract(dt_inicio);

                        MessageBox.Show("Processado Carga Biometria - Tempo: " + ts_diff.TotalMinutes.ToString() + " minutos");
                    }

                }
                catch
                {
                    MessageBox.Show("Isto é um Erro.");
                }
            }catch( Exception e )
            {
                MessageBox.Show(e.Message, "Isto é um erro?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return listaOp;    
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

        private void dGViewSenior_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // TODO: Futuramente gravar um dump 
            // Pasta Local
            path_dump = AppDomain.CurrentDomain.BaseDirectory;
            // Arquivo de Config
            path_dump += "dump.txt";

            DataGridViewRow row = this.dGViewSenior.Rows[e.RowIndex];

            string temp = "";
            // Grava o retorno do WS em um arquivo
            StreamWriter wr = new StreamWriter(path_dump, true);
            for (int i = 0; i < 3300; i++ )
            {
                temp = dGViewSenior.Rows[i].Cells[i].Value.ToString();
                wr.WriteLine(temp);
            }
            wr.Close();
                       
        }
    }
}
