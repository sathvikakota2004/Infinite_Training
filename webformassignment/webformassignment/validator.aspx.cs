using System;
using System.Text.RegularExpressions;

namespace webformassignment
{
    public partial class ValidatorPage : System.Web.UI.Page
    {
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string family = txtFamilyName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string city = txtCity.Text.Trim();
            string zip = txtZip.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (name == family)
            {
                lblResult.Text = "Name and Family Name cannot be the same!";
                return;
            }
            if (address.Length < 2)
            {
                lblResult.Text = "Address must have at least 2 characters!";
                return;
            }
            if (city.Length < 2)
            {
                lblResult.Text = "City must have at least 2 characters!";
                return;
            }
            if (!Regex.IsMatch(zip, @"^\d{5}$"))
            {
                lblResult.Text = "Zip Code must be exactly 5 digits!";
                return;
            }
            if (!Regex.IsMatch(phone, @"^(\d{2}-\d{7}|\d{3}-\d{7})$"))
            {
                lblResult.Text = "Phone format must be XX-XXXXXXX or XXX-XXXXXXX!";
                return;
            }
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblResult.Text = "Invalid Email format!";
                return;
            }

            lblResult.ForeColor = System.Drawing.Color.Green;
            lblResult.Text = "All validations passed!";
        }
    }
}
