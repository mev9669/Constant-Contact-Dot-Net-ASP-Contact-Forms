using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using ConstantContactBO;
using ConstantContactUtility;
using UploadContactForm.App_Code;

public partial class SearchContact : Page
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
    #endregion

    #region Event handler
    /// <summary>
    /// Page load event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Search button click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        string[] emailAddress = new string[] {txtEmail.Text.Trim()};
        try
        {
            string nextChunkId;
            // search Contact by email
            IList<Contact> list = Utility.SearchContactByEmail(AuthenticationData, emailAddress, out nextChunkId);
            if (list.Count != 0)
            {
                // save Contact Id to be updated into Session
                Session.Add("ContactIdToUpdate", list[0].Id);
                Session.Add("ContactEditLink", list[0].Link);
                // redirect to update Contact
                Response.Redirect("~/UpdateContactSmallForm.aspx");
            }
            else
            {
                // save the Contact E-mail Address
                Session.Add("NewContactEmailAddress", txtEmail.Text.Trim());
                // redirect to add Contact
                Response.Redirect("~/AddContactSmallForm.aspx");
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
    /// Validate user provided information
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void customValidator_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
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
}
