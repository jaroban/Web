using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class RegistrationInfo : System.Web.UI.UserControl
{
    public bool DeleteClicked;
    private bool _single;
    public bool Single
    {
        get { return _single; }
        set 
        {
            _single = value;
            btnRemove.Visible = !value;
        }
    }
    public List<string> Validate()
    {
        var errors = new List<string>();

        lblMenoError.Text = "";
        if (string.IsNullOrWhiteSpace(txtMeno.Text))
        {
            errors.Add(Common.ChybaMeno);
            lblMenoError.Text = Common.ChybaMeno;
        }

        lblPriezviskoError.Text = "";
        if (string.IsNullOrWhiteSpace(txtPriezvisko.Text))
        {
            errors.Add(Common.ChybaPriezvisko);
            lblPriezviskoError.Text = Common.ChybaPriezvisko;
        }

        lblEmailError.Text = "";
        if (!Common.ValidateEmail(txtEmail.Text.Trim()))
        {
            errors.Add(Common.ChybaEmail);
            lblEmailError.Text = Common.ChybaEmail;
        }

        lblSluzbaError.Text = "";
        if (ddlSluzba.SelectedValue == "0" || string.IsNullOrWhiteSpace(Sluzba))
        {
            errors.Add(Common.ChybaSluzba);
            lblSluzbaError.Text = Common.ChybaSluzba;
        }

        return errors;
    }

    public RegistrationEntry Data
    {
        get
        {
            int idSluzba = 0;
            int.TryParse(ddlSluzba.SelectedValue, out idSluzba);
            return new RegistrationEntry
            {
                Meno = txtMeno.Text.Trim(),
                Priezvisko = txtPriezvisko.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefon = txtTelefon.Text.Trim(),
                IdSluzba = idSluzba,
                InaSluzba = txtSluzba.Text.Trim(),
                Poznamka = txtPoznamka.Text.Trim(),
                Errors = Validate()
            };
        }
        set
        {
            txtMeno.Text = value.Meno;
            txtPriezvisko.Text = value.Priezvisko;
            txtEmail.Text = string.IsNullOrEmpty(value.Email) ? "@" : value.Email;
            txtTelefon.Text = string.IsNullOrEmpty(value.Telefon) ? "+421" : value.Telefon;
            ddlSluzba.SelectedValue = value.IdSluzba.ToString();
            txtSluzba.Text = value.InaSluzba;
            txtPoznamka.Text = value.Poznamka;

            Update(null, null);
        }
    }

    public string Sluzba
    {
        get
        {
            if(ddlSluzba.SelectedValue == "-1") return txtSluzba.Text;
            return ddlSluzba.SelectedItem.Text;
        }
    }

    public string Title
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }

    protected void Update(object sender, EventArgs e)
    {
        txtSluzba.Visible = ddlSluzba.SelectedValue == "-1";
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        DeleteClicked = true;
    }

    public void FillSluzby(List<IdName> sluzby)
    {
        var i = ddlSluzba.SelectedValue;
        ddlSluzba.Items.Clear();

        ddlSluzba.Items.Add(new ListItem("Prosím vyber službu", "0"));
        if (sluzby != null)
        {
            foreach (var item in sluzby)
            {
                ddlSluzba.Items.Add(new ListItem(item.Name, item.Id.ToString()));
            }
        }
        ddlSluzba.Items.Add(new ListItem("Iná...", "-1"));
        ddlSluzba.SelectedValue = i;
    }
}