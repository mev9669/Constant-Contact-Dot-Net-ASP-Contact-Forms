<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ListContacts.aspx.cs"
    Inherits="ListContacts" Title="Constant Contact - List all Contacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterContentPlaceHolder" runat="Server">
    <div style="margin-left: 30px; margin-top: 10px">
        <asp:Label runat="server" ID="lbl1" Text="List all Contacts" Font-Names="Calibri"
            Font-Size="20pt">
        </asp:Label>
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                CellPadding="0" ForeColor="Black" OnPageIndexChanging="GridView1_PageIndexChanging"
                OnRowDeleting="GridView1_RowDeleting" Width="712px" Font-Names="Calibri" Font-Size="11pt"
                OnRowDataBound="GridView1_RowDataBound" PageSize="50" OnRowEditing="GridView1_RowEditing">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <b>
                                <asp:Label ID="lblNrCrt" runat="server"></asp:Label>
                            </b>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="[Edit]"></asp:LinkButton>                            
                            <asp:LinkButton ID="lnkBtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="[Remove]"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:LinkButton ID="LinkFirst" OnClick="LinkFirst_Click" runat="server">First</asp:LinkButton>
            <asp:LinkButton ID="LinkNext" OnClick="LinkNext_Click" runat="server">Next</asp:LinkButton>
        </asp:Panel>
    </div>
</asp:Content>
