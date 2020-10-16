using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;


namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AddProspectService" in both code and config file together.
    public class SeminarScheduleService : ISeminarScheduleService
    {
        SeminarScheduleBAL obj = new SeminarScheduleBAL();

        public List<PostSeminarAppointment> GetPostSeminarAppointment(DateTime StartDate, string Clinic)
        {
            return obj.GetPostSeminarAppointment(StartDate, Clinic);
        }
        public int InsertUpdateProspects(int ProspectID, string Address, string AltPhone, string City, string ContactMethod,
                                               string Email, string FirstName, bool Flagged, string LastName, string MainPhone, string MarketingSources,
                                               string Notes, string State, int StatusID, string Zip, int UserName, int EventId)
        {
           return obj.InsertUpdateProspect(ProspectID, Address, AltPhone, City, ContactMethod,
                                         Email, FirstName, Flagged, LastName, MainPhone, MarketingSources,
                                         Notes, State, StatusID, Zip, UserName, EventId);
        }
       
    }
}
