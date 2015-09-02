<%@ Page Language="C#" MasterPageFile="~/MasterPageLocal.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Detail" %>

<asp:Content ID="Content" ContentPlaceHolderID="Body" Runat="Server">
    <%-- 
    <asp:UpdatePanel runat="server" ID="upRegistration">
        <ContentTemplate>
            <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="upRegistration" DisplayAfter="100" /> --%>
            <h2><asp:Label runat="server" ID="lblTitle" /></h2>
            <asp:Panel runat="server" ID="pnlGroup">
                V skupine je ešte:
                <asp:Literal runat="server" ID="lblGroup" />
            </asp:Panel>
            <table style="width:100%">
                <tr>
                    <td>
                        <%-- first column --%>
                        <table>
                            <tr>
                                <td>Meno</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMeno" Width="200px" />
                                </td>
                            </tr>
                            <tr>
                                <td>Priezvisko</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPriezvisko" Width="200px" />
                                </td>
                            </tr>
                            <tr>
                                <td>Email</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEmail" Width="200px" />
                                </td>
                            </tr>
                            <tr>
                                <td>Telefón</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTelefon" Width="200px" />
                                </td>
                            </tr>
                            <tr>
                                <td>Zbor</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlZbor"/>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtInyZbor" Width="200px" />
                                </td>
                            </tr>
                            <tr>
                                <td>Tričko</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlTricko" />
                                </td>
                            </tr>
                            <tr>
                                <td>Visačka</td>
                                <td>
                                    <asp:Label runat="server" ID="lblId" />
                                </td>
                            </tr>
                            <tr>
                                <td>Poznámka</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPoznamka" TextMode="MultiLine" Rows="5" Width="200px"/>
                                </td>
                            </tr>
                            <tr>
                                <td>Šach</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chbSach" />
                                </td>
                            </tr>
                            <tr>
                                <td>Ping pong</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chbPingPong" />
                                </td>
                            </tr>

                        </table>
                    </td>
                    <td>
                        <%-- second column --%>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Piatok</td>
                                            <td>Sobota</td>
                                            <td>Nedeľa</td>
                                            <td>Cena</td>
                                        </tr>
                                        <tr>
                                            <td>Raňajky</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbSobotaRanajky" />
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbNedelaRanajky" />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCenaRanajky" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Obed</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbSobotaObed" />
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbNedelaObed" />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCenaObed" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Večera</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbPiatokVecera" />
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbSobotaVecera" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCenaVecera" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Bageta</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbPiatokVecera2" />
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbSobotaVecera2" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCenaVecera2" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Ubytovanie</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbUbytovaniePiatokSobota" />
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbUbytovanieSobotaNedela" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCenaUbytovanie" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tichá trieda</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbTichaTriedaPiatokSobota" />
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chbTichaTriedaSobotaNedela" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Internát 1</td>
                                            <td colspan="3">
                                                <asp:CheckBox runat="server" ID="chbInternat1" />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCenaInternat1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Internát 2</td>
                                            <td colspan="3">
                                                <asp:CheckBox runat="server" ID="chbInternat2" />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCenaInternat2" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <%-- third column --%>
                        <table>
                            <tr>
                                <td>Isto príde</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chbIstoPride" />
                                </td>
                            </tr>
                            <tr>
                                <td>Prišli</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chbPrisli" />
                                </td>
                            </tr>
                            <tr>
                                <td>Dobrovoľník</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chbDobrovolnik" />
                                </td>
                            </tr>
                            <tr>
                                <td>Internát zadarmo</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chbInternatZadarmo" />
                                </td>
                            </tr>
                            <tr>
                                <td>Registrácia zadarmo</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chbRegistraciaZadarmo" />
                                </td>
                            </tr>
                            <tr>
                                <td>Jedlo zadarmo</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chbJedloZadarmo" />
                                </td>
                            </tr>
                            <tr>
                                <td>Registrácia</td>
                                <td>
                                    <asp:Label runat="server" ID="lblRegistrationInfo" />
                                </td>
                            </tr>
                            <tr>
                                <td>Zaplatili</td>
                                <td>
                                    <asp:Label runat="server" ID="lblPlatba" />
                                    <br />
                                    <asp:Label runat="server" ID="lblZaplatili" />
                                </td>
                            </tr>
                            <tr>
                                <td>Z toho bol dar</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDar" />
                                </td>
                            </tr>
                            <tr>
                                <td>Preplácame im</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCashBack" Width="100px" />
                                </td>
                            </tr>
                            <tr runat="server" id="trSkupina">
                                <td>Skupina dlží</td>
                                <td>
                                    <asp:Label runat="server" ID="lblSkupinaDlzi" />
                                </td>
                            </tr>
                            <tr>
                                <td>Tento človek dlží</td>
                                <td>
                                    <asp:Label runat="server" ID="lblSuma" />
                                </td>
                            </tr>
                            <tr>
                                <td>Preplatok</td>
                                <td>
                                    <asp:Label runat="server" ID="lblPreplatok" />
                                </td>
                            </tr>
                            <tr runat="server" id="trDoplatili">
                                <td>
                                    <asp:Button runat="server" ID="btnDoplatili" Text="Doplatili na mieste" OnClick="btnDoplatili_Click" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDoplatili" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" />
            <br />
            <asp:Label ID="lblError" runat="server" Text="" CssClass="error"/>
            <asp:Label ID="lblSuccess" runat="server" Text="" CssClass="valid"/>
    <%-- 
        </ContentTemplate>
    </asp:UpdatePanel>  --%>
</asp:Content>
