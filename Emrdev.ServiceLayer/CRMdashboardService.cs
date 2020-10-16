using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CRMdashboardService" in both code and config file together.
    public class CRMdashboardService : ICRMdashboardService
    {
        CRMdashboardBAL objBAL = new CRMdashboardBAL();
        public List<PlottGraphViewModel> GetCampaignGraph(int CampaignID)
        {
            return objBAL.GetCampaignGraph(CampaignID);
        }

        public CRMStatisticViewModel GetCRMStatisticData()
        {
            return objBAL.GetCRMStatisticData();
        }

        public List<CRMStatisticViewModel> GetEventGraph(int EventID)
        {
            return objBAL.GetEventGraph(EventID);
        }
    }
}
