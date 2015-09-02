using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        var data = Database.GetSummary();

        lblPiatokVecera.Text = data.PiatokVecera.ToString();
        lblPiatokVeceraZaplatene.Text = data.PiatokVeceraZaplatene.ToString();
        lblPiatokVecera2.Text = data.PiatokVecera2.ToString();
        lblPiatokVecera2Zaplatene.Text = data.PiatokVecera2Zaplatene.ToString();
        lblUbytovaniePiatokSobota.Text = data.UbytovaniePiatokSobota.ToString();
        lblTichaTriedaPiatokSobota.Text = data.TichaTriedaPiatokSobota.ToString();
        lblSobotaRanajky.Text = data.SobotaRanajky.ToString();
        lblSobotaRanajkyZaplatene.Text = data.SobotaRanajkyZaplatene.ToString();
        lblSobotaObed.Text = data.SobotaObed.ToString();
        lblSobotaObedZaplatene.Text = data.SobotaObedZaplatene.ToString();
        lblSobotaVecera.Text = data.SobotaVecera.ToString();
        lblSobotaVeceraZaplatene.Text = data.SobotaVeceraZaplatene.ToString();
        lblSobotaVecera2.Text = data.SobotaVecera2.ToString();
        lblSobotaVecera2Zaplatene.Text = data.SobotaVecera2Zaplatene.ToString();
        lblUbytovanieSobotaNedela.Text = data.UbytovanieSobotaNedela.ToString();
        lblTichaTriedaSobotaNedela.Text = data.TichaTriedaSobotaNedela.ToString();
        lblNedelaRanajky.Text = data.NedelaRanajky.ToString();
        lblNedelaRanajkyZaplatene.Text = data.NedelaRanajkyZaplatene.ToString();
        lblNedelaObed.Text = data.NedelaObed.ToString();
        lblNedelaObedZaplatene.Text = data.NedelaObedZaplatene.ToString();
        lblSach.Text = data.Sach.ToString();
        lblPingPong.Text = data.PingPong.ToString();
        lblVolunteers.Text = data.VolunteersTotal.ToString();
        lblInternat1.Text = data.Internat1.ToString();
        lblInternat2.Text = data.Internat2.ToString();
        lblRegisteredPeople.Text = data.TotalPeople.ToString();

        lblExpectingEur.Text = string.Format("{0:0.00} EUR", data.ExpectingEur);
        lblExpectingCzk.Text = string.Format("{0:0.00} CZK", data.ExpectingCzk);
        lblExpectingTotal.Text = string.Format("{0:0.00} EUR", data.ExpectingEur + data.ExpectingCzk / Common.ExchangeRateCZK);
        lblMoneyFromPeople.Text = string.Format("{0:0.00} EUR", data.MoneyFromPeople);
        lblMoneyFromChurches.Text = string.Format("{0:0.00} EUR", data.MoneyFromChurches);

        gridTShirts.DataSource = data.Tricka;
        gridTShirts.DataBind();

        gridVolunteers.DataSource = data.Volunteers.Where(x => !string.IsNullOrWhiteSpace(x.Note));
        gridVolunteers.DataBind();

        gridCommenters.DataSource = data.Commenters;
        gridCommenters.DataBind();
        /*
        gridNeedHelp.DataSource = data.NeedHelp;
        gridNeedHelp.DataBind();
         */
        gridChurches.DataSource = data.Churches;
        gridChurches.DataBind();
    }
}