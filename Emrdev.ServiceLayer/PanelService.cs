using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public class PanelService : IPanelService
    {
        #region Global

        Emrdev.GeneralClasses.PanelBAL objBAL;

        #endregion


        #region Select All External Panel(s)

        public List<ViewModelLayer.ExternalPanelViewModel> SelectAllPanel()
        {
            objBAL = new GeneralClasses.PanelBAL();
            return objBAL.SelectAllPanel();
        }

        #endregion


        #region Delete Selected Panel

        public void DeletePanel(int panelId)
        {
            objBAL = new GeneralClasses.PanelBAL();
            objBAL.DeletePanel(panelId);
        }

        #endregion

        #region Update Panel

        public void UpdatePanel(int panelId, string panelName)
        {
            objBAL = new GeneralClasses.PanelBAL();
            objBAL.UpdatePanel(panelId, panelName);
        }

        #endregion


        #region Insert Panel

        public void InsertPanel(string panelName)
        {
            objBAL = new GeneralClasses.PanelBAL();
            objBAL.InsertPanel(panelName);
        }

        #endregion
    }
}
