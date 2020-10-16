using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;


namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOfficeVisitService" in both code and config file together.
    [ServiceContract]
    public interface IOfficeVisitService
    {
        [OperationContract]
        List<Sympt> GetSymptom(int AptID);

        [OperationContract]
        List<Sympt> GetGoalItem(int AptID);

        [OperationContract]
        List<Sympt> GetApt_Symptoms(int AptID);

        [OperationContract]
        List<Sympt> GetApt_Goals(int AptID);

        [OperationContract]
        List<OfficeVisitViewModel> GetOfficeVisitDetails(int AptID);

        [OperationContract]
        void InsetOfficeVisit(apt_BloodWork bloodWork, apt_LisfeStyle life, apt_Misc misc);

        [OperationContract]
        void InsertSymptoms(List<apt_Symtpoms> symptom, int AptID);
        [OperationContract]
         //void InsertGoals(apt_Goals Goals);


        void InsertGoals(List<apt_Goals> Goals, int AptID);

        
    }
}
