using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using Obout.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class ICD10Code : LMCBase
{
    IProtocolService _objService = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindICDCodes();
    }

    private void BindICDCodes()
    {
        try
        {
             _objService = new ProtocolService();
            List<ICD10CodesViewmodel> ICDCode = _objService.GetIcd10Codes().ToList();
            grdIcdCodes.DataSource = ICDCode;
            grdIcdCodes.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }
    protected void grdIcdCodes_Rebind(object sender, EventArgs e)
    {
        BindICDCodes();
    }

    protected void grdIcdCodes_InsertCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        _objService = new ProtocolService();
        try
        {
            ICD10CodesViewmodel IcdCode = new ICD10CodesViewmodel();
            IcdCode.Id = 0;
            IcdCode.IsActive = false;
            if (e.Record["Id"].ToString() != "")
            {
                IcdCode.Id = int.Parse(e.Record["Id"].ToString());
            }
            
                IcdCode.Description = (e.Record["Description"].ToString());
            
            if (e.Record["ICD10Code"].ToString() != "")
            {
                IcdCode.ICD10Code = (e.Record["ICD10Code"].ToString());
            }

            if (e.Record["IsActive"].ToString() != "")
            {
                IcdCode.IsActive = bool.Parse(e.Record["IsActive"].ToString());
            }

            if (e.Record["Gender"].ToString() != "")
            {
                IcdCode.Gender = (e.Record["Gender"].ToString());
            }
           
            _objService.InserUpdateIcd10Codes(IcdCode);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }
}