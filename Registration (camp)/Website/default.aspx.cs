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
    int _totalPeople = 0;
    List<IdName> _sluzby;

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
        var data = Database.GetSummary();
        _totalPeople = data.TotalPeople;
        _sluzby = data.Sluzby;
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
        lblStatus1.Text = string.Format("Zatiaľ je na kemp zaregistovaných {0} ľudí.", _totalPeople);
        if (_totalPeople < Common.SpaceInside)
        {
            lblStatus2.Text = string.Format("Ešte {0} voľných miest v chate!", Common.SpaceInside - _totalPeople);
        }
        else if (_totalPeople < Common.SpaceOutside)
        {
            lblStatus2.Text = string.Format("Ešte {0} voľných miest v stanoch!", Common.SpaceOutside - _totalPeople);
        }
        else if (_totalPeople == Common.SpaceOutside)
        {
            lblStatus2.Text = "Sme plní!";
        }
        else
        {
            lblStatus2.Text = string.Format("Už sme o {0} miest prekročili kapacitu.", _totalPeople - Common.SpaceOutside);
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
        control.FillSluzby(_sluzby);
        plcGenerated.Controls.Add(control);
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        _data.Add(new RegistrationEntry());
        _countChanged = true;
    }

    private void CheckCaptcha()
    {
        lblCaptchaError.Text = IsCaptchaValid() ? "" : Common.ChybaCaptcha;
    }

    protected void btnCheckCaptcha_Click(object sender, EventArgs e)
    {
        CheckCaptcha();
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        _registerClicked = true;
        CheckCaptcha();
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

    private bool IsCaptchaValid()
    {
        var reCorrect = new Regex(@"xxxxxxxxxxx", RegexOptions.IgnoreCase);
        return reCorrect.IsMatch(txtCaptcha.Text.Trim());
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
            _data[i].Single = _data.Count == 1;
            _controls[i].Single = _data.Count == 1;
            sum += _data[i].RegistrationFee;
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

        if (!IsCaptchaValid()) valid = false;

        var amountToPay = sum;
        var toPay = Common.FormatMoney(amountToPay);
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

        lblResult.Text = "";
        if (valid && _registerClicked)
        {
            try
            {
                var emails = new List<Email>();

                if(_data.Count == 1)
                {
                    emails.Add(new Email(payerEmail, Emails.RegistrationSubject, Emails.GetSingle(_data[0], toPay, _totalPeople)));
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
                            emails.Add(new Email(payerEmail, Emails.RegistrationSubject, Emails.GetMultiplePayerRegistered(_data, i, toPay, _totalPeople)));
                        }
                        else
                        {
                            emails.Add(new Email(_data[i].Email, Emails.RegistrationSubject, Emails.GetMultiple(_data[i], payerEmail, _totalPeople)));
                        }
                    }
                    if(!payerIsRegistered)
                    {
                        emails.Add(new Email(payerEmail, Emails.RegistrationSubject, Emails.GetMultiplePayerNotRegistered(_data, toPay, payerEmail)));
                    }
                }

                // write to database
                var data = Database.WriteData(_data, emails, payerEmail, amountToPay);
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
