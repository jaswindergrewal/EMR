using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPendingPrescriptionAproveService" in both code and config file together.
    [ServiceContract]
    public interface IPendingPrescriptionAproveService
    {
        [OperationContract]
        PendingPrescriptionAproveViewModel GetPescriptionDetail(int StaffId, int PrescriptionID);

        [OperationContract]
        int UpdatePrescription(int StaffID,int PrescriptionID,string Sig,string Dispenses,string NumbRefills,DateTime DateEntered,string PreNotes);

        [OperationContract]
        int UpdateSuppliments(int StaffID, int PrescriptionID, string Sig, string Dispenses, string NumbRefills, DateTime DateEntered, string PreNotes);

        [OperationContract]
        PendingPrescriptionAproveViewModel GetPescriptionSupDetail(int StaffId, int PrescriptionID);
    }
}
