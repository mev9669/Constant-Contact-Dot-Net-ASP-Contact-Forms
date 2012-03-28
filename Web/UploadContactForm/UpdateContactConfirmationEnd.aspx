<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateContactConfirmationEnd.aspx.cs"
    Inherits="UpdateContactConfirmationEnd" MasterPageFile="~/Master.master" Title="Constant Contact - Update Contact [Contact updated successfully]" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterContentPlaceHolder" runat="Server">
    <div>
        <asp:Label ID="lbl1" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="16pt"
            Text="Profile have been updated."></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbl2" runat="server" Text="Thank you for updating." Font-Bold="False"
            Font-Names="Calibri" Font-Size="12pt"></asp:Label></div>
    <br />
    <asp:Label ID="lbl3" runat="server" Text="Please" Font-Names="Calibri"></asp:Label>
    <asp:HyperLink ID="hLink1" runat="server" NavigateUrl="~/Default.aspx" Font-Names="Calibri">CLICK HERE</asp:HyperLink>
    <asp:Label ID="lbl4" runat="server" Text="to return to our main page." Font-Names="Calibri"></asp:Label>
</asp:Content>
