using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer.GeneralClasses;


namespace Emrdev.GeneralClasses
{
    public class MailChimpCampaignBAL
    {
        MailChimpCampaignDAL objBAL = new MailChimpCampaignDAL();

        public MailChimpCampaignViewModel GetMalChimpCampaign()
        {
            return objBAL.GetMalChimpCampaign();
        }


        public void SaveMailChimpCampaign(MailChimpCampaignViewModel MailChimpCampaign)
        {
            objBAL.SaveMailChimpCampaign(MailChimpCampaign);
        }
    }
}
