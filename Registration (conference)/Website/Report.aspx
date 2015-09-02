<%@ Page Language="C#" MasterPageFile="~/MasterPagePublic.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<asp:Content ID="Content" ContentPlaceHolderID="Body" Runat="Server">
    <table>
        <tr>
            <td>Účastníci:</td>
            <td>
                <asp:Label runat="server" ID="lblRegisteredPeople"/>
            </td>
        </tr>
        <tr>
            <td>Dobrovoľníkov:</td>
            <td>
                <asp:Label runat="server" ID="lblVolunteers"/>
            </td>
        </tr>
        <tr>
            <td>Očakávame:</td>
            <td>
                <asp:Label runat="server" ID="lblExpectingEur"/>
                &nbsp;+&nbsp;
                <asp:Label runat="server" ID="lblExpectingCzk"/>
                &nbsp;=&nbsp;
                <asp:Label runat="server" ID="lblExpectingTotal"/>
            </td>
        </tr>
        <tr>
            <td>Prišlo od účastníkov:</td>
            <td>
                <asp:Label runat="server" ID="lblMoneyFromPeople"/>
            </td>
        </tr>
        <tr>
            <td>Prišlo od zborov:</td>
            <td>
                <asp:Label runat="server" ID="lblMoneyFromChurches"/>
            </td>
        </tr>
        <tr>
            <td>Šach:</td>
            <td>
                <asp:Label runat="server" ID="lblSach"/>
            </td>
        </tr>
        <tr>
            <td>Ping pong:</td>
            <td>
                <asp:Label runat="server" ID="lblPingPong"/>
            </td>
        </tr>
        <tr>
            <td>Internát 1:</td>
            <td>
                <asp:Label runat="server" ID="lblInternat1"/>
            </td>
        </tr>
        <tr>
            <td>Internát 2:</td>
            <td>
                <asp:Label runat="server" ID="lblInternat2"/>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>&nbsp;</td>
            <td>Piatok</td>
            <td>Zap.</td>
            <td>Sobota</td>
            <td>Zap.</td>
            <td>Nedeľa</td>
            <td>Zap.</td>
        </tr>
        <tr>
            <td>Raňajky</td>
            <td colspan="2">&nbsp;</td>
            <td><asp:Label runat="server" ID="lblSobotaRanajky" /></td>
            <td><asp:Label runat="server" ID="lblSobotaRanajkyZaplatene" /></td>
            <td><asp:Label runat="server" ID="lblNedelaRanajky" /></td>
            <td><asp:Label runat="server" ID="lblNedelaRanajkyZaplatene" /></td>
        </tr>
        <tr>
            <td>Obed</td>
            <td colspan="2">&nbsp;</td>
            <td><asp:Label runat="server" ID="lblSobotaObed" /></td>
            <td><asp:Label runat="server" ID="lblSobotaObedZaplatene" /></td>
            <td><asp:Label runat="server" ID="lblNedelaObed" /></td>
            <td><asp:Label runat="server" ID="lblNedelaObedZaplatene" /></td>
        </tr>
        <tr>
            <td>Večera</td>
            <td><asp:Label runat="server" ID="lblPiatokVecera"/></td>
            <td><asp:Label runat="server" ID="lblPiatokVeceraZaplatene"/></td>
            <td><asp:Label runat="server" ID="lblSobotaVecera" /></td>
            <td><asp:Label runat="server" ID="lblSobotaVeceraZaplatene" /></td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td>Druhá večera</td>
            <td><asp:Label runat="server" ID="lblPiatokVecera2" /></td>
            <td><asp:Label runat="server" ID="lblPiatokVecera2Zaplatene" /></td>
            <td><asp:Label runat="server" ID="lblSobotaVecera2" /></td>
            <td><asp:Label runat="server" ID="lblSobotaVecera2Zaplatene" /></td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td>Ubytovanie</td>
            <td colspan="2">
                <asp:Label runat="server" ID="lblUbytovaniePiatokSobota" />
            </td>
            <td colspan="2">
                <asp:Label runat="server" ID="lblUbytovanieSobotaNedela" />
            </td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td>Tichá trieda</td>
            <td colspan="2">
                <asp:Label runat="server" ID="lblTichaTriedaPiatokSobota" />
            </td>
            <td colspan="2">
                <asp:Label runat="server" ID="lblTichaTriedaSobotaNedela" />
            </td>
            <td colspan="2">&nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gridTShirts" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Veľkosť">
                <ItemTemplate>
                    <%# Eval("Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reg">
                <ItemTemplate>
                    <%# Eval("Ordered") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Zap">
                <ItemTemplate>
                    <%# Eval("Paid") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    Dobrovoľníci
    <asp:GridView ID="gridVolunteers" runat="server" AutoGenerateColumns="false">
        <Columns>
            <%--
            <asp:TemplateField HeaderText="Meno">
                <ItemTemplate>
                    <%# Eval("FirstName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Priezvisko">
                <ItemTemplate>
                    <%# Eval("LastName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <%# Eval("Email") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Telefón">
                <ItemTemplate>
                    <%# Eval("Phone") %>
                </ItemTemplate>
            </asp:TemplateField>
            --%>
            <asp:TemplateField HeaderText="Poznámka">
                <ItemTemplate>
                    <%# Eval("Note") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    Iné poznámky
    <asp:GridView ID="gridCommenters" runat="server" AutoGenerateColumns="false">
        <Columns>
            <%--
            <asp:TemplateField HeaderText="Meno">
                <ItemTemplate>
                    <%# Eval("FirstName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Priezvisko">
                <ItemTemplate>
                    <%# Eval("LastName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <%# Eval("Email") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Telefón">
                <ItemTemplate>
                    <%# Eval("Phone") %>
                </ItemTemplate>
            </asp:TemplateField>
            --%>
            <asp:TemplateField HeaderText="Poznámka">
                <ItemTemplate>
                    <%# Eval("Note") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <%--
    Need help
    <asp:GridView ID="gridNeedHelp" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Meno">
                <ItemTemplate>
                    <%# Eval("FirstName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Priezvisko">
                <ItemTemplate>
                    <%# Eval("LastName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <%# Eval("Email") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Telefón">
                <ItemTemplate>
                    <%# Eval("Phone") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Poznámka">
                <ItemTemplate>
                    <%# Eval("Note") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    --%>
    <br />
    <asp:GridView ID="gridChurches" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="Zbor">
                <ItemTemplate>
                    <%# Eval("Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Počet">
                <ItemTemplate>
                    <%# Eval("Count") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>