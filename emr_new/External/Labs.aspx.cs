using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using Obout.ComboBox;
using System.Configuration;
using System.Collections;
using System.Text;

public partial class External_Labs : LMCBase
{
    #region Global

    int PanelId=0;
    Emrdev.ServiceLayer.LabsService objService;

    #endregion


    #region Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        BindPanel();
        PopulateGrids();
    }

    #endregion


    #region Populate Assigned and Avail List Grid(s)

    protected void PopulateGrids()
    {
        int.TryParse(cboPanels.SelectedValue, out PanelId);
        if (PanelId > 0)
        {
            objService = new Emrdev.ServiceLayer.LabsService();
            List<List<Emrdev.ViewModelLayer.LabsViewModel>> lstExternalLab=objService.GetExternalLabListByPanelId(PanelId);
            //Binding Assigned List
            grdAssigned.DataSource = lstExternalLab.First();
            grdAssigned.DataBind();
            //Binding Available List
            grdAvailable.DataSource = lstExternalLab.Last();
            grdAvailable.DataBind();
        }
    }

    #endregion


    #region Methdods to called by Events

    protected void btnGoClick(object sender, EventArgs e)
    {
        PopulateGrids();
    }

    #endregion


    #region Assign New

    protected void btnAssignClick(object sender, EventArgs e)
    {
        objService = new Emrdev.ServiceLayer.LabsService();
        int.TryParse(cboPanels.SelectedValue, out PanelId);
        if (PanelId > 0)
        {
            ArrayList items = grdAvailable.SelectedRecords;
            List<string> lstExternalIds=new List<string>();
            StringBuilder appendedIds=new StringBuilder();
            string lst=string.Empty;
            try
            {
                if (items != null)
                {
                    int i=0;
                    foreach (Hashtable item in items)
                    {
                        if (i != 0)
                        {
                            //Skip For First Element
                            appendedIds.Append(",");
                        }
                        appendedIds.Append(item["ExternalLabListID"].ToString());
                        i++;
                    }
                    objService.UpdatePanelForLabList(appendedIds.ToString(), PanelId);
                }
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
            }
        }
        PopulateGrids();
    }

    #endregion


    #region Set PanleId Null

    protected void DeleteRecord(object sender, GridRecordEventArgs e)
    {
        if (cboPanels.SelectedValue != String.Empty)
        {
            try
            {
                objService = new Emrdev.ServiceLayer.LabsService();
                int extrenalListId=int.Parse(e.Record["ExternalLabListID"].ToString());
                objService.SetPanelIdNull(extrenalListId);
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
            }
        }
        PopulateGrids();
    }

    #endregion


    #region Bind Panel Dropdown List

    void BindPanel()
    {
        objService = new Emrdev.ServiceLayer.LabsService();
        cboPanels.DataSource = objService.SelectAllExternalPanel();
        cboPanels.DataBind();
    }

    #endregion

}