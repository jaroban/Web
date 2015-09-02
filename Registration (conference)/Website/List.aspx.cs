using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class List : Page
{
    private bool _reload;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFilter();
            LoadData();
        }
    }
    
    private void LoadFilter()
    {
        var data = Database.GetFilter();

        ddlChurch.Items.Clear();
        ddlChurch.Items.Add(new ListItem { Value = "", Text = "Všetky" });
        foreach(var item in data)
        {
            ddlChurch.Items.Add(new ListItem { Value = item.Id.ToString(), Text = item.Name});
        }
    }

    private void LoadData()
    {
        _reload = true;
    }

    protected void txtFrom_TextChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void txtTo_TextChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void ddlChurch_TextChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void btnClearName_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        LoadData();
    }

    protected void btnClearChurch_Click(object sender, EventArgs e)
    {
        ddlChurch.SelectedValue = "";
    }

    protected void chbNotArrived_CheckedChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void gridResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex;
        if(int.TryParse(e.CommandArgument.ToString(), out rowIndex))
        {
            GridViewRow row = gridResults.Rows[rowIndex];

            var hdnIdUser = (HiddenField)row.FindControl("hdnIdUser");
            var hdnIdVariabilny = (HiddenField)row.FindControl("hdnIdVariabilny");
            var hdnAmount = (HiddenField)row.FindControl("hdnAmount");

            int idUser, idVariabilny;
            float amount;

            if (int.TryParse(hdnIdUser.Value, out idUser) &&
                int.TryParse(hdnIdVariabilny.Value, out idVariabilny) &&
                float.TryParse(hdnAmount.Value, out amount))
            {
                try
                {
                    switch (e.CommandName)
                    {
                        case "Darovali":
                            // donation += preplatok
                            Database.AddDonation(idUser, amount);
                            break;
                        case "Doplatili":
                            // add payment = -preplatok
                            Database.AddPayment(idUser, -amount, "Platba na mieste, IP = " + Common.GetIpAddress());
                            break;
                        case "Vratili":
                            // add payment = -preplatok
                            Database.AddPayment(idUser, -amount, "Vratenie preplatku, IP = " + Common.GetIpAddress());
                            break;
                        case "Prisli":
                            Database.ShowedUp(idUser);
                            break;
                    }
                }
                catch(Exception ex)
                {

                }
                LoadData();
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        if(_reload)
        {
            int idChurch = 0;
            int.TryParse(ddlChurch.SelectedValue, out idChurch);

            var findName = txtName.Text.Trim().ToUpper();
            var from = txtFrom.Text.Trim().ToUpper();
            var to = txtTo.Text.Trim().ToUpper();
            var notArrived = chbNotArrived.Checked;

            var data = Database.GetList(findName, idChurch, from, to, notArrived);

            gridResults.DataSource = data;
            gridResults.DataBind();
        }
        base.OnPreRender(e);
    }

    protected string CashBackFormat(object x)
    {
        var f = x as float?;
        if (!f.HasValue || f.Value == 0) return "";
        return string.Format("{0:0.00}", x);
    }

    protected string CashBackClass(object x)
    {
        var f = x as float?;
        if (!f.HasValue || f.Value == 0) return "";
        return f.Value >= -0.01 ? "positive" : "negative";
    }
}
