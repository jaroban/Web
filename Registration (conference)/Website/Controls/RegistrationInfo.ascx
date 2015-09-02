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
            <td>Zbor</td>
            <td>
                <asp:DropDownList runat="server" ID="ddlZbor" AutoPostBack="true" OnSelectedIndexChanged="Update" />
                <asp:TextBox runat="server" ID="txtZbor" Width="300px" Visible="false" AutoPostBack="true" />
                <%-- <asp:Label runat="server" ID="lblZborError" CssClass="error"/> --%>
            </td>
        </tr>

        <tr>
            <td>Strava<br />a ubytovanie</td>
            <td>
                <table>
                    <tr>
                        <td>Piatok</td>
                        <td>Sobota</td>
                        <td>Nedeľa</td>
                        <td>Cena</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chbSobotaRanajky" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chbNedelaRanajky" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCenaRanajky" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chbSobotaObed" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chbNedelaObed" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCenaObed" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chbPiatokVecera" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chbSobotaVecera" AutoPostBack="true" />
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label runat="server" ID="lblCenaVecera" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chbPiatokVecera2" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chbSobotaVecera2" AutoPostBack="true" />
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label runat="server" ID="lblCenaVecera2" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chbUbytovaniePiatokSobota" AutoPostBack="true" OnCheckedChanged="Update" />
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chbUbytovanieSobotaNedela" AutoPostBack="true" OnCheckedChanged="Update" />
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label runat="server" ID="lblCenaUbytovanie" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chbTichaTriedaPiatokSobota" Enabled="false" />
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chbTichaTriedaSobotaNedela" Enabled="false" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnVsetkoPiatok" Text="Všetko" OnClick="btnVsetkoPiatok_Click"/>
                            <asp:Button runat="server" ID="btnNicPiatok" Text="Nič" OnClick="btnNicPiatok_Click"/>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnVsetkoSobota" Text="Všetko" OnClick="btnVsetkoSobota_Click"/>
                            <asp:Button runat="server" ID="btnNicSobota" Text="Nič" OnClick="btnNicSobota_Click"/>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnVsetkoNedela" Text="Všetko" OnClick="btnVsetkoNedela_Click"/>
                            <asp:Button runat="server" ID="btnNicNedela" Text="Nič" OnClick="btnNicNedela_Click"/>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnVsetko" Text="Úplne všetko" OnClick="btnVsetko_Click"/>
                            <asp:Button runat="server" ID="btnNic" Text="Vôbec nič" OnClick="btnNic_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td>Športy</td>
            <td>
                <asp:CheckBox runat="server" ID="chbSach" Text="Šach"/>
                <br />
                <asp:CheckBox runat="server" ID="chbPingPong" Text="Ping pong"/>
            </td>
        </tr>

        <tr>
            <td>Tričko</td>
            <td>
                <asp:DropDownList runat="server" ID="ddlTricko" />
                <ajaxToolkit:HoverMenuExtender runat="server" ID="hme1"
                    TargetControlID="ddlTricko"
                    PopupControlID="pnlTricko"
                    HoverCssClass="popupHover"
                    PopupPosition="Right"
                    OffsetX="0"
                    OffsetY="-80"
                    PopDelay="50" />
                <asp:Panel runat="server" ID="pnlTricko">
                    <asp:Image runat="server" ID="imgTricko" ImageUrl="~/images/tricko.png"/>
                </asp:Panel>
            </td>
        </tr>
        <%-- 
        <tr>
            <td>Iné</td>
            <td>
                <asp:CheckBox runat="server" ID="chbSluziaci"
                    Text="Som ochotný slúžiť a byť k dispozícii pre rôzne úlohy počas celého času trvania konferencie" 
                    AutoPostBack="true" />
            </td>
        </tr>
        --%>
        <tr>
            <td>Poznámka</td>
            <td>
                <asp:TextBox runat="server" ID="txtPoznamka" TextMode="MultiLine" Rows="5" Width="80%"/>
            </td>
        </tr>
    </table>
</asp:Panel>