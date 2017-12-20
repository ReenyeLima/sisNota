using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nota
{
    public partial class home : System.Web.UI.Page
    {

        funcao funcao = new funcao();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (usuario.Text.Equals("Bruno") && senha.Text.Equals("123"))
            {
                Server.Transfer("admin.aspx");
            }
            else
            {

            }
        }
        
    }
}