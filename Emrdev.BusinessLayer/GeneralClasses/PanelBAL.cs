using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.GeneralClasses
{

    public class PanelBAL
    {
        #region Global

        Emrdev.DataLayer.GeneralClasses.PanelDAL objDAL;

        #endregion


        #region Select All External Panel(s)

        public List<Emrdev.ViewModelLayer.ExternalPanelViewModel> SelectAllPanel()
        {
            objDAL = new DataLayer.GeneralClasses.PanelDAL();
            return objDAL.SelectAllPanel();
        }

        #endregion

        #region Delete Selected Panel

        public void DeletePanel(int panelId)
        {
            objDAL = new DataLayer.GeneralClasses.PanelDAL();
            objDAL.DeletePanel(panelId);
        }

        #endregion


        #region Update Panel

        public void UpdatePanel(int panelId, string panelName)
        {
            objDAL = new DataLayer.GeneralClasses.PanelDAL();
            objDAL.UpdatePanel(panelId, panelName);
        }

        #endregion


        #region Insert Panel

        public void InsertPanel(string panelName)
        {
            objDAL = new DataLayer.GeneralClasses.PanelDAL();
            objDAL.InsertPanel(panelName);
        }

        #endregion

    }
}
