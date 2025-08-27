using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

namespace ElectricityWeb
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Simple inline auth for assignment
            if (txtUser.Text == "admin" && txtPass.Text == "admin123")
            {
                Session["Admin"] = true;
                Response.Redirect("AddBills.aspx");
            }
            else
            {
                lblMsg.Text = "Invalid credentials!";
            }
        }
    }
}