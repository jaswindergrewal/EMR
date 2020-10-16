using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;



namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAddProspectService" in both code and config file together.
    [ServiceContract]
    public interface ISeminarScheduleService
    {
        [OperationContract]
        List<PostSeminarAppointment> GetPostSeminarAppointment(DateTime StartDate,string Clinic);

        [OperationContract]
        int InsertUpdateProspects(int ProspectID, string Address, string AltPhone, string City, string ContactMethod,
                                        string Email, string FirstName, bool Flagged, string LastName, string MainPhone, string MarketingSources,
                                        string Notes, string State, int StatusID, string Zip, int UserName, int EventId);
    }
}
