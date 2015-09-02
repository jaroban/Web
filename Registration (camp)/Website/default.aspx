<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Register" %>

<%@ Register TagPrefix="uc" TagName="RegistrationInfo" Src="~/Controls/RegistrationInfo.ascx" %>

<asp:Content ID="Content" ContentPlaceHolderID="Body" Runat="Server">
    <asp:UpdatePanel runat="server" ID="upRegistration">
        <ContentTemplate>
            <div style="display:none">
            <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="upRegistration" DisplayAfter="500" />
            <asp:Label runat="server" ID="lblStatus1" />
            <br />
            <asp:Label runat="server" ID="lblStatus2" />
            <h2>Zoznam účastníkov</h2>
            <asp:PlaceHolder runat="server" ID="plcGenerated" />
            <br />
            <asp:Button runat="server" ID="btnAddUser" Text="Pridať účastníka" OnClick="btnAddUser_Click" />
            <hr />
            <h2>Zhrnutie</h2>
            <asp:GridView ID="gridSummary" runat="server" AutoGenerateColumns="false" OnRowCommand="gridView_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Meno">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Chyby">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblErrors" Text='<%# Eval("ErrorString") %>' CssClass='<%# Eval("CssClass") %> '/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Suma">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblSuma" Text='<%# Eval("CostString") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Akcia">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnDeleteUser"
                                Text="Zmazať"
                                CommandName="Vymazat" 
                                CommandArgument='<%# Eval("Id") %>' 
                                Visible='<%# !(bool)Eval("Single") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <hr />
            <h2>Platba</h2>
            <table>
                <tr>
                    <td>Bude to stáť</td>
                    <td>
                        <strong>
                            <asp:Label ID="lblSuma" runat="server" />
                        </strong>
                    </td>
                </tr>

                <tr runat="server" id="trPayerEmail">
                    <td>Email o platbe</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmail" Width="300px" Text="@"/>
                        <asp:Label runat="server" ID="lblEmailError" CssClass="error"/>
                    </td>
                </tr>

                <tr>
                    <td>Kontrolná otázka</td>
                    <td>
                        Ako sa volá obec, v ktorej sa koná tento kemp?<br />
                        <asp:TextBox runat="server" ID="txtCaptcha" Width="100px"/>
                        <asp:Button runat="server" ID="btnCheckCaptcha" Text="Overiť" OnClick="btnCheckCaptcha_Click" />
                        <asp:Label runat="server" ID="lblCaptchaError" CssClass="error"/>
                    </td>
                </tr>

            </table>
            <br />
            <asp:Button ID="btnRegister" runat="server" Text="Registrovať" OnClick="btnRegister_Click" />
            <asp:Label runat="server" ID="lblErrorOnPage"/>
            <br />
            <br />
            <asp:Label ID="lblResult" runat="server" Text="" CssClass="error"/>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
