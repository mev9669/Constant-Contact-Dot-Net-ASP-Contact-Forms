using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConstantContactBO;
using ConstantContactUtility;
using UploadContactForm.App_Code;

public partial class AddContactFullForm : Page
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
    /// State/Province data table
    /// </summary>
    private DataTable ProvinceDataTable
    {
        get { return (DataTable) ViewState["ProvinceTable"]; }
        set { ViewState["ProvinceTable"] = value; }
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
        if (IsPostBack) return;

        // get user Contact List collection
        List = ConstantContact.GetUserContactListCollection(ClientScript);

        chkListContactLists.DataSource = List;
        chkListContactLists.DataBind();

        ProvinceDataTable = ConstantContact.GetStateCollection();
        dropDownState.DataSource = ProvinceDataTable;
        dropDownState.DataBind();

        dropDownCountry.DataSource = ConstantContact.GetCountryCollection();
        dropDownCountry.DataBind();
    }

    /// <summary>
    /// Add a new Contact
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        
        string[] emailAddress = new string[] {txtEmail.Text.Trim()};
        try
        {
            string nextChunkId;
            IList<Contact> list = Utility.SearchContactByEmail(AuthenticationData, emailAddress, out nextChunkId);
            if (list.Count == 0)
            {
                // create new Contact
                Contact contact = GetContactInformation();


                Utility.CreateNewContact(AuthenticationData, contact);
                Response.Redirect("~/AddContactConfirmation.aspx");
            }
            else
            {
                throw new ConstantException(String.Format(CultureInfo.CurrentCulture,
                    "Email address \"{0}\" is already a contact", txtEmail.Text.Trim()));
            }
        }
        catch (ConstantException ce)
        {
            #region display alert message
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(@"<script language='javascript'>");
            stringBuilder.AppendFormat(@"alert('{0}')", ce.Message);
            stringBuilder.Append(@"</script>");
            ClientScript.RegisterStartupScript(typeof (Page), "AlertMessage", stringBuilder.ToString());
            #endregion
        }
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

        #region Contact Lists validation
        if (args.IsValid)
        {
            bool selected = false;
            foreach (ListItem item in chkListContactLists.Items)
            {
                if (item.Selected)
                {
                    selected = true;
                    break;
                }
            }
            if (!selected)
            {
                errorMessage = "Please select the list to which your contact will be added.";
            }
            args.IsValid = selected;
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
    #endregion

    #region Private methods
    /// <summary>
    /// Get Contact information from the form
    /// </summary>
    /// <returns></returns>
    private Contact GetContactInformation()
    {
        Contact contact = new Contact();

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
