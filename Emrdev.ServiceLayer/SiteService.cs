using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Site" in both code and config file together.
    public class SiteService : ISiteService
    {
        SiteBAL _SiteBAL = new SiteBAL();
        public dynamic GetAllPatients(string prefixText)
        {
            var objPatients=_SiteBAL.GetAllPatients(prefixText);
            return objPatients;

        }
    }
}
