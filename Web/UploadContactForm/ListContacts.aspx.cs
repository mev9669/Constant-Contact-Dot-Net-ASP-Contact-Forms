using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConstantContactBO;
using ConstantContactUtility;
using UploadContactForm.App_Code;

public partial class ListContacts : Page
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
    /// Collection of Contacts
    /// </summary>
    private IList<Contact> List
    {
        get { return (IList<Contact>)ViewState["contacts"]; }
        set { ViewState["contacts"] = value; }
    }

    /// <summary>
    /// Chunk index.
    /// Stored to be able to write the Contact count number correctly into the grid
    /// </summary>
    private long ChunkIndex
    {
        get
        {
            object index = ViewState["chunkIndex"];
            return index != null ? (long) index : 1;
        }
        set { ViewState["chunkIndex"] = value;}
    }

    /// <summary>
    /// Current chunk of data
    /// </summary>
    private string CurrentChunk
    {
        get { return (string)ViewState["currentChunk"]; }
        set { ViewState["currentChunk"] = value; }
    }

    /// <summary>
    /// Next chunk of data
    /// </summary>
    private string NextChunk
    {
        get { return (string)ViewState["nextChunk"]; }
        set { ViewState["nextChunk"] = value; }
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
        if (!IsPostBack)
        {
            RefreshData();
        }
    }

    /// <summary>
    /// Grid row deleting event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex + (GridView1.PageIndex*GridView1.PageSize) >= List.Count) return;

        try
        {
            Utility.RemoveContactFromAllLists(AuthenticationData,
                                              List[e.RowIndex + (GridView1.PageIndex*GridView1.PageSize)].Id);

            // refresh Contacts
            RefreshData();
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
    /// Grid row editing event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session.Add("ContactIdToUpdate", List[e.NewEditIndex + (GridView1.PageIndex*GridView1.PageSize)].Id);
        Session.Add("ContactEditLink", List[e.NewEditIndex + (GridView1.PageIndex*GridView1.PageSize)].Link);
        Response.Redirect("~/UpdateContactFullForm.aspx");
    }

    /// <summary>
    /// Grid page index changing event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        GridView1.SelectedIndex = -1;
        GridView1.DataSource = List;
        GridView1.DataBind();
    }

    /// <summary>
    /// Grid row data bound event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // update the Contact count index
        Label lblNrCrt = (Label) e.Row.FindControl("lblNrCrt");
        if (null != lblNrCrt)
        {
            lblNrCrt.Text = (ChunkIndex + e.Row.RowIndex + (GridView1.PageIndex*GridView1.PageSize))
                .ToString(CultureInfo.CurrentCulture);
        }

        // update the delete link
        LinkButton deleteLink = (LinkButton)e.Row.FindControl("lnkBtnDelete");
        if (null == deleteLink) return;

        Contact contact = List[e.Row.RowIndex + (GridView1.PageIndex*GridView1.PageSize)];
        deleteLink.Enabled = contact.Status == ContactStatus.Active;
    }

    /// <summary>
    /// Next link control click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkNext_Click(object sender, EventArgs e)
    {
        CurrentChunk = NextChunk;
        NextChunk = null;
        ChunkIndex += 50;

        RefreshData();
    }

    /// <summary>
    /// First link control click event handler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkFirst_Click(object sender, EventArgs e)
    {
        CurrentChunk = null;
        NextChunk = null;
        ChunkIndex = 1;

        RefreshData();
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Refresh data
    /// </summary>
    private void RefreshData()
    {
        GridView1.SelectedIndex = -1;
        GetContactCollection();
    }

    /// <summary>
    /// Get collection of Contacts from the server
    /// </summary>
    private void GetContactCollection()
    {
        try
        {
            string nextChunk;
            List = string.IsNullOrEmpty(CurrentChunk)
                       ? Utility.GetContactCollection(AuthenticationData, out nextChunk)
                       : Utility.GetContactCollection(AuthenticationData, CurrentChunk, out nextChunk);
            NextChunk = nextChunk;

            // update chunk links
            UpdateChunkLinks();

            GridView1.DataSource = List;
            GridView1.DataBind();
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
    /// Update chunk links controls
    /// </summary>
    private void UpdateChunkLinks()
    {
        // enable or disable getting next chunk of data
        LinkNext.Enabled = !string.IsNullOrEmpty(NextChunk);
    }
    #endregion
}
