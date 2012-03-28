using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConstantContactBO;
using System.Collections.Generic;
using ConstantContactUtility;
using UploadContactForm.App_Code;

public partial class UpdateContactFullForm : Page
{
    #region Properties
    /// <summary>
    /// Authentication data
    /// </summary>
    private AuthenticationData AuthenticationData
    {
        get
        {
            if (Session["AuthenticationData"] == null)
            {
                Session.Add("AuthenticationData", ConstantContact.AuthenticationData);
            }
            return (AuthenticationData) Session["AuthenticationData"];
        }
    }

    /// <summary>
    /// User Contact List collection
    /// </summary>
    private List<ContactList> List
    {
        get { return (List<ContactList>)ViewState["contactList"]; }
        set { ViewState["contactList"] = value; }
    }

    /// <summary>
    /// Province Data Table
    /// </summary>
    private DataTable ProvinceDataTable
    {
        get { return (DataTable)ViewState["ProvinceTable"]; }
        set { ViewState["ProvinceTable"] = value; }
    }

    /// <summary>
    /// Status of Contact
    /// </summary>
    private ContactStatus ContactStatus
    {
        get { return (ContactStatus) ViewState["StatusOfContactToUpdate"]; }
        set { ViewState["StatusOfContactToUpdate"] = value; }
    }
    #endregion

    #region Event handlers
    /// <summary>
    /// Page Load event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // get user Contact List collection
            List = ConstantContact.GetUserContactListCollection(ClientScript);

            chkListContactLists.DataSource = List;
            chkListContactLists.DataBind();

            ProvinceDataTable = ConstantContact.GetStateCollection();
            dropDownState.DataSource = ProvinceDataTable;
            dropDownState.DataBind();

            dropDownCountry.DataSource = ConstantContact.GetCountryCollection();
            dropDownCountry.DataBind();

            // initialize controls with Contact information
            InitializeContactInformation((string) Session["ContactIdToUpdate"]);
        }

        chkUnsubscribe.Enabled = ContactStatus != ContactStatus.DoNotMail;
    }

    /// <summary>
    /// Initialize controls with Contact information
    /// </summary>
    /// <param name="contactId"></param>
    private void InitializeContactInformation(string contactId)
    {
        Contact contactToUpdate;
        try
        {
            contactToUpdate = Utility.GetContactDetailsById(AuthenticationData, contactId);
        }
        catch (ConstantException ce)
        {
            #region display alert message
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(@"<script language='javascript'>");
            stringBuilder.AppendFormat(@"alert('{0}')", ce.Message);
            stringBuilder.Append(@"</script>");
            ClientScript.RegisterStartupScript(typeof(Page), "AlertMessage", stringBuilder.ToString());
            #endregion

            return;
        }
        ContactStatus = contactToUpdate.Status;
        lblStatus.Text = String.Format(CultureInfo.CurrentCulture, "Contact status: {0}.", ContactStatus);
        if (contactToUpdate.InsertTime.HasValue)
        {
            lblInsert.Text = String.Format(CultureInfo.CurrentCulture, "Insert time: {0}.",
                                           contactToUpdate.InsertTime.Value.ToLocalTime());
        }
        if (contactToUpdate.LastUpdateTime.HasValue)
        {
            lblLastUpdate.Text = String.Format(CultureInfo.CurrentCulture, "Last Update time: {0}.",
                                               contactToUpdate.LastUpdateTime.Value.ToLocalTime());
        }

        #region contact information
        txtEmail.Text = contactToUpdate.EmailAddress;
        txtFirst.Text = contactToUpdate.FirstName;
        txtLast.Text = contactToUpdate.LastName;
        txtMiddle.Text = contactToUpdate.MiddleName;
        txtHome.Text = contactToUpdate.HomePhone;
        txtAddr1.Text = contactToUpdate.AddressLine1;
        txtAddr2.Text = contactToUpdate.AddressLine2;
        txtAddr3.Text = contactToUpdate.AddressLine3;
        txtCity.Text = contactToUpdate.City;
        if (string.IsNullOrEmpty(contactToUpdate.StateCode))
        {
            txtOtherState.Text = contactToUpdate.StateName;
        }
        else
        {
            dropDownState.Text = contactToUpdate.StateCode; 
        }
        txtZip.Text = contactToUpdate.PostalCode;
        txtSubZip.Text = contactToUpdate.SubPostalCode;
        dropDownCountry.Text = contactToUpdate.CountryCode;
        #endregion

        #region company
        txtComp.Text = contactToUpdate.CompanyName;
        txtJob.Text = contactToUpdate.JobTitle;
        txtWork.Text = contactToUpdate.WorkPhone;
        rbtnListEmail.Text = contactToUpdate.EmailType.ToString();
        #endregion

        #region custom fields
        txtCust1.Text = contactToUpdate.CustomField1;
        txtCust2.Text = contactToUpdate.CustomField2;
        txtCust3.Text = contactToUpdate.CustomField3;
        txtCust4.Text = contactToUpdate.CustomField4;
        txtCust5.Text = contactToUpdate.CustomField5;
        txtCust6.Text = contactToUpdate.CustomField6;
        txtCust7.Text = contactToUpdate.CustomField7;
        txtCust8.Text = contactToUpdate.CustomField8;
        txtCust9.Text = contactToUpdate.CustomField9;
        txtCust10.Text = contactToUpdate.CustomField10;
        txtCust11.Text = contactToUpdate.CustomField11;
        txtCust12.Text = contactToUpdate.CustomField12;
        txtCust13.Text = contactToUpdate.CustomField13;
        txtCust14.Text = contactToUpdate.CustomField14;
        txtCust15.Text = contactToUpdate.CustomField15;
        #endregion

        #region Notes
        txtNotes.Text = contactToUpdate.Note;
        #endregion

        #region contact lists
        // loop throught all the checkbox controls
        foreach (ListItem item in chkListContactLists.Items)
        {
            // loop throught the contact List collection
            foreach (ContactOptInList contactOptInList in contactToUpdate.ContactLists)
            {
                if (item.Value == contactOptInList.ContactList.Id)
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Validate form Contact informations
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string errorMessage = string.Empty;

        #region Email Address validation
        if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
        {
            errorMessage = "Please enter the contact email address.";
            args.IsValid = false;
        }

        if (args.IsValid && !Utility.IsEmail(txtEmail.Text.Trim()))
        {
            errorMessage = "Please enter a valid contact email address.";
            args.IsValid = false;
        }
        #endregion

        #region State/Province validation
        if (args.IsValid && !string.IsNullOrEmpty(txtOtherState.Text.Trim()) 
            && !string.IsNullOrEmpty(dropDownState.SelectedValue))
        {
            errorMessage = "Only one State/Province value is allowed, not both.";
            args.IsValid = false;
        }
        #endregion

        #region Country - State/Province validation
        if (args.IsValid 
            && !string.IsNullOrEmpty(dropDownState.SelectedValue)
            && !string.IsNullOrEmpty(dropDownCountry.SelectedValue)
            && (string)ProvinceDataTable.Rows[dropDownState.SelectedIndex]["CountryCode"] != dropDownCountry.SelectedValue)
        {
            errorMessage = "Mismatched State/Province and Country";
            args.IsValid = false;
        }

        if (args.IsValid
            && !string.IsNullOrEmpty(dropDownCountry.SelectedValue)
            && (string.Compare(dropDownCountry.SelectedValue, ConstantContact.UnitedStatesCountryCode, StringComparison.Ordinal) == 0
                || string.Compare(dropDownCountry.SelectedValue, ConstantContact.CanadaCountryCode, StringComparison.Ordinal) == 0)
            && !string.IsNullOrEmpty(txtOtherState.Text.Trim()))
        {
            errorMessage = "For US & Canada, select State/Province from the dropdown list box.";
            args.IsValid = false;
        }
        #endregion

        if (args.IsValid) return;

        #region display alert message
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(@"<script language='javascript'>");
        stringBuilder.AppendFormat(@"alert('{0}')", errorMessage);
        stringBuilder.Append(@"</script>");
        ClientScript.RegisterStartupScript(typeof(Page), "AlertMessage", stringBuilder.ToString());
        #endregion
    }

    /// <summary>
    /// Unsubscribe checked changed event handler.
    /// Enable / disable the Contact List checked list control when user will be unsubscribed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkUnsubscribe_CheckedChanged(object sender, EventArgs e)
    {
        chkListContactLists.Enabled = !chkUnsubscribe.Checked;
    }

    /// <summary>
    /// Update button click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        try
        {
            // update Contact profile
            Utility.UpdateContactFullForm(AuthenticationData, GetContactInformation());

            if (chkUnsubscribe.Checked)
            {
                // remove Contact from all lists and add the user to the Do-Not-Mail list
                Utility.UnsubscribeContact(AuthenticationData, (string)Session["ContactIdToUpdate"]);
            }

            Response.Redirect("~/UpdateContactConfirmationEnd.aspx");
            
        }
        catch (ConstantException ce)
        {
            #region display alert message
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(@"<script language='javascript'>");
            stringBuilder.AppendFormat(@"alert('{0}')", ce.Message);
            stringBuilder.Append(@"</script>");
            ClientScript.RegisterStartupScript(typeof(Page), "AlertMessage", stringBuilder.ToString());
            #endregion
        }
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Get Contact information from the form
    /// </summary>
    /// <returns></returns>
    private Contact GetContactInformation()
    {
        Contact contact = new Contact();
        contact.Id = (string) Session["ContactIdToUpdate"];
        contact.Link = (string)Session["ContactEditLink"];
        contact.Status = ContactStatus;

        #region contact information
        contact.EmailAddress = Server.HtmlEncode(txtEmail.Text.Trim());
        contact.FirstName = Server.HtmlEncode(txtFirst.Text.Trim());
        contact.MiddleName = Server.HtmlEncode(txtMiddle.Text.Trim());
        contact.LastName = Server.HtmlEncode(txtLast.Text.Trim());
        contact.HomePhone = Server.HtmlEncode(txtHome.Text.Trim());
        contact.AddressLine1 = Server.HtmlEncode(txtAddr1.Text.Trim());
        contact.AddressLine2 = Server.HtmlEncode(txtAddr2.Text.Trim());
        contact.AddressLine3 = Server.HtmlEncode(txtAddr3.Text.Trim());
        contact.City = Server.HtmlEncode(txtCity.Text.Trim());
        contact.StateCode = string.IsNullOrEmpty(dropDownState.SelectedValue)
                                ? string.Empty
                                : dropDownState.SelectedValue;
        contact.StateName = string.IsNullOrEmpty(dropDownState.SelectedValue)
                                ? Server.HtmlEncode(txtOtherState.Text.Trim())
                                : dropDownState.SelectedItem.Text;
        contact.PostalCode = Server.HtmlEncode(txtZip.Text.Trim());
        contact.SubPostalCode = Server.HtmlEncode(txtSubZip.Text.Trim());
        contact.CountryName = dropDownCountry.SelectedItem.Text;
        contact.CountryCode = dropDownCountry.SelectedValue;
        #endregion

        #region company
        contact.CompanyName = Server.HtmlEncode(txtComp.Text.Trim());
        contact.JobTitle = Server.HtmlEncode(txtJob.Text.Trim());
        contact.WorkPhone = Server.HtmlEncode(txtWork.Text.Trim());
        contact.EmailType = (ContactEmailType)Enum.Parse(typeof(ContactEmailType), rbtnListEmail.SelectedValue);
        #endregion

        #region custom fields
        contact.CustomField1 = Server.HtmlEncode(txtCust1.Text.Trim());
        contact.CustomField2 = Server.HtmlEncode(txtCust2.Text.Trim());
        contact.CustomField3 = Server.HtmlEncode(txtCust3.Text.Trim());
        contact.CustomField4 = Server.HtmlEncode(txtCust4.Text.Trim());
        contact.CustomField5 = Server.HtmlEncode(txtCust5.Text.Trim());
        contact.CustomField6 = Server.HtmlEncode(txtCust6.Text.Trim());
        contact.CustomField7 = Server.HtmlEncode(txtCust7.Text.Trim());
        contact.CustomField8 = Server.HtmlEncode(txtCust8.Text.Trim());
        contact.CustomField9 = Server.HtmlEncode(txtCust9.Text.Trim());
        contact.CustomField10 = Server.HtmlEncode(txtCust10.Text.Trim());
        contact.CustomField11 = Server.HtmlEncode(txtCust11.Text.Trim());
        contact.CustomField12 = Server.HtmlEncode(txtCust12.Text.Trim());
        contact.CustomField13 = Server.HtmlEncode(txtCust13.Text.Trim());
        contact.CustomField14 = Server.HtmlEncode(txtCust14.Text.Trim());
        contact.CustomField15 = Server.HtmlEncode(txtCust15.Text.Trim());
        #endregion

        #region Notes
        contact.Note = Server.HtmlEncode(txtNotes.Text.Trim());
        #endregion

        #region contact lists
        // loop throught all the checkbox controls
        foreach (ListItem item in chkListContactLists.Items)
        {
            if (!item.Selected) continue;

            ContactOptInList contactOptInList = new ContactOptInList();
            contactOptInList.ContactList = new ContactList(item.Value);
            contact.ContactLists.Add(contactOptInList);
        }
        #endregion

        return contact;
    }
    #endregion
}
