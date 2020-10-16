using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public class LabsService : ILabsService
    {
        #region Global

        Emrdev.GeneralClasses.LabsBAL objBAL;

        #endregion

        #region Select ExternalLabLists By PanelId

        public List<List<Emrdev.ViewModelLayer.LabsViewModel>> GetExternalLabListByPanelId(int? panelId)
        {
            objBAL = new GeneralClasses.LabsBAL();
            return objBAL.GetExternalLabListByPanelId(panelId);
        }

        #endregion


        #region Update Panel for ExternalLabList By Id

        public void UpdatePanelForLabList(string labListId, int panelId)
        {
            objBAL = new GeneralClasses.LabsBAL();
            objBAL.UpdatePanelForLabList(labListId, panelId);
        }

        #endregion

        #region Set PanelId Null

        public void SetPanelIdNull(int ExternalListId)
        {
            objBAL = new GeneralClasses.LabsBAL();
            objBAL.SetPanelIdNull(ExternalListId);
        }

        #endregion


        public List<ViewModelLayer.ExternalPanelViewModel> SelectAllExternalPanel()
        {
            objBAL = new GeneralClasses.LabsBAL();
            return objBAL.SelectAllExternalPanel();
        }

        /// <summary>
        /// Get the details for lab old chart details for appointment console page
        /// Jaswinder 8th oct 2013
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public DataTable GetLabOldChartDetails(int patientId)
        {
            //code review point dispose object
            DataTable LabTable = new DataTable();
            LabTable.Locale = CultureInfo.InvariantCulture;
            try
            {
                //do something

                objBAL = new GeneralClasses.LabsBAL();
                LabTable = objBAL.GetLabOldChartDetails(patientId);
                return LabTable;
            }
            finally
            {
                if (LabTable != null)
                {
                   ((IDisposable)LabTable).Dispose();
                  
                }
            }
            
            
        }
    }
}
