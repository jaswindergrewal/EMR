using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProviderService" in both code and config file together.
    public class ProviderService : IProviderService
    {
        ProviderBAL objProviderBAL = new ProviderBAL();
        public void DoWork()
        {
        }

        public List<ProviderViewModel> GetProviderDetails()
        {
            List<ProviderViewModel> lstObj = objProviderBAL.GetProviderDetails();
            return lstObj;
        }


        public bool CheckDuplicateData(string Text, int ID, string Name)
        {
            return objProviderBAL.CheckDuplicateData(Text, ID, Name);
        }
    }
}
