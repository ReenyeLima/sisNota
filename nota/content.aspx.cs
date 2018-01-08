using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Compression;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;

namespace nota
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        Boolean pode = false;

        funcoes fnc = new funcoes();

        data db = new data();

        file fl = new file();

        sci_nfse nf = new sci_nfse();

        DataTable dt = new DataTable();

        private string codemp = "";
        private int codLay = -1;

        int pg = 0, linhasret = 0;

        string[] cLog;

        public void MsgBox(String ex) 
        {
            //string message = "Hello! Mudassar.";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(ex);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        DataRow CreateRow(String Text, String Value, DataTable dt)
        {

            DataRow dr = dt.NewRow();

            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }

        public void carregaSelect()
        {

            if (!Page.IsPostBack)
            {

                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("nome", typeof(String)));
                dt.Columns.Add(new DataColumn("codigo", typeof(String)));

                SqlDataReader ret = db.rQuery("SELECT nome_ori, codigo_nf FROM nf_controle WHERE cod_emp = " + codemp + " ORDER BY data_importacao");

                if (ret.HasRows)
                {

                    while (ret.Read())
                    {

                        dt.Rows.Add(CreateRow(ret.GetString(0), ret.GetInt32(1).ToString(), dt));

                    }

                }
                else
                {
                    dt.Rows.Add(CreateRow("", "", dt));
                }

                DataView dv = new DataView(dt);

                sel_notas.DataSource = dv;
                sel_notas.DataTextField = "nome";
                sel_notas.DataValueField = "codigo";

                sel_notas.DataBind();

                sel_notas.SelectedIndex = 0;

            }

        }

        public void atualizaSelect()
        {

                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("nome", typeof(String)));
                dt.Columns.Add(new DataColumn("codigo", typeof(String)));

                SqlDataReader ret = db.rQuery("SELECT nome_ori, codigo_nf FROM nf_controle WHERE cod_emp = " + codemp + " ORDER BY data_importacao");

                if (ret.HasRows)
                {

                    while (ret.Read())
                    {

                        dt.Rows.Add(CreateRow(ret.GetString(0), ret.GetInt32(1).ToString(), dt));

                    }

                }
                else
                {
                    dt.Rows.Add(CreateRow("", "", dt));
                }

                DataView dv = new DataView(dt);

                sel_notas.DataSource = dv;
                sel_notas.DataTextField = "nome";
                sel_notas.DataValueField = "codigo";

                sel_notas.DataBind();

                sel_notas.SelectedIndex = 0;

        }

        public void carregaInfo(string cod_emp)
        {

            if (!Page.IsPostBack)
            {

                listaLayout.Items.Clear();

                listaLayout.Items.Insert(0, "LSC");
                listaLayout.Items.Insert(1, "LMC");
                listaLayout.Items.Insert(2, "LARC");
                listaLayout.Items.Insert(3, "RONDO");
                listaLayout.Items.Insert(4, "TBT");
                listaLayout.Items.Insert(5, "PRESI");

                SqlDataReader ret = db.rQuery("SELECT * FROM dados_empresa WHERE codigo_empresa = '" + cod_emp + "'");

                if (ret.HasRows)
                {

                    ret.Read();

                    nat_op.Text = ret.GetString(0);

                    reg_trib.Text = ret.GetString(1);

                    opt_nac.Text = ret.GetString(2);

                    inc_cult.Text = ret.GetString(3);

                    status.Text = ret.GetString(4);

                    aliq.Text = ret.GetString(5);

                    lis_serv.Text = ret.GetString(6);

                    trib_mun.Text = ret.GetString(7);

                    mun_serv.Text = ret.GetString(8);

                    codLay = ret.GetInt32(10);

                    listaLayout.SelectedIndex = codLay;


                }
                
            }
            else if(pode)
            {

                SqlDataReader ret = db.rQuery("SELECT * FROM dados_empresa WHERE codigo_empresa = '" + cod_emp + "'");

                if (ret.HasRows)
                {

                    ret.Read();

                    nat_op.Text = ret.GetString(0);

                    reg_trib.Text = ret.GetString(1);

                    opt_nac.Text = ret.GetString(2);

                    inc_cult.Text = ret.GetString(3);

                    status.Text = ret.GetString(4);

                    aliq.Text = ret.GetString(5);

                    lis_serv.Text = ret.GetString(6);

                    trib_mun.Text = ret.GetString(7);

                    mun_serv.Text = ret.GetString(8);

                    codLay = ret.GetInt32(10);

                    listaLayout.SelectedIndex = codLay;

                }

            }

        }

        private void carrega_panel()
        {



        }

        protected void check_cookie()
        {

            Boolean ret = false;

            String cod = "", query = "";
            
            if (Request.Cookies["cdemp"] != null)
            {

                cod = Request.Cookies["cdemp"].Value;

                query = "SELECT * FROM empresa WHERE codigo = '" + cod + "'";

                SqlDataReader reader = db.rQuery(query);

                if (reader.HasRows)
                {

                    ret = true;

                    codemp = cod;

                    Request.Cookies["cdemp"].Expires = DateTime.Now.AddHours(1);

                }
                else
                {

                    ret = false;

                }

            }
            else
            {
            
                ret = false;
            
            }

            if(!ret)
            {

                MsgBox("Sua Sessão Expirou !");

                Server.Transfer("default.aspx");

            }

        }

        protected void setuser()
        {

            if (Request.Cookies["cdemp"] != null)
            {

                n_user.Text = "Usuário   " + Request.Cookies["nuser"].Value;

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            set(btPn1);

           // check_cookie();

            //setuser();
                        
            carregaInfo(codemp);

            carregaSelect();

        }

        protected void clear()
        {

            btPn1.Style.Clear();
            btPn2.Style.Clear();
            btPn3.Style.Clear();

        }

        protected void set(Button bt)
        {

            bt.Style.Add("border-left-style", "solid");
            bt.Style.Add("border-left-width", "6px");
            bt.Style.Add("border-left-color", "#405dcc");
            
        }

        protected void btPn1_Click(object sender, EventArgs e)
        {

            MultiView1.ActiveViewIndex = 0;

            clear();

            set(btPn1);
            
        }

        protected void btPn2_Click(object sender, EventArgs e)
        {

            MultiView1.ActiveViewIndex = 1;

            clear();

            set(btPn2);
             
        }

        protected void btPn3_Click(object sender, EventArgs e)
        {

            MultiView1.ActiveViewIndex = 2;

            clear();

            set(btPn3);

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
                        

            if (FileUploadControl.HasFile)
            {
                
                try
                {

                    string filename = Path.GetFileName(FileUploadControl.FileName);
                    DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/uploads/"));
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    FileUploadControl.SaveAs(Server.MapPath("~/uploads/") + filename);

                    linhasret = fnc.salva_banco(Server.MapPath("~/uploads/") + filename, codemp);

                    infoST.Text = "Status : " + linhasret.ToString() + " Linhas carregadas com sucesso !";

                    //carregaSelect();
                    atualizaSelect();

                }
                catch (Exception ex)
                {

                    infoST.Text = "Status : Falha no Upload. Erro - " + ex.Message;

                }

            }
           
        }


        protected void DownloadButton_Click(object sender, EventArgs e)
        {

            string cdnota = sel_notas.SelectedValue.ToString();

            string nome_nota = sel_notas.SelectedItem.ToString();

            nome_nota = nome_nota.Substring(0, nome_nota.Length - 4);
            
            DateTime dt = DateTime.Now;

            string data = dt.ToString("yyyy-MM-dd");

            try
            {

                int t = listaLayout.SelectedIndex;

                infoST.Text = "cod layout - " + t;

                MsgBox(t.ToString() + " - " + cdnota + " - " + codemp);

                if (t == 0)
                {

                    infoST.Text = "aqui 0";                    
                    if (fnc.gera_lote(cdnota, codemp))                    
                    {

                        DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/downloads/"));
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
                        response.End();

                    }

                }
                else if (t == 1)
                {
                    
                    if(fnc.gera_lote_lmc(cdnota, codemp)){

                        DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/downloads/"));
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
                        response.End();
                        
                    }

                }
                else if(t == 2)
                {

                    if (fnc.gera_lote_aracatuba(cdnota, codemp))
                    {

                        DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/downloads/"));
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
                        response.End();

                    }

                }
                else if (t == 3)
                {

                    if (fnc.gera_lote_rondo(cdnota, codemp))
                    {

                        DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/downloads/"));
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
                        response.End();

                    }

                }
                else if (t == 4)
                {

                    if (fnc.gera_lote_taubate(cdnota, codemp))
                    {

                        try
                        {

                            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/downloads/"));
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
                            response.End();

                        }
                        catch(Exception ex)
                        {

                            db.dataLog(ex.ToString());

                        }                        

                    }

                }
                else if (t.ToString() == "5")
                {

                    infoST.Text = "TO AQUI - " + cdnota + " - " + codemp;
                    
                    if (fnc.gera_lote_presprud(cdnota, codemp))
                    {

                         infoST.Text =  "TRUE";

                    }
                    else
                    {

                        infoST.Text = "FALSE";

                    }

                }
                
            }
            catch (Exception ex)
            {

                db.dataLog("CATCH DOWNLOAD : " + ex.ToString());

                MsgBox(ex.Message);
                
                cLog[0] = "Erro não esperado - " + ex.ToString();

                fl.wFile(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar, "log.log", cLog, true);


            }
                        
        }

        protected void btSalva_Click(object sender, EventArgs e)
        {

            int qnt = 0;

            string nat = nat_op.Text, reg = reg_trib.Text, opt = opt_nac.Text, inc = inc_cult.Text,  st = status.Text;
            string ali = aliq.Text, ilista = lis_serv.Text, codtrib = trib_mun.Text, munic = mun_serv.Text;
            string pw = senha.Text;
            int codLayout = listaLayout.SelectedIndex;

            //MsgBox("Nome - " + listaLayout.SelectedItem + "| codigo - " + listaLayout.SelectedValue);

            string insert = "INSERT INTO dados_empresa (NaturezaOperacao, RegimeEspecialTributacao, OptanteSimplesNacional, IncentivadorCultural, Status, Aliquota, ItemListaServico, CodigoTributacaoMunicipio, MunicipioPrestacaoServico, codigo_empresa, codigo_layout, pwSis)" +
                            "VALUES ('" + nat + "', '" + reg + "', '" + opt + "', '" + inc + "', '" + st + "', " +
                            "'" + ali + "', '" + ilista + "', '" + codtrib + "', '" + munic + "', '" + codemp + "', " + codLayout + ", '" + pw + "')";

            string update = "UPDATE dados_empresa SET NaturezaOperacao = '" + nat + "'," +
                                                      "RegimeEspecialTributacao = '" + reg + "'," +
                                                      "OptanteSimplesNacional = '" + opt + "'," +
                                                      "IncentivadorCultural = '" + inc + "'," +
                                                      "Status = '" + st + "'," +
                                                      "Aliquota = '" + ali + "'," +
                                                      "ItemListaServico = '" + ilista + "'," +
                                                      "CodigoTributacaoMunicipio = '" + codtrib + "'," +
                                                      "MunicipioPrestacaoServico = '" + munic + "'," +
                                                      "codigo_layout = " + codLayout + " ," +
                                                      "pwSis = '" + pw + "'" +
                              " WHERE codigo_empresa = " + Int32.Parse(codemp) + ";";

            string count = "SELECT count(*) FROM empresa WHERE codigo = " + Int32.Parse(codemp) + "";

            //string debug = "";

            SqlDataReader ret = db.rQuery(count);

            if (ret.HasRows)
            {

                //debug += "aqui1";

                qnt = db.execQuery(update);

                //debug += " - " + qnt;

                if (qnt == 1) 
                {

                    infoST.Text = "Status : Dados Atualizados Com Sucesso !";

                }
                else
                {

                    //infoST.Text = "Status : Não foi possível efetuar a atualização !";

                    //debug += " - " + update;

                    qnt = db.execQuery(insert);

                    if (qnt == 1)
                    {

                        infoST.Text = "Status : Dados Inseridos Com Sucesso !";

                    }
                    else
                    {

                        infoST.Text = "Status : Dados não foram inseridos !";

                    }

                }
                
            }
            else
            {

                //debug += "aqui2";

                qnt = db.execQuery(insert);

                if (qnt == 1)
                {

                    infoST.Text = "Status : Dados Inseridos Com Sucesso !";

                }
                else
                {

                    infoST.Text = "Status : Dados não foram inseridos !";

                }

            }

            //infoST.Text = debug;

            carregaInfo(codemp);

        }

        protected void btCancela_Click(object sender, EventArgs e)
        {

            pode = true;

            carregaInfo(codemp);

            pode = false;

        }

        protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
        {

            infoST.Text = "Status : ";

        }

        protected void listaLayout_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void listaLayout_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void logout_Click(object sender, EventArgs e)
        {

            Response.Cookies["cdemp"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["nuser"].Expires = DateTime.Now.AddDays(-1);
            Server.Transfer("default.aspx");

        }
        
    }
}