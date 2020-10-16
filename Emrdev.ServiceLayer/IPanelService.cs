using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public interface IPanelService
    {
        [OperationContract]
        List<Emrdev.ViewModelLayer.ExternalPanelViewModel> SelectAllPanel();

        [OperationContract]
        void DeletePanel(int panelId);

        [OperationContract]
        void UpdatePanel(int panelId, string panelName);

        [OperationContract]
        void InsertPanel(string panelName);
    }
}
