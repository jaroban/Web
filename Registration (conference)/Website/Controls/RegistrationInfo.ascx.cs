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
        /*
        lblTelefonError.Text = "";
        if (!Common.ValidatePhoneNumber(txtTelefon.Text.Trim()))
        {
            errors.Add(Common.ChybaTelefon);
            lblTelefonError.Text = Common.ChybaTelefon;
        }

        lblZborError.Text = "";
        if (ddlZbor.SelectedValue == "0" ||
            (ddlZbor.SelectedValue == "-1" && string.IsNullOrWhiteSpace(txtZbor.Text)))
        {
            errors.Add(Common.ChybaZbor);
            lblZborError.Text = Common.ChybaZbor;
        }
         */
        return errors;
    }

    public RegistrationEntry Data
    {
        get
        {
            int idZbor = 0;
            int idTricko = 0;
            try
            {
                idZbor = int.Parse(ddlZbor.SelectedValue);
                idTricko = int.Parse(ddlTricko.SelectedValue);
            }
            catch
            {
            }
            return new RegistrationEntry
            {
                Meno = txtMeno.Text.Trim(),
                Priezvisko = txtPriezvisko.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefon = txtTelefon.Text.Trim(),
                IdZbor = idZbor,
                InyZbor = txtZbor.Text.Trim(),
                PiatokVecera = chbPiatokVecera.Checked,
                PiatokVecera2 = chbPiatokVecera2.Checked,
                UbytovaniePiatokSobota = chbUbytovaniePiatokSobota.Checked,
                TichaTriedaPiatokSobota = chbTichaTriedaPiatokSobota.Checked,
                SobotaRanajky = chbSobotaRanajky.Checked,
                SobotaObed = chbSobotaObed.Checked,
                SobotaVecera = chbSobotaVecera.Checked,
                SobotaVecera2 = chbSobotaVecera2.Checked,
                UbytovanieSobotaNedela = chbUbytovanieSobotaNedela.Checked,
                TichaTriedaSobotaNedela = chbTichaTriedaSobotaNedela.Checked,
                NedelaRanajky = chbNedelaRanajky.Checked,
                NedelaObed = chbNedelaObed.Checked,
                Sach = chbSach.Checked,
                PingPong = chbPingPong.Checked,
                IdTricko = idTricko,
                Tricko = ddlTricko.SelectedItem.Text,
                Sluziaci = false, // chbSluziaci.Checked,
                Poznamka = txtPoznamka.Text.Trim(),
                Errors = Validate()
            };
        }
        set
        {
            txtMeno.Text = value.Meno;
            txtPriezvisko.Text = value.Priezvisko;
            txtEmail.Text = string.IsNullOrEmpty(value.Email) ? "@" : value.Email;
            txtTelefon.Text = string.IsNullOrEmpty(value.Telefon) ? "+42" : value.Telefon;
            ddlZbor.SelectedValue = value.IdZbor.ToString();
            txtZbor.Text = value.InyZbor;
            chbPiatokVecera.Checked = value.PiatokVecera;
            chbPiatokVecera2.Checked = value.PiatokVecera2;
            chbUbytovaniePiatokSobota.Checked = value.UbytovaniePiatokSobota;
            chbTichaTriedaPiatokSobota.Checked = value.TichaTriedaPiatokSobota;
            chbSobotaRanajky.Checked = value.SobotaRanajky;
            chbSobotaObed.Checked = value.SobotaObed;
            chbSobotaVecera.Checked = value.SobotaVecera;
            chbSobotaVecera2.Checked = value.SobotaVecera2;
            chbUbytovanieSobotaNedela.Checked = value.UbytovanieSobotaNedela;
            chbTichaTriedaSobotaNedela.Checked = value.TichaTriedaSobotaNedela;
            chbNedelaRanajky.Checked = value.NedelaRanajky;
            chbNedelaObed.Checked = value.NedelaObed;
            chbSach.Checked = value.Sach;
            chbPingPong.Checked = value.PingPong;
            ddlTricko.SelectedValue = value.IdTricko.ToString();
            //chbSluziaci.Checked = value.Sluziaci;
            txtPoznamka.Text = value.Poznamka;

            Update(null, null);
        }
    }

    public string Zbor
    {
        get
        {
            if(ddlZbor.SelectedValue == "-1") return txtZbor.Text;
            return ddlZbor.SelectedItem.Text;
        }
    }

    public string Title
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }

    public int IdCurrency { get; set; }

    public void Fill()
    {
        Common.FillTeeShirts(ddlTricko);
        Common.FillChurches(ddlZbor);
    }

    protected override void OnPreRender(EventArgs e)
    {
        var ranajky = "Raňajky";
        var obed = "Obed";
        var vecera = "Večera";
        var vecera2 = "Druhá večera - bageta";
        var ubytovanie = "Ubytovanie";
        var tichaTrieda = "Tichá trieda";

        chbSobotaRanajky.Text = ranajky;
        chbNedelaRanajky.Text = ranajky;
        lblCenaRanajky.Text = Common.FormatMoney(Prices.Ranajky, IdCurrency);

        chbSobotaObed.Text = obed;
        chbNedelaObed.Text = obed;
        lblCenaObed.Text = Common.FormatMoney(Prices.Obed, IdCurrency);

        chbPiatokVecera.Text = vecera;
        chbSobotaVecera.Text = vecera;
        lblCenaVecera.Text = Common.FormatMoney(Prices.Vecera, IdCurrency);

        chbPiatokVecera2.Text = vecera2;
        chbSobotaVecera2.Text = vecera2;
        lblCenaVecera2.Text = Common.FormatMoney(Prices.Vecera2, IdCurrency);

        chbUbytovaniePiatokSobota.Text = ubytovanie;
        chbUbytovanieSobotaNedela.Text = ubytovanie;
        lblCenaUbytovanie.Text = Common.FormatMoney(Prices.Ubytovanie, IdCurrency);

        chbTichaTriedaPiatokSobota.Text = tichaTrieda;
        chbTichaTriedaSobotaNedela.Text = tichaTrieda;

        base.OnPreRender(e);
    }

    protected void Update(object sender, EventArgs e)
    {
        txtZbor.Visible = ddlZbor.SelectedValue == "-1";
        chbTichaTriedaPiatokSobota.Enabled = chbUbytovaniePiatokSobota.Checked;
        chbTichaTriedaSobotaNedela.Enabled = chbUbytovanieSobotaNedela.Checked;
    }

    private bool Friday
    {
        set
        {
            chbPiatokVecera.Checked = value;
            chbPiatokVecera2.Checked = value;
            chbUbytovaniePiatokSobota.Checked = value;
            Update(null, null);
        }
    }
    private bool Saturday
    {
        set
        {
            chbSobotaRanajky.Checked = value;
            chbSobotaObed.Checked = value;
            chbSobotaVecera.Checked = value;
            chbSobotaVecera2.Checked = value;
            chbUbytovanieSobotaNedela.Checked = value;
            Update(null, null);
        }
    }
    private bool Sunday
    {
        set
        {
            chbNedelaRanajky.Checked = value;
            chbNedelaObed.Checked = value;
            Update(null, null);
        }
    }
    protected void btnVsetkoPiatok_Click(object sender, EventArgs e)
    {
        Friday = true;
    }
    protected void btnNicPiatok_Click(object sender, EventArgs e)
    {
        Friday = false;
    }
    protected void btnVsetkoSobota_Click(object sender, EventArgs e)
    {
        Saturday = true;
    }
    protected void btnNicSobota_Click(object sender, EventArgs e)
    {
        Saturday = false;
    }
    protected void btnVsetkoNedela_Click(object sender, EventArgs e)
    {
        Sunday = true;
    }
    protected void btnNicNedela_Click(object sender, EventArgs e)
    {
        Sunday = false;
    }
    protected void btnVsetko_Click(object sender, EventArgs e)
    {
        Friday = true;
        Saturday = true;
        Sunday = true;
    }
    protected void btnNic_Click(object sender, EventArgs e)
    {
        Friday = false;
        Saturday = false;
        Sunday = false;
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        DeleteClicked = true;
    }
}