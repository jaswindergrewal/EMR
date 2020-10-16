using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SpecialAttentionService" in both code and config file together.
    public class SpecialAttentionService : ISpecialAttentionService
    {
        SpecialAttentionBAL objBAL = new SpecialAttentionBAL();
        public List<SpecialAttentionViewModel >GetSpecialAttentionByPatientId(int PatientID)
        {
            return objBAL.GetSpecialAttentionByPatientId(PatientID);
        }

        public void DeleteSpecialAttentionFlag(int SpecialAttentionID)
        {
             objBAL.DeleteSpecialAttentionFlag(SpecialAttentionID);
        }

        public void AddSpecialAttentionByPatientId(int PatientID, string Content, int StaffId)
        {
            objBAL.AddSpecialAttentionByPatientId(PatientID, Content, StaffId);
        }

        public long GetSpecialAttentionCount(int PatientID)
        {

           return objBAL.GetSpecialAttentionCount(PatientID);
        }
    }
}
