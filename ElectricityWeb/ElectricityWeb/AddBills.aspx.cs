using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DataAccess;

namespace ElectricityWeb
{
    public partial class AddBills : System.Web.UI.Page
    {
        private List<ElectricityBill> SessionBills
        {
            get => (List<ElectricityBill>)(Session["SessBills"] ?? (Session["SessBills"] = new List<ElectricityBill>()));
            set => Session["SessBills"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Admin"] is bool ok) || !ok)
                Response.Redirect("AdminLogin.aspx");

            if (!IsPostBack) BindGrid();
        }

        protected void btnSetCount_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCount.Text, out int toAdd) && toAdd > 0)
            {
                Session["ToAdd"] = toAdd;
                Session["Added"] = 0;
                pnlEntry.Visible = true;
                lblDone.Text = "";
                lblEntryMsg.Text = "";
                SessionBills.Clear();
                BindGrid();
                UpdateCounter();
            }
            else
            {
                lblDone.CssClass = "error";
                lblDone.Text = "Please enter a valid positive number of bills.";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int target = Convert.ToInt32(Session["ToAdd"] ?? 0);
            int added = Convert.ToInt32(Session["Added"] ?? 0);

            if (target == 0)
            {
                lblEntryMsg.CssClass = "error";
                lblEntryMsg.Text = "Set the number of bills first.";
                return;
            }
            if (added >= target)
            {
                lblEntryMsg.CssClass = "error";
                lblEntryMsg.Text = "You have already added the specified number of bills.";
                return;
            }

            try
            {
                var eb = new ElectricityBill
                {
                    ConsumerNumber = txtConsumerNo.Text.Trim(),
                    ConsumerName = txtConsumerName.Text.Trim(),
                    UnitsConsumed = int.TryParse(txtUnits.Text.Trim(), out int u) ? u : -1
                };

                // Validation 2: Units must be >= 0 (loop-like prompt behavior)
                var validator = new BillValidator();
                if (validator.ValidateUnitsConsumed(eb.UnitsConsumed) != "Valid")
                {
                    lblEntryMsg.CssClass = "error";
                    lblEntryMsg.Text = "Given units is invalid";
                    txtUnits.Focus();
                    return; // stay on page until valid
                }

                var board = new ElectricityBoard();
                board.CalculateBill(eb);
                board.AddBill(eb); // store to DB

                // also show in the session grid
                SessionBills.Add(eb);
                Session["Added"] = added + 1;

                lblEntryMsg.CssClass = "success";
                lblEntryMsg.Text = $"Output : {eb.ConsumerNumber} {eb.ConsumerName} {eb.UnitsConsumed} Bill Amount : {eb.BillAmount:F0}";

                // clear inputs for next entry
                txtConsumerNo.Text = txtConsumerName.Text = txtUnits.Text = "";
                txtConsumerNo.Focus();

                BindGrid();
                UpdateCounter();

                // If finished:
                if ((int)Session["Added"] >= (int)Session["ToAdd"])
                {
                    lblDone.Text = "All bills added successfully.";
                }
            }
            catch (FormatException ex)
            {
                // Validation 1: Invalid consumer number -> show exception message itself
                lblEntryMsg.CssClass = "error";
                lblEntryMsg.Text = ex.Message;
            }
        }

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            pnlEntry.Visible = false;
            lblDone.Text = "Finished adding bills.";
        }

        private void BindGrid()
        {
            gvAdded.DataSource = SessionBills;
            gvAdded.DataBind();
        }

        private void UpdateCounter()
        {
            int target = Convert.ToInt32(Session["ToAdd"] ?? 0);
            int added = Convert.ToInt32(Session["Added"] ?? 0);
            lblCount.Text = target > 0 ? $"Added {added} of {target}" : "";
        }
    }
}