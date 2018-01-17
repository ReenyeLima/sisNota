using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using nota;
using System.Threading.Tasks;

namespace nota
{
    
    public class funcoes
    {

        data db = new data();

        file fl = new file();

        char separetor = Path.DirectorySeparatorChar;

        string line = "";
        string ind = "", vsLay = "", ie = "", diC = "", dfC = "";
        string nLine = "", nServ = "";
        string rps = "", a = "", nRps = "", dtRps = "", t = "", valServ = "", dedu = "", fx = "", aliq = "", iss = "";
        string tpDoc = "", doc = "", fxi = "", nome = "", ende = "", num = "", comp = "", bairro = "", spcs = "", uf = "", cep = "";
        string email = "", email2 = "", desc = "", cod_ibge = "", cidade = "", frmPag = "";
        string iDoc = "Cpf", numRps = "";
        string query = "";
        string pad = "";
        int tamLote = 50;
        int nfse = 0, control = 0, linhaAt = 0, conta = 0, qntRps = 0;
        string[] cLog;

        StreamReader reader;

        private int prog = 0;

        public void setProg(int prg)
        {

            prog = prg;

        }
        
        public int getProg()
        {

            return prog;

        }
                
        public int salva_banco(string path, string codemp)
        {

            int conta = 0;

            //string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar + "uploads" + Path.DirectorySeparatorChar + fname;
           
            string line = "";

            string[] file = path.Split(separetor);

            string fname = file[file.Length - 1];

            string file_name_original = fname;

            //string str = path + " - " + file_name_original;

            DateTime dt = DateTime.Now;

            string s = dt.ToString("yyyy-MM-dd");

            string file_name_backup = s + file_name_original;

            string cod_sis = "";
            
            query = "INSERT INTO nf_controle (data_importacao, nome_ori, nome_bkp, cod_emp) VALUES (getdate(), '" + file_name_original + "', '" + file_name_backup + "', " + codemp + ")";

            int ret = db.execQuery(query);

            if (ret == 1)
            {

                SqlDataReader reti = db.rQuery("select top 1 codigo_nf from nf_controle order by codigo_nf desc");

                if (reti.Read())
                {

                    cod_sis = reti.GetInt32(0).ToString();

                }

                if (!cod_sis.Equals(""))
                {

                    StreamReader reader = new StreamReader(path);

                    while ((line = reader.ReadLine()) != null)
                    {

                        prog++;

                        ind = line.Substring(0, 1);

                        if (ind.Equals("1"))
                        {

                            vsLay = line.Substring(1, 3);
                            ie = line.Substring(4, 8);
                            diC = line.Substring(12, 8);
                            dfC = line.Substring(20, 8);

                            query = "INSERT INTO nf_dados (codigo_nf, indice, versao_layout, inscricao_estadual, data_inicio_cabecalho, data_final_cabecalho) "
                                    + "VALUES ('" + cod_sis + "', '" + ind + "', '" + vsLay + "', '" + ie + "', '" + diC + "', '" + dfC + "')";

                            if (db.execQuery(query) == 0)
                            {
                                return 0;
                            }
                            else
                            {
                                conta++;
                            }


                        }
                        else if (ind.Equals("9"))
                        {

                            nLine = line.Substring(1, 7);
                            nServ = line.Substring(8, 15);

                            query = "INSERT INTO nf_dados (codigo_nf, indice, total_linha, valor_total_servicos) VALUES ('" + cod_sis + "', '" + ind + "', '" + nLine + "', '" + nServ + "')";

                            if (db.execQuery(query) == 0)
                            {
                                return 0;
                            }
                            else
                            {
                                conta++;
                            }

                        }
                        else
                        {

                            rps = line.Substring(1, 5).Trim();
                            a = line.Substring(6, 5).Trim();
                            nRps = line.Substring(11, 12).Trim();
                            dtRps = line.Substring(23, 8).Trim();
                            t = line.Substring(31, 1).Trim();
                            valServ = line.Substring(32, 15).Trim();
                            dedu = line.Substring(47, 15).Trim();
                            fx = line.Substring(62, 5).Trim();
                            aliq = line.Substring(67, 4).Trim();
                            iss = line.Substring(71, 1).Trim();
                            tpDoc = line.Substring(72, 1).Trim();
                            doc = line.Substring(73, 14).Trim();
                            fxi = line.Substring(87, 20).Trim();
                            nome = line.Substring(107, 78).Trim();
                            if (nome.Contains("'"))
                            {
                                nome = nome.Replace("'", "''");
                            }
                            ende = line.Substring(185, 50).Trim();
                            if (ende.Contains("'"))
                            {
                                ende = ende.Replace("'", "''");
                            }
                            num = line.Substring(235, 10).Trim();
                            comp = line.Substring(245, 30).Trim();
                            bairro = line.Substring(275, 30).Trim();
                            spcs = line.Substring(305, 50).Trim();
                            uf = line.Substring(355, 2).Trim();
                            cep = line.Substring(357, 8).Trim();
                            email = line.Substring(365, 75).Trim();
                            desc = line.Substring(440, 64).Trim();
                            cod_ibge = line.Substring(504, 8).Trim();
                            cidade = line.Substring(512, 60).Trim();
                            if (cidade.Contains("'"))
                            {
                                cidade = cidade.Replace("'", "''");
                            }
                            frmPag = line.Substring(572, 60).Trim();
                            email2 = line.Substring(632, 150).Trim();

                            query = "INSERT INTO nf_dados" +
                               "(codigo_nf" +
                               ",indice" +
                               ",rps" +
                               ",a" +
                               ",numero_rps" +
                               ",data_rps" +
                               ",t" +
                               ",valor_servico" +
                               ",deducoes" +
                               ",fixa" +
                               ",aliquota" +
                               ",iss" +
                               ",tipo_documento" +
                               ",documento" +
                               ",fixoi" +
                               ",nome" +
                               ",endereco" +
                               ",numero" +
                               ",complemento" +
                               ",bairro" +
                               ",uf" +
                               ",cep" +
                               ",email" +
                               ",descricao_servico" +
                               ",cod_ibge" +
                               ",cidade" +
                               ",forma_pagamento" +
                               ",email2) VALUES " +
                               "('" + cod_sis + "'," +
                               "'" + ind + "'," +
                               "'" + rps + "'," +
                               "'" + a + "'," +
                               "'" + nRps + "'," +
                               "'" + dtRps + "'," +
                               "'" + t + "'," +
                               "'" + valServ + "'," +
                               "'" + dedu + "'," +
                               "'" + fx + "'," +
                               "'" + aliq + "'," +
                               "'" + iss + "'," +
                               "'" + tpDoc + "'," +
                               "'" + doc + "'," +
                               "'" + fxi + "'," +
                               "'" + nome + "'," +
                               "'" + ende + "'," +
                               "'" + num + "'," +
                               "'" + comp + "'," +
                               "'" + bairro + "'," +
                               "'" + uf + "'," +
                               "'" + cep + "'," +
                               "'" + email + "'," +
                               "'" + desc + "'," +
                               "'" + cod_ibge + "'," +
                               "'" + cidade + "'," +
                               "'" + frmPag + "'," +
                               "'" + email2 + "')";

                            if (db.execQuery(query) == 0)
                            {
                                return 0;
                            }
                            else
                            {
                                conta++;
                            }

                        }

                    }

                }

            }

            db.closeDB();

            return conta;

        }

        //FORMATO XML
        public Boolean gera_lote(string cod_nf, string cod_emp)
        {

            Boolean ret = false;

            string path_exp = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar + "importacoes" + Path.DirectorySeparatorChar + cod_emp + Path.DirectorySeparatorChar;
            
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberGroupSeparator = ".";

            string natuop = "", regesp = "", optsmp = "", incult = "", status = "", aliquota = "", itemlista = "";

            string codtrib = "", muniprest = "", cnpj = "", inscricao = "";


            try
            {

                DirectoryInfo dir = new DirectoryInfo(path_exp);

                if (!dir.Exists)
                {
                    dir.Create();
                }

                int ct = 0;

                decimal count = 0;

                string query = "select count(indice) from nf_dados where nf_dados.codigo_nf = " + cod_nf;

                SqlDataReader totalReader = db.rQuery(query);

                if (totalReader.HasRows)
                {
                    if (totalReader.Read())
                    {
                        count = Convert.ToDecimal(totalReader.GetInt32(0));
                        ct = totalReader.GetInt32(0);
                    }
                }

                decimal nLotes = Math.Floor(count / tamLote);

                DateTime dt = DateTime.Now;

                string s = dt.ToString("yyyy-MM-dd");

                query = "SELECT * FROM dados_empresa INNER JOIN empresa ON empresa.codigo = dados_empresa.codigo_empresa WHERE codigo_empresa = '" + cod_emp + "'";

                SqlDataReader info = db.rQuery(query);
                
                if (info.HasRows)
                {

                    info.Read();

                    natuop = info.GetString(0);

                    regesp = info.GetString(1);

                    optsmp = info.GetString(2);

                    incult = info.GetString(3);

                    status = info.GetString(4);

                    aliquota = info.GetString(5);

                    itemlista = info.GetString(6);

                    codtrib = info.GetString(7);

                    muniprest = info.GetString(8);

                    cnpj = info.GetInt64(14).ToString();

                    inscricao = info.GetInt64(15).ToString();

                }
                else
                {

                    cLog[0] = "Empresa não cadastrada ou não possui o cadastro completo";

                    fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

                    return false;

                }

                string ns = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio";

                //query = "select *, (CONCAT(cod_uf.cod, SUBSTRING(cod_ibge, 4, 6))) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                query = "select *, (cast(cod_uf.cod as varchar) + SUBSTRING(cod_ibge, 4, 6)) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                SqlDataReader dataReader = db.rQuery(query);

                //reader = new StreamReader(path);

                if (dataReader.HasRows)
                {

                    if (!Directory.Exists(path_exp + cod_nf + " - " + s))
                    {

                        System.IO.Directory.CreateDirectory(path_exp + cod_nf + " - " + s);

                    }
                    else
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(path_exp + cod_nf + " - " + s);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dire in di.GetDirectories())
                        {
                            dire.Delete(true);
                        }

                    }

                    for (int i = 0; i <= nLotes; i++)
                    {

                        nfse = 0;
                        
                        var settings = new XmlWriterSettings
                        {
                            Encoding = new UTF8Encoding(false),
                            Indent = true,
                            IndentChars = @"  ",
                            NamespaceHandling = NamespaceHandling.OmitDuplicates,
                            NewLineChars = "\n",
                            NewLineHandling = NewLineHandling.Replace,
                        };

                        using (XmlWriter write = XmlWriter.Create(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + s + "-" + i + ".xml", settings))
                        {

                            write.WriteStartDocument();

                            write.WriteStartElement("EnviarLoteRpsEnvio", ns);

                            write.WriteAttributeString("xmlns", "tip", null, "http://www.ginfes.com.br/tipos");

                            conta = ct - linhaAt;

                            if (conta < tamLote)
                            {

                                qntRps = conta - 1;

                            }
                            else
                            {

                                qntRps = tamLote;

                            }

                            //while ((line = reader.ReadLine()) != null)

                            while (dataReader.Read())
                            {

                                linhaAt++;

                                ind = dataReader.GetString(1);

                                if (control == 0)
                                {

                                    if (ind.Equals("1"))
                                    {

                                        vsLay = dataReader.GetString(2);
                                        ie = dataReader.GetString(3);
                                        diC = dataReader.GetString(4);
                                        dfC = dataReader.GetString(5);

                                    }

                                    write.WriteStartElement("NumeroLote");

                                    write.WriteValue(i);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Cnpj");

                                    //write.WriteValue("23824011000191");

                                    write.WriteValue(cnpj);

                                    write.WriteEndElement();

                                    write.WriteStartElement("InscricaoMunicipal");

                                    //write.WriteValue("20036425");

                                    write.WriteValue(inscricao);

                                    write.WriteEndElement();

                                    write.WriteStartElement("QuantidadeRps");

                                    write.WriteValue(qntRps);

                                    write.WriteEndElement();

                                    write.WriteStartElement("ListaRps");

                                    control++;

                                }

                                if (ind.Equals("9"))
                                {

                                    nLine = dataReader.GetString(6);

                                    nServ = dataReader.GetString(7);

                                }

                                if (ind.Equals("2"))
                                {

                                    rps = dataReader.GetString(8).Trim();
                                    a = dataReader.GetString(9).Trim();
                                    nRps = dataReader.GetString(10).Trim();
                                    dtRps = dataReader.GetString(11).Trim();
                                    t = dataReader.GetString(12).Trim();
                                    valServ = dataReader.GetString(13).Trim();
                                    dedu = dataReader.GetString(14).Trim();
                                    aliq = dataReader.GetString(16).Trim();
                                    iss = dataReader.GetString(17).Trim();
                                    tpDoc = dataReader.GetString(18).Trim();
                                    doc = dataReader.GetString(19).Trim();
                                    nome = dataReader.GetString(21).Trim();
                                    ende = dataReader.GetString(22).Trim();
                                    num = dataReader.GetString(23).Trim();
                                    comp = dataReader.GetString(24).Trim();
                                    bairro = dataReader.GetString(25).Trim();
                                    uf = dataReader.GetString(26).Trim();
                                    cep = dataReader.GetString(27).Trim();
                                    email = dataReader.GetString(28).Trim();
                                    desc = dataReader.GetString(29).Trim();
                                    cod_ibge = dataReader.GetString(36).Trim();
                                    cidade = dataReader.GetString(31).Trim();
                                    frmPag = dataReader.GetString(32).Trim();
                                    email2 = dataReader.GetString(33).Trim();

                                    nfse++;

                                    numRps = nfse.ToString().PadLeft(7, '0');

                                    string dataFmt = dtRps.Substring(0, 4) + "-" + dtRps.Substring(4, 2) + "-" + dtRps.Substring(6, 2) + "T00:01:00";

                                    string texto = desc + "\r\nVal. Aprox. Tributos R$ " +
                                        ((double.Parse(valServ) / 100) * 0.1788).ToString("0.00").Replace(',', '.')
                                        + ", correspondente a alíquota de (16,46%) Fonte: IBPT.";
 
                                    if (tpDoc.Equals("2"))
                                    {

                                        iDoc = "Cnpj";

                                    }
                                    else if (tpDoc.Equals("1"))
                                    {

                                        iDoc = "Cpf";

                                        doc = doc.Substring(3, 11);

                                    }

                                    write.WriteStartElement("Rps");

                                    write.WriteStartElement("tip", "IdentificacaoRps", null);

                                    write.WriteStartElement("tip", "Numero", null);

                                    write.WriteValue(nRps.Substring(5, 7));

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Serie", null);

                                    write.WriteValue(a);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Tipo", null);

                                    write.WriteValue("1");

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha IdentificacaoRps

                                    write.WriteStartElement("tip", "DataEmissao", null);

                                    write.WriteValue(s + "T00:01:00");

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "NaturezaOperacao", null);

                                    //write.WriteValue("1"); // MANUAL
                                    write.WriteValue(natuop);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "RegimeEspecialTributacao", null);

                                    //write.WriteValue("3"); // MANUAL
                                    write.WriteValue(regesp);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "OptanteSimplesNacional", null);

                                    //write.WriteValue("2"); // MANUAL
                                    write.WriteValue(optsmp);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "IncentivadorCultural", null);

                                    //write.WriteValue("2"); // MANUAL
                                    write.WriteValue(incult);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Status", null);

                                    //write.WriteValue("1"); // MANUAL
                                    write.WriteValue(status);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Servico", null);

                                    write.WriteStartElement("tip", "Valores", null);

                                    write.WriteStartElement("tip", "ValorServicos", null);

                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "IssRetido", null);

                                    write.WriteValue(iss);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "BaseCalculo", null);

                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Aliquota", null);

                                    //write.WriteValue("0." + aliq); //MANUAL
                                    write.WriteValue("0." + aliquota);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "ValorLiquidoNfse", null);

                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha Valores

                                    write.WriteStartElement("tip", "ItemListaServico", null);

                                    //write.WriteValue("6.04");//MANUAL
                                    write.WriteValue(itemlista);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "CodigoTributacaoMunicipio", null);

                                    //write.WriteValue("6.04 / 00060401");//MANUAL
                                    write.WriteValue(codtrib);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Discriminacao", null);

                                    write.WriteValue(texto);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "MunicipioPrestacaoServico", null);

                                    //write.WriteValue("3543402");//MANUAL
                                    write.WriteValue(muniprest);

                                    write.WriteEndElement();

                                    write.WriteEndElement();// fecha Servico 

                                    write.WriteStartElement("tip", "Prestador", null);

                                    write.WriteStartElement("tip", "Cnpj", null);

                                    //write.WriteValue("23824011000191");//MANUAL
                                    write.WriteValue(cnpj);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "InscricaoMunicipal", null);

                                    //write.WriteValue("20036425");//MANUAL
                                    write.WriteValue(inscricao);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha Prestador

                                    write.WriteStartElement("tip", "Tomador", null);

                                    write.WriteStartElement("tip", "IdentificacaoTomador", null);

                                    write.WriteStartElement("tip", "CpfCnpj", null);

                                    write.WriteStartElement("tip", iDoc, null);

                                    write.WriteValue(doc);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha CpfCnpj

                                    write.WriteEndElement();//fecha IdentificacaoTomador

                                    write.WriteStartElement("tip", "RazaoSocial", null);

                                    write.WriteValue(nome);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Endereco", null);

                                    write.WriteStartElement("tip", "Endereco", null);

                                    write.WriteValue(ende);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Numero", null);

                                    write.WriteValue(num);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Bairro", null);

                                    write.WriteValue(bairro);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Cidade", null);

                                    write.WriteValue(cod_ibge);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Estado", null);

                                    write.WriteValue(uf);

                                    write.WriteEndElement();

                                    write.WriteStartElement("tip", "Cep", null);

                                    write.WriteValue(cep);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha Endereco

                                    write.WriteStartElement("tip", "Contato", null);

                                    write.WriteStartElement("tip", "Email", null);

                                    write.WriteValue(email);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha Contato

                                    write.WriteEndElement();//fecha Tomador

                                    write.WriteEndElement();//fecha Rps

                                    if (nfse == qntRps)
                                    {
                                        break;
                                    }

                                }

                            }

                            write.WriteEndElement();//fecha listaRps

                            control = 0;

                        }

                        conta = 0;

                    }

                }

                ret = true;
                
            }
            catch (Exception ex)
            {

                ret = false;

                cLog[0] = "Erro não esperado - " + ex.ToString();

                fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

            }
            
            return ret;

        }

        public Boolean gera_lote_lmc(string cod_nf, string cod_emp)
        {

            Boolean ret = false;
                        
            string natuop = "", regesp = "", optsmp = "", incult = "", status = "", aliquota = "", itemlista = "";

            string codtrib = "", muniprest = "", cnpj = "", inscricao = "";
            
            string cabecalho = "", rodape = "";

            double vts = 0, vtd = 0, vti = 0;

            int ct = 0;

            decimal count = 0;

            string query = "select count(indice) from nf_dados where nf_dados.codigo_nf = " + cod_nf;

            SqlDataReader totalReader = db.rQuery(query);

            if (totalReader.HasRows)
            {
                if (totalReader.Read())
                {
                    count = Convert.ToDecimal(totalReader.GetInt32(0));
                    ct = totalReader.GetInt32(0);
                }
            }

            decimal nLotes = Math.Floor(count / tamLote);

            DateTime dt = DateTime.Now;

            string s = dt.ToString("yyyy-MM-dd");
            
            string path_exp = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar + "importacoes" + Path.DirectorySeparatorChar + cod_emp + Path.DirectorySeparatorChar + s + Path.DirectorySeparatorChar;

            DirectoryInfo dir = new DirectoryInfo(path_exp);

            if (!dir.Exists)
            {

                dir.Create();

            }

            string line = "";

            //StreamWriter writer = new StreamWriter(path_exp + "arq.txt");

            query = "SELECT * FROM dados_empresa INNER JOIN empresa ON empresa.codigo = dados_empresa.codigo_empresa WHERE codigo_empresa = '" + cod_emp + "'";

            //writer.WriteLine(query);

            SqlDataReader info = db.rQuery(query);

            try
            {
                if (info.HasRows)
                {

                    info.Read();

                    natuop = info.GetString(0);

                    regesp = info.GetString(1);

                    optsmp = info.GetString(2);

                    incult = info.GetString(3);

                    status = info.GetString(4);

                    aliquota = info.GetString(5);

                    itemlista = info.GetString(6);

                    codtrib = info.GetString(7);

                    muniprest = info.GetString(8);

                    cnpj = info.GetInt64(14).ToString();

                    inscricao = info.GetInt64(15).ToString();

                }
                else
                {

                    cLog[0] = "Empresa não cadastrada ou não possui o cadastro completo";

                    fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);
                    
                    return false;

                }

                query = "select *, (cast(cod_uf.cod as varchar) + SUBSTRING(cod_ibge, 4, 6)) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                SqlDataReader dataReader = db.rQuery(query);

                if (dataReader.HasRows)
                {

                    if (!Directory.Exists(path_exp + cod_nf + " - " + s))
                    {

                        System.IO.Directory.CreateDirectory(path_exp + cod_nf + " - " + s);

                    }
                    else
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(path_exp + cod_nf + " - " + s);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dire in di.GetDirectories())
                        {
                            dire.Delete(true);
                        }

                    }

                    /*for (int i = 0; i <= nLotes; i++)
                    {

                        nfse = 0;

                        conta = ct - linhaAt;

                        if (conta < tamLote)
                        {

                            qntRps = conta - 1;

                        }
                        else
                        {

                            qntRps = tamLote;

                        }*/

                        //StreamWriter writer = new StreamWriter(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + s + "-" + i + ".txt");

                        StreamWriter writer = new StreamWriter(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + s + ".txt");

                        while (dataReader.Read())
                        {

                            linhaAt++;

                            ind = dataReader.GetString(1);

                            if(control == 0)
                            {

                                if (ind.Equals("1"))
                                {

                                    vsLay = dataReader.GetString(2);
                                    ie = dataReader.GetString(3);
                                    diC = dataReader.GetString(4);
                                    dfC = dataReader.GetString(5);

                                }

                                string cmm = "84298";//inscricao municipal

                                cabecalho = "01" + cnpj.PadRight(14, '0') + cmm.PadLeft(8, '0') + "09";
                                cabecalho = cabecalho.PadRight(150, ' ');

                                writer.WriteLine(cabecalho);

                                control++;

                            }

                            if (ind.Equals("9"))
                            {

                                nLine = dataReader.GetString(6);

                                nServ = dataReader.GetString(7);

                            }

                            if (ind.Equals("2"))
                            {

                                rps = dataReader.GetString(8).Trim();
                                a = dataReader.GetString(9).Trim();
                                nRps = dataReader.GetString(10).Trim();
                                dtRps = dataReader.GetString(11).Trim();
                                t = dataReader.GetString(12).Trim();
                                valServ = dataReader.GetString(13).Trim();
                                dedu = dataReader.GetString(14).Trim();
                                aliq = dataReader.GetString(16).Trim();
                                iss = dataReader.GetString(17).Trim();
                                tpDoc = dataReader.GetString(18).Trim();
                                doc = dataReader.GetString(19).Trim();
                                nome = dataReader.GetString(21).Trim();
                                ende = dataReader.GetString(22).Trim();
                                num = dataReader.GetString(23).Trim();
                                comp = dataReader.GetString(24).Trim();
                                bairro = dataReader.GetString(25).Trim();
                                uf = dataReader.GetString(26).Trim();
                                cep = dataReader.GetString(27).Trim();
                                email = dataReader.GetString(28).Trim();
                                desc = dataReader.GetString(29).Trim();
                                cod_ibge = dataReader.GetString(36).Trim();
                                cidade = dataReader.GetString(31).Trim();
                                frmPag = dataReader.GetString(32).Trim();
                                email2 = dataReader.GetString(33).Trim();

                                nfse++;

                                numRps = nfse.ToString().PadLeft(8, '0');

                                string dataFmt = dtRps.Substring(6, 2) + dtRps.Substring(4, 2) + dtRps.Substring(0, 4);

                                string codAtividade = "000604", cfps = "511", impRetido = "N", vlrImposto = "", vlrTotal = "", vlrAprxImposto = "";
                                string alqImpAprx = "", fontImpAprx = "", filler = "";
                                string dataEmissao = s.Substring(8, 2) + s.Substring(5, 2) + s.Substring(0,4);//"yyyy-MM-dd";
                                string infoTransp = pad.PadLeft(525, ' '), qtdTransp = pad.PadLeft(14, '0'),  espTrans = pad.PadLeft(50, ' ');
                                string infroTranspeso = pad.PadRight(28, '0'), infoTranspd = pad.PadLeft(34, '0');
                                
                                if (tpDoc.Equals("2"))
                                {

                                    //iDoc = "Cnpj";
                                    iDoc = "J";

                                }
                                else if (tpDoc.Equals("1"))
                                {

                                    //iDoc = "Cpf";
                                    iDoc = "F";
                                    
                                }

                                //(double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.');
                                double value = double.Parse(valServ) / 100;

                                vts += value;

                                aliq = aliq.Replace(".", "");

                                aliq = aliq.Replace(",", "");

                                aliq = aliq.Replace("0", "");

                                double aliquotaD = double.Parse(aliq) / 100;

                                double vlimp = aliquotaD * value;

                                vlimp = Math.Round(vlimp,2);
                                //2,997
                                vti += vlimp;

                                vlrImposto = vlimp.ToString("0.00").PadRight(4, '0');

                                vlrImposto = vlrImposto.Replace(",", "").PadLeft(14, '0');

                                string vlrServico = value.ToString("0.00").Replace(",", "").PadLeft(14, '0');

                                vlrTotal = vlrServico;

                                string vlrServicol = value.ToString("0.000000000").Replace(",", "").PadLeft(19, '0');

                                string vlrTotall = vlrServicol;

                                nRps = nRps.TrimStart('0');

                                fontImpAprx = "IBPT";

                                if (dedu.Length > 14)
                                {
                                    dedu = dedu.Substring(0, 14);
                                }
                                else
                                {
                                    dedu = dedu.PadLeft(14, '0');
                                }

                                double vai = (value * ((16.46) / 100));

                                vai = Math.Round(vai,2);

                                vlrAprxImposto = vai.ToString("0.00").Replace(",","").PadLeft(14, '0');

                                aliq = aliq.PadRight(3, '0');

                                aliq = aliq.PadLeft(5, '0');

                                //VERSAO FINAL SEM ;
                                line = "1" + numRps + "000000001" + dataFmt + codAtividade.PadLeft(6, '0') + cfps.PadRight(3, '0') + "17" + doc.PadRight(14, '0') + nome.PadRight(100, ' ') + cep.PadRight(8, ' ') + ende.PadRight(100, ' ') + bairro.PadRight(50, ' ') + cidade.PadRight(60, ' ') + uf.PadRight(2, ' ') + pad.PadRight(100, ' ') + email.PadRight(80, ' ') + "S" + impRetido + vlrServico.PadRight(14, '0');//valServ.Substring(valServ.Length - 13);
                                line += dedu + vlrImposto.PadRight(14, '0')+ aliq + vlrTotal.PadRight(14, '0') + pad.PadLeft(70, '0') + nRps.PadLeft(8, '0') + "S" + pad.PadLeft(255, ' ') + dataEmissao + iDoc;
                                line += ie.PadRight(20, ' ') + infoTransp + qtdTransp + espTrans + infroTranspeso + "22" + infoTranspd + cidade.PadRight(100, ' ') + uf;
                                line += vlrAprxImposto + /*alqImpAprx.PadRight(5, '0')*/"01646" + fontImpAprx.PadRight(10, ' ') + filler.PadLeft(39, ' ');

                                //VERSAO FINAL COM ;
                                /*line = "1;" + numRps + ";00000000;1;" + dataFmt + ";" + codAtividade.PadLeft(6, '0') + ";" + cfps.PadRight(3, '0') + ";17;" + doc.PadRight(14, '0') + ";" + nome.PadRight(100, ' ') + ";" + cep.PadRight(8, ' ') + ";" + ende.PadRight(100, ' ') + ";" + bairro.PadRight(50, ' ') + ";" + cidade.PadRight(60, ' ') + ";" + uf.PadRight(2, ' ') + ";" + pad.PadRight(100, ' ') + ";" + email.PadRight(80, ' ') + ";S;" + impRetido + ";" + vlrServico.PadRight(14, '0');//valServ.Substring(valServ.Length - 13);
                                line += ";" + dedu + ";" + vlrImposto.PadRight(14, '0') + ";" + aliq.PadLeft(5, '0') + ";" + vlrTotal.PadRight(14, '0') + ";" + pad.PadLeft(70, '0') + ";" + nRps.PadLeft(8, '0') + ";F;" + pad.PadLeft(255, ' ') + ";" + dataEmissao + ";" + iDoc;
                                line += ";" + ie.PadRight(20, ' ') + ";" + infoTransp + ";" + qtdTransp + ";" + espTrans + ";" + infoTranspd + ";" + cidade.PadRight(100, ' ') + ";" + uf;
                                line += ";" + vlrAprxImposto + ";" + /*alqImpAprx.PadRight(5, '0')"01646" + ";" + fontImpAprx.PadRight(10, ' ') + ";" + filler.PadLeft(39, ' ');*/

                                /*line = "1" + numRps.PadLeft(8, '0') + "000000001" + dataFmt + codAtividade.PadLeft(6, '0') + cfps.PadRight(3, '0') + "17" + doc.PadRight(14, '0') + nome.PadRight(100, ' ') + cep.PadRight(8, ' ') + ende.PadRight(100, ' ') + bairro.PadRight(50, ' ') + cidade.PadRight(60, ' ') + uf.PadRight(2, ' ') + pad.PadRight(100, ' ') + email.PadRight(80, ' ') + "S" + impRetido + vlrServico.PadRight(14, '0');//valServ.Substring(valServ.Length - 13);
                                line += dedu.Substring(valServ.Length - 13) + vlrImposto.PadRight(14, '0') + aliq.PadRight(5, '0') + vlrTotal.PadRight(14,'0') + pad.PadLeft(70, '0') + nRps.PadRight(8, '0') + "F" + pad.PadLeft(255, ' ') + dataEmissao + iDoc;
                                line += doc.PadLeft(20, ' ') + infoTransp + qtdTransp + espTrans + infoTranspd + cidade.PadRight(100, ' ') + uf;
                                line += vlrAprxImposto.PadRight(14, '0') +alqImpAprx.PadRight(5, '0') + fontImpAprx.PadLeft(10, ' ') + filler.PadLeft(39, ' ');*/


                                /*line = "1;" + numRps + ";000000001;" + dataFmt + ";" + codAtividade + ";" + cfps + ";17;" + cnpj + ";" + nome.PadRight(100, ' ') + ";" + cep.PadRight(8, ' ') + ";" + ende.PadRight(100, ' ') + ";" + bairro.PadRight(50, ' ') + ";" + cidade.PadRight(60, ' ') + ";" + uf.PadRight(2, ' ') + ";" + pad.PadRight(100, ' ') + ";" + email.PadRight(80, ' ') + ";N;" + impRetido + ";" + valServ.Substring(valServ.Length - 13);
                                line += ";" + dedu.Substring(valServ.Length - 13) + ";" + vlrImposto.PadRight(14, '0') + ";" + aliq.PadLeft(5, '0') + ";" + vlrTotal.PadRight(14, '0') + ";" + pad.PadLeft(70, '0') + ";" + rps.PadLeft(8, '0') + ";F;" + pad.PadLeft(255, '0') + ";" + dataEmissao + ";" + iDoc;
                                line += ";" + doc.PadLeft(20, ' ') + ";" + infoTransp + ";" + qtdTransp + ";" + espTrans + ";" + infoTranspd + ";" + cidade.PadRight(100, ' ') + ";" + uf;
                                line += ";" + vlrAprxImposto + ";" + alqImpAprx.PadRight(5, '0') + ";" + fontImpAprx.PadRight(10, ' ') + ";" + filler.PadRight(39, ' ') + ";";*/

                                writer.WriteLine(line);

                                line = "2" + numRps + "00000001" + "00000000010000" + "UN" + vlrServicol + vlrTotall + desc.PadRight(911, ' ') + pad.PadLeft(118, ' ');

                                writer.WriteLine(line);

                            }

                            /*if (nfse == qntRps)
                            {
                                break;
                            }*/
                                     
                        }

                        //vts = Math.Round(vts);

                        //vti = Math.Round(vti);

                        line = "9" + nfse.ToString().PadLeft(4, '0') + vts.ToString("0.00").Replace(",", "").PadLeft(14, '0') + pad.PadLeft(14, '0') + vti.ToString("0.00").Replace(",", "").PadLeft(14, '0');

                        writer.WriteLine(line);

                        control = 0;

                        writer.Close();

                    //}

                }

            }
            catch (Exception e)
            {
                ret = false;

                cLog[0] = "Erro não esperado - " + e.ToString();

                fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);
            }

            
            

            //line = "01" + cnpj + inscricao + "09";

            //writer.WriteLine(line);

            //writer.Close();

            return ret;

        }
        
        public Boolean gera_lote_aracatuba(string cod_nf, string cod_emp)//NAO FUNFA
        {

            Boolean ret = false;

            string path_exp = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar + "importacoes" + Path.DirectorySeparatorChar + cod_emp + Path.DirectorySeparatorChar;

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberGroupSeparator = ".";

            string natuop = "", regesp = "", optsmp = "", incult = "", status = "", aliquota = "", itemlista = "";

            string codtrib = "", muniprest = "", cnpj = "", inscricao = "";

            string codnf = "";

            try
            {

                DirectoryInfo dir = new DirectoryInfo(path_exp);

                if (!dir.Exists)
                {
                    
                    dir.Create();

                }

                int ct = 0;

                decimal count = 0;

                string query = "select count(indice) from nf_dados where nf_dados.codigo_nf = " + cod_nf;

                SqlDataReader totalReader = db.rQuery(query);

                if (totalReader.HasRows)
                {

                    if (totalReader.Read())
                    {

                        count = Convert.ToDecimal(totalReader.GetInt32(0));
                        ct = totalReader.GetInt32(0);

                    }

                }

                decimal nLotes = Math.Floor(count / tamLote);

                DateTime dt = DateTime.Now;

                string s = dt.ToString("yyyy-MM-dd");

                query = "SELECT * FROM dados_empresa INNER JOIN empresa ON empresa.codigo = dados_empresa.codigo_empresa WHERE codigo_empresa = '" + cod_emp + "'";

                SqlDataReader info = db.rQuery(query);

                if (info.HasRows)
                {

                    info.Read();

                    natuop = info.GetString(0);

                    regesp = info.GetString(1);

                    optsmp = info.GetString(2);

                    incult = info.GetString(3);

                    status = info.GetString(4);

                    aliquota = info.GetString(5);

                    itemlista = info.GetString(6);

                    codtrib = info.GetString(7);

                    muniprest = info.GetString(8);

                    cnpj = info.GetInt64(14).ToString();

                    inscricao = info.GetInt64(15).ToString();

                }
                else
                {

                    cLog[0] = "Empresa não cadastrada ou não possui o cadastro completo";

                    fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

                    return false;

                }

                string ns = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio";

                //query = "select *, (CONCAT(cod_uf.cod, SUBSTRING(cod_ibge, 4, 6))) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                query = "select *, (cast(cod_uf.cod as varchar) + SUBSTRING(cod_ibge, 4, 6)) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                SqlDataReader dataReader = db.rQuery(query);

                //reader = new StreamReader(path);

                string cmm = "";

                if (dataReader.HasRows)
                {
                    
                    if (!Directory.Exists(path_exp + cod_nf + " - " + s))
                    {

                        System.IO.Directory.CreateDirectory(path_exp + cod_nf + " - " + s);

                    }
                    else
                    {

                        System.IO.DirectoryInfo di = new DirectoryInfo(path_exp + cod_nf + " - " + s);

                        foreach (FileInfo file in di.GetFiles())
                        {

                            file.Delete();

                        }

                        foreach (DirectoryInfo dire in di.GetDirectories())
                        {

                            dire.Delete(true);

                        }

                    }

                    for (int i = 1; i <= nLotes + 1; i++)
                    {

                        nfse = 0;

                        var settings = new XmlWriterSettings
                        {
                            Encoding = new UTF8Encoding(false),
                            Indent = true,
                            IndentChars = @"  ",
                            NamespaceHandling = NamespaceHandling.OmitDuplicates,
                            NewLineChars = "\n",
                            NewLineHandling = NewLineHandling.Replace,
                        };

                        //using (XmlWriter write = XmlWriter.Create(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + s + "-" + i + ".xml", settings))
                        //26537455000134_I_201706-0_05062017
                        String ano = System.DateTime.Now.ToString("yyyy");
                        String mes = System.DateTime.Now.ToString("MM");
                        String dia = System.DateTime.Now.ToString("dd");

                        using (XmlWriter write = XmlWriter.Create(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + cnpj + "_I_" + ano + mes + "-" + i + "_" + dia + mes + ano + ".xml", settings))
                        {

                            conta = ct - linhaAt;

                            if (conta < tamLote)
                            {

                                qntRps = conta - 1;

                            }
                            else
                            {

                                qntRps = tamLote;

                            }

                            //while ((line = reader.ReadLine()) != null)

                            StreamWriter writer = new StreamWriter("C:\\teste\\teste.txt", true);

                            write.WriteStartDocument();

                            control = 0;

                            while (dataReader.Read())
                            {

                                String ind = dataReader.GetString(1);

                                writer.Write(ind);

                                if (ind.Equals("1"))
                                {

                                    vsLay = dataReader.GetString(2);
                                    ie = dataReader.GetString(3);
                                    diC = dataReader.GetString(4);
                                    dfC = dataReader.GetString(5);

                                }

                                if (ind.Equals("2"))
                                {

                                    codnf = dataReader.GetString(0).Trim();
                                    rps = dataReader.GetString(8).Trim();
                                    a = dataReader.GetString(9).Trim();
                                    nRps = dataReader.GetString(10).Trim();
                                    dtRps = dataReader.GetString(11).Trim();
                                    t = dataReader.GetString(12).Trim();
                                    valServ = dataReader.GetString(13).Trim();
                                    //dedu = dataReader.GetString(14).Trim();
                                    dedu = (Convert.ToDecimal(dataReader.GetString(14)) / 100).ToString();
                                    aliq = dataReader.GetString(16).Trim();
                                    iss = dataReader.GetString(17).Trim();
                                    tpDoc = dataReader.GetString(18).Trim();
                                    doc = dataReader.GetString(19).Trim();
                                    nome = dataReader.GetString(21).Trim();
                                    ende = dataReader.GetString(22).Trim();
                                    num = dataReader.GetString(23).Trim();
                                    comp = dataReader.GetString(24).Trim();
                                    bairro = dataReader.GetString(25).Trim();
                                    uf = dataReader.GetString(26).Trim();
                                    cep = dataReader.GetString(27).Trim();
                                    email = dataReader.GetString(28).Trim();
                                    desc = dataReader.GetString(29).Trim();
                                    cod_ibge = dataReader.GetString(36).Trim();
                                    cidade = dataReader.GetString(31).Trim();
                                    frmPag = dataReader.GetString(32).Trim();
                                    email2 = dataReader.GetString(33).Trim();

                                    nfse++;

                                    //numRps = nfse.ToString().PadLeft(9, '0');
                                    numRps = nfse.ToString();

                                }

                                if (ind.Equals("9"))
                                {

                                    

                                }

                                if (control == 0)
                                {


                                    write.WriteStartElement("NFSE");

                                    write.WriteStartElement("IDENTIFICACAO");

                                    write.WriteStartElement("MESCOMP");

                                    write.WriteValue(mes);

                                    write.WriteEndElement();

                                    write.WriteStartElement("ANOCOMP");

                                    write.WriteValue(ano);

                                    write.WriteEndElement();

                                    write.WriteStartElement("INSCRICAO");

                                    write.WriteValue("82661");

                                    write.WriteEndElement();

                                    write.WriteStartElement("VERSAO");

                                    write.WriteValue("1.00");

                                    write.WriteEndElement();

                                    write.WriteEndElement();

                                    write.WriteStartElement("NOTAS");//ABRE TAG NOTAS

                                }
                                else if ((control > 0) && !ind.Equals("9"))
                                {

                                    write.WriteStartElement("NOTA");//ABRE TAG NOTA

                                    write.WriteStartElement("RPS");
                                    
                                    String rpss = String.Format("{0:000000000000}", Int32.Parse(nRps)).Insert(4, "-").Insert(9, "-");

                                    write.WriteValue(rpss);

                                    write.WriteEndElement();

                                    write.WriteStartElement("LOTE");

                                    write.WriteValue(codnf);
                                     
                                    write.WriteEndElement();

                                    write.WriteStartElement("SEQUENCIA");

                                    write.WriteValue(numRps);

                                    write.WriteEndElement();

                                    write.WriteStartElement("DATAEMISSAO");

                                    write.WriteValue(dia + "/" + mes + "/" + ano);

                                    write.WriteEndElement();

                                    write.WriteStartElement("HORAEMISSAO");
                                    
                                    write.WriteValue(System.DateTime.Now.ToString("HH:mm:ss"));

                                    write.WriteEndElement();

                                    write.WriteStartElement("LOCAL");

                                    write.WriteValue("D");

                                    write.WriteEndElement();

                                    write.WriteStartElement("SITUACAO");

                                    write.WriteValue("1");

                                    write.WriteEndElement();

                                    write.WriteStartElement("RETIDO");

                                    write.WriteValue("N");

                                    write.WriteEndElement();

                                    write.WriteStartElement("ATIVIDADE");

                                    write.WriteValue(codtrib); //ADICIONAR NAS CONFIGURAÇÕES DA EMPRESA  "9313100"

                                    write.WriteEndElement();

                                    write.WriteStartElement("ALIQUOTAAPLICADA");

                                    write.WriteValue(Convert.ToDecimal(Double.Parse(aliquota) / 100).ToString("0.00").Replace(",","."));

                                    write.WriteEndElement();

                                    write.WriteStartElement("DEDUCAO");

                                    write.WriteValue("0.00");

                                    write.WriteEndElement();

                                    write.WriteStartElement("IMPOSTO");

                                    String tr = Math.Round(((Convert.ToDouble(valServ)) * (Convert.ToDouble(aliquota) / 10000))).ToString("#.##").Replace(",", "");
                                    
                                    //string imposto = (Convert.ToDecimal(valServ) * (Convert.ToDecimal(Double.Parse(aliquota) / 10000))).ToString().Replace(",","");
                                    
                                    write.WriteValue(tr.Insert(tr.Length - 2, ".")); //ADICIONAR NAS CONFIGURAÇÕES DA EMPRESA

                                    write.WriteEndElement();

                                    write.WriteStartElement("RETENCAO");

                                    write.WriteValue("0.00"); //ADICIONAR NAS CONFIGURAÇÕES DA EMPRESA

                                    write.WriteEndElement();

                                    write.WriteStartElement("OBSERVACAO");

                                    write.WriteValue(""); //ADICIONAR NAS CONFIGURAÇÕES DA EMPRESA

                                    write.WriteEndElement();

                                    write.WriteStartElement("CPFCNPJ");

                                    if (tpDoc == "1")
                                    {

                                        //00035030287817
                                        //08155119000145

                                        doc = doc.Substring(doc.Length - 11, 11);

                                    }

                                    write.WriteValue(doc);

                                    write.WriteEndElement();

                                    write.WriteStartElement("NOMERAZAO");

                                    write.WriteValue(nome);

                                    write.WriteEndElement();

                                    write.WriteStartElement("NOMEFANTASIA");

                                    write.WriteValue(nome);

                                    write.WriteEndElement();

                                    write.WriteStartElement("MUNICIPIO");

                                    write.WriteValue(cod_ibge);

                                    write.WriteEndElement();

                                    write.WriteStartElement("BAIRRO");

                                    write.WriteValue(bairro);

                                    write.WriteEndElement();

                                    write.WriteStartElement("CEP");

                                    write.WriteValue(cep);

                                    write.WriteEndElement();

                                    write.WriteStartElement("PREFIXO");

                                    write.WriteValue("RUA"); //VER COMO VAI SER FEITO

                                    write.WriteEndElement();

                                    write.WriteStartElement("LOGRADOURO");

                                    write.WriteValue(ende);

                                    write.WriteEndElement();

                                    write.WriteStartElement("COMPLEMENTO");

                                    write.WriteValue("");

                                    write.WriteEndElement();

                                    write.WriteStartElement("NUMERO");

                                    write.WriteValue(num);

                                    write.WriteEndElement();

                                    write.WriteStartElement("DENTROPAIS");

                                    write.WriteValue("S");

                                    write.WriteEndElement();

                                    write.WriteStartElement("PIS");

                                    write.WriteValue("0.00");

                                    write.WriteEndElement();

                                    write.WriteStartElement("COFINS");

                                    write.WriteValue("0.00");

                                    write.WriteEndElement();

                                    write.WriteStartElement("INSS");

                                    write.WriteValue("0.00");

                                    write.WriteEndElement();

                                    write.WriteStartElement("IR");

                                    write.WriteValue("0.00");

                                    write.WriteEndElement();

                                    write.WriteStartElement("CSLL");

                                    write.WriteValue("0.00");

                                    write.WriteEndElement();

                                    write.WriteStartElement("OUTRASRETENCOES");

                                    write.WriteValue("0.00");

                                    write.WriteEndElement();

                                    write.WriteStartElement("SERVICOS");//ABRE TAG SERVICOS

                                    write.WriteStartElement("SERVICO");//ABRE TAG SERVICO

                                    write.WriteStartElement("DESCRICAO");

                                    write.WriteValue(desc);

                                    write.WriteEndElement();

                                    write.WriteStartElement("VALORUNIT");

                                    String val = Convert.ToDouble(valServ).ToString();

                                    write.WriteValue(val.Insert(val.Length - 2, "."));

                                    write.WriteEndElement();

                                    write.WriteStartElement("QUANTIDADE");

                                    write.WriteValue("01");

                                    write.WriteEndElement();

                                    write.WriteEndElement();//FECHA TAG SERVICO

                                    write.WriteEndElement();//FECHA TAG SERVICOS

                                    write.WriteEndElement();//FECHA TAG NOTA

                                }

                                control++;

                            }

                            write.WriteEndElement();//FECHA TAG NOTAS

                            control = 0;

                            writer.Close();

                        }

                        conta = 0;

                    }

                }

                ret = true;

            }
            catch (Exception ex)
            {

                ret = false;

                cLog[0] = "Erro não esperado - " + ex.ToString();

                fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

                db.dataLog(ex.ToString());

            }

            return ret;

        }


        public Boolean gera_lote_taubate(string cod_nf, string cod_emp)//NAO FUNFA
        {

            Boolean ret = false;

            string natuop = "", regesp = "", optsmp = "", incult = "", status = "", aliquota = "", itemlista = "";

            string codtrib = "", muniprest = "", cnpj = "", inscricao = "";

            string cabecalho = "", rodape = "";

            string vlrImposto = "";

            double vlrTotal = 0, vlrImpTotal = 0;

            double vts = 0, vtd = 0, vti = 0;



            int ct = 0;

            decimal count = 0;

            string query = "select count(indice) from nf_dados where nf_dados.codigo_nf = " + cod_nf;

            SqlDataReader totalReader = db.rQuery(query);

            if (totalReader.HasRows)
            {
                if (totalReader.Read())
                {
                    count = Convert.ToDecimal(totalReader.GetInt32(0));
                    ct = totalReader.GetInt32(0);
                }
            }

            decimal nLotes = Math.Floor(count / tamLote);

            DateTime dt = DateTime.Now;

            string s = dt.ToString("yyyy-MM-dd");

            string path_exp = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar + "importacoes" + Path.DirectorySeparatorChar + cod_emp + Path.DirectorySeparatorChar + s + Path.DirectorySeparatorChar;

            DirectoryInfo dir = new DirectoryInfo(path_exp);

            if (!dir.Exists)
            {

                dir.Create();

            }

            string line = "";

            //StreamWriter writer = new StreamWriter(path_exp + "arq.txt");

            query = "SELECT * FROM dados_empresa INNER JOIN empresa ON empresa.codigo = dados_empresa.codigo_empresa WHERE codigo_empresa = '" + cod_emp + "'";

            //writer.WriteLine(query);

            SqlDataReader info = db.rQuery(query);

            try
            {

                if (info.HasRows)
                {

                    info.Read();

                    natuop = info.GetString(0);

                    regesp = info.GetString(1);

                    optsmp = info.GetString(2);

                    incult = info.GetString(3);

                    status = info.GetString(4);

                    aliquota = info.GetString(5);

                    itemlista = info.GetString(6);

                    codtrib = info.GetString(7);

                    muniprest = info.GetString(8);

                    cnpj = info.GetInt64(14).ToString();

                    inscricao = info.GetInt64(15).ToString();
                    
                }
                else
                {

                    cLog[0] = "Empresa não cadastrada ou não possui o cadastro completo";

                    fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

                    return false;

                }

                query = "select *, (cast(cod_uf.cod as varchar) + SUBSTRING(cod_ibge, 4, 6)) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                SqlDataReader dataReader = db.rQuery(query);

                if (dataReader.HasRows)
                {

                    if (!Directory.Exists(path_exp + cod_nf + " - " + s))
                    {

                        System.IO.Directory.CreateDirectory(path_exp + cod_nf + " - " + s);

                    }
                    else
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(path_exp + cod_nf + " - " + s);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dire in di.GetDirectories())
                        {
                            dire.Delete(true);
                        }

                    }
                    
                    StreamWriter writer = new StreamWriter(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + s + ".txt");

                    String d1 = "";
                    String d2 = "";

                    while (dataReader.Read())
                    {

                        linhaAt++;

                        ind = dataReader.GetString(1);

                        if (control == 0)
                        {

                            if (ind.Equals("1"))
                            {

                                vsLay = dataReader.GetString(2);
                                ie = dataReader.GetString(3);
                                diC = dataReader.GetString(4);
                                d1 = diC.Substring(6, 2) + "/" + diC.Substring(4, 2) + "/" + diC.Substring(0, 4);
                                dfC = dataReader.GetString(5);
                                d2 = dfC.Substring(6, 2) + "/" + dfC.Substring(4, 2) + "/" + dfC.Substring(0, 4);

                            }

                            cabecalho = "10|" + cnpj + "|" + d1 + "|" + d2 + "|1|||2.00";

                            writer.WriteLine(cabecalho);

                            control++;

                        }

                        if (ind.Equals("9"))
                        {

                            nLine = dataReader.GetString(6);

                            nServ = dataReader.GetString(7);

                        }

                        if (ind.Equals("2"))
                        {

                            rps = dataReader.GetString(8).Trim();
                            a = dataReader.GetString(9).Trim();
                            nRps = dataReader.GetString(10).Trim();
                            dtRps = dataReader.GetString(11).Trim();
                            t = dataReader.GetString(12).Trim();
                            valServ = dataReader.GetString(13).Trim();
                            dedu = dataReader.GetString(14).Trim();
                            aliq = dataReader.GetString(16).Trim();
                            iss = dataReader.GetString(17).Trim();
                            tpDoc = dataReader.GetString(18).Trim();
                            doc = dataReader.GetString(19).Trim();
                            nome = dataReader.GetString(21).Trim();
                            ende = dataReader.GetString(22).Trim();
                            num = dataReader.GetString(23).Trim();
                            comp = dataReader.GetString(24).Trim();
                            bairro = dataReader.GetString(25).Trim();
                            uf = dataReader.GetString(26).Trim();
                            cep = dataReader.GetString(27).Trim();
                            email = dataReader.GetString(28).Trim();
                            desc = dataReader.GetString(29).Trim();
                            cod_ibge = dataReader.GetString(36).Trim();
                            cidade = dataReader.GetString(31).Trim();
                            frmPag = dataReader.GetString(32).Trim();
                            email2 = dataReader.GetString(33).Trim();

                            nfse++;

                            numRps = nfse.ToString().PadLeft(8, '0');

                            //string dataFmt = dtRps.Substring(6, 2) + dtRps.Substring(4, 2) + dtRps.Substring(0, 4);
                            
                            //20160305
                            string dataFmt = dtRps.Substring(6, 2) + "/" + dtRps.Substring(4, 2) + "/" + dtRps.Substring(0, 4);

                            string codAtividade = "000604", cfps = "511", impRetido = "N", vlrAprxImposto = "";
                            string alqImpAprx = "", fontImpAprx = "", filler = "";
                            //string dataEmissao = s.Substring(8, 2) + s.Substring(5, 2) + s.Substring(0, 4);//"yyyy-MM-dd";
                            string dataEmissao = s.Substring(8, 2) + "/" + s.Substring(5, 2) + "/" + s.Substring(0, 4);//"yyyy-MM-dd";
                            string infoTransp = pad.PadLeft(525, ' '), qtdTransp = pad.PadLeft(14, '0'), espTrans = pad.PadLeft(50, ' ');
                            string infroTranspeso = pad.PadRight(28, '0'), infoTranspd = pad.PadLeft(34, '0');

                            if (tpDoc.Equals("2"))
                            {

                                //iDoc = "Cnpj";
                                iDoc = "J";

                            }
                            else if (tpDoc.Equals("1"))
                            {

                                //iDoc = "Cpf";
                                iDoc = "F";

                            }

                            //(double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.');
                            double value = double.Parse(valServ) / 100;

                            vts += value;

                            aliq = aliquota;

                            aliq = aliq.Replace(".", "");

                            aliq = aliq.Replace(",", "");

                            aliq = aliq.Replace("0", "");

                            double aliquotaD = double.Parse(aliq) / 100;

                            double vlimp = aliquotaD * value;
                            //double vlimp = 0.05 * value;

                            vlimp = Math.Round(vlimp, 2);
                            //2,997
                            vti += vlimp;

                            vlrImposto = vlimp.ToString("0.00").PadRight(4, '0');

                            vlrImpTotal += vti;

                            //vlrImposto = vlrImposto.Replace(",", "").PadLeft(14, '0');

                            //vlrImposto = vlrImposto.Replace(",", "");

                            string vlrServico = value.ToString("n2");

                            vlrTotal += double.Parse(vlrServico);

                            string vlrServicol = value.ToString("0.000000000").Replace(",", "").PadLeft(19, '0');

                            string vlrTotall = vlrServicol;

                            nRps = nRps.TrimStart('0');

                            fontImpAprx = "IBPT";

                            if (dedu.Length > 14)
                            {
                                dedu = dedu.Substring(0, 14);
                            }
                            else
                            {
                                dedu = dedu.PadLeft(14, '0');
                            }

                            double vai = (value * ((16.46) / 100));

                            vai = Math.Round(vai, 2);

                            vlrAprxImposto = vai.ToString("0.00").Replace(",", "").PadLeft(14, '0');

                            aliq = aliq.PadRight(3, '0');

                            aliq = aliq.PadLeft(5, '0');

                            if (tpDoc == "1")
                            {

                                //00035030287817
                                //08155119000145

                                doc = doc.Substring(doc.Length - 11, 11);

                            }

                            line = "20|RPS|" + nRps + "||" + dataFmt + "|NAO|" + codtrib + "|" + desc + "|" + value.ToString("n2").Replace(".", "") + "|||" + value.ToString("n2") + "|" + (aliquotaD * 100).ToString("n2") + "|" + vlrImposto + "||" + doc + "|" + nome;
                            line += "|RUA|" + ende + "|" + num + "||" + bairro + "|" + cidade + "|" + uf + "|" + cep + "||";
                            line += "||||||||";
                            line += "|" + email + "|||";
                            
                            writer.WriteLine(line); 

                        }

                        /*if (nfse == qntRps)
                        {
                            break;
                        }*/

                    }

                    //vts = Math.Round(vts);

                    //vti = Math.Round(vti);

                    line = "90|" + nfse + "|" + vlrTotal.ToString("n2").Replace(".", "") + "|" + vti.ToString("n2").Replace(".", "") + "|0,00|0,00|0|0,00";

                    writer.WriteLine(line);

                    control = 0;

                    writer.Close();

                    //}

                }

            }
            catch (Exception e)
            {
                ret = false;

                cLog[0] = "Erro não esperado - " + e.ToString();

                fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

                db.dataLog(e.ToString());

            }
            
            //line = "01" + cnpj + inscricao + "09";

            //writer.WriteLine(line);

            //writer.Close();

            return ret;

        }

        //FORMATO XML
        public Boolean gera_lote_rondo(string cod_nf, string cod_emp)
        {

            Boolean ret = false;

            string path_exp = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar + "importacoes" + Path.DirectorySeparatorChar + cod_emp + Path.DirectorySeparatorChar;

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberGroupSeparator = ".";

            string natuop = "", regesp = "", optsmp = "", incult = "", status = "", aliquota = "", itemlista = "";

            string codtrib = "", muniprest = "", cnpj = "", inscricao = "";

            try
            {

                DirectoryInfo dir = new DirectoryInfo(path_exp);

                if (!dir.Exists)
                {
                    dir.Create();
                }

                int ct = 0;

                decimal count = 0;

                string query = "select count(indice) from nf_dados where nf_dados.codigo_nf = " + cod_nf;

                SqlDataReader totalReader = db.rQuery(query);

                if (totalReader.HasRows)
                {
                    if (totalReader.Read())
                    {
                        count = Convert.ToDecimal(totalReader.GetInt32(0));
                        ct = totalReader.GetInt32(0);
                    }
                }

                decimal nLotes = Math.Floor(count / tamLote);

                DateTime dt = DateTime.Now;

                string s = dt.ToString("yyyy-MM-dd");

                query = "SELECT * FROM dados_empresa INNER JOIN empresa ON empresa.codigo = dados_empresa.codigo_empresa WHERE codigo_empresa = '" + cod_emp + "'";

                SqlDataReader info = db.rQuery(query);

                if (info.HasRows)
                {

                    info.Read();

                    natuop = info.GetString(0);

                    regesp = info.GetString(1);

                    optsmp = info.GetString(2);

                    incult = info.GetString(3);

                    status = info.GetString(4);

                    aliquota = info.GetString(5);

                    itemlista = info.GetString(6);

                    codtrib = info.GetString(7);

                    muniprest = info.GetString(8);

                    cnpj = info.GetInt64(13).ToString();

                    inscricao = info.GetInt64(14).ToString();

                }
                else
                {

                    cLog[0] = "Empresa não cadastrada ou não possui o cadastro completo";

                    //fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);
                    
                    fl.wFile("C://inetpub//wwwroot//sci_nfse//", "log.log", cLog, true);

                    return false;

                }

                string ns = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio";

                //query = "select *, (CONCAT(cod_uf.cod, SUBSTRING(cod_ibge, 4, 6))) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                query = "select *, (cast(cod_uf.cod as varchar) + SUBSTRING(cod_ibge, 4, 6)) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                SqlDataReader dataReader = db.rQuery(query);

                //reader = new StreamReader(path);

                if (dataReader.HasRows)
                {

                    if (!Directory.Exists(path_exp + cod_nf + " - " + s))
                    {

                        System.IO.Directory.CreateDirectory(path_exp + cod_nf + " - " + s);

                    }
                    else
                    {

                        System.IO.DirectoryInfo di = new DirectoryInfo(path_exp + cod_nf + " - " + s);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dire in di.GetDirectories())
                        {
                            dire.Delete(true);
                        }

                    }

                    for (int i = 0; i <= nLotes; i++)
                    {

                        nfse = 0;

                        var settings = new XmlWriterSettings
                        {
                            Encoding = new UTF8Encoding(false),
                            Indent = true,
                            IndentChars = @"  ",
                            NamespaceHandling = NamespaceHandling.OmitDuplicates,
                            NewLineChars = "\n",
                            NewLineHandling = NewLineHandling.Replace,
                        };

                        using (XmlWriter write = XmlWriter.Create(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + s + "-" + i + ".xml", settings))
                        {

                            write.WriteStartDocument();

                            write.WriteStartElement("EnviarLoteRpsEnvio", ns);

                            write.WriteAttributeString("xmlns", "tip", null, "http://www.e-nfs.com.br");

                            write.WriteStartElement("LoteRps");

                            write.WriteAttributeString("id", i.ToString());
                                                        
                            conta = ct - linhaAt;

                            if (conta < tamLote)
                            {

                                qntRps = conta - 1;

                            }
                            else
                            {

                                qntRps = tamLote;

                            }

                            //while ((line = reader.ReadLine()) != null)

                            while (dataReader.Read())
                            {

                                linhaAt++;

                                ind = dataReader.GetString(1);

                                if (control == 0)
                                {

                                    if (ind.Equals("1"))
                                    {

                                        vsLay = dataReader.GetString(2);
                                        ie = dataReader.GetString(3);
                                        diC = dataReader.GetString(4);
                                        dfC = dataReader.GetString(5);

                                    }

                                    write.WriteStartElement("NumeroLote");

                                    write.WriteValue(i);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Cnpj");

                                    write.WriteValue(cnpj);

                                    write.WriteEndElement();

                                    write.WriteStartElement("InscricaoMunicipal");

                                    write.WriteValue("");

                                    write.WriteEndElement();

                                    write.WriteStartElement("QuantidadeRps");

                                    write.WriteValue(qntRps);

                                    write.WriteEndElement();

                                    write.WriteStartElement("ListaRps");

                                    control++;

                                }

                                if (ind.Equals("9"))
                                {

                                    nLine = dataReader.GetString(6);

                                    nServ = dataReader.GetString(7);

                                }

                                if (ind.Equals("2"))
                                {

                                    rps = dataReader.GetString(8).Trim();
                                    a = dataReader.GetString(9).Trim();
                                    nRps = dataReader.GetString(10).Trim();
                                    dtRps = dataReader.GetString(11).Trim();
                                    t = dataReader.GetString(12).Trim();
                                    valServ = dataReader.GetString(13).Trim();
                                    dedu = dataReader.GetString(14).Trim();
                                    aliq = dataReader.GetString(16).Trim();
                                    iss = dataReader.GetString(17).Trim();
                                    tpDoc = dataReader.GetString(18).Trim();
                                    doc = dataReader.GetString(19).Trim();
                                    nome = dataReader.GetString(21).Trim();
                                    ende = dataReader.GetString(22).Trim();
                                    num = dataReader.GetString(23).Trim();
                                    comp = dataReader.GetString(24).Trim();
                                    bairro = dataReader.GetString(25).Trim();
                                    uf = dataReader.GetString(26).Trim();
                                    cep = dataReader.GetString(27).Trim();
                                    email = dataReader.GetString(28).Trim();
                                    desc = dataReader.GetString(29).Trim();
                                    cod_ibge = dataReader.GetString(36).Trim();
                                    cidade = dataReader.GetString(31).Trim();
                                    frmPag = dataReader.GetString(32).Trim();
                                    email2 = dataReader.GetString(33).Trim();

                                    nfse++;

                                    numRps = nfse.ToString().PadLeft(7, '0');

                                    string dataFmt = dtRps.Substring(0, 4) + "-" + dtRps.Substring(4, 2) + "-" + dtRps.Substring(6, 2) + "T00:01:00";

                                    string texto = desc + "\r\nVal. Aprox. Tributos R$ " +
                                        ((double.Parse(valServ) / 100) * 0.1788).ToString("0.00").Replace(',', '.')
                                        + ", correspondente a alíquota de (16,46%) Fonte: IBPT.";

                                    if (tpDoc.Equals("2"))
                                    {

                                        iDoc = "Cnpj";

                                    }
                                    else if (tpDoc.Equals("1"))
                                    {

                                        iDoc = "Cpf";

                                        doc = doc.Substring(3, 11);

                                    }

                                    write.WriteStartElement("Rps");

                                        write.WriteStartElement("InfRps");

                                        write.WriteAttributeString("id", nRps);

                                            write.WriteStartElement("IdentificacaoRps");

                                                write.WriteStartElement("Numero");
                                                
                                                write.WriteValue(qntRps);
                                                    
                                                write.WriteEndElement();

                                                write.WriteStartElement("Serie");

                                                write.WriteValue("1");

                                                write.WriteEndElement();

                                                write.WriteStartElement("Tipo");

                                                write.WriteValue("1");

                                                write.WriteEndElement();

                                            write.WriteEndElement();//FECHA IdentificacaoRps

                                            write.WriteStartElement("DataEmissao");

                                            write.WriteValue(dataFmt);

                                            write.WriteEndElement();

                                            write.WriteStartElement("NaturezaOperacao");

                                            write.WriteValue(natuop);

                                            write.WriteEndElement();

                                            write.WriteStartElement("RegimeEspecialTributacao");

                                            write.WriteValue(regesp);

                                            write.WriteEndElement();

                                            write.WriteStartElement("OptanteSimplesNacional");

                                            write.WriteValue(optsmp);

                                            write.WriteEndElement();

                                            write.WriteStartElement("IncentivadorCultural");

                                            write.WriteValue(incult);

                                            write.WriteEndElement();

                                            write.WriteStartElement("Status");

                                            write.WriteValue("1");

                                            write.WriteEndElement();

                                            write.WriteStartElement("Servico");

                                                write.WriteStartElement("Valores");

                                                    write.WriteStartElement("ValorServicos");

                                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("ValorDeducoes");

                                                    write.WriteValue("0.00");

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("ValorPis");

                                                    write.WriteValue("0.00");

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("ValorCofins");

                                                    write.WriteValue("0.00");

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("ValorInss");

                                                    write.WriteValue("0.00");

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("ValorIr");

                                                    write.WriteValue("0.00");

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("ValorCsll");

                                                    write.WriteValue("0.00");

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("IssRetido");

                                                    write.WriteValue("0");

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("ValorIss");
                                    
                                                    String tr = Math.Round(((Convert.ToDouble(valServ)) * (Convert.ToDouble(aliquota) / 10000))).ToString("#.##").Replace(",", "");
                                    
                                                    write.WriteValue(tr.Insert(tr.Length - 2, ".")); 

                                                    write.WriteEndElement();
                                    
                                                    write.WriteStartElement("ValorIssRetido");

                                                    write.WriteValue("0.00");

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("OutrasRetencoes");

                                                    write.WriteValue("0.00");

                                                    write.WriteEndElement();
                                                    
                                                    write.WriteStartElement("BaseCalculo");

                                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("Aliquota");

                                                    write.WriteValue(Convert.ToDecimal(Double.Parse(aliquota) / 100).ToString("0.00").Replace(",","."));

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("ValorLiquidoNfse");

                                                    Double val = double.Parse((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                                    Double imp = double.Parse(tr.Insert(tr.Length - 2, "."));

                                                    write.WriteValue(val - imp);

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("DescontoIncondicionado");

                                                    write.WriteValue("0.00");

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("DescontoCondicionado");

                                                    write.WriteValue("0.00");

                                                    write.WriteEndElement();
                                                                                                        
                                                write.WriteEndElement();//FECHA Valores

                                                write.WriteStartElement("ItemListaServico");

                                                write.WriteValue(itemlista);

                                                write.WriteEndElement();

                                                write.WriteStartElement("CodigoCnae");

                                                write.WriteValue("");

                                                write.WriteEndElement();

                                                write.WriteStartElement("CodigoTributacaoMunicipio");

                                                write.WriteValue(codtrib);

                                                write.WriteEndElement();

                                                write.WriteStartElement("Discriminacao");

                                                write.WriteValue(desc);

                                                write.WriteEndElement();

                                                write.WriteStartElement("CodigoMunicipio");

                                                write.WriteValue(muniprest);

                                                write.WriteEndElement();
                                                                
                                            write.WriteEndElement();//FECHA Servico
                                                                                
                                            write.WriteStartElement("Prestador");
                                                
                                                write.WriteStartElement("Cnpj");

                                                write.WriteValue(cnpj);

                                                write.WriteEndElement();
                                    
                                                write.WriteStartElement("InscricaoMunicipal");

                                                write.WriteValue("");

                                                write.WriteEndElement();

                                            write.WriteEndElement();//FECHA Prestador

                                            write.WriteStartElement("Tomador");

                                                write.WriteStartElement("IdentificacaoTomador");

                                                    write.WriteStartElement("CpfCnpj");
                                                    
                                                        write.WriteStartElement("Cpf");

                                                        write.WriteValue(doc);

                                                        write.WriteEndElement();

                                                    write.WriteEndElement();

                                                write.WriteEndElement();//FECHA IdentificacaoTomador

                                                write.WriteStartElement("RazaoSocial");

                                                write.WriteValue(nome);

                                                write.WriteEndElement();

                                                write.WriteStartElement("Endereco");

                                                    write.WriteStartElement("Endereco");

                                                    write.WriteValue(ende);

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("Numero");

                                                    write.WriteValue(num);

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("Complemento");

                                                    write.WriteValue(comp);

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("Bairro");

                                                    write.WriteValue(bairro);

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("Uf");

                                                    write.WriteValue(uf);

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("Cep");

                                                    write.WriteValue(cep);

                                                    write.WriteEndElement();

                                                write.WriteEndElement();//FECHA Endereco

                                                write.WriteStartElement("Contato");

                                                    write.WriteStartElement("Email");

                                                    write.WriteValue(email);

                                                    write.WriteEndElement();

                                                write.WriteEndElement();//FECHA Contato

                                            write.WriteEndElement();//FECHA Tomador

                                        write.WriteEndElement();//FECHA InfRps

                                    write.WriteEndElement();//FECHA Rps

                                    if (nfse == qntRps)
                                    {
                                        break;
                                    }

                                }

                            }

                            write.WriteEndElement();//FECHA ListaRps

                            write.WriteEndElement();//FECHA LoteRps

                            write.WriteEndElement();//FECHA EnviarLoteRpsEnvio

                            write.Close();
                            
                            control = 0;

                        }

                        conta = 0;

                    }

                }

                ret = true;

            }
            catch (Exception ex)
            {

                ret = false;

                cLog[0] = "Erro não esperado - " + ex.ToString();

                //fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

                fl.wFile("C://inetpub//wwwroot//sci_nfse//", "log.log", cLog, true);

            }

            return ret;

        }


        //FORMATO XML
        /*public Boolean gera_lote_presprud(string cod_nf, string cod_emp)
        {
            
            Boolean ret = false;

            string path_exp = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar + "importacoes" + Path.DirectorySeparatorChar + cod_emp + Path.DirectorySeparatorChar;

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberGroupSeparator = ".";

            string natuop = "", regesp = "", optsmp = "", incult = "", status = "", aliquota = "", itemlista = "";

            string codtrib = "", muniprest = "", cnpj = "", inscricao = "", senha = "";

            int ct = 0;

            db.dataLog("AQUI 1");

            try
            {

                DirectoryInfo dir = new DirectoryInfo(path_exp);

                if (!dir.Exists)
                {
                    dir.Create();
                }

                decimal count = 0;

                string query = "select count(indice) from nf_dados where nf_dados.codigo_nf = " + cod_nf;

                SqlDataReader totalReader = db.rQuery(query);

                if (totalReader.HasRows)
                {
                    if (totalReader.Read())
                    {
                        count = Convert.ToDecimal(totalReader.GetInt32(0));
                        ct = totalReader.GetInt32(0);
                    }
                }


                db.dataLog("AQUI 2");


                decimal nLotes = Math.Floor(count / tamLote);

                DateTime dt = DateTime.Now;

                string s = dt.ToString("yyyy-MM-dd");

                query = "SELECT * FROM dados_empresa INNER JOIN empresa ON empresa.codigo = dados_empresa.codigo_empresa WHERE codigo_empresa = '" + cod_emp + "'";

                SqlDataReader info = db.rQuery(query);
                
                db.dataLog("AQUI 3");

                if (info.HasRows)
                {

                    db.dataLog("tem linha");

                    info.Read();

                    db.dataLog("consegui ler");

                    natuop = info.GetString(0);

                    regesp = info.GetString(1);

                    optsmp = info.GetString(2);
                    
                    incult = info.GetString(3);

                    status = info.GetString(4);
                    
                    aliquota = info.GetString(5);

                    itemlista = info.GetString(6);

                    codtrib = info.GetString(7);

                    muniprest = info.GetString(8);

                    senha = info.GetString(11);

                    cnpj = info.GetInt64(14).ToString();

                    inscricao = info.GetInt64(15).ToString();



                }
                else
                {

                    cLog[0] = "Empresa não cadastrada ou não possui o cadastro completo";

                    //fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);
                    fl.wFile("C://inetpub//wwwroot//sci_nfse//", "log.log", cLog, true);
                    return false;

                }


                db.dataLog("AQUI 4");

                string ns = "http:\\www.ginfes.com.br\\servico_enviar_lote_rps_envio";

                //query = "select *, (CONCAT(cod_uf.cod, SUBSTRING(cod_ibge, 4, 6))) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                query = "select *, (cast(cod_uf.cod as varchar) + SUBSTRING(cod_ibge, 4, 6)) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                SqlDataReader dataReader = db.rQuery(query);

                //reader = new StreamReader(path);


                db.dataLog("AQUI 5");

                if (dataReader.HasRows)
                {

                    if (!Directory.Exists(path_exp + cod_nf + " - " + s))
                    {

                        System.IO.Directory.CreateDirectory(path_exp + cod_nf + " - " + s);

                    }
                    else
                    {

                        System.IO.DirectoryInfo di = new DirectoryInfo(path_exp + cod_nf + " - " + s);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dire in di.GetDirectories())
                        {
                            dire.Delete(true);
                        }

                    }


                    db.dataLog("AQUI 5");

                    for (int i = 0; i <= nLotes; i++)
                    {

                        nfse = 0;

                        var settings = new XmlWriterSettings
                        {
                            Encoding = new UTF8Encoding(false),
                            Indent = true,
                            IndentChars = @"  ",
                            NamespaceHandling = NamespaceHandling.OmitDuplicates,
                            NewLineChars = "\n",
                            NewLineHandling = NewLineHandling.Replace,
                        };


                        db.dataLog("AQUI 6");

                        using (XmlWriter write = XmlWriter.Create(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + s + "-" + i + ".xml", settings))
                        {

                            while (dataReader.Read())
                            {

                                linhaAt++;

                                ind = dataReader.GetString(1);

                                db.dataLog("Valor ind = " + ind);

                                if (control == 0)
                                {

                                    write.WriteStartDocument();

                                    write.WriteStartElement("soapenv", "Envelope");

                                    write.WriteAttributeString("xmlns", "soapenv", null, "http://schemas.xmlsoap.org/soap/envelope/");

                                    write.WriteAttributeString("xmlns", "sis", null, "http://www.sistema.com.br/Sistema.Ws.Nfse");

                                    write.WriteAttributeString("xmlns", "nfse", null, "http://www.sistema.com.br/Nfse/arquivos/nfse_3.xsd");

                                    write.WriteAttributeString("xmlns", "xd", null, "http://www.w3.org/2000/09/xmldsig#");

                                    write.WriteAttributeString("xmlns", "sis1", null, "http://www.sistema.com.br/Sistema.Ws.Nfse.Cn");

                                    write.WriteStartElement("soapenv", "Header");

                                    write.WriteStartElement("soapenv", "Body");

                                    write.WriteStartElement("sis", "RecepcionarLoteRps");

                                    write.WriteStartElement("sis", "EnviarLoteRpsEnvio");

                                    write.WriteStartElement("nfse", "LoteRps");

                                    write.WriteStartElement("nfse", "NumeroLote");

                                    write.WriteValue(i);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Cnpj");

                                    write.WriteValue(cnpj);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "InscricaoMunicipal");

                                    write.WriteValue(inscricao);

                                    write.WriteEndElement();

                                    conta = ct - linhaAt;

                                    if (conta < tamLote)
                                    {

                                        qntRps = conta - 1;

                                    }
                                    else
                                    {

                                        qntRps = tamLote;

                                    }

                                    write.WriteStartElement("nfse", "QuantidadeRps");

                                    write.WriteValue(qntRps);

                                    write.WriteEndElement();

                                    control++;

                                }

                                if (ind.Equals("2"))
                                {

                                    rps = dataReader.GetString(8).Trim();
                                    db.dataLog("rps");
                                    a = dataReader.GetString(9).Trim();
                                    db.dataLog("a");
                                    nRps = dataReader.GetString(10).Trim();
                                    db.dataLog("nrps");
                                    dtRps = dataReader.GetString(11).Trim();
                                    db.dataLog("dtrps");
                                    t = dataReader.GetString(12).Trim();
                                    db.dataLog("t");
                                    valServ = dataReader.GetString(13).Trim();
                                    db.dataLog("valServ");
                                    dedu = dataReader.GetString(14).Trim();
                                    aliq = dataReader.GetString(16).Trim();
                                    iss = dataReader.GetString(17).Trim();
                                    tpDoc = dataReader.GetString(18).Trim();
                                    doc = dataReader.GetString(19).Trim();
                                    nome = dataReader.GetString(21).Trim();
                                    ende = dataReader.GetString(22).Trim();
                                    num = dataReader.GetString(23).Trim();
                                    comp = dataReader.GetString(24).Trim();
                                    bairro = dataReader.GetString(25).Trim();
                                    uf = dataReader.GetString(26).Trim();
                                    cep = dataReader.GetString(27).Trim();
                                    email = dataReader.GetString(28).Trim();
                                    desc = dataReader.GetString(29).Trim();
                                    cod_ibge = dataReader.GetString(36).Trim();
                                    cidade = dataReader.GetString(31).Trim();
                                    frmPag = dataReader.GetString(32).Trim();
                                    email2 = dataReader.GetString(33).Trim();

                                    nfse++;

                                    string dia = dtRps.Substring(6, 2);

                                    string mes = dtRps.Substring(4, 2);

                                    string ano = dtRps.Substring(0, 4);

                                    write.WriteStartElement("nfse", "Rps");

                                    write.WriteStartElement("nfse", "InfRps");

                                    write.WriteAttributeString("id", "");//FALTA VALOR

                                    write.WriteStartElement("nfse", "IdentificacaoRps");

                                    write.WriteStartElement("nfse", "Numero");

                                    write.WriteValue(nRps.TrimStart('0'));
                                    
                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Serie");

                                    string serie = "1";

                                    if (nRps.TrimStart('0').Length >= 15) serie = "2";

                                    write.WriteValue(serie);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Tipo");

                                    write.WriteValue(1);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//FEHCA IdentificacaoRps

                                    write.WriteStartElement("nfse", "DataEmissao");

                                    write.WriteValue(ano + "-" + dia + "-" + mes);//FALTA VALOR AAAA-DD-MM

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "NaturezaOperacao");

                                    write.WriteValue(natuop);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "RegimeEspecialTributacao");

                                    write.WriteValue(regesp);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "OptanteSimplesNacional");

                                    write.WriteValue(optsmp);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "IncentivadorCultural");

                                    write.WriteValue(incult);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Status");

                                    write.WriteValue(status);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "OutrasInformacoes");

                                    write.WriteValue("");

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Servico");

                                    write.WriteStartElement("nfse", "Valores");

                                    write.WriteStartElement("nfse", "ValorServicos");

                                    //write.WriteValue(valServ.Insert(valServ.Length - 2, "."));

                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "ValorIss");

                                    double valAliq = Math.Round(((Convert.ToDouble(valServ)) * (Convert.ToDouble(aliquota) / 10000)));

                                    db.dataLog(valAliq.ToString());

                                    String tr = valAliq.ToString("#.##");

                                    write.WriteValue(tr.Insert(tr.Length - 2, "."));

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "BaseCalculo");

                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Aliquota");

                                    write.WriteValue(Convert.ToDecimal(Double.Parse(aliquota) / 100).ToString("0.00").Replace(",", "."));

                                    //write.WriteValue(tr.Insert(tr.Length - 2, "."));

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "ValorLiquidoNfse");

                                    double val = Convert.ToDouble(valServ);

                                    db.dataLog(val.ToString());

                                    double valLiq = val - valAliq;

                                    //write.WriteValue(valLiq.ToString().Insert(valLiq.ToString().Length - 2, "."));

                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                    write.WriteEndElement();

                                    write.WriteEndElement();//FECHA Valores

                                    write.WriteStartElement("nfse", "ItemListaServico");

                                    write.WriteValue(itemlista);//FALTA VALOR

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "CodigoCnae");

                                    write.WriteValue("");//FALTA VALOR

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "CodigoTributacaoMunicipio");

                                    write.WriteValue(codtrib);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Discriminacao");

                                    write.WriteValue("");

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "CodigoMunicipio");

                                    write.WriteValue(muniprest);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "ItensServico");

                                    write.WriteStartElement("nfse", "Descricao");

                                    write.WriteValue(desc);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Quantidade");

                                    write.WriteValue(1);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "ValorUnitario");

                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "IssTributavel");

                                    write.WriteValue(iss);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//FECHA ItensServico

                                    write.WriteEndElement();//FECHA Servico

                                    write.WriteStartElement("nfse", "Prestador");

                                    write.WriteStartElement("nfse", "Cnpj");

                                    write.WriteValue(cnpj);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "InscricaoMunicipal");

                                    write.WriteValue(inscricao);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//FECHA Prestador

                                    write.WriteStartElement("nfse", "Tomador");

                                    write.WriteStartElement("nfse", "IdentificacaoTomador");

                                    write.WriteStartElement("nfse", "CpfCnpj");

                                    write.WriteStartElement("nfse", "Cpf");

                                    doc = doc.Substring(3, 11);

                                    write.WriteValue(doc);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//FECHA CpfCnpj

                                    write.WriteStartElement("nfse", "RazaoSocial");

                                    write.WriteValue(nome);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Endereco");

                                    write.WriteStartElement("nfse", "Endereco");

                                    write.WriteValue(ende);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Numero");

                                    write.WriteValue(num);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Complemento");

                                    write.WriteValue(comp);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Bairro");

                                    write.WriteValue(bairro);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "CodigoMunicipio");

                                    write.WriteValue(muniprest);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Uf");

                                    write.WriteValue(uf);

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Cep");

                                    write.WriteValue(cep);//FALTA VALOR

                                    write.WriteEndElement();

                                    write.WriteEndElement();//FECHA Endereco

                                    write.WriteStartElement("nfse", "Contato");

                                    write.WriteStartElement("nfse", "Telefone");

                                    write.WriteValue("");//FALTA VALOR

                                    write.WriteEndElement();

                                    write.WriteStartElement("nfse", "Email");

                                    write.WriteValue(email);//FALTA VALOR

                                    write.WriteEndElement();

                                    write.WriteEndElement();//FEHCA Contato

                                    write.WriteEndElement();//FECHA IdentificacaoTomador

                                    write.WriteEndElement();//FEHCA Tomador

                                    write.WriteEndElement();//FECHA InfRps

                                    write.WriteEndElement();//FECHA RPS
                                    
                                }

                                ct++;

                            }

                            write.WriteEndElement();//FECHA LoteRps

                            write.WriteEndElement();//FECHA EnviarLoteRpsEnvio

                            write.WriteStartElement("sis", "pParam");

                            write.WriteStartElement("sis", "P1");

                            write.WriteValue(cnpj);//FALTA VALOR

                            write.WriteEndElement();

                            write.WriteStartElement("sis", "P2");

                            write.WriteValue(senha);//FALTA VALOR

                            write.WriteEndElement();

                            write.WriteEndElement();//FECHA pParam

                            write.WriteEndElement();//FECHA RecepcionarLoteRps

                            write.WriteEndElement();//FECHA BODYEnvelope

                            write.WriteEndElement();//FECHA Envelope

                            write.Close();



                            control = 0;

                        }

                        conta = 0;

                    }

                }

                ret = true;

            }
            catch (Exception ex)
            {

                db.dataLog("Erro não esperado - " + ex.ToString());

                ret = false;

                cLog[0] = "Erro não esperado - " + ex.ToString();

                //fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

                fl.wFile("C://inetpub//wwwroot//sci_nfse//", "log.log", cLog, true);

            }
            
            return ret;

        }*/

        //FORMATO TXT
        public Boolean gera_lote_presprud(string cod_nf, string cod_emp)
        {

            Boolean ret = false;
            
            string path_exp = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar + "importacoes" + Path.DirectorySeparatorChar + cod_emp + Path.DirectorySeparatorChar;

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            nfi.NumberGroupSeparator = ".";

            string cabecalho = "", rodape = "";

            string natuop = "", regesp = "", optsmp = "", incult = "", status = "", aliquota = "", itemlista = "";

            string codtrib = "", muniprest = "", cnpj = "", inscricao = "";
            
            double vts = 0, vtd = 0, vti = 0;

            string vlrImposto = "";

            double vlrTotal = 0, vlrImpTotal = 0;
            
            string ibge_tomador = "";

            string codEmpresa = "";

            int codnf = 0;

            tamLote = 100;
            
            try
            {

                DirectoryInfo dir = new DirectoryInfo(path_exp);

                if (!dir.Exists)
                {
                    dir.Create();
                }

                int ct = 0;

                decimal count = 0;

                string query = "select count(indice) from nf_dados where nf_dados.codigo_nf = " + cod_nf;

                SqlDataReader totalReader = db.rQuery(query);

                if (totalReader.HasRows)
                {

                    if (totalReader.Read())
                    {

                        count = Convert.ToDecimal(totalReader.GetInt32(0));
                        ct = totalReader.GetInt32(0);

                    }

                }

                decimal nLotes = Math.Floor(count / tamLote) + 1;

                db.dataLog(nLotes.ToString());

                DateTime dt = DateTime.Now;

                string s = dt.ToString("yyyy-MM-dd");

                query = "SELECT * FROM dados_empresa INNER JOIN empresa ON empresa.codigo = dados_empresa.codigo_empresa WHERE codigo_empresa = '" + cod_emp + "'";

                SqlDataReader info = db.rQuery(query);

                if (info.HasRows)
                {

                    info.Read();

                    natuop = info.GetString(0);

                    regesp = info.GetString(1);

                    optsmp = info.GetString(2);

                    incult = info.GetString(3);

                    status = info.GetString(4);

                    aliquota = info.GetString(5);

                    itemlista = info.GetString(6);

                    codtrib = info.GetString(7);

                    muniprest = info.GetString(8);

                    cnpj = info.GetInt64(14).ToString();

                    inscricao = info.GetInt64(15).ToString();

                    codnf = info.GetInt32(16);

                    codEmpresa = info.GetInt32(12).ToString();

                }
                else
                {

                    cLog[0] = "Empresa não cadastrada ou não possui o cadastro completo";

                    fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

                    return false;

                }

                string ns = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio";

                //query = "select *, (CONCAT(cod_uf.cod, SUBSTRING(cod_ibge, 4, 6))) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                query = "select *, (cast(cod_uf.cod as varchar) + SUBSTRING(cod_ibge, 4, 6)) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                SqlDataReader dataReader = db.rQuery(query);

                //reader = new StreamReader(path);

                if (dataReader.HasRows)
                {

                    dt = DateTime.Now;

                    //int sec = dt.Second;

                    int sec = 10;
                    
                    for (int i = 0; i <= nLotes; i++)
                    {

                        if (!Directory.Exists(path_exp + cod_nf + " - " + s))
                        {

                            System.IO.Directory.CreateDirectory(path_exp + cod_nf + " - " + s);

                        }
                        else
                        {
                            System.IO.DirectoryInfo di = new DirectoryInfo(path_exp + cod_nf + " - " + s);

                            foreach (FileInfo file in di.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo dire in di.GetDirectories())
                            {
                                dire.Delete(true);
                            }

                        }
                                               

                        string datanome = dt.ToString("yyyyMMddHHmm") + sec++.ToString();

                        db.dataLog("abre arquivo : " + path_exp + cnpj + datanome + ".txt");

                        //StreamWriter writer = new StreamWriter(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + s + ".txt");
                        StreamWriter writer = new StreamWriter(path_exp + cnpj + datanome + ".txt", true);

                        String d1 = "";
                        String d2 = "";

                        int ctLinha = 0;

                        while (dataReader.Read())
                        {

                            linhaAt++;

                            ind = dataReader.GetString(1);

                            db.dataLog("ind : " + ind + " linhaAt : " + linhaAt);

                            if (control == 0)
                            {

                                if (ind.Equals("1"))
                                {

                                    vsLay = dataReader.GetString(2);
                                    ie = dataReader.GetString(3);
                                    diC = dataReader.GetString(4);
                                    d1 = diC.Substring(6, 2) + "/" + diC.Substring(4, 2) + "/" + diC.Substring(0, 4);
                                    dfC = dataReader.GetString(5);
                                    d2 = dfC.Substring(6, 2) + "/" + dfC.Substring(4, 2) + "/" + dfC.Substring(0, 4);

                                }

                                cabecalho = "0|1.06|";

                                writer.WriteLine(cabecalho);

                                control++;

                            }
                            
                            if (ind.Equals("9"))
                            {

                                nLine = dataReader.GetString(6);

                                nServ = dataReader.GetString(7);

                            }

                            if (ind.Equals("2"))
                            {

                                //codnf = dataReader.GetString(0).Trim();
                                rps = dataReader.GetString(8).Trim();
                                a = dataReader.GetString(9).Trim();
                                nRps = dataReader.GetString(10).Trim();
                                dtRps = dataReader.GetString(11).Trim();
                                t = dataReader.GetString(12).Trim();
                                valServ = dataReader.GetString(13).Trim();
                                dedu = dataReader.GetString(14).Trim();
                                aliq = dataReader.GetString(16).Trim();
                                iss = dataReader.GetString(17).Trim();
                                tpDoc = dataReader.GetString(18).Trim();
                                doc = dataReader.GetString(19).Trim();
                                nome = dataReader.GetString(21).Trim();
                                ende = dataReader.GetString(22).Trim();
                                num = dataReader.GetString(23).Trim();
                                comp = dataReader.GetString(24).Trim();
                                bairro = dataReader.GetString(25).Trim();
                                uf = dataReader.GetString(26).Trim();
                                cep = dataReader.GetString(27).Trim();
                                email = dataReader.GetString(28).Trim();
                                desc = dataReader.GetString(29).Trim();
                                cod_ibge = dataReader.GetString(36).Trim();
                                cidade = dataReader.GetString(31).Trim();
                                frmPag = dataReader.GetString(32).Trim();
                                email2 = dataReader.GetString(33).Trim();
                                ibge_tomador = dataReader.GetString(36).Trim();

                                nfse++;

                                numRps = nfse.ToString().PadLeft(8, '0');

                                //string dataFmt = dtRps.Substring(6, 2) + dtRps.Substring(4, 2) + dtRps.Substring(0, 4);

                                //20160305
                                //string dataFmt = dtRps.Substring(6, 2) + "/" + dtRps.Substring(4, 2) + "/" + dtRps.Substring(0, 4);
                                dtRps = dtRps.Insert(4, "-");
                                dtRps = dtRps.Insert(7, "-");

                                string codAtividade = "000604", cfps = "511", impRetido = "N", vlrAprxImposto = "";
                                string alqImpAprx = "", fontImpAprx = "", filler = "";
                                //string dataEmissao = s.Substring(8, 2) + s.Substring(5, 2) + s.Substring(0, 4);//"yyyy-MM-dd";
                                string dataEmissao = s.Substring(8, 2) + "/" + s.Substring(5, 2) + "/" + s.Substring(0, 4);//"yyyy-MM-dd";
                                string infoTransp = pad.PadLeft(525, ' '), qtdTransp = pad.PadLeft(14, '0'), espTrans = pad.PadLeft(50, ' ');
                                string infroTranspeso = pad.PadRight(28, '0'), infoTranspd = pad.PadLeft(34, '0');

                                if (tpDoc.Equals("2"))
                                {

                                    //iDoc = "Cnpj";
                                    iDoc = "J";

                                }
                                else if (tpDoc.Equals("1"))
                                {

                                    //iDoc = "Cpf";
                                    iDoc = "F";

                                }

                                //(double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.');
                                double value = double.Parse(valServ) / 100;

                                vts += value;

                                aliq = aliquota;

                                aliq = aliq.Replace(".", "");

                                aliq = aliq.Replace(",", "");

                                aliq = aliq.Replace("0", "");

                                double aliquotaD = double.Parse(aliq) / 100;

                                double ali = double.Parse(aliq);

                                double vlimp = aliquotaD * value;
                                //double vlimp = 0.05 * value;

                                vlimp = Math.Round(vlimp, 2);
                                //2,997
                                vti += vlimp;

                                vlrImposto = vlimp.ToString("0.00").PadRight(4, '0');

                                vlrImpTotal += vti;

                                //vlrImposto = vlrImposto.Replace(",", "").PadLeft(14, '0');

                                //vlrImposto = vlrImposto.Replace(",", "");

                                string vlrServico = value.ToString("n2");

                                vlrTotal += double.Parse(vlrServico);

                                string vlrServicol = value.ToString("0.000000000").Replace(",", "").PadLeft(19, '0');

                                string vlrTotall = vlrServicol;

                                nRps = nRps.TrimStart('0');

                                fontImpAprx = "IBPT";

                                if (dedu.Length > 14)
                                {
                                    dedu = dedu.Substring(0, 14);
                                }
                                else
                                {
                                    dedu = dedu.PadLeft(14, '0');
                                }

                                double vai = (value * ((16.46) / 100));

                                vai = Math.Round(vai, 2);

                                vlrAprxImposto = vai.ToString("0.00").Replace(",", "").PadLeft(14, '0');

                                aliq = aliq.PadRight(3, '0');

                                aliq = aliq.PadLeft(5, '0');

                                if (tpDoc == "1")
                                {

                                    //00035030287817
                                    //08155119000145

                                    doc = doc.Substring(doc.Length - 11, 11);

                                }

                                /*line = "20|RPS|" + nRps + "||" + dataFmt + "|NAO|" + codtrib + "|" + desc + "|" + value.ToString("n2").Replace(".", "") + "|||" + value.ToString("n2") + "|" + (aliquotaD * 100).ToString("n2") + "|" + vlrImposto + "||" + doc + "|" + nome;
                                line += "|RUA|" + ende + "|" + num + "||" + bairro + "|" + cidade + "|" + uf + "|" + cep + "||";
                                line += "||||||||";
                                line += "|" + email + "|||";*/
                                //58
                                line = "1|" + codnf + "|" + cnpj + "|" + inscricao + "|" + muniprest + "|" + nRps + "|E|" + dtRps + "|" + natuop + "|" + regesp + "|" + optsmp + "|1|" + itemlista + "|" + codtrib;
                                line += "||" + desc + "|" + value.ToString("0.00").Replace(',', '.') + "|0.00|0.00|0.00|0.00|0.00|0.00|2|" + vlimp.ToString("0.00").Replace(',', '.') + "|0.00|0.00|" + value.ToString("0.00").Replace(',', '.') + "|";
                                line += ali.ToString("0.0000").Replace(",", ".") +"|" + value.ToString("0.00").Replace(',', '.') + "|||" + tpDoc + "|" + doc + "||" + nome + "|" + ende + "|" + num + "||" + bairro + "|" + uf + "|" + cep;
                                line += "|" + ibge_tomador + "||" + email + "|||||||||" + cod_ibge + "|SP||||";

                                writer.WriteLine(line);

                                line = "2|" + desc + "|1.00000|" + value.ToString("0.00").Replace(',', '.') + "|1|";

                                writer.WriteLine(line);

                                ctLinha++;
                                
                                if (ctLinha == 99)
                                {
                                    ctLinha = 0;
                                    db.dataLog("break");
                                    break;
                                }

                            }

                            Task.Delay(500);

                        }

                        //vts = Math.Round(vts);

                        //vti = Math.Round(vti);

                        //line = "90|" + nfse + "|" + vlrTotal.ToString("n2").Replace(".", "") + "|" + vti.ToString("n2").Replace(".", "") + "|0,00|0,00|0|0,00";

                        //writer.WriteLine(line);

                        codnf++;

                        db.dataLog("fecha arquivo e control 0");

                        control = 0;

                        writer.Close();

                        //}

                    }

                    String update = "UPDATE empresa SET nLote = " + codnf + " WHERE codigo = '" + codEmpresa + "'";

                    db.execQuery(update);

                }

                ret = true;

            }
            catch(Exception e)
            {

                db.dataLog("EXCEPTION GERA_LOTE_PRESPRUD. ERRO : " + e.ToString());

            }

            return ret;

        }

        //FORMATO XML
        public Boolean gera_lote_cuiaba(string cod_nf, string cod_emp)
        {

            Boolean ret = false;

            string path_exp = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar + "importacoes" + Path.DirectorySeparatorChar + cod_emp + Path.DirectorySeparatorChar;

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            nfi.NumberGroupSeparator = ".";

            string natuop = "", regesp = "", optsmp = "", incult = "", status = "", aliquota = "", itemlista = "";

            string codtrib = "", muniprest = "", cnpj = "", inscricao = "";

            try
            {

                DirectoryInfo dir = new DirectoryInfo(path_exp);

                if (!dir.Exists)
                {
                    dir.Create();
                }

                int ct = 0;

                decimal count = 0;

                string query = "select count(indice) from nf_dados where nf_dados.codigo_nf = " + cod_nf;

                SqlDataReader totalReader = db.rQuery(query);

                if (totalReader.HasRows)
                {

                    if (totalReader.Read())
                    {

                        count = Convert.ToDecimal(totalReader.GetInt32(0));
                        ct = totalReader.GetInt32(0);

                    }

                }

                decimal nLotes = Math.Floor(count / tamLote);

                DateTime dt = DateTime.Now;

                string s = dt.ToString("yyyy-MM-dd");

                query = "SELECT * FROM dados_empresa INNER JOIN empresa ON empresa.codigo = dados_empresa.codigo_empresa WHERE codigo_empresa = '" + cod_emp + "'";

                SqlDataReader info = db.rQuery(query);

                if (info.HasRows)
                {

                    info.Read();

                    natuop = info.GetString(0);

                    regesp = info.GetString(1);

                    optsmp = info.GetString(2);

                    incult = info.GetString(3);

                    status = info.GetString(4);

                    aliquota = info.GetString(5);

                    itemlista = info.GetString(6);

                    codtrib = info.GetString(7);

                    muniprest = info.GetString(8);

                    cnpj = info.GetInt64(13).ToString();

                    inscricao = info.GetInt64(14).ToString();

                }
                else
                {

                    cLog[0] = "Empresa não cadastrada ou não possui o cadastro completo";

                    fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

                    return false;

                }

                string ns = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio";

                //query = "select *, (CONCAT(cod_uf.cod, SUBSTRING(cod_ibge, 4, 6))) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                query = "select *, (cast(cod_uf.cod as varchar) + SUBSTRING(cod_ibge, 4, 6)) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                SqlDataReader dataReader = db.rQuery(query);

                //reader = new StreamReader(path);

                if (dataReader.HasRows)
                {

                    if (!Directory.Exists(path_exp + cod_nf + " - " + s))
                    {

                        System.IO.Directory.CreateDirectory(path_exp + cod_nf + " - " + s);

                    }
                    else
                    {

                        System.IO.DirectoryInfo di = new DirectoryInfo(path_exp + cod_nf + " - " + s);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dire in di.GetDirectories())
                        {
                            dire.Delete(true);
                        }

                    }

                    for (int i = 0; i <= nLotes; i++)
                    {

                        nfse = 0;

                        var settings = new XmlWriterSettings
                        {

                            Encoding = new UTF8Encoding(false),
                            Indent = true,
                            IndentChars = @"  ",
                            NamespaceHandling = NamespaceHandling.OmitDuplicates,
                            NewLineChars = "\n",
                            NewLineHandling = NewLineHandling.Replace,

                        };

                        using (XmlWriter write = XmlWriter.Create(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + s + "-" + i + ".xml", settings))
                        {

                            write.WriteStartDocument();

                            write.WriteStartElement("EnviarLoteRpsEnvio", ns);

                            write.WriteAttributeString("xmlns", null, "http://www.issnetonline.com.br/webserviceabrasf/vsd/servico_enviar_lote_rps_envio.xsd");

                            write.WriteAttributeString("xmlns", "tc", null, "http://www.issnetonline.com.br/webserviceabrasf/vsd/tipos_complexos.xsd");

                                write.WriteStartElement("LoteRps");

                                    write.WriteStartElement("tc", "NumeroLote");

                                    write.WriteValue("");//FALTA VALOR

                                    write.WriteEndElement();

                                    write.WriteStartElement("tc", "CpfCnpj");

                                        write.WriteStartElement("tc", "Cnpj");

                                        write.WriteValue("");//FALTA VALOR

                                        write.WriteEndElement();

                                    write.WriteEndElement();//FEHCA CpfCnpj

                                    write.WriteStartElement("tc", "InscricaoMunicipal");

                                    write.WriteValue("");//FALTA VALOR

                                    write.WriteEndElement();

                                    write.WriteStartElement("tc", "QuantidadeRps");

                                    write.WriteValue("");//FALTA VALOR

                                    write.WriteEndElement();

                                    write.WriteStartElement("tc", "ListaRps");

                                        write.WriteStartElement("tc", "Rps");

                                            write.WriteStartElement("tc", "InfRps");

                                                write.WriteStartElement("tc", "IdentificacaoRps");

                                                    write.WriteStartElement("tc", "Numero");

                                                    write.WriteValue("");//FALTA VALOR

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("tc", "Serie");

                                                    write.WriteValue("");//FALTA VALOR

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("tc", "Tipo");

                                                    write.WriteValue("");//FALTA VALOR

                                                    write.WriteEndElement();

                                                write.WriteEndElement();//FECHA IdentificacaoRps

                                                write.WriteStartElement("tc", "DataEmissao");

                                                write.WriteValue("");//FALTA VALOR

                                                write.WriteEndElement();

                                                write.WriteStartElement("tc", "NaturezaOperacao");

                                                write.WriteValue("");//FALTA VALOR

                                                write.WriteEndElement();

                                                write.WriteStartElement("tc", "OptanteSimplesNacional");

                                                write.WriteValue("");//FALTA VALOR

                                                write.WriteEndElement();

                                                write.WriteStartElement("tc", "IncentivadorCultural");

                                                write.WriteValue("");//FALTA VALOR

                                                write.WriteEndElement();

                                                write.WriteStartElement("tc", "Status");

                                                write.WriteValue("");//FALTA VALOR

                                                write.WriteEndElement();

                                                write.WriteStartElement("tc", "RegimeEspecialTributacao");

                                                write.WriteValue("");//FALTA VALOR

                                                write.WriteEndElement();

                                                write.WriteStartElement("tc", "Servico");

                                                    write.WriteStartElement("tc", "Valores");

                                                        write.WriteStartElement("tc", "ValorServicos");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                        write.WriteStartElement("tc", "ValorIss");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                        write.WriteStartElement("tc", "BaseCalculo");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                        write.WriteStartElement("tc", "Aliquota");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                        write.WriteStartElement("tc", "ValorLiquidoNfse");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                    write.WriteEndElement();//FECHA Valores

                                                    write.WriteStartElement("tc", "ItemListaServico");

                                                    write.WriteValue("");//FALTA VALOR

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("tc", "CodigoCnae");

                                                    write.WriteValue("");//FALTA VALOR

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("tc", "CodigoTributacaoMunicipio");

                                                    write.WriteValue("");//FALTA VALOR

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("tc", "Discriminacao");

                                                    write.WriteValue("");//FALTA VALOR

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("tc", "MunicipioPrestacaoServico");

                                                    write.WriteValue("");//FALTA VALOR

                                                    write.WriteEndElement();

                                                write.WriteEndElement();//FECHA Servico

                                                write.WriteStartElement("tc", "Prestador");

                                                    write.WriteStartElement("tc", "CpfCnpj");

                                                        write.WriteStartElement("tc", "Cnpj");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                    write.WriteEndElement();//FECHA CpfCnpj

                                                    write.WriteStartElement("tc", "InscricaoMunicipal");

                                                    write.WriteValue("");//FALTA VALOR

                                                    write.WriteEndElement();

                                                write.WriteEndElement();//FECHA Prestador

                                                write.WriteStartElement("tc", "Tomador");

                                                    write.WriteStartElement("tc", "IdentificacaoTomador");

                                                        write.WriteStartElement("tc", "CpfCnpj");

                                                            write.WriteStartElement("tc", "Cpf");

                                                            write.WriteValue("");//FALTA VALOR

                                                            write.WriteEndElement();

                                                        write.WriteEndElement();//FECHA CpfCnpj

                                                    write.WriteEndElement();//FECHA IdentificacaoTomador

                                                    write.WriteStartElement("tc", "RazaoSocial");

                                                    write.WriteValue("");//FALTA VALOR

                                                    write.WriteEndElement();

                                                    write.WriteStartElement("tc", "Endereco");

                                                        write.WriteStartElement("tc", "Endereco");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                        write.WriteStartElement("tc", "Numero");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                        write.WriteStartElement("tc", "Complemento");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                        write.WriteStartElement("tc", "Bairro");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                        write.WriteStartElement("tc", "Cidade");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                        write.WriteStartElement("tc", "Estado");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                        write.WriteStartElement("tc", "Cep");

                                                        write.WriteValue("");//FALTA VALOR

                                                        write.WriteEndElement();

                                                    write.WriteEndElement();//FECHA Endereco

                                                write.WriteEndElement();//FECHA Tomador

                                            write.WriteEndElement();//FECHA InfRps

                                        write.WriteEndElement();//FECHA Rps

                                    write.WriteEndElement();//FECHA ListaRps

                                write.WriteEndElement();//FECHA LoteRps

                            write.WriteEndElement();//FEHCA EnviarLoteRpsEnvio

                            write.Close();

                            control = 0;

                        }

                        conta = 0;

                    }

                }

                ret = true;

            }
            catch (Exception ex)
            {

                ret = false;

                cLog[0] = "Erro não esperado - " + ex.ToString();

                fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

            }

            return ret;

        }

        //FORMATO XML
        //LAYOUT UBERABA
        public Boolean gera_lote_uberaba(string cod_nf, string cod_emp)
        {

            Boolean ret = false;

            string path_exp = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar + "importacoes" + Path.DirectorySeparatorChar + cod_emp + Path.DirectorySeparatorChar;

            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberGroupSeparator = ".";

            string natuop = "", regesp = "", optsmp = "", incult = "", status = "", aliquota = "", itemlista = "";

            string codtrib = "", muniprest = "", cnpj = "", inscricao = "";

            int codnf = 0;

            try
            {

                DirectoryInfo dir = new DirectoryInfo(path_exp);

                if (!dir.Exists)
                {
                    dir.Create();
                }

                int ct = 0;

                decimal count = 0;

                string query = "select count(indice) from nf_dados where nf_dados.codigo_nf = " + cod_nf;

                SqlDataReader totalReader = db.rQuery(query);

                if (totalReader.HasRows)
                {
                    if (totalReader.Read())
                    {
                        count = Convert.ToDecimal(totalReader.GetInt32(0));
                        ct = totalReader.GetInt32(0);
                    }
                }

                decimal nLotes = Math.Floor(count / tamLote);

                DateTime dt = DateTime.Now;

                string s = dt.ToString("yyyy-MM-dd");

                query = "SELECT * FROM dados_empresa INNER JOIN empresa ON empresa.codigo = dados_empresa.codigo_empresa WHERE codigo_empresa = '" + cod_emp + "'";

                SqlDataReader info = db.rQuery(query);

                if (info.HasRows)
                {

                    info.Read();

                    natuop = info.GetString(0);

                    regesp = info.GetString(1);

                    optsmp = info.GetString(2);

                    incult = info.GetString(3);

                    status = info.GetString(4);

                    aliquota = info.GetString(5);

                    itemlista = info.GetString(6);

                    codtrib = info.GetString(7);

                    muniprest = info.GetString(8);

                    cnpj = info.GetInt64(14).ToString();

                    inscricao = info.GetInt64(15).ToString();

                    codnf = info.GetInt32(16);

                }
                else
                {

                    cLog[0] = "Empresa não cadastrada ou não possui o cadastro completo";

                    fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

                    return false;

                }

                string xmlnsxsi = "http://www.w3.org/2001/XMLSchema-instance";
                string xmlnsxsd = "http://www.w3.org/2001/XMLSchema";
                string xmlns = "http://www.abrasf.org.br/nfse";

                //string ns = "http://www.ginfes.com.br/servico_enviar_lote_rps_envio";

                //query = "select *, (CONCAT(cod_uf.cod, SUBSTRING(cod_ibge, 4, 6))) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                query = "select *, (cast(cod_uf.cod as varchar) + SUBSTRING(cod_ibge, 4, 6)) as ibge from nf_dados left join cod_uf on nf_dados.uf = cod_uf.uf where nf_dados.codigo_nf = " + cod_nf + " order by indice, numero_rps";

                SqlDataReader dataReader = db.rQuery(query);

                //reader = new StreamReader(path);

                if (dataReader.HasRows)
                {

                    if (!Directory.Exists(path_exp + cod_nf + " - " + s))
                    {

                        System.IO.Directory.CreateDirectory(path_exp + cod_nf + " - " + s);

                    }
                    else
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(path_exp + cod_nf + " - " + s);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dire in di.GetDirectories())
                        {
                            dire.Delete(true);
                        }

                    }

                    for (int i = 0; i <= nLotes; i++)
                    {

                        nfse = 0;

                        var settings = new XmlWriterSettings
                        {
                            Encoding = new UTF8Encoding(false),
                            Indent = true,
                            IndentChars = @"  ",
                            NamespaceHandling = NamespaceHandling.OmitDuplicates,
                            NewLineChars = "\n",
                            NewLineHandling = NewLineHandling.Replace,
                        };

                        using (XmlWriter write = XmlWriter.Create(path_exp + cod_nf + " - " + s + Path.DirectorySeparatorChar + s + "-" + i + ".xml", settings))
                        {

                            write.WriteStartDocument();

                            write.WriteStartElement("EnviarLoteRpsEnvio", xmlns);

                            //write.WriteAttributeString("xmlns", "tip", null, "http://www.ginfes.com.br/tipos");

                            write.WriteAttributeString("xmlns", "xsi", null, xmlnsxsi);
                            
                            write.WriteAttributeString("xmlns", "xsd", null, xmlnsxsd);
                            
                            write.WriteStartElement("LoteRps", "Lote" + codnf.ToString().PadLeft(5, '0'));

                            conta = ct - linhaAt;

                            if (conta < tamLote)
                            {

                                qntRps = conta - 1;

                            }
                            else
                            {

                                qntRps = tamLote;

                            }

                            //while ((line = reader.ReadLine()) != null)

                            while (dataReader.Read())
                            {

                                linhaAt++;

                                ind = dataReader.GetString(1);

                                if (control == 0)
                                {

                                    if (ind.Equals("1"))
                                    {

                                        vsLay = dataReader.GetString(2);
                                        ie = dataReader.GetString(3);
                                        diC = dataReader.GetString(4);
                                        dfC = dataReader.GetString(5);

                                    }

                                    write.WriteStartElement("NumeroLote");

                                    write.WriteValue(i);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Cnpj");

                                    //write.WriteValue("23824011000191");

                                    write.WriteValue(cnpj);

                                    write.WriteEndElement();

                                    write.WriteStartElement("InscricaoMunicipal");

                                    //write.WriteValue("20036425");

                                    write.WriteValue(inscricao);

                                    write.WriteEndElement();

                                    write.WriteStartElement("QuantidadeRps");

                                    write.WriteValue(qntRps);

                                    write.WriteEndElement();

                                    write.WriteStartElement("ListaRps");

                                    control++;

                                }

                                if (ind.Equals("9"))
                                {

                                    nLine = dataReader.GetString(6);

                                    nServ = dataReader.GetString(7);

                                }

                                if (ind.Equals("2"))
                                {

                                    rps = dataReader.GetString(8).Trim();
                                    a = dataReader.GetString(9).Trim();
                                    nRps = dataReader.GetString(10).Trim();
                                    dtRps = dataReader.GetString(11).Trim();
                                    t = dataReader.GetString(12).Trim();
                                    valServ = dataReader.GetString(13).Trim();
                                    dedu = dataReader.GetString(14).Trim();
                                    aliq = dataReader.GetString(16).Trim();
                                    iss = dataReader.GetString(17).Trim();
                                    tpDoc = dataReader.GetString(18).Trim();
                                    doc = dataReader.GetString(19).Trim();
                                    nome = dataReader.GetString(21).Trim();
                                    ende = dataReader.GetString(22).Trim();
                                    num = dataReader.GetString(23).Trim();
                                    comp = dataReader.GetString(24).Trim();
                                    bairro = dataReader.GetString(25).Trim();
                                    uf = dataReader.GetString(26).Trim();
                                    cep = dataReader.GetString(27).Trim();
                                    email = dataReader.GetString(28).Trim();
                                    desc = dataReader.GetString(29).Trim();
                                    cod_ibge = dataReader.GetString(36).Trim();
                                    cidade = dataReader.GetString(31).Trim();
                                    frmPag = dataReader.GetString(32).Trim();
                                    email2 = dataReader.GetString(33).Trim();

                                    nfse++;

                                    numRps = nfse.ToString().PadLeft(7, '0');

                                    string dataFmt = dtRps.Substring(0, 4) + "-" + dtRps.Substring(4, 2) + "-" + dtRps.Substring(6, 2) + "T00:01:00";

                                    string texto = desc + "\r\nVal. Aprox. Tributos R$ " +
                                        ((double.Parse(valServ) / 100) * 0.1788).ToString("0.00").Replace(',', '.')
                                        + ", correspondente a alíquota de (16,46%) Fonte: IBPT.";

                                    if (tpDoc.Equals("2"))
                                    {

                                        iDoc = "Cnpj";

                                    }
                                    else if (tpDoc.Equals("1"))
                                    {

                                        iDoc = "Cpf";

                                        doc = doc.Substring(3, 11);

                                    }

                                    write.WriteStartElement("Rps");

                                    write.WriteStartElement("InfRps", "Id", "Rps" + control + "A" + i);

                                    write.WriteStartElement("IdentificacaoRps", null);

                                    write.WriteStartElement("Numero", null);

                                    write.WriteValue(nRps.Substring(5, 7));

                                    write.WriteEndElement();

                                    write.WriteStartElement("Serie", null);

                                    write.WriteValue(a);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Tipo", null);

                                    write.WriteValue("1");

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha IdentificacaoRps

                                    write.WriteStartElement("DataEmissao", null);

                                    write.WriteValue(s + "T00:01:00");

                                    write.WriteEndElement();

                                    write.WriteStartElement("NaturezaOperacao", null);

                                    //write.WriteValue("1"); // MANUAL
                                    write.WriteValue(natuop);

                                    write.WriteEndElement();

                                    write.WriteStartElement("RegimeEspecialTributacao", null);

                                    //write.WriteValue("3"); // MANUAL
                                    write.WriteValue(regesp);

                                    write.WriteEndElement();

                                    write.WriteStartElement("OptanteSimplesNacional", null);

                                    //write.WriteValue("2"); // MANUAL
                                    write.WriteValue(optsmp);

                                    write.WriteEndElement();

                                    write.WriteStartElement("IncentivadorCultural", null);

                                    //write.WriteValue("2"); // MANUAL
                                    write.WriteValue(incult);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Status", null);

                                    //write.WriteValue("1"); // MANUAL
                                    write.WriteValue(status);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Servico", null);

                                    write.WriteStartElement("Valores", null);

                                    write.WriteStartElement("ValorServicos", null);

                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                    write.WriteEndElement();

                                    write.WriteStartElement("IssRetido", null);

                                    write.WriteValue(iss);

                                    write.WriteEndElement();

                                    write.WriteStartElement("BaseCalculo", null);

                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                    write.WriteEndElement();

                                    write.WriteStartElement("Aliquota", null);

                                    //write.WriteValue("0." + aliq); //MANUAL
                                    write.WriteValue("0." + aliquota);

                                    write.WriteEndElement();

                                    write.WriteStartElement("ValorLiquidoNfse", null);

                                    write.WriteValue((double.Parse(valServ) / 100).ToString("0.00").Replace(',', '.'));

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha Valores

                                    write.WriteStartElement("ItemListaServico", null);

                                    //write.WriteValue("6.04");//MANUAL
                                    write.WriteValue(itemlista);

                                    write.WriteEndElement();

                                    write.WriteStartElement("CodigoTributacaoMunicipio", null);

                                    //write.WriteValue("6.04 / 00060401");//MANUAL
                                    write.WriteValue(codtrib);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Discriminacao", null);

                                    write.WriteValue(texto);

                                    write.WriteEndElement();

                                    write.WriteStartElement("CodigoMunicipo", null);

                                    //write.WriteValue("3543402");//MANUAL
                                    write.WriteValue(muniprest);

                                    write.WriteEndElement();

                                    write.WriteEndElement();// fecha Servico 

                                    write.WriteStartElement("Prestador", null);

                                    write.WriteStartElement("Cnpj", null);

                                    //write.WriteValue("23824011000191");//MANUAL
                                    write.WriteValue(cnpj);

                                    write.WriteEndElement();

                                    write.WriteStartElement("InscricaoMunicipal", null);

                                    //write.WriteValue("20036425");//MANUAL
                                    write.WriteValue(inscricao);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha Prestador

                                    write.WriteStartElement("Tomador", null);

                                    write.WriteStartElement("IdentificacaoTomador", null);

                                    write.WriteStartElement("CpfCnpj", null);

                                    write.WriteStartElement(iDoc, null);

                                    write.WriteValue(doc);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha CpfCnpj

                                    write.WriteEndElement();//fecha IdentificacaoTomador

                                    write.WriteStartElement("RazaoSocial", null);

                                    write.WriteValue(nome);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Endereco", null);

                                    write.WriteStartElement("Endereco", null);

                                    write.WriteValue(ende);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Numero", null);

                                    write.WriteValue(num);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Bairro", null);

                                    write.WriteValue(bairro);

                                    write.WriteEndElement();

                                    write.WriteStartElement("CodigoMunicipo", null);

                                    write.WriteValue(cod_ibge);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Uf", null);

                                    write.WriteValue(uf);

                                    write.WriteEndElement();

                                    write.WriteStartElement("Cep", null);

                                    write.WriteValue(cep);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha Endereco

                                    write.WriteStartElement("Contato", null);

                                    write.WriteStartElement("Email", null);

                                    write.WriteValue(email);

                                    write.WriteEndElement();

                                    write.WriteEndElement();//fecha Contato

                                    write.WriteEndElement();//fecha Tomador

                                    write.WriteEndElement();//fecha InfRps

                                    write.WriteEndElement();//fecha Rps

                                    if (nfse == qntRps)
                                    {
                                        break;
                                    }

                                }

                            }

                            write.WriteEndElement();//fecha LoteRps

                            write.WriteEndElement();//fecha listaRps

                            control = 0;

                        }

                        conta = 0;

                    }

                }

                ret = true;

            }
            catch (Exception ex)
            {

                ret = false;

                cLog[0] = "Erro não esperado - " + ex.ToString();

                fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);

            }

            return ret;

        }

        public string lista_arquivos()
        {

            string ret = "";

            string query = "select data_importacao, nome_ori, codigo_nf from nf_controle";

            SqlDataReader reader = db.rQuery(query);

            if (reader.HasRows)
            {

                while(reader.Read())
                {

                    ret += reader.GetDateTime(0).ToString() + ";" + reader.GetString(1) + ";" + reader.GetInt32(2).ToString() + @"|";

                }

            }

            return ret;

        }

        public string[] lista_notas(string cod_nf)
        {

            string query = "SELECT count(numero_rps) FROM nf_dados WHERE codigo_nf = " + cod_nf + " AND indice <> '1' AND indice <> '9'";

            SqlDataReader reader = db.rQuery(query);

            string[] linhas = {};

            if(reader.HasRows)
            {

                reader.Read();

                linhas = new string[reader.GetInt32(0)];

                int c = 0;
                
                query = "SELECT numero_rps, data_rps, valor_servico, deducoes, aliquota, documento, nome, descricao_servico, forma_pagamento, tipo_documento FROM nf_dados WHERE codigo_nf = " + cod_nf + " AND indice <> '1' AND indice <> '9' ORDER BY numero_rps";

                string numero_rps, data_rps, valor_servico, deducoes, aliquota, documento, nome, descricao_servico, forma_pagamento, tpDoc;

                reader = db.rQuery(query);

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        numero_rps = reader.GetString(0).TrimStart('0');
                        data_rps = reader.GetString(1);
                        data_rps = data_rps.Substring(6, 2) + "/" + data_rps.Substring(4, 2) + "/" + data_rps.Substring(0, 4);
                        valor_servico = (Convert.ToDecimal(reader.GetString(2)) / 100).ToString("C");
                        deducoes = (Convert.ToDecimal(reader.GetString(3)) / 100).ToString("C");
                        aliquota = (double.Parse(reader.GetString(4)) / 100).ToString() + "%";
                        documento = reader.GetString(5);
                        nome = reader.GetString(6).ToUpper();
                        descricao_servico = reader.GetString(7);
                        forma_pagamento = reader.GetString(8);
                        tpDoc = reader.GetString(9);

                        if (tpDoc.Equals("1"))
                        {
                            documento = Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00");
                        }
                        else if (tpDoc.Equals("2"))
                        {
                            documento = Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");
                        }
                        
                        linhas[c] = numero_rps + ";" + data_rps + ";" + valor_servico + ";" + deducoes + ";" + aliquota + ";" + documento + ";" + nome + ";" + descricao_servico + ";" + forma_pagamento + ";" + tpDoc;

                        c++;

                    }

                }
            
            }
            
            return linhas;

        }

        public void downloadLote(String pathDownload, String cdnota, String nome_nota)
        {

            /*DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/downloads/"));
            if (!di.Exists)
            {
                di.Create();
            }

            //string FileName = cdnota + " - " + data + ".zip";

            string FileName = nome_nota + ".zip";

            string folder = cdnota + " - " + data;

            FileInfo newFile = new FileInfo(Server.MapPath("~/downloads/") + FileName);
            newFile.Delete();

            String path = Server.MapPath("~/importacoes/" + codemp + "/" + folder);

            String zip = Server.MapPath("~/downloads/") + FileName;

            ZipFile.CreateFromDirectory(path, zip);

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "application/zip";
            response.ContentType = "application/force-download";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/downloads/") + FileName);
            response.Flush();
            response.End();*/


        }

    }

}