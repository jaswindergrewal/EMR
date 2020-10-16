using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;


namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class SeminarScheduleBAL
    {
        SeminarScheduleDAL obj = new SeminarScheduleDAL();


        public List<PostSeminarAppointment> GetPostSeminarAppointment(DateTime StartDate,string Clinic)
        {
            return obj.GetPostSeminarAppointment(StartDate,Clinic);
        }

        public int InsertUpdateProspect(int ProspectID, string Address, string AltPhone, string City, string ContactMethod,
                                        string Email, string FirstName, bool Flagged, string LastName, string MainPhone, string MarketingSources,
                                        string Notes, string State, int StatusID, string Zip, int UserName, int EventId)
        {

            Emrdev.DataLayer.CRM_Prospects pros;
            CRM_Registrants res;

            if (ProspectID == 0)
            {
                pros = new Emrdev.DataLayer.CRM_Prospects();
            }
            else
            {
                pros = obj.Get<Emrdev.DataLayer.CRM_Prospects>(o => o.ProspectID == ProspectID);
            }
            pros.Address = Address;
            pros.AltPhone = AltPhone;
            pros.City = City;
            pros.ContactMethod = ContactMethod;
            pros.Email = Email;
            pros.FirstName = FirstName;
            pros.Flagged = Flagged;
            pros.LastName = LastName;
            pros.MainPhone = MainPhone;
            pros.MarketingSources = MarketingSources;
            pros.Notes = Notes;
            pros.State = State;
            pros.StatusID = StatusID;
            pros.Zip = Zip;


            CampaignTypeDAL objCRMprospectinsert = new CampaignTypeDAL();
            if (ProspectID == 0)
            {
                pros.CreatedBy = UserName;

                obj.Create(pros);
                //Added Condition by Jaswinder to not make event mandatory
                if (EventId > 0)
                {
                    res = new CRM_Registrants();
                    res.EventID = EventId;
                    res.ProspectID = pros.ProspectID;
                    obj.Create(res);
                }

                objCRMprospectinsert.InserMarketSource(pros.ProspectID, MarketingSources, false, 1);

            }
            else
            {
                obj.Edit(pros);
                res = obj.Get<Emrdev.DataLayer.CRM_Registrants>(o => o.ProspectID == ProspectID);
                //added by jaswinder for CRM to add event if eventid>0 and it not exists in the event table and delete if eventid=0 
                if (res != null && EventId > 0)
                {
                    res.EventID = EventId;
                    obj.Edit(res);
                }
                else if (res == null && EventId > 0)
                {
                    res = new CRM_Registrants();
                    res.EventID = EventId;
                    res.ProspectID = pros.ProspectID;
                    obj.Create(res);
                }
                else if (res != null && EventId <= 0)
                {
                    obj.Delete(res);
                }
                objCRMprospectinsert.InserMarketSource(ProspectID, MarketingSources, true, 1);


            }


            //Added by jaswinder to add the atendent stauts in Crm_attendee table
            if (StatusID > 0)
            {
                if (StatusID == Convert.ToInt32(crmstatus.Attendant))
                {
                    CRM_Attendees objAttendant;
                    objAttendant = obj.Get<Emrdev.DataLayer.CRM_Attendees>(o => o.ProspectID == ProspectID && o.EventID == EventId);
                    if (objAttendant != null)
                    {
                        objAttendant.EventID = EventId;
                        objAttendant.ProspectID = pros.ProspectID;
                        obj.Edit(objAttendant);
                    }
                    else
                    {
                        objAttendant = new CRM_Attendees();
                        objAttendant.EventID = EventId;
                        objAttendant.ProspectID = pros.ProspectID;
                        obj.Create(objAttendant);
                    }

                }

            }

            return pros.ProspectID;


        }
    
    }
}
