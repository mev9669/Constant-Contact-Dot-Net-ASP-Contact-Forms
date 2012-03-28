<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="UpdateContactSmallForm.aspx.cs"
    Inherits="UpdateContactSmallForm" MasterPageFile="~/Master.master" Title="Constant Contact - Update Contact [Simplified Form]"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterContentPlaceHolder" runat="Server">
    <div style="margin-left: 30px; margin-top: 10px">
        <asp:Label runat="server" ID="lbl1" Text="Update Contact - Simplified Form" Font-Names="Calibri"
            Font-Size="20pt">
        </asp:Label>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Enabled="False" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblLastUpdate" runat="server" Enabled="False" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblInsert" runat="server" Enabled="False" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <table style="margin-top: 10px; vertical-align: top">
        <tr>
            <td style="padding-left: 25px; width: 480px; height: 457px;">
                <table style="border-right: green thin solid; border-top: green thin solid; border-left: green thin solid;
                    border-bottom: green thin solid;">
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="lblEmail" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Email Address:"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txtEmail" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFirst" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="First Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFirst" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLast" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Text="Last Name:"
                                Font-Size="11pt"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLast" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMiddle" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Middle Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMiddle" runat="server" Font-Names="Calibri" Font-Size="11pt"
                                Width="254px" MaxLength="50" TabIndex="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblHome" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="11pt"
                                Text="Home Phone:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtHome" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAddr" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="11pt"
                                Text="Address:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddr1" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddr2" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="7"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddr3" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="8"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCity" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="11pt"
                                Text="City:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCity" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="9"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUSCanadaState" runat="server" Font-Bold="True" Font-Names="Calibri"
                                Font-Size="11pt" Text="State/Province (US/Canada):"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropDownState" runat="server" Width="200px" DataTextField="Name"
                                DataValueField="Code" Font-Names="Calibri" Font-Size="11pt" TabIndex="10">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblOtherState" runat="server" Font-Bold="True" Font-Names="Calibri"
                                Font-Size="11pt" Text="State/Province (Other):"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherState" runat="server" Width="254px" MaxLength="50" Font-Names="Calibri"
                                Font-Size="11pt" TabIndex="11"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblZip" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="11pt"
                                Text="Zip/Postal Code:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtZip" runat="server" Width="254px" MaxLength="25" Font-Names="Calibri"
                                Font-Size="11pt" TabIndex="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubZip" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="11pt"
                                Text="Sub Zip/Postal Code:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubZip" runat="server" Width="254px" MaxLength="25" Font-Names="Calibri"
                                Font-Size="11pt" TabIndex="13"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCountry" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="11pt"
                                Text="Country:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropDownCountry" runat="server" Width="230px" DataTextField="Name"
                                DataValueField="Code" Font-Names="Calibri" Font-Size="11pt" TabIndex="14">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="height: 457px">
                <div>
                    <div style="text-align: center">
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:CheckBox ID="chkUnsubscribe" runat="server" Font-Names="Calibri" OnCheckedChanged="chkUnsubscribe_CheckedChanged"
                                        Text="Unsubscribe me from all lists" AutoPostBack="True" Font-Size="11pt" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblContactList" runat="server" Font-Bold="True" Font-Names="Calibri"
                                        Font-Size="11pt" Width="416px" Text="Please select the areas of interest for which you would like to receive occasional email from us."></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="contactListsPanel" runat="server" BackColor="White" BorderColor="Green"
                        BorderWidth="1px" Height="348px" Width="470px" ScrollBars="Both">
                        <asp:CheckBoxList ID="chkListContactLists" runat="server" Width="300px" DataTextField="Name"
                            DataValueField="Id" CellSpacing="5" Font-Names="Calibri" Font-Size="11pt">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </div>
            </td>
        </tr>                
        <tr>
            <td style="padding-left: 25px; width: 480px; height: 53px;">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Contact" OnClick="btnUpdate_Click"
                                Font-Names="Calibri" Font-Size="11pt" />
                            <asp:CustomValidator ID="customValidator" runat="server" Display="None" OnServerValidate="customValidator_ServerValidate">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="height: 53px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>