using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class Register : System.Web.UI.Page
{
    public const string KeyRegistrationData = "KeyRegistrationData";

    private List<RegistrationEntry> _data;
    private List<RegistrationInfo> _controls;
    bool _registerClicked = false;
    bool _countChanged = false;

    protected override void LoadViewState(object savedState)
    {
        base.LoadViewState(savedState);
        _data = (List<RegistrationEntry>)ViewState[KeyRegistrationData] ?? new List<RegistrationEntry>();
        GenerateControls();
    }

    protected override object SaveViewState()
    {
        ViewState[KeyRegistrationData] = _data;
        return base.SaveViewState();
    }

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            _data = new List<RegistrationEntry>();
            _data.Add(new RegistrationEntry());
            GenerateControls();
            _countChanged = true;
        }
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlMena.Items.Add(new ListItem("EUR", "1"));
            ddlMena.Items.Add(new ListItem("CZK", "2"));
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        for (int i = 0; i < _data.Count; i++)
        {
            _data[i] = _controls[i].Data;
            _data[i].Id = i;
        }
        base.OnLoad(e);
    }

    private void GenerateControls()
    {
        plcGenerated.Controls.Clear();
        _controls = new List<RegistrationInfo>();

        for (int i = 0; i < _data.Count; i++)
        {
            AddControl(i);
        }
    }

    private void AddControl(int i)
    {
        var control = (RegistrationInfo)LoadControl("~/Controls/RegistrationInfo.ascx");
        _controls.Add(control);
        control.ID = string.Format("RegistrationInfo_{0}", i);
        control.Fill();
        plcGenerated.Controls.Add(control);
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        _data.Add(new RegistrationEntry());
        _countChanged = true;
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        _registerClicked = true;
    }

    protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Vymazat"))
        {
            try
            {
                int id = int.Parse((string)e.CommandArgument);
                if (id < _data.Count && _data.Count > 1)
                {
                    _data.RemoveAt(id);
                    _countChanged = true;
                }
            }
            catch
            {
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        for (int i = 0; i < _controls.Count; i++)
        {
            if (_controls[i].DeleteClicked && i >= 0 && i < _data.Count)
            {
                _data.RemoveAt(i);
                _countChanged = true;
            }
        }

        if(_countChanged) GenerateControls();

        int idCurrency = 1;
        int.TryParse(ddlMena.SelectedValue, out idCurrency);

        var currencyName = idCurrency == 1 ? "EUR" : "CZK";
        var currencyRate = idCurrency == 1 ? 1 : Common.ExchangeRateCZK;

        float sum = 0;
        bool valid = true;
        for (int i = 0; i < _data.Count; i++)
        {
            if(_countChanged)
            {
                _controls[i].Data = _data[i];
            }
            _data[i] = _controls[i].Data;
            _data[i].Id = i;
            _controls[i].Title = _data[i].Title;
            _controls[i].IdCurrency = idCurrency;
            _data[i].IdCurrency = idCurrency;
            _data[i].Single = _data.Count == 1;
            _controls[i].Single = _data.Count == 1;
            sum += _data[i].Cost;
            valid = valid && _data[i].Valid;
        }

        trPayerEmail.Visible = _data.Count > 1;
        btnRegister.Text = _data.Count > 1 ? string.Format("Registrovať {0} účastníkov", _data.Count) : "Registrovať 1 účastníka";

        lblEmailError.Text = "";
        if (_data.Count > 1 && !Common.ValidateEmail(txtEmail.Text.Trim()))
        {
            valid = false;
            lblEmailError.Text = Common.ChybaEmail;
        }

        var payerEmail = _data.Count > 1 ? txtEmail.Text.Trim().ToLower() : _data[0].Email.Trim().ToLower();

        lblCaptchaError.Text = "";
        var reCorrect = new Regex(@"((andrej\s+)?kiska)|((milo(s|š)\s+)?zeman)", RegexOptions.IgnoreCase);
        if(!reCorrect.IsMatch(txtCaptcha.Text.Trim()))
        {
            valid = false;
            lblCaptchaError.Text = Common.ChybaCaptcha;
        }

        float sponzorskyDar = 0;
        lblSponzorskyDar.Text = "";
        if (!string.IsNullOrWhiteSpace(txtDar.Text))
        {
            if(!float.TryParse(txtDar.Text, out sponzorskyDar))
            {
                lblSponzorskyDar.Text = Common.ChybaSponzorskyDar;
                valid = false;
            }
        }

        var amountToPay = sum * currencyRate + sponzorskyDar;
        var toPay = string.Format("{0:0.00} {1}", amountToPay, currencyName);
        lblSuma.Text = toPay;

        // check for duplicate emails
        var emailHash = new Dictionary<string, int>();
        foreach(var item in _data)
        {
            var email = item.Email.Trim().ToLower();
            emailHash[email] = emailHash.ContainsKey(email) ? emailHash[email] + 1 : 1;
        }
        foreach (var item in emailHash)
        {
            if (item.Value > 1)
            {
                valid = false;
                for (int i = 0; i < _data.Count; i++)
                {
                    if (_data[i].Email.Trim().ToLower() == item.Key)
                    {
                        _data[i].Errors.Add(Common.ChybaEmailSaOpakuje);
                    }
                }
            }
        }

        var needHelp = chbPomoc.Checked;

        lblResult.Text = "";
        if (valid && _registerClicked)
        {
            try
            {
                var emails = new List<Email>();

                if(_data.Count == 1)
                {
                    emails.Add(new Email(payerEmail, Emails.RegistrationSubject, Emails.GetSingle(_data[0], toPay)));
                }
                else
                {
                    var payerIsRegistered = false;
                    for(var i = 0; i < _data.Count; i++)
                    {
                        _data[i].Email = _data[i].Email.Trim().ToLower();
                        if(payerEmail == _data[i].Email)
                        {
                            payerIsRegistered = true;
                            emails.Add(new Email(payerEmail, Emails.RegistrationSubject, Emails.GetMultiplePayerRegistered(_data, i, toPay)));
                        }
                        else
                        {
                            emails.Add(new Email(_data[i].Email, Emails.RegistrationSubject, Emails.GetMultiple(_data[i], payerEmail)));
                        }
                        /*
                        if(_data[i].Sluziaci)
                        {
                            // mail o dobrovolnikovi
                            emails.Add(new Email(Emails.VolunteerCoordinator, Emails.VolunteerSubject, Emails.GetVolunteer(_data[i])));
                        }
                         */
                    }
                    if(!payerIsRegistered)
                    {
                        emails.Add(new Email(payerEmail, Emails.RegistrationSubject, Emails.GetMultiplePayerNotRegistered(_data, toPay, payerEmail)));
                    }
                }

                // write to database
                var data = Database.WriteData(_data, emails, payerEmail, amountToPay, sponzorskyDar, idCurrency, needHelp);
                if(data.Success)
                {
                    Response.Redirect("~/Success.aspx");
                    return;
                }

                // there are bad emails
                valid = false;
                foreach (var item in data.AlreadyRegisteredEmails)
                {
                    for (int i = 0; i < _data.Count; i++)
                    {
                        if (_data[i].Email.Trim().ToLower() == item.Trim().ToLower())
                        {
                            _data[i].Errors.Add(Common.ChybaEmailUzZaregistrovany);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message + " " + ex.InnerException;
            }
        }

        lblErrorOnPage.Text = valid ? "Všetko OK" : "Ešte niečo chýba (viď červené texty vyššie)";
        lblErrorOnPage.CssClass = valid ? "valid" : "error";

        gridSummary.DataSource = _data;
        gridSummary.DataBind();

        base.OnPreRender(e);
    }
}
