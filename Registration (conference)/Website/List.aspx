<%@ Page Language="C#" MasterPageFile="~/MasterPageLocal.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="List" %>

<asp:Content ID="Content" ContentPlaceHolderID="Body" Runat="Server">
    <table>
        <tr>
            <td>Rozsah</td>
            <td>
                <asp:TextBox runat="server" ID="txtFrom" AutoPostBack="true" OnTextChanged="txtFrom_TextChanged" Width="20px"/>
                &nbsp;-&nbsp;
                <asp:TextBox runat="server" ID="txtTo" AutoPostBack="true" OnTextChanged="txtTo_TextChanged" Width="20px"/>
            </td>
        </tr>
        <tr>
            <td>Meno</td>
            <td>
                <asp:TextBox runat="server" ID="txtName" AutoPostBack="true" OnTextChanged="txtName_TextChanged" Width="300px"/>
            </td>
            <td>
                <asp:Button runat="server" ID="btnClearName" Text="Všetky" OnClick="btnClearName_Click" />
            </td>
        </tr>
        <tr>
            <td>Zbor</td>
            <td>
                <asp:DropDownList runat="server" ID="ddlChurch" AutoPostBack="true" OnTextChanged="ddlChurch_TextChanged"/>
            </td>
            <td>
                <asp:Button runat="server" ID="btnClearChurch" Text="Všetky" OnClick="btnClearChurch_Click" />
            </td>
        </tr>
        <tr>
            <td>Neprišli</td>
            <td>
                <asp:CheckBox runat="server" ID="chbNotArrived" AutoPostBack="true" OnCheckedChanged="chbNotArrived_CheckedChanged"/>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView runat="server" ID="gridResults"  
        AllowPaging="False"
        AllowSorting="False" 
        PageSize="20"
        AutoGenerateColumns="false" 
        OnRowCommand="gridResults_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Meno" SortExpression="Meno">
                <ItemTemplate>
                    <a href='<%# String.Format("/Detail.aspx?id={0}", Eval("Id")) %>' target="new"><%# Eval("Meno") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Priezvisko" SortExpression="Priezvisko">
                <ItemTemplate>
                    <a href='<%# String.Format("/Detail.aspx?id={0}", Eval("Id")) %>' target="new"><%# Eval("Priezvisko") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <%--
            <asp:TemplateField HeaderText="Zbor" SortExpression="Zbor">
                <ItemTemplate>
                    <%# Eval("Zbor") %>
                </ItemTemplate>
            </asp:TemplateField>
            --%>
            <asp:TemplateField HeaderText="Tričko" SortExpression="Tricko">
                <ItemTemplate>
                    <%# Eval("Tricko") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Visačka" SortExpression="Id">
                <ItemTemplate>
                    <%# Eval("Id") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Preplatok" SortExpression="Preplatok">
                <ItemTemplate>
                    <asp:Label ID="lblSuma" runat="server" Text='<%# CashBackFormat(Eval("Preplatok")) %>' CssClass='<%# CashBackClass(Eval("Preplatok")) %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Akcia">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hdnIdUser" Value='<%# Eval("Id") %>'/>
                    <asp:HiddenField runat="server" ID="hdnIdVariabilny" Value='<%# Eval("IdVariabilny") %>'/>
                    <asp:HiddenField runat="server" ID="hdnAmount" Value='<%# Eval("Preplatok") %>'/>
                    <asp:Button runat="server" ID="btnDoplatil"  
                        Visible='<%# (float)Eval("Preplatok") < 0 %>' 
                        Text='<%# string.Format("Doplatili {0:0.00} euro", -(float)Eval("Preplatok")) %>'
                        CommandName="Doplatili" 
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    <asp:Button ID="btnVratili" runat="server" 
                        Visible='<%# (float)Eval("Preplatok") > 0 %>' 
                        Text='<%# string.Format("Vrátili sme {0:0.00} euro", Eval("Preplatok")) %>' 
                        CommandName="Vratili" 
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    <asp:Button ID="btnDaroval" runat="server" 
                        Visible='<%# (float)Eval("Preplatok") > 0 %>' 
                        Text='<%# string.Format("Darovali nám {0:0.00} euro", Eval("Preplatok")) %>' 
                        CommandName='Darovali' 
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    <asp:Button runat="server" ID="btnPrisli"
                        Visible='<%# !(bool)Eval("Prisli") %>' 
                        Text='Prišli'
                        CommandName='Prisli' 
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
