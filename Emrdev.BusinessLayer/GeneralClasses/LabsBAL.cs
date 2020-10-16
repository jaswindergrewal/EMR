using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.GeneralClasses
{

    public class LabsBAL
    {
        #region Global

        Emrdev.DataLayer.GeneralClasses.LabsDAL objDAL;

        #endregion

        #region Select ExternalLabLists By PanelId

        public List<List<Emrdev.ViewModelLayer.LabsViewModel>> GetExternalLabListByPanelId(int? panelId)
        {
            objDAL = new DataLayer.GeneralClasses.LabsDAL();
            return objDAL.GetExternalLabListByPanelId(panelId);
        }

        #endregion

        #region Update Panel for ExternalLabList By Id

        public void UpdatePanelForLabList(string labListId, int panelId)
        {
            objDAL = new DataLayer.GeneralClasses.LabsDAL();
            objDAL.UpdatePanelForLabList(labListId, panelId);
        }

        #endregion

        #region Set PanelId Null

        public void SetPanelIdNull(int ExternalListId)
        {
            objDAL = new DataLayer.GeneralClasses.LabsDAL();
            objDAL.SetPanelIdNull(ExternalListId);
        }

        #endregion

        #region Select External Panel List

        public List<Emrdev.ViewModelLayer.ExternalPanelViewModel> SelectAllExternalPanel()
        {
            objDAL = new DataLayer.GeneralClasses.LabsDAL();
            return objDAL.SelectAllExternalPanel();
        }

        #endregion

        /// <summary>
        /// Get the details for lab old chart details for appointment console page
        /// Jaswinder 8th oct 2013
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public DataTable GetLabOldChartDetails(int patientId)
        {
            objDAL = new DataLayer.GeneralClasses.LabsDAL();
            DataTable LabTable = new DataTable();
            LabTable = objDAL.GetLabOldChartDetails(patientId);
            return LabTable;
        }


    }
}
