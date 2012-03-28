<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddContactConfirmation.aspx.cs"
    Inherits="AddContactConfirmation" MasterPageFile="~/Master.master" Title="Constant Contact - Upload Forms [Contact created successfully]" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterContentPlaceHolder" runat="Server">
    <div>
        <asp:Label ID="lbl1" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="16pt"
            Text="You have been added to our list."></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbl2" runat="server" Text="Thank you for subscribing." Font-Bold="False"
            Font-Names="Calibri" Font-Size="12pt"></asp:Label></div>
    <br />
    <asp:Label ID="lbl3" runat="server" Text="Please" Font-Names="Calibri"></asp:Label>
    <asp:HyperLink ID="hLink1" runat="server" NavigateUrl="~/Default.aspx" Font-Names="Calibri">CLICK HERE</asp:HyperLink>
    <asp:Label ID="lbl4" runat="server" Text="to return to our main page." Font-Names="Calibri"></asp:Label>
</asp:Content>
