<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="UpdateContactFullForm.aspx.cs"
    Inherits="UpdateContactFullForm" MasterPageFile="~/Master.master" Title="Constant Contact - Update Contact [Full Form]" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterContentPlaceHolder" runat="Server">
    <div style="margin-left: 30px; margin-top: 10px">
        <asp:Label runat="server" ID="lbl1" Text="Update Contact - Full Form" Font-Names="Calibri"
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
            <td style="padding-left: 25px; width: 480px">
                <table style="border-right: green thin solid; border-top: green thin solid; border-left: green thin solid;
                    border-bottom: green thin solid;">
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="lblCompany" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Company Name:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtComp" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="15">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblJob" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Job Title:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtJob" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="16">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblWork" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Work Phone:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWork" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="17">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblEmailFormat" runat="server" Font-Bold="True" Font-Italic="False"
                                Font-Names="Calibri" Font-Overline="False" Font-Size="11pt" Font-Strikeout="False"
                                Font-Underline="False" Text="Email Format:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbtnListEmail" runat="server" RepeatDirection="Horizontal"
                                Font-Names="Calibri" Font-Size="11pt" TabIndex="18">
                                <asp:ListItem Selected="True" Value="Html">HTML</asp:ListItem>
                                <asp:ListItem Value="Text">Text only</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 474px">
                <table style="border-right: green thin solid; border-top: green thin solid; border-left: green thin solid;
                    border-bottom: green thin solid">
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="lblNote" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Notes:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotes" runat="server" Font-Names="Calibri" Font-Size="11pt" TextMode="MultiLine"
                                Width="353px" MaxLength="500" TabIndex="19" Height="109px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 25px; width: 480px; height: 250px;">
                <table style="border-right: green thin solid; border-top: green thin solid; border-left: green thin solid;
                    border-bottom: green thin solid;">
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="lblCust1" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 1:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust1" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="20">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust2" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 2:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust2" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="21">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust3" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 3:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust3" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="22">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust4" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 4:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust4" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="23">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust5" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 5:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust5" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="24">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust6" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 6:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust6" runat="server" Width="254px" MaxLength="50" Font-Names="Calibri"
                                Font-Size="11pt" TabIndex="25">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust7" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 7:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust7" runat="server" Width="254px" MaxLength="50" Font-Names="Calibri"
                                Font-Size="11pt" TabIndex="26">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust8" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 8:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust8" runat="server" Width="254px" MaxLength="50" Font-Names="Calibri"
                                Font-Size="11pt" TabIndex="27">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="height: 250px; width: 474px;">
                <table style="border-right: green thin solid; border-top: green thin solid; border-left: green thin solid;
                    border-bottom: green thin solid;">
                    <tr>
                        <td style="width: 200px">
                            <asp:Label ID="lblCust9" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 9:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust9" runat="server" Font-Names="Calibri" Font-Size="11pt" Width="254px"
                                MaxLength="50" TabIndex="28">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust10" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 10:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust10" runat="server" Font-Names="Calibri" Font-Size="11pt"
                                Width="254px" MaxLength="50" TabIndex="29">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust11" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 11:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust11" runat="server" Font-Names="Calibri" Font-Size="11pt"
                                Width="254px" MaxLength="50" TabIndex="30">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust12" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 12:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust12" runat="server" Font-Names="Calibri" Font-Size="11pt"
                                Width="254px" MaxLength="50" TabIndex="31">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust13" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 13:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust13" runat="server" Font-Names="Calibri" Font-Size="11pt"
                                Width="254px" MaxLength="50" TabIndex="32">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust14" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 14:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust14" runat="server" Width="254px" MaxLength="50" Font-Names="Calibri"
                                Font-Size="11pt" TabIndex="33">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCust15" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Calibri"
                                Font-Overline="False" Font-Size="11pt" Font-Strikeout="False" Font-Underline="False"
                                Text="Custom field 15:">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust15" runat="server" Width="254px" MaxLength="50" Font-Names="Calibri"
                                Font-Size="11pt" TabIndex="34">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 26px">
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
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
