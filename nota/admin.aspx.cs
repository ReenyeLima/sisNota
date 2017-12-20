using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nota
{
    public partial class admin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void MultiView2_ActiveViewChanged(object sender, EventArgs e)
        {

        }

        protected void uploadbutton_Click(object sender, EventArgs e)
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

    }
}