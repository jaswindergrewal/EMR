using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.GeneralClasses
{
    public class CRMdashboardBAL
    {
        CRMdashboardDAL objDAL = new CRMdashboardDAL();
        public List<PlottGraphViewModel> GetCampaignGraph(int CampaignID)
        {
            return objDAL.GetCampaignGraph(CampaignID);
        }

        public CRMStatisticViewModel GetCRMStatisticData()
        {
            return objDAL.GetCRMStatisticData();
        }

        public List<CRMStatisticViewModel> GetEventGraph(int EventID)
        {
            return objDAL.GetEventGraph(EventID);
        }
    
    }
}
