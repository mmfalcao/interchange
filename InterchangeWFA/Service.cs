using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
//using System.Net.Http;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using InterchangeWFA.Objects;
using InterchangeWFA.DiginetWebServiceRef;

namespace InterchangeWFA
{
    class Service 
    {
        DiginetWebServiceRef.DigiNetWSService consumirWS = new DiginetWebServiceRef.DigiNetWSService();

        ConnectWS Config = new ConnectWS();
        ValidateConn ValConn = new ValidateConn();

        public int Autenticando(ConnectWS Config)
        {
            if (!OAuth(Config))
            {
                throw new Exception("Usuario/Senha Invalido");
            }
            return 0;
        }

        // Valida Usuário e Senha
        private bool OAuth(ConnectWS Config)
        {
            if ( !string.IsNullOrEmpty( Config.Userdig ) &&
                 !string.IsNullOrEmpty( Config.Senhadig) &&
                 !string.IsNullOrEmpty( Config.Userdb )  &&
                 !string.IsNullOrEmpty( Config.Senhadb ) )
            {
                if (!Config.Userdig.Equals("admin-digicon") && !Config.Senhadig.Equals("digicon"))
                {
                    return false;
                }

                if (!Config.Userdb.Equals("dclock") && !Config.Senhadb.Equals("dclock@)!%"))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        // Valida campos de configuração
        private bool ChecaConfig(ConnectWS Config)
        {
            if (string.IsNullOrEmpty(Config.Url))
            {
                throw new Exception("URL WebService Invalida");
                //return false;
            }

            if (string.IsNullOrEmpty(Config.Host))
            {
                throw new Exception("Host WebService Invalido");
                //return false;
            }

            if (string.IsNullOrEmpty(Config.Ip))
            {
                throw new Exception("IP Banco do Ronda Invalido");
                //return false;
            }

            if (string.IsNullOrEmpty(Config.Porta))
            {
                throw new Exception("Porta Banco do Ronda Invalido");
                //return false;
            }

            if (string.IsNullOrEmpty(Config.Instancia))
            {
                throw new Exception("Instancia Banco do Ronda Invalido");
                //return false;
            }

            if (string.IsNullOrEmpty(Config.Porta))
            {
                throw new Exception("Porta Banco do Ronda Invalido");
                //return false;
            }

            return true;
        }

        // Ler o arquivo de configuração
        public void ImportConfig(ConnectWS Config, string strFilePath)
        {
            try
            {
                if (File.Exists(strFilePath))
                {
                    using (StreamReader str = new StreamReader(strFilePath))
                    {
                        string linha;

                        while ((linha = str.ReadLine()) != null)
                        {
                            linha.Trim();
                            //Console.WriteLine(linha);

                            string[] palavras = linha.Split('=');

                            foreach (string palavra in palavras)
                            {
                                //Console.WriteLine(palavra);
                                // [vars-wsdl]
                                if (palavras[0] == "url")
                                {
                                    Config.Url = palavras[1].Trim();
                                    Console.WriteLine(Config.Url);
                                }
                                
                                if (palavras[0] == "host")
                                {
                                    Config.Host = palavras[1].Trim();
                                    //Console.WriteLine(Config.Host);
                                }

                                if (palavras[0] == "usuario-wsdl")
                                {
                                    Config.Userdig = palavras[1].Trim();
                                    //Console.WriteLine(Config.Userdig);
                                }
                                else
                                {   // Apelão
                                    Config.Userdig = "admin-digicon";
                                }

                                if (palavras[0] == "senha-wsdl")
                                {
                                    Config.Senhadig = palavras[1].Trim();
                                    //Console.WriteLine(Config.Senhadig);
                                }
                                else
                                {   // Apelão
                                    Config.Senhadig = "digicon";
                                }

                                // [vars-db]
                                if (palavras[0] == "ip")
                                {
                                    Config.Ip = palavras[1].Trim();
                                    //Console.WriteLine(Config.Ip);
                                }

                                if (palavras[0] == "porta")
                                {
                                    Config.Porta = palavras[1].Trim();
                                    //Console.WriteLine(Config.Porta);
                                }
 
                                if (palavras[0] == "instancia")
                                {
                                    Config.Instancia = palavras[1].Trim();
                                    //Console.WriteLine(Config.Instancia);
                                }

                                if (palavras[0] == "usuario-db")
                                {
                                    Config.Userdb = palavras[1].Trim();
                                    Console.WriteLine(Config.Userdb);
                                }

                                if (palavras[0] == "senha-db")
                                {
                                    Config.Senhadb = palavras[1].Trim();
                                    //Console.WriteLine(Config.Senhadb);
                                }

                            } // foreach
                        } //while
                    } //using

                    //Valida se existe config
                    if (!ChecaConfig(Config))
                    {   // Apelão nunca vaui entrar porque estoura erro
                        Config.Url = "http://localhost:8080/diginet2/diginet-ws?wsdl";
                        Config.Host = "10.104.30.136:8080";
                        Config.Ip = "192.168.5.29";
                        Config.Porta = "1433";
                        Config.Instancia = "SENIOR";
                        Config.Userdb = "dclock";
                        Config.Senhadb = "dclock@)!%";
                    }

                    // Monta Connection String
                    Config.Connectionstring = "Provider=SQLOLEDB;Data Source=" + Config.Ip + ";Initial Catalog=" + Config.Instancia + ";Persist Security Info=True;User ID=" + Config.Userdb + "; Password=" + Config.Senhadb;
                }
                else
                {
                    Console.WriteLine("Erro ao abrir o Arquivo de Configuração");
                }
                //return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro no Arquivo de Configuração: ");
                Console.WriteLine(e.Message);
                //return false;
            }
        }

        // Faz a Requisição SOAP
        public string ReqSoap_Diginet(ConnectWS Config, int max, List<ConnectWS> userRecords, string metodo)
        {
            // Autenticação Básica
            NetworkCredential netCredential = new NetworkCredential(Config.Userdig, Config.Senhadig);
            
            Uri url = new Uri(Config.Url);

            ICredentials credentials = netCredential.GetCredential(url, "Basic");

            string resulXmlFromWebService;
            string resultado =  "";
            int y = 0;

            for (int j = 0; j < max; j++)
            {
                // Cria a Request
                WebRequest webRequest = WebRequest.Create(url);
                HttpWebRequest httpRequest = (HttpWebRequest)webRequest;

                // Inicio Cabeçalho
                httpRequest.Method = "POST";
                httpRequest.ProtocolVersion = HttpVersion.Version11;
                httpRequest.ServicePoint.Expect100Continue = false;
                httpRequest.Headers.Add("Accept-Encoding: gzip,deflate");
                httpRequest.ContentType = "text/xml;charset=UTF-8";
                httpRequest.Headers.Add("SOAPAction:");
                httpRequest.Host = Config.Host;
                httpRequest.PreAuthenticate = true;
                httpRequest.UseDefaultCredentials = false;
                httpRequest.Credentials = credentials;
                Stream requestStream = httpRequest.GetRequestStream();
                // Cria Stream e Completa para a Requisão             
                StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.ASCII);

                // Monta o Corpo da Mensagem
                StringBuilder soapRequest = new StringBuilder("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"  xmlns:web=\"http://webservice.diginet.digicon.com.br/\">");
                soapRequest.Append("<soap:Body>");
                soapRequest.Append("<web:" + metodo + ">");
                if (metodo == "insereColaborador")
                {
                    soapRequest.Append("<nome>" + userRecords[j].Nome[j] + "</nome>");
                    soapRequest.Append("<matricula>" + userRecords[j].Matricula[j] + "</matricula>");
                    soapRequest.Append("<pis>" + userRecords[j].NumPis[j] + "</pis>");
                    soapRequest.Append("<mifare>" + userRecords[j].NumMifare[j] + "</mifare>");
                    soapRequest.Append("<codigoBarras></codigoBarras>");
                    soapRequest.Append("<rfid></rfid>");
                    soapRequest.Append("<tipoIdentificacaoEmpresa>1</tipoIdentificacaoEmpresa>");
                    soapRequest.Append("<identificacaoEmpresa>92080035000287</identificacaoEmpresa>");
                    soapRequest.Append("<setor>digicon</setor>");
                    soapRequest.Append("<permiteTeclado>0</permiteTeclado>");
                    soapRequest.Append("<cei>000000000000</cei>");
                }
                else
                {
                    y = j;
                    soapRequest.Append("<pis>" + userRecords[j].NumPis[j] + "</pis>");
                    if (userRecords[j].Tbio2[j] == "0")
                    {
                        soapRequest.Append("<biometriaSagem1>" + userRecords[j].Tbio1[j+1].ToString() + "</biometriaSagem1>");    // 0
                        soapRequest.Append("<biometriaSagem2>" + userRecords[y + 1].Tbio1[j].ToString() + "</biometriaSagem2>");  // 1
                    }
                    else if (userRecords[j].Tbio2[j] == "1" && userRecords[j].Tbio2[y + 1] == "2")
                    {
                        soapRequest.Append("<biometriaSagem1>" + userRecords[j].Tbio1[j+1].ToString() + "</biometriaSagem1>");    // 1
                        soapRequest.Append("<biometriaSagem2>" + userRecords[y + 1].Tbio1[j].ToString() + "</biometriaSagem2>");  // 2
                    }
                    else if (userRecords[j].Tbio2[j] == "1" && userRecords[j].Tbio2[y - 1] == "0")
                    {
                        soapRequest.Append("<biometriaSagem1>" + userRecords[y - 1].Tbio1[j].ToString() + "</biometriaSagem1>");  // 0
                        soapRequest.Append("<biometriaSagem2>" + userRecords[j].Tbio1[j-1].ToString() + "</biometriaSagem2>");      // 1
                    }
                    else if (userRecords[j].Tbio2[j] == "2" && userRecords[j].Tbio2[y - 1] == "1")
                    {
                        soapRequest.Append("<biometriaSagem1>" + userRecords[y - 1].Tbio1[j].ToString() + "</biometriaSagem1>");     // 1
                        soapRequest.Append("<biometriaSagem2>" + userRecords[j].Tbio1[j-1].ToString() + "</biometriaSagem2>");         // 2
                    }
                    else
                    {
                        soapRequest.Append("<biometriaSagem1>" + userRecords[j].Tbio1[j].ToString() + "</biometriaSagem1>");
                        soapRequest.Append("<biometriaSagem2>" + userRecords[j].Tbio1[j].ToString() + "</biometriaSagem2>");
                    }

                }
                soapRequest.Append("</web:" + metodo + ">");
                soapRequest.Append("</soap:Body>");
                soapRequest.Append("</soap:Envelope>");

                Console.WriteLine(soapRequest.ToString());
                streamWriter.Write(soapRequest.ToString());
                streamWriter.Close();

                // Pega a Resposta
                HttpWebResponse wr = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader srd = new StreamReader(wr.GetResponseStream());
                resulXmlFromWebService = srd.ReadToEnd();
                srd.Close();
                
                Console.WriteLine(resulXmlFromWebService);

                resultado = ValConn.ValidaResposta(resulXmlFromWebService);
            }

            return resultado;
            
        }

    }
}
