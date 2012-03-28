<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="SearchContact.aspx.cs"
    Inherits="SearchContact" Title="Constant Contact - Search Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterContentPlaceHolder" runat="Server">
    <div style="margin-left: 30px; margin-top: 10px">
        <asp:Label runat="server" ID="lbl1" Text="Enter Contact Email Address" Font-Names="Calibri" Font-Size="20pt"></asp:Label>
    </div>
    <table style="margin-top: 10px; vertical-align: top">
        <tr>
            <td style="padding-left: 25px">
                <asp:Label ID="lbl2" runat="server" Text="E-mail address:" Font-Bold="True" Font-Names="Calibri"
                    Font-Size="11pt">
                </asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Font-Names="Calibri" Font-Size="11pt">
                </asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Sign Up" Font-Bold="False" Font-Names="Calibri"
                    Font-Size="11pt" OnClick="btnSearch_Click" />
                <asp:CustomValidator ID="customValidator" runat="server" Display="None" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator></td>
        </tr>
    </table>
</asp:Content>
