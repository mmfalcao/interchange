using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace InterchangeWFA
{
    class ValidateConn
    {
        int[] contaRet = new int[57] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
                    /*0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0,
                    0,0,0,0,0,0,0,0,0,0};*/

        // Baseado nos codigos do WSDL do Diginet
        public enum CodRetorno
        {
            SUCESSO = 0,
            ERRO_GENERICO = 1,
            USUARIO_SEM_PERMISSAO = 2,
            OBRIGATORIO_N_PREENCHIDO = 3,
            SETOR_404 = 4,
            TIPOID_INVALIDO = 5,
            DISPOSITIVO_404 = 6,
            COLABORADOR_404 = 7,
            TECCRA_S_SUPORTE = 8,
            EMPRESA_404 = 9,
            INTEGRIDADE_VIOLADA = 10,
            LOCAL_404 = 11,
            VALID_BIO_INVALIDA = 12,
            SETOR_CADASTRADO = 13,
            HRS_VERAO_INVALIDO = 14,
            FUSO_HRS_INVALIDO = 15,
            NOME_NULL = 16,
            LOCAL_CADASTRADO = 17,
            NOME_LOCAL_OBRIGATORIO = 18,
            LOCAL_404_AGAIN = 19,
            SETOR_OBRIGATORIO = 20,
            DISPOSITIVO_OBRIGATORIO = 21,
            IP_DISP_OBRIGATORIO = 22,
            NRP_OBRIGATORIO = 23,
            LOCAL_OBRIGATORIO = 24,
            SETOR_N_PODE_EXCLUIR = 25,
            PIS_OBRIGATORIO = 26,
            NOME_OBRIGATORIO = 27,
            MATRICULA_OBRIGATORIO = 28,
            PERMITE_PONTO_INVALIDO = 29,
            PIS_INVALIDO = 30,
            CARTAO_ISNUM = 31,
            PARAMS_INI_END_OBRIGATORIO = 32,
            TECCRA_INVALIDO = 33,
            IDEMPRESA_OBRIGATORIO = 34,
            CEI_OBRIGATORIO = 35,
            CIDADE_OBRIGATORIO = 36,
            UF_OBRIGATORIO = 37,
            MIFARE_ISNUM = 38,
            RFID_ISNUM = 39,
            BARCODE_ISNUM = 40,
            DATA_INI_INVALIDA = 41,
            DATA_END_INVALIDA = 42,
            DATA_END_IGUAL = 43,
            NSR_OBRIGATORIO = 44,
            DATA_END_EXEC = 45,
            STATUS_INVALIDO = 46,
            DATA_END_INCLUSAO = 47,
            EMPRESA_CADASTRADA = 48,
            DISPOSITIVO_CADASTRADO = 49,
            CNPJ_INVALIDO = 50,
            CPF_INVALIDO = 51,
            PIS_EM_USO = 52,
            SETOR_N_ALTERA = 53,
            IP_EM_USO = 54,
            CEI_INVALIDO = 55
        }

        public string ValidaResposta(string sxml)
        {
            string retorno;
            string retsxml;

            if (sxml.Length > 10)
            {
                // Create an XmlReader
                using (XmlReader reader = XmlReader.Create(new StringReader(sxml)))
                {
                    reader.ReadToFollowing("return");
                    retsxml = reader.ReadElementContentAsString();
                    //Console.WriteLine("Codigo de Retorno: {0} ", retsxml);
                }

                retsxml.Trim();
                String.Join("", System.Text.RegularExpressions.Regex.Split(retsxml, @"[^\d]"));

                if (retsxml != "" || retsxml != null)
                {
                    retorno = Identifica(retsxml);
                }
                else
                {
                    retorno = "Resposta nao identificada";
                }
            }
            else
            {
                retorno = "Sem Resposta do Web Service";
            }

            return retorno;
        }

        public string Identifica(string str)
        {
            int num = Convert.ToInt32(str);

            switch (num)
            {
                case (int)CodRetorno.SUCESSO:
                    contaRet[0] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.ERRO_GENERICO:
                    contaRet[1] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.USUARIO_SEM_PERMISSAO:
                    contaRet[2] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.OBRIGATORIO_N_PREENCHIDO:
                    contaRet[3] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.SETOR_404:
                    contaRet[4] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.TIPOID_INVALIDO:
                    contaRet[5] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.DISPOSITIVO_404:
                    contaRet[6] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.COLABORADOR_404:
                    contaRet[7] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.TECCRA_S_SUPORTE:
                    contaRet[8] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.EMPRESA_404:
                    contaRet[9] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.INTEGRIDADE_VIOLADA:
                    contaRet[10] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.LOCAL_404:
                    contaRet[11] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.VALID_BIO_INVALIDA:
                    contaRet[12] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.SETOR_CADASTRADO:
                    contaRet[13] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.HRS_VERAO_INVALIDO:
                    contaRet[14] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.FUSO_HRS_INVALIDO:
                    contaRet[15] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.NOME_NULL:
                    contaRet[16] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.LOCAL_CADASTRADO:
                    contaRet[17] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.NOME_LOCAL_OBRIGATORIO:
                    contaRet[18] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.LOCAL_404_AGAIN:
                    contaRet[19] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.SETOR_OBRIGATORIO:
                    contaRet[20] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.DISPOSITIVO_OBRIGATORIO:
                    contaRet[21] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.IP_DISP_OBRIGATORIO:
                    contaRet[22] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.NRP_OBRIGATORIO:
                    contaRet[23] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.LOCAL_OBRIGATORIO:
                    contaRet[24] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.SETOR_N_PODE_EXCLUIR:
                    contaRet[25] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.PIS_OBRIGATORIO:
                    contaRet[26] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.NOME_OBRIGATORIO:
                    contaRet[27] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.MATRICULA_OBRIGATORIO:
                    contaRet[28] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.PERMITE_PONTO_INVALIDO:
                    contaRet[29] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.PIS_INVALIDO:
                    contaRet[30] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.CARTAO_ISNUM:
                    contaRet[31] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.PARAMS_INI_END_OBRIGATORIO:
                    contaRet[32] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.TECCRA_INVALIDO:
                    contaRet[33] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.IDEMPRESA_OBRIGATORIO:
                    contaRet[34] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.CEI_OBRIGATORIO:
                    contaRet[35] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.CIDADE_OBRIGATORIO:
                    contaRet[36] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.UF_OBRIGATORIO:
                    contaRet[37] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.MIFARE_ISNUM:
                    contaRet[38] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.RFID_ISNUM:
                    contaRet[39] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.BARCODE_ISNUM:
                    contaRet[40] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.DATA_INI_INVALIDA:
                    contaRet[41] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.DATA_END_INVALIDA:
                    contaRet[42] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.DATA_END_IGUAL:
                    contaRet[43] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.NSR_OBRIGATORIO:
                    contaRet[44] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.DATA_END_EXEC:
                    contaRet[45] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.STATUS_INVALIDO:
                    contaRet[46] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.DATA_END_INCLUSAO:
                    contaRet[47] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.EMPRESA_CADASTRADA:
                    contaRet[48] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.DISPOSITIVO_CADASTRADO:
                    contaRet[49] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.CNPJ_INVALIDO:
                    contaRet[50] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.CPF_INVALIDO:
                    contaRet[51] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.PIS_EM_USO:
                    contaRet[52] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", (int)CodRetorno.PIS_EM_USO, contaRet[0]);
                    break;
                case (int)CodRetorno.SETOR_N_ALTERA:
                    contaRet[53] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.IP_EM_USO:
                    contaRet[54] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                case (int)CodRetorno.CEI_INVALIDO:
                    contaRet[55] += 1;
                    //Console.WriteLine("Codigo de Retorno: {0} - Total de Retornos: {1}", retorno, contaRet[0]);
                    break;
                default:
                    contaRet[56] += 1;
                    //Console.WriteLine("Nao achou retorno {0} - Total Sem Retornos: {1} ", retorno, contaRet[55]);
                    break;
            }

            StringBuilder stBirl = new StringBuilder();

            for (int h = 0; h < 57; h++)
            {
                if (h == 56)
                {
                    stBirl.Append("Retorno Nao Identificado - Total Sem Retornos: [" + contaRet[h] + "] ");
                }
                else
                {
                    stBirl.Append("Codigo de Retorno [" + h + "] - Total Retornos: [" + contaRet[h] + "] ");
                }
                stBirl.Append("\n");
            }

            //Console.WriteLine(stBirl.ToString());

            return stBirl.ToString();
        }
    }
}
