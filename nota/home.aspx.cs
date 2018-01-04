using System;
using System.Drawing;
using System.Web;

namespace nota
{
    public partial class home : System.Web.UI.Page
    {

        funcao funcao = new funcao();
        data db = new data();
       

        protected void Page_Load(object sender, EventArgs e)
         {
          db.conDB("nfse");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // função para decifrar MD5.
            string usu = usuario.Text, pw = funcao.md5(senha.Text).ToLower();

            int cod = db.login(usu, pw);
            

            switch (cod)
            {

                case 0:
                    infolg.Text = "Falha ao Efetuar o Login !";
                    infolg.ForeColor = Color.IndianRed;
                    break;

                case -1:
                    infolg.Text = "Usuário/Senha Incorretos !";
                    infolg.ForeColor = Color.IndianRed;
                    break;

                default:

             /*
              *string senhamd5 = funcao.md5(senha.Text);

             if (usuario.Text.Equals("camila") && senhamd5.Equals("202cb962ac59075b964b07152d234b70"))
             {*/
            
                    HttpCookie ckemp = new HttpCookie("cdemp");
                    ckemp.Name = "cdemp";
                    ckemp.Value = cod.ToString();
                    ckemp.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(ckemp);

                    HttpCookie lguser = new HttpCookie("nuser");
                    lguser.Name = "nuser";
                    lguser.Value = usu;
                    lguser.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(lguser);

                    Server.Transfer("admin.aspx");
                    break;

            }

        }
        
    }
}