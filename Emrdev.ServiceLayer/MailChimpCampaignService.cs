using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;
using System.Collections.Generic;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RenewalPackagesService" in both code and config file together.
    public class MailChimpCampaignService : IMailChimpCampaignService
    {
        MailChimpCampaignBAL objService = new MailChimpCampaignBAL();

        
        public MailChimpCampaignViewModel GetMalChimpCampaign( )
        {
            return objService.GetMalChimpCampaign();
        }

      
        public void SaveMailChimpCampaign(MailChimpCampaignViewModel MailChimpCampaign)
        {
            objService.SaveMailChimpCampaign(MailChimpCampaign);
        }

     
    }
}
