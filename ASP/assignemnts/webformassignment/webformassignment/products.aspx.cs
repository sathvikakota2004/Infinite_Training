using System;
using System.Collections.Generic;

namespace webformassignment
{
    public partial class products : System.Web.UI.Page
    {
        Dictionary<string, (string image, string price)> productsDict = new Dictionary<string, (string, string)>()
        {
            { "Laptop", ("~/images/laptop.jpg", "$800") },
            { "Mobile", ("~/images/mobile.jpg", "$500") },
            { "Headphones", ("~/images/headphones.jpg", "$100") },
            { "Camera", ("~/images/camera.jpg", "$600") }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlProducts.DataSource = productsDict.Keys;
                ddlProducts.DataBind();
            }
        }

        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ddlProducts.SelectedValue;
            imgProduct.ImageUrl = productsDict[selected].image;
            lblPrice.Text = "";
        }

        protected void btnGetPrice_Click(object sender, EventArgs e)
        {
            string selected = ddlProducts.SelectedValue;
            lblPrice.Text = "Price: " + productsDict[selected].price;
        }
    }
}
