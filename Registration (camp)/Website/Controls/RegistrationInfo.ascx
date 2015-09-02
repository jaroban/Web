<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegistrationInfo.ascx.cs" Inherits="RegistrationInfo" %>

<ajaxToolkit:CollapsiblePanelExtender runat="server" ID="cpeRegistracia"
    TargetControlID="pnlRegistracia"
    ExpandControlID="pnlStatus"
    CollapseControlID="pnlStatus" 
    CollapsedText="(Ukázať)"
    ExpandedText="(Skryť)" 
    TextLabelID="lblStatus"
    ImageControlID="imgStatus"
    ExpandedImage="~/images/collapse.jpg"
    CollapsedImage="~/images/expand.jpg" />
<asp:Panel runat="server" ID="pnlStatus">
    <h3>
        <asp:Label runat="server" ID="lblTitle" />&nbsp;
        <asp:Image runat="server" ID="imgStatus" ImageUrl="~/images/collapse.jpg"/>&nbsp;
        <asp:Label runat="server" ID="lblStatus" />
        <asp:Button runat="server" ID="btnRemove" Text="Zmazať" OnClick="btnRemove_Click"/>
    </h3>
</asp:Panel>
<asp:Panel runat="server" ID="pnlRegistracia">
    <table>
        <tr>
            <td>Meno</td>
            <td>
                <asp:TextBox runat="server" ID="txtMeno" Width="300px" />
                <asp:Label runat="server" ID="lblMenoError" CssClass="error"/>
            </td>
        </tr>

        <tr>
            <td>Priezvisko</td>
            <td>
                <asp:TextBox runat="server" ID="txtPriezvisko" Width="300px" AutoPostBack="true"/>
                <asp:Label runat="server" ID="lblPriezviskoError" CssClass="error"/>
            </td>
        </tr>

        <tr>
            <td>Email</td>
            <td>
                <asp:TextBox runat="server" ID="txtEmail" Width="300px" AutoPostBack="true"/>
                <asp:Label runat="server" ID="lblEmailError" CssClass="error"/>
            </td>
        </tr>

        <tr>
            <td>Telefón</td>
            <td>
                <asp:TextBox runat="server" ID="txtTelefon" Width="300px" />
                <%-- <asp:Label runat="server" ID="lblTelefonError" CssClass="error"/> --%>
            </td>
        </tr>

        <tr>
            <td>Služba</td>
            <td>
                <asp:DropDownList runat="server" ID="ddlSluzba" AutoPostBack="true" OnSelectedIndexChanged="Update" />
                <asp:TextBox runat="server" ID="txtSluzba" Width="300px" Visible="false" AutoPostBack="true" />
                <asp:Label runat="server" ID="lblSluzbaError" CssClass="error"/>
            </td>
        </tr>

        <tr>
            <td>Poznámka</td>
            <td>
                <asp:TextBox runat="server" ID="txtPoznamka" TextMode="MultiLine" Rows="5" Width="80%"/>
            </td>
        </tr>
    </table>
</asp:Panel>