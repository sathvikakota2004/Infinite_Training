using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;


namespace ElectricityWeb
{
    public partial class RetrieveBills : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Admin"] is bool ok) || !ok)
                Response.Redirect("AdminLogin.aspx");
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtN.Text, out int n) || n <= 0)
            {
                lblMsg.Text = "Please enter a valid positive number.";
                return;
            }

            var board = new ElectricityBoard();
            List<ElectricityBill> bills = board.Generate_N_BillDetails(n);

            gvBills.DataSource = bills;
            gvBills.DataBind();

            bltSummary.Items.Clear();
            foreach (var b in bills)
            {
                // Sample style: "EB Bill for Sid is 350"
                bltSummary.Items.Add($"EB Bill for {b.ConsumerName} is {b.BillAmount:F0}");
            }
        }
    }
}