using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public interface ILabsService
    {
        [OperationContract]
        List<List<Emrdev.ViewModelLayer.LabsViewModel>> GetExternalLabListByPanelId(int? panelId);

        [OperationContract]
        void UpdatePanelForLabList(string labListId, int panelId);

        [OperationContract]
        void SetPanelIdNull(int ExternalListId);
        
        [OperationContract]
        List<Emrdev.ViewModelLayer.ExternalPanelViewModel> SelectAllExternalPanel();

        [OperationContract]
        DataTable GetLabOldChartDetails(int patientId);
        
    }
}
