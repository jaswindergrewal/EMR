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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AppointmentConsole" in both code and config file together.
    public class AppointmentConsole : IAppointmentConsole
    {
        AppointmentConsoleBAL objAppConsoleBAL = new AppointmentConsoleBAL();
        public void DoWork()
        {
        }

        public List<AppConsole_MedNotesViewModal> GetMedicalNotes(int AppointmentId)
        {
            List<AppConsole_MedNotesViewModal> objMedNotesViewModal = objAppConsoleBAL.GetMedicalNotes(AppointmentId);
            return objMedNotesViewModal;
        }

        public List<AppConsole_ConsultViewModal> GetScheduleConsult(int AppointmentId)
        {
            List<AppConsole_ConsultViewModal> objConsultViewModal = objAppConsoleBAL.GetScheduleConsult(AppointmentId);
            return objConsultViewModal;
        }

        public List<AppConsole_FollowUpViewModal> GetScheduleFollowUp(int AppointmentId)
        {
            List<AppConsole_FollowUpViewModal> objFollowUpViewModal = objAppConsoleBAL.GetScheduleFollowUp(AppointmentId);
            return objFollowUpViewModal;
        }

        public List<AppConsole_BloodDrawViewModal> GetScheduleBloodDraw(int AppointmentId)
        {
            List<AppConsole_BloodDrawViewModal> objBloodDrawViewModal = objAppConsoleBAL.GetScheduleBloodDraw(AppointmentId);
            return objBloodDrawViewModal;
        }

        public List<AppConsole_PrescriptionViewModal> GetPrescription(int AppointmentId)
        {
            List<AppConsole_PrescriptionViewModal> objPrescriptionViewModal = objAppConsoleBAL.GetPrescription(AppointmentId);
            return objPrescriptionViewModal;
        }

        public dynamic GetDrugs(int PatinetId)
        {
            var objDrugs = objAppConsoleBAL.GetDrugs(PatinetId);
            return objDrugs;
        }

        public dynamic GetPatientSupplement(int PatinetId)
        {
            var objSupplement = objAppConsoleBAL.GetPatientSupplement(PatinetId);
            return objSupplement;
        }

        public dynamic GetThirdPartyRX(int PatinetId)
        {
            var objThirdParty = objAppConsoleBAL.GetThirdPartyRX(PatinetId);
            return objThirdParty;
        }


        public List<AppConsole_MedNoteDetailsViewModal> GetMedNoteTabDetails(int PatientId,int ContactType)
        {
            List<AppConsole_MedNoteDetailsViewModal> objMedTabNoteViewModal = objAppConsoleBAL.GetMedTabNoteDetails(PatientId,ContactType);
            return objMedTabNoteViewModal;
        }


        //public dynamic GetLabWorkDetails(int PatinetId)
        //{
        //    var objLabWork = objAppConsoleBAL.GetLabWorkDetails(PatinetId);
        //    return objLabWork;
        //}


        public dynamic GetVitalDetails(int PatinetId)
        {
            var objVitalDetails = objAppConsoleBAL.GetVitalDetails(PatinetId);
            return objVitalDetails;
        }


        public List<Upload_tblViewModel> GetUploadDetails(int PatientId)
        {
            List<Upload_tblViewModel> objUploadDetails = objAppConsoleBAL.GetUploadDetails(PatientId);
            return objUploadDetails;
        }


        public List<AppConsole_CriticalTaskViewModal> GetCriticalTasks(int PatinetId)
        {
            List<AppConsole_CriticalTaskViewModal> objCriticalTask = objAppConsoleBAL.GetCriticalTaskDetails(PatinetId);
            return objCriticalTask;
        }


        public List<AppConsole_LabWorkViewModal> GetLabWorkDetails(int PatientId)
        {
            List<AppConsole_LabWorkViewModal> objCriticalTask = objAppConsoleBAL.GetLabWorkDetails(PatientId);
            return objCriticalTask;
        }

        List<AppConsole_LabListViewModal> IAppointmentConsole.GetLabListDetails(int PatinetId)
        {
            List<AppConsole_LabListViewModal> objLabList = objAppConsoleBAL.GetLabListDetails(PatinetId);
            return objLabList;
        }


        public List<AppConsole_DiagnosisListViewModal> GetDiagnosisList(int PatinetId)
        {
            List<AppConsole_DiagnosisListViewModal> lstDiagnosis = objAppConsoleBAL.GetDiagnosisList(PatinetId);
            return lstDiagnosis;
        }


        public dynamic GetDiagnosisDetails()
        {
            var objDrugs = objAppConsoleBAL.GetDiagnosisDetails();
            return objDrugs;
        }


        public List<AppConsole_PatientSymptViewModal> GetPatientSymptDetails(int PatientId)
        {
            List<AppConsole_PatientSymptViewModal> lstPatientSympt = objAppConsoleBAL.GetPatientSymptDetails(PatientId);
            return lstPatientSympt;
        }

        public List<AppConsole_DiagnosisListViewModal> GetPatientMiscDiag(int PatientId)
        {
            List<AppConsole_DiagnosisListViewModal> lstPatientMiscDiag = objAppConsoleBAL.GetPatientMiscDiag(PatientId);
            return lstPatientMiscDiag;
        }


        public void InsertProblem(int DiagnosisID, decimal Priority_num, decimal Severity_num, int PatientID)
        {
            objAppConsoleBAL.InsertProblem(DiagnosisID, Priority_num, Severity_num, PatientID);
        }

        public void InsertProblemSymptomjoin(int SymptomID, decimal Priority_num, decimal Severity_num, int PatientID)
        {
            objAppConsoleBAL.InsertProblemSymptomjoin(SymptomID, Priority_num, Severity_num, PatientID);
        }

        public void Insert3rdPartyDisgnosis(int DiagnosisID, decimal Priority_num, decimal Severity_num, int PatientID)
        {
            objAppConsoleBAL.Insert3rdPartyDisgnosis(DiagnosisID, Priority_num, Severity_num, PatientID);
        }


        public List<PatientViewModel> GetPatientDetails(int PatientId)
        {
            List<PatientViewModel> objPatient = objAppConsoleBAL.GetPatientDetails(PatientId);
            return objPatient;
        }


        public PatientViewModel GetPatientList(int PatientId)
        {
            PatientViewModel objPatient = objAppConsoleBAL.GetPatientList(PatientId);
            return objPatient;
        }


        public List<AppConsole_InvoiceViewModal> GetInvoiceDetails(string ids, int PatientId)
        {
            List<AppConsole_InvoiceViewModal> lstInvoice = objAppConsoleBAL.GetInvoiceDetails(ids, PatientId);
            return lstInvoice;
        }


        public List<SymptomViewModel> GetShowSymptomDetails(int AptId)
        {
            List<SymptomViewModel> lstSymptom = objAppConsoleBAL.GetShowSymptomDetails(AptId);
            return lstSymptom;
        }


        public List<GoalItemViewModel> GetShowGoalDetails(int AptId)
        {
            List<GoalItemViewModel> lstGoal = objAppConsoleBAL.GetShowGoalDetails(AptId);
            return lstGoal;
        }


        public QB_InvoicesViewModel GetQBInvoiceDetails(string Ids, int PatientId)
        {
            QB_InvoicesViewModel lstQBInvoice = objAppConsoleBAL.GetQBInvoiceDetails(Ids, PatientId);
            return lstQBInvoice;
        }


        public void InsertMedicalNotes(int PatientID, int EnteredBy, string MessageBody, int Apt_ID)
        {
            objAppConsoleBAL.InsertMedicalNotes(PatientID, EnteredBy, MessageBody, Apt_ID);
        }


        public ProviderViewModel GetProviderList(int EmployeeID)
        {
            ProviderViewModel objProvider = objAppConsoleBAL.GetProviderList(EmployeeID);
            return objProvider;
        }


        public apt_recViewModel GetAPTList(int AptId)
        {
            apt_recViewModel objAPT = objAppConsoleBAL.GetAPTList(AptId);
            return objAPT;
        }


        public dynamic GetAptDateDetails(int PatientId)
        {
            var lstInvoice = objAppConsoleBAL.GetAptDateDetails(PatientId);
            return lstInvoice;
        }

        public List<OvuAppointment> GetOVUDetails(int AptId)
        {
            return objAppConsoleBAL.GetOVUDetails(AptId);
            
        }

        public List<OvuAppointment> GetOVUOldDetails(int AptId)
        {
            return objAppConsoleBAL.GetOVUOldDetails(AptId);
          
        }


        public Patient_VitalsViewModel GetCVitalsList(int CVID)
        {
            Patient_VitalsViewModel objAPT = objAppConsoleBAL.GetCVitalsList(CVID);
            return objAPT;
        }


        public dynamic GetVindexDetails(int PatientID)
        {
            var obj = objAppConsoleBAL.GetVindexDetails(PatientID);
            return obj;
        }

        public List<AppConsole_LabDetailsViewModal> GetLabDetails(int PatientID)
        {
            List<AppConsole_LabDetailsViewModal> lstInvoice = objAppConsoleBAL.GetLabDetails(PatientID);
            return lstInvoice;
        }

        public dynamic GetTestLabDetails()
        {
            var obj = objAppConsoleBAL.GetTestLabDetails();
            return obj;
        }


        public List<LabReportPanelViewModel> GetLabReportPanel()
        {
            List<LabReportPanelViewModel> obj = objAppConsoleBAL.GetLabReportPanel();
            return obj;
        }


        public dynamic GetPatientDetailList(int PatientId)
        {
            var obj = objAppConsoleBAL.GetPatientDetailList(PatientId);
            return obj;
        }


        public List<Patient_Details_ViewModel> GetPatientDetailListNew(int PatientId)
        {
            List<Patient_Details_ViewModel> lstPatinet = objAppConsoleBAL.GetPatientDetailListNew(PatientId);
            return lstPatinet;
        }

        public List<apt_FollowUp_typesViewModel> GetFollowUpTypesList()
        {
            List<apt_FollowUp_typesViewModel> objAPT = objAppConsoleBAL.GetFollowUpTypesList();
            return objAPT;
        }

        public void InsertAptFollowUp(string FollowUp_Body, DateTime? Range_Start, DateTime? Range_End, int FollowUp_Cat, int Entered_By, int? Apt_ID, int PatientID)
        {
            objAppConsoleBAL.InsertAptFollowUp(FollowUp_Body, Range_Start, Range_End, FollowUp_Cat, Entered_By, Apt_ID, PatientID);
        }
      
        public List<Contact_tblViewModel> GetContactTblDetails(int ContactID)
        {
            List<Contact_tblViewModel> objAPT = objAppConsoleBAL.GetContactTblDetails(ContactID);
            return objAPT;
        }


        public List<PStaffUsersViewModel> GetStaffDetails()
        {
            List<PStaffUsersViewModel> objAPT = objAppConsoleBAL.GetStaffDetails();
            return objAPT;
        }


        public List<ResellersViewModel> GetRellersDetails()
        {
            List<ResellersViewModel> objAPT = objAppConsoleBAL.GetRellersDetails();
            return objAPT;
        }


        public PatientViewModel GetPatientListByCriteria(string FirstName, string LastName, string MiddleInitial, int patientId)
        {
            PatientViewModel objPatient = objAppConsoleBAL.GetPatientListByCriteria(FirstName, LastName, MiddleInitial, patientId);
            return objPatient;
        }


        public List<AppointmentTypeViewModel> GetAppointmentTypeList()
        {
            List<AppointmentTypeViewModel> lstObj = objAppConsoleBAL.GetAppointmentTypeList();
            return lstObj;
        }


        public void UpdateMedicalNote(string MessageBody, int ContactID)
        {
            objAppConsoleBAL.UpdateMedicalNote(MessageBody, ContactID);
        }


        public List<Contact_tblViewModel> GetContactTblByFollowupId(int FollowUp_ID)
        {
            List<Contact_tblViewModel> boxContactsOnly = objAppConsoleBAL.GetContactTblByFollowupId(FollowUp_ID);
            return boxContactsOnly;
        }


        public List<Contact_tblViewModel> GetContactTblByPatientId(int PatientId, int cboAutoBox, int cboCalBox, int pageSize, int Contacttype, DateTime txtEventDate)
        {
            List<Contact_tblViewModel> lstObj = objAppConsoleBAL.GetContactTblByPatientId(PatientId, cboAutoBox, cboCalBox, pageSize, Contacttype, txtEventDate);
            return lstObj;
        }


        public apt_FollowUp_typesViewModel GetCustomInfoFromFollowUpType(int FollowUp_ID)
        {
            apt_FollowUp_typesViewModel clsFollowType = objAppConsoleBAL.GetCustomInfoFromFollowUpType(FollowUp_ID);
            return clsFollowType;
        }


        public void InsertMedicalNotesByTicketForm(int PatientID, int EnteredBy, string MessageBody)
        {
            objAppConsoleBAL.InsertMedicalNotesByTicketForm(PatientID, EnteredBy, MessageBody);
        }


        public List<Contact_Type_tblViewModel> GetContactTypeTblList()
        {
            List<Contact_Type_tblViewModel> lstObj = objAppConsoleBAL.GetContactTypeTblList();
            return lstObj;
        }


        public void AddContactRecords(int AptType, int PatientID, string MessageBody, int EmployeeID, int Apt_ID)
        {
            objAppConsoleBAL.AddContactRecords(AptType, PatientID, MessageBody, EmployeeID, Apt_ID);
        }


        public ContactStaffPatientTypeViewModel GetContactFromMultipleTableByContactId(int ContactId)
        {
            ContactStaffPatientTypeViewModel clsModel = objAppConsoleBAL.GetContactFromMultipleTableByContactId(ContactId);
            return clsModel;
        }


        public int InsertAptFollowUpByTicketUtilCls(int StaffID, string Message, int Category, int? PatientID, int Severity, string AssignType, int AssignTo, string Subject, int? DueOffset)
        {
            int i = 0;
            i = objAppConsoleBAL.InsertAptFollowUpByTicketUtilCls(StaffID, Message, Category, PatientID, Severity, AssignType, AssignTo, Subject, DueOffset);
            return i;
        }


        public int AssignApptsUtilities(int PatientID, int FolloupID)
        {
            return objAppConsoleBAL.AssignApptsUtilities(PatientID, FolloupID);
        }

        public void AddContactFollowpRecords(int AptType, int PatientID, string MessageBody, int EmployeeID, int FollowupID)
        {
            objAppConsoleBAL.AddContactFollowpRecords(AptType, PatientID, MessageBody, EmployeeID, FollowupID);
        }

        public aptDoctorconsoleViewModel GetAptFordoctorconsole(int aptID)
        {
            return objAppConsoleBAL.GetAptFordoctorconsole(aptID);
        }

        public List<aptDoctorconsoleViewModel> GetFutureAppointments(int StaffID)
        {
            return objAppConsoleBAL.GetFutureAppointments(StaffID);
        }

        public long CountContactRecords(int PatientId, int cboAutoBox, int cboCalBox, int pageSize, int Contacttype, DateTime txtEventDate)
        {
            return objAppConsoleBAL.CountContactRecords(PatientId, cboAutoBox, cboCalBox, pageSize, Contacttype, txtEventDate);
        }
    }
}
