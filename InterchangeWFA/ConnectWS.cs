using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace InterchangeWFA.Objects
{

    public class Templates : GenericObject
    {

        private int templateVendor = 0;
        public int TemplateVendor
        {
            get { return templateVendor; }
            set { templateVendor = value; }
        }

        private byte[] template = new byte[4096];
        public byte[] Template
        {
            get { return template; }
            set
            {
                template = value;
                Array.Resize<byte>(ref template, value.Length);
                templateSize = (short)template.Length;
            }
        }

        private short templateSize = 0;
        public short TemplateSize
        {
            get { return templateSize; }
            set { templateSize = value; }
        }

        public override byte[] Bytes
        {
            get
            {
                Reset();
                Add((short)template.Length);
                Add(template);
                Resize();
                return bytes;
            }
        }

    }

    public class ConnectWS
    {
        //public ConnectWS() { }

        private string url;
        private string host;
        private string userdig;
        private string senhadig;

        // para configuração banco de dados
        private string ip;
        private string porta;
        private string instancia;
        private string userdb;
        private string senhadb;
        private string connectionstring;

        // Para Configuração WSDL
        public string Url { 
            get{
                return this.url;
            } 
            set{
                this.url = value;
            }
        }

        public string Host {
            get
            {
                return this.host;
            }
            set
            {
                this.host = value;
            }
        }

        public string Userdig {
            get
            {
                return this.userdig;
            }
            set
            {
                this.userdig = value;
            }
        }

        public string Senhadig {
            get
            {
                return this.senhadig;
            }
            set
            {
                this.senhadig = value;
            }        
        }

        // Para Configuração Banco de Dados
        public string Ip {
            get
            {
                return this.ip;
            }
            set
            {
                this.ip = value;
            }           
        }

        public string Porta {
            get
            {
                return this.porta;
            }
            set
            {
                this.porta = value;
            }           
        }

        public string Instancia {
            get
            {
                return this.instancia;
            }
            set
            {
                this.instancia = value;
            }   
        }

        public string Userdb {
            get
            {
                return this.userdb;
            }
            set
            {
                this.userdb = value;
            }   
        }

        public string Senhadb {
            get
            {
                return this.senhadb;
            }
            set
            {
                this.senhadb = value;
            }           
        }

        public string Connectionstring {
            get
            {
                return this.connectionstring;
            }
            set
            {
                this.connectionstring = value;
            }   
        }

        // Campos para Requisição
        private string[] matricula = new string[3309];
        private string[] nome = new string[3309];
        private string[] numPis = new string[3309];
        private string[] numMifare = new string[3309];
        /*private string codigoBarras;
        private string numRfid;
        private int tipoIdentificacaoEmpresa;
        private string cnpj;
        private string setor;
        private int permiteTeclado;
        private string cei;*/
        private string[] tbio1 = new string[3309];
        private string[] tbio2 = new string[3309];

        // Campos para Requisição
        public string[] Matricula
        {
            get
            {
                return this.matricula;
            }
            set
            {
                this.matricula = value;
            }
        }

        public string[] Nome
        {
            get
            {
                return this.nome;
            }
            set
            {
                this.nome = value;
            }
        }

        public string[] NumPis
        {
            get
            {
                return this.numPis;
            }
            set
            {
                this.numPis = value;
            }
        }

        public string[] NumMifare
        {
            get
            {
                return this.numMifare;
            }
            set
            {
                this.numMifare = value;
            }
        }

        public string[] Tbio1
        {
            get
            {
                return this.tbio1;
            }
            set
            {
                this.tbio1 = value;
            }
        }

        public string[] Tbio2
        {
            get
            {
                return this.tbio2;
            }
            set
            {
                this.tbio2 = value;
            }
        }

        private Templates[] template;
        public Templates[] Template
        {
            get
            {
                return template;
            }
        }

        private byte[] tembio = new byte[170];

        public byte[] Tembio
        {
            get
            {
                return tembio;
            }
            set
            {
                this.tembio = value;
            }
        }
    }

} //end namespace

