using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ServiceLayer;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LMCBaseService" in both code and config file together.
    public class LMCBaseService : ILMCBaseService
    {
        LMCBaseBAL objBAL = new LMCBaseBAL();
        public StaffViewModel Get(string UserName)
        {
           return objBAL.Get(UserName);
        }

        public long Count(int StaffID)
        {
            return objBAL.Count(StaffID);
        }
    }
}
