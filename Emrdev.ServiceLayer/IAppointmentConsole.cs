using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAppointmentConsole" in both code and config file together.
    [ServiceContract]
    public interface IAppointmentConsole
    {
        [OperationContract]
        List<AppConsole_MedNotesViewModal> GetMedicalNotes(int AppointmentId);

        [OperationContract]
        List<AppConsole_ConsultViewModal> GetScheduleConsult(int AppointmentId);

        [OperationContract]
        List<AppConsole_FollowUpViewModal> GetScheduleFollowUp(int AppointmentId);

        [OperationContract]
        List<AppConsole_BloodDrawViewModal> GetScheduleBloodDraw(int AppointmentId);

        [OperationContract]
        List<AppConsole_PrescriptionViewModal> GetPrescription(int AppointmentId);

        [OperationContract]
        dynamic GetDrugs(int PatinetId);

        [OperationContract]
        dynamic GetPatientSupplement(int PatinetId);

        [OperationContract]
        dynamic GetThirdPartyRX(int PatinetId);

        [OperationContract]
        List<AppConsole_MedNoteDetailsViewModal> GetMedNoteTabDetails(int PatientId ,int ContactType);


        //[OperationContract]
        //dynamic GetLabWorkDetails(int PatinetId);

        [OperationContract]
        List<AppConsole_LabWorkViewModal> GetLabWorkDetails(int PatientId);

        [OperationContract]
        dynamic GetVitalDetails(int PatinetId);

        [OperationContract]
        List<Upload_tblViewModel> GetUploadDetails(int PatientId);

        [OperationContract]
        List<AppConsole_CriticalTaskViewModal> GetCriticalTasks(int PatinetId);

        [OperationContract]
        List<AppConsole_LabListViewModal> GetLabListDetails(int PatinetId);

        [OperationContract]
        List<AppConsole_DiagnosisListViewModal> GetDiagnosisList(int PatinetId);

        [OperationContract]
        dynamic GetDiagnosisDetails();

        [OperationContract]
        List<AppConsole_PatientSymptViewModal> GetPatientSymptDetails(int PatientId);

        [OperationContract]
        List<AppConsole_DiagnosisListViewModal> GetPatientMiscDiag(int PatientId);

        [OperationContract]
        void InsertProblem(int DiagnosisID, decimal Priority_num, decimal Severity_num, int PatientID);
                                       

        [OperationContract]
        void InsertProblemSymptomjoin(int SymptomID, decimal Priority_num, decimal Severity_num, int PatientID);

        [OperationContract]
        void Insert3rdPartyDisgnosis(int DiagnosisID, decimal Priority_num, decimal Severity_num, int PatientID);

        [OperationContract]
        List<PatientViewModel> GetPatientDetails(int PatientId);

        [OperationContract]
        PatientViewModel GetPatientList(int PatientId);

        [OperationContract]
        List<AppConsole_InvoiceViewModal> GetInvoiceDetails(string ids, int PatientId);

        [OperationContract]
        List<SymptomViewModel> GetShowSymptomDetails(int AptId);

        [OperationContract]
        List<GoalItemViewModel> GetShowGoalDetails(int AptId);

        [OperationContract]
        QB_InvoicesViewModel GetQBInvoiceDetails(string Ids, int PatientId);

        [OperationContract]
        void InsertMedicalNotes(int PatientID, int EnteredBy, string MessageBody, int Apt_ID);

        [OperationContract]
        ProviderViewModel GetProviderList(int EmployeeID);

        [OperationContract]
        apt_recViewModel GetAPTList(int AptId);

        [OperationContract]
        dynamic GetAptDateDetails(int PatientId);

        [OperationContract]
        List<OvuAppointment> GetOVUDetails(int AptId);

        [OperationContract]
        List<OvuAppointment> GetOVUOldDetails(int AptId);

        [OperationContract]
        Patient_VitalsViewModel GetCVitalsList(int CVID);

        [OperationContract]
        dynamic GetVindexDetails(int PatientID);

        [OperationContract]
        List<AppConsole_LabDetailsViewModal> GetLabDetails(int PatientID);

        [OperationContract]
        dynamic GetTestLabDetails();

        [OperationContract]
        List<LabReportPanelViewModel> GetLabReportPanel();

        [OperationContract]
        dynamic GetPatientDetailList(int PatientId);

        [OperationContract]
        List<Patient_Details_ViewModel> GetPatientDetailListNew(int PatientId);

        [OperationContract]
        List<apt_FollowUp_typesViewModel> GetFollowUpTypesList();

        [OperationContract]
        void InsertAptFollowUp(string FollowUp_Body, DateTime? Range_Start, DateTime? Range_End, int FollowUp_Cat, int Entered_By, int? Apt_ID, int PatientID);

        [OperationContract]
        List<Contact_tblViewModel> GetContactTblDetails(int ContactID);

        [OperationContract]
        List<PStaffUsersViewModel> GetStaffDetails();

        [OperationContract]
        List<ResellersViewModel> GetRellersDetails();

        [OperationContract]
        PatientViewModel GetPatientListByCriteria(string FirstName, string LastName, string MiddleInitial, int patientId);

        [OperationContract]
        List<AppointmentTypeViewModel> GetAppointmentTypeList();

        [OperationContract]
        void UpdateMedicalNote(string MessageBody, int ContactID);

        [OperationContract]
        List<Contact_tblViewModel> GetContactTblByFollowupId(int FollowUp_ID);

        [OperationContract]
        List<Contact_tblViewModel> GetContactTblByPatientId(int PatientId, int cboAutoBox, int cboCalBox, int pageSize, int Contacttype, DateTime txtEventDate);

        [OperationContract]
        apt_FollowUp_typesViewModel GetCustomInfoFromFollowUpType(int FollowUp_ID);

        [OperationContract]
        void InsertMedicalNotesByTicketForm(int PatientID, int EnteredBy, string MessageBody);

        [OperationContract]
        List<Contact_Type_tblViewModel> GetContactTypeTblList();

        [OperationContract]
        void AddContactRecords(int AptType, int PatientID, string MessageBody, int EmployeeID, int Apt_ID);

        [OperationContract]
        ContactStaffPatientTypeViewModel GetContactFromMultipleTableByContactId(int ContactId);

        [OperationContract]
        int InsertAptFollowUpByTicketUtilCls(int StaffID, string Message, int Category, int? PatientID, int Severity, string AssignType, int AssignTo, string Subject, int? DueOffset);

        [OperationContract]
        int AssignApptsUtilities(int PatientID, int FolloupID);

        [OperationContract]
        void AddContactFollowpRecords(int AptType, int PatientID, string MessageBody, int EmployeeID, int FollowupID);

        [OperationContract]
        aptDoctorconsoleViewModel GetAptFordoctorconsole(int aptID);

        [OperationContract]
        List<aptDoctorconsoleViewModel> GetFutureAppointments(int StaffID);

        [OperationContract]
        long CountContactRecords(int PatientId, int cboAutoBox, int cboCalBox, int pageSize, int Contacttype, DateTime txtEventDate);

        [OperationContract]
        void DoWork();
    }
}
