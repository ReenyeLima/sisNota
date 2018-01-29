using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace nota
{
    public partial class admin : System.Web.UI.Page
    {
        funcoes fnc = new funcoes();
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
        private string codemp = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void MultiView2_ActiveViewChanged(object sender, EventArgs e)
        {

        }
        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 0;

        }

        protected void Unnamed_ServerClick1(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 1;

        }

        protected void Unnamed_ServerClick2(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 2;
        }

        protected void upbutton_Click(object sender, EventArgs e)
        {
            if (myfile.HasFile)
            {

                try
                {
                    int linhasret = 0;
                    string filename = Path.GetFileName(myfile.FileName);
                    DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/uploads/"));
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    myfile.SaveAs(Server.MapPath("~/uploads/") + filename);

                    linhasret = fnc.salva_banco(Server.MapPath("~/uploads/") + filename, codemp);
                    MsgBox("Arquivo enviado com sucesso!");
                }
                catch (Exception ex)

                {
                    MsgBox("Falha no Upload");
                }
            }
                }

        protected void btConsultaLote_Click(object sender, EventArgs e)
        {
            fnc.carrega_aury();
        }
    }
        
    }
