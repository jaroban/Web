using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Detail : Page
{
    private bool _reload;
    private int? _id;

    protected void Page_Load(object sender, EventArgs e)
    {
        var idString = Request.QueryString["id"];
        int dummy;
        if (!string.IsNullOrEmpty(idString) && int.TryParse(idString, out dummy))
        {
            _id = dummy;
        }
        btnSave.Text = _id.HasValue ? "Uložiť zmeny" : "Registrovať nového účastníka";
        if (!_id.HasValue)
        {
            trSkupina.Visible = false;
            pnlGroup.Visible = false;
            lblTitle.Text = "Nový účastník / účastníčka";
            trDoplatili.Visible = false;
        }
        if (!IsPostBack)
        {
            Common.FillChurches(ddlZbor);
            Common.FillTeeShirts(ddlTricko);
            LoadData();
        }
        lblSuccess.Text = "";
    }

    private void LoadData()
    {
        _reload = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(txtMeno.Text)) errors.Add(Common.ChybaMeno);
        if (string.IsNullOrWhiteSpace(txtPriezvisko.Text)) errors.Add(Common.ChybaPriezvisko);
        if (!Common.ValidateEmail(txtEmail.Text.Trim())) errors.Add(Common.ChybaEmail);
        float sponzorskyDar = 0;
        if (!string.IsNullOrWhiteSpace(txtDar.Text))
        {
            if (!float.TryParse(txtDar.Text, out sponzorskyDar))
            {
                errors.Add(Common.ChybaSponzorskyDar);
            }
        }
        var idTricko = ddlTricko.SelectedValue.StringToInt();
        if (idTricko == 0) idTricko = null;
        var idZbor = ddlZbor.SelectedValue.StringToInt();
        if (idZbor == 0 || idZbor == -1) idZbor = null;
        float cashback = 0;
        if (!string.IsNullOrWhiteSpace(txtCashBack.Text))
        {
            if (!float.TryParse(txtCashBack.Text, out cashback))
            {
                errors.Add(Common.ChybaPreplacame);
            }
        }

        lblError.Text = "";
        if (errors.Count > 0)
        {
            lblError.Text = string.Join("<br/>", errors);
            return;
        }

        var data = new DetailData
        {
            Id = _id,
            Meno = txtMeno.Text,
            Priezvisko = txtPriezvisko.Text,
            Email = txtEmail.Text,
            Telefon = txtTelefon.Text,
            IdZbor = idZbor,
            InyZbor = txtInyZbor.Text,
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
            Dobrovolnik = chbDobrovolnik.Checked,
            Poznamka = txtPoznamka.Text,
            Internat1 = chbInternat1.Checked,
            Internat2 = chbInternat2.Checked,
            CashBack = cashback,
            InternatZadarmo = chbInternatZadarmo.Checked,
            RegistraciaZadarmo = chbRegistraciaZadarmo.Checked,
            JedloZadarmo = chbJedloZadarmo.Checked,
            Prisli = chbPrisli.Checked,
            IstoPride = chbIstoPride.Checked,
            Dar = sponzorskyDar
        };

        int? newId;
        try
        {
            newId = Database.UpdateOrCreate(data);
        }
        catch(Exception ex)
        {
            lblError.Text = ex.Message + "<br/>" + ex.InnerException;
            return;
        }
        if (!_id.HasValue && newId.HasValue)
        {
            // just created a user
            Response.Redirect(string.Format("/Detail.aspx?id={0}", newId.Value));
            return;
        }
        if (_id.HasValue && !data.Id.HasValue)
        {
            // user is not in DB
            Response.Redirect("/Detail.aspx");
            return;
        }
        // updated a user
        _reload = true;
        lblSuccess.Text = "Zmeny boli úspešne uložené";
    }

    protected void btnDoplatili_Click(object sender, EventArgs e)
    {
        if (!_id.HasValue) return;
        var errors = new List<string>();
        float doplatili = 0;
        if (!string.IsNullOrWhiteSpace(txtDoplatili.Text))
        {
            if (!float.TryParse(txtDoplatili.Text, out doplatili))
            {
                errors.Add(Common.ChybaDoplatili);
            }
        }
        try
        {
            Database.AddPayment(_id.Value, doplatili, "Platba na mieste, IP = " + Common.GetIpAddress());
            LoadData();
        }
        catch(Exception ex)
        {
            errors.Add(ex.Message + ' ' + ex.InnerException);
        }
        lblError.Text = "";
        if (errors.Count > 0)
        {
            lblError.Text = string.Join("<br/>", errors);
            return;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        if(_id.HasValue && _reload)
        {
            trSkupina.Visible = false;
            pnlGroup.Visible = false;

            var data = Database.GetDetail(_id.Value);
            if(data.Id.HasValue)
            {
                lblId.Text = data.Id.ToString();
                txtMeno.Text = data.Meno;
                txtPriezvisko.Text = data.Priezvisko;
                txtEmail.Text = data.Email;
                txtTelefon.Text = data.Telefon;
                ddlZbor.SelectedValue = data.IdZbor.HasValue ? data.IdZbor.ToString() : "0";
                txtInyZbor.Text = data.InyZbor;
                chbPiatokVecera.Checked = data.PiatokVecera;
                chbPiatokVecera2.Checked = data.PiatokVecera2;
                chbUbytovaniePiatokSobota.Checked = data.UbytovaniePiatokSobota;
                chbTichaTriedaPiatokSobota.Checked = data.TichaTriedaPiatokSobota;
                chbSobotaRanajky.Checked = data.SobotaRanajky;
                chbSobotaObed.Checked = data.SobotaObed;
                chbSobotaVecera.Checked = data.SobotaVecera;
                chbSobotaVecera2.Checked = data.SobotaVecera2;
                chbUbytovanieSobotaNedela.Checked = data.UbytovanieSobotaNedela;
                chbTichaTriedaSobotaNedela.Checked = data.TichaTriedaSobotaNedela;
                chbNedelaRanajky.Checked = data.NedelaRanajky;
                chbNedelaObed.Checked = data.NedelaObed;
                chbSach.Checked = data.Sach;
                chbPingPong.Checked = data.PingPong;
                ddlTricko.SelectedValue = data.IdTricko.HasValue ? data.IdTricko.ToString() : "0";
                chbDobrovolnik.Checked = data.Dobrovolnik;
                txtPoznamka.Text = data.Poznamka;
                lblRegistrationInfo.Text = data.DtRegistered.ToString();
                chbInternat1.Checked = data.Internat1;
                chbInternat2.Checked = data.Internat2;
                txtCashBack.Text = data.CashBack.HasValue ? Currency(data.CashBack.Value) : "";
                chbInternatZadarmo.Checked = data.InternatZadarmo;
                chbRegistraciaZadarmo.Checked = data.RegistraciaZadarmo;
                chbJedloZadarmo.Checked = data.JedloZadarmo;
                lblPlatba.Text = data.DtPlatba.ToString();
                chbPrisli.Checked = data.Prisli;
                lblSuma.Text = Currency(data.Suma);
                chbIstoPride.Checked = data.IstoPride;
                lblZaplatili.Text = Currency(data.Zaplatili);
                lblSkupinaDlzi.Text = Currency(data.SkupinaDlzi);
                txtDar.Text = Currency(data.Dar);
                lblPreplatok.Text = Currency(data.Preplatok);
                lblPreplatok.CssClass = data.Preplatok >= -0.01 ? "positive" : "negative";

                lblTitle.Text = data.Meno + " " + data.Priezvisko;

                if(data.Group.Count > 0)
                {
                    trSkupina.Visible = true;
                    pnlGroup.Visible = true;
                    lblGroup.Text = string.Join(" ", data.Group.Select(x => string.Format("<a href=\"/Detail.aspx?id={0}\" target=\"new\">{1}</a>", x.Id, x.Name)));
                }
            }
            else
            {
                Response.Redirect("/Detail.aspx");
                return;
            }
        }

        lblCenaRanajky.Text = Common.FormatMoney(Prices.Ranajky, 1);
        lblCenaObed.Text = Common.FormatMoney(Prices.Obed, 1);
        lblCenaVecera.Text = Common.FormatMoney(Prices.Vecera, 1);
        lblCenaVecera2.Text = Common.FormatMoney(Prices.Vecera2, 1);
        lblCenaUbytovanie.Text = Common.FormatMoney(Prices.Ubytovanie, 1);
        lblCenaInternat1.Text = Common.FormatMoney(Prices.Internat1, 1);
        lblCenaInternat2.Text = Common.FormatMoney(Prices.Internat2, 1);
        base.OnPreRender(e);
    }

    private string Currency(float f)
    {
        return string.Format("{0:0.00}", f);
    }
}
