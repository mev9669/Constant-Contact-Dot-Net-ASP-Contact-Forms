using System;
using System.Text;
using System.Web.UI;
using ConstantContactUtility;
using UploadContactForm.App_Code;

public partial class Default : Page
{
    #region Event handlers
    /// <summary>
    /// Page load event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        try
        {
            // authenticate
            AuthenticationData authenticationData = ConstantContact.AuthenticationData;

            // check if username, password and API Key are valid
            Utility.IsValidUserAuthentication(authenticationData);

            // successfuly
            // save the authentication data in the Session to be able to access resources
            Session.Add("AuthenticationData", authenticationData);
        }
        catch (ConstantAuthenticationException cae)
        {
            #region display alert message

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(@"<script language='javascript'>");
            stringBuilder.AppendFormat(@"alert('{0}')", cae.Message);
            stringBuilder.Append(@"</script>");
            ClientScript.RegisterStartupScript(typeof(Page), "AlertMessage", stringBuilder.ToString());

            #endregion

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
}
