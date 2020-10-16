using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;
using AutoMapper;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class AppointmentConsoleBAL
    {
        AppointmentConsoleDAL objAConsoleDAL = new AppointmentConsoleDAL();
        public List<AppConsole_MedNotesViewModal> GetMedicalNotes(int AppointmentId)
        {
            List<AppConsole_MedNotesViewModal> objMedNotesViewModal = objAConsoleDAL.GetMedicalNotes(AppointmentId);
            return objMedNotesViewModal;
        }

        public List<AppConsole_ConsultViewModal> GetScheduleConsult(int AppointmentId)
        {
            List<AppConsole_ConsultViewModal> objConsultViewModal = objAConsoleDAL.GetScheduleConsult(AppointmentId);
            return objConsultViewModal;
        }

        public List<AppConsole_FollowUpViewModal> GetScheduleFollowUp(int AppointmentId)
        {
            List<AppConsole_FollowUpViewModal> objFollowUpViewModal = objAConsoleDAL.GetScheduleFollowUp(AppointmentId);
            return objFollowUpViewModal;
        }

        public List<AppConsole_BloodDrawViewModal> GetScheduleBloodDraw(int AppointmentId)
        {
            List<AppConsole_BloodDrawViewModal> objBloodDrawViewModal = objAConsoleDAL.GetScheduleBloodDraw(AppointmentId);
            return objBloodDrawViewModal;
        }

        public List<AppConsole_PrescriptionViewModal> GetPrescription(int AppointmentId)
        {
            List<AppConsole_PrescriptionViewModal> objPrescriptionViewModal = objAConsoleDAL.GetPrescription(AppointmentId);
            return objPrescriptionViewModal;
        }

        public dynamic GetDrugs(int PatinetId)
        {
            var objDrugs = objAConsoleDAL.GetDrugs(PatinetId);
            return objDrugs;
        }

        public dynamic GetPatientSupplement(int PatinetId)
        {
            var objSupplement = objAConsoleDAL.GetPatientSupplement(PatinetId);
            return objSupplement;
        }

        public dynamic GetThirdPartyRX(int PatinetId)
        {
            var objThirdParty = objAConsoleDAL.GetThirdPartyRX(PatinetId);
            return objThirdParty;
        }

        public List<AppConsole_MedNoteDetailsViewModal> GetMedTabNoteDetails(int PatientId,int ContactType)
        {
            List<AppConsole_MedNoteDetailsViewModal> objMedTabNoteViewModal = objAConsoleDAL.GetMedTabNoteDetails(PatientId, ContactType);
            return objMedTabNoteViewModal;
        }

        //public dynamic GetLabWorkDetails(int PatinetId)
        //{
        //    var objLabWork = objAConsoleDAL.GetLabWorkDetails(PatinetId);
        //    return objLabWork;
        //}

        public List<AppConsole_CriticalTaskViewModal> GetCriticalTaskDetails(int PatientId)
        {
            List<AppConsole_CriticalTaskViewModal> objCriticalTask = objAConsoleDAL.GetCriticalTaskDetails(PatientId);
            return objCriticalTask;
        }

        /// <summary>
        /// get the list of Vital tab details by PatientId
        /// </summary>
        /// <param name="PatinetId"></param>
        /// <returns></returns>
        public dynamic GetVitalDetails(int PatinetId)
        {
            var objVitalDetails = objAConsoleDAL.GetVitalDetails(PatinetId);
            return objVitalDetails;
        }

        public List<Upload_tblViewModel> GetUploadDetails(int PatientId)
        {
            List<Upload_tblViewModel> objUploadDetails = objAConsoleDAL.GetUploadDetails(PatientId);
            return objUploadDetails;
        }

        public List<AppConsole_LabWorkViewModal> GetLabWorkDetails(int PatientId)
        {
            List<AppConsole_LabWorkViewModal> objCriticalTask = objAConsoleDAL.GetLabWorkDetails(PatientId);
            return objCriticalTask;
        }

        public List<AppConsole_LabListViewModal> GetLabListDetails(int PatinetId)
        {
            List<AppConsole_LabListViewModal> objLabList = objAConsoleDAL.GetLabListDetails(PatinetId);
            return objLabList;
        }

        public List<AppConsole_DiagnosisListViewModal> GetDiagnosisList(int PatientId)
        {
            List<AppConsole_DiagnosisListViewModal> lstDiagnosis = objAConsoleDAL.GetDiagnosisList(PatientId);
            return lstDiagnosis;
        }

        public dynamic GetDiagnosisDetails()
        {
            var objDrugs = objAConsoleDAL.GetDiagnosisDetails();
            return objDrugs;
        }

        public List<AppConsole_PatientSymptViewModal> GetPatientSymptDetails(int PatientId)
        {
            List<AppConsole_PatientSymptViewModal> lstPatientSympt = objAConsoleDAL.GetPatientSymptDetails(PatientId);
            return lstPatientSympt;
        }

        public List<AppConsole_DiagnosisListViewModal> GetPatientMiscDiag(int PatientId)
        {
            List<AppConsole_DiagnosisListViewModal> lstPatientMiscDiag = objAConsoleDAL.GetPatientMiscDiag(PatientId);
            return lstPatientMiscDiag;
        }

        public void InsertProblem(int DiagnosisID, decimal Priority_num, decimal Severity_num, int PatientID )
        {
            Problem_Diagnosis_join objProblem = new Problem_Diagnosis_join();
            objProblem.DiagnosisID = DiagnosisID;
            objProblem.Priority_num = Priority_num;
            objProblem.Severity_num = Severity_num;
            objProblem.PatientID = PatientID;
            objProblem.DateEntered = DateTime.Now;
            objProblem.Active_YN = true;
            objProblem.BeingAddressed_YN = true;
            objAConsoleDAL.Create(objProblem);
        }

        public void InsertProblemSymptomjoin(int SymptomID, decimal Priority_num, decimal Severity_num, int PatientID)
        {
            Problem_Symptom_join clsProbSymptom = new Problem_Symptom_join();
            clsProbSymptom.SymptomID = SymptomID;
            clsProbSymptom.Priority_num = Priority_num;
            clsProbSymptom.Severity_num = Severity_num;
            clsProbSymptom.PatientID = PatientID;
            clsProbSymptom.DateEntered = DateTime.Now;
            clsProbSymptom.Active_YN = true;
            clsProbSymptom.BeingAddressed_YN = true;
            objAConsoleDAL.Create(clsProbSymptom);
        }

        public void Insert3rdPartyDisgnosis(int DiagnosisID, decimal Priority_num, decimal Severity_num, int PatientID)
        {
            Problem_MiscDiagnosis_join clsProblemMiscDiagnosisjoin = new Problem_MiscDiagnosis_join();
            clsProblemMiscDiagnosisjoin.DiagnosisID = DiagnosisID;
            clsProblemMiscDiagnosisjoin.Priority_num = Priority_num;
            clsProblemMiscDiagnosisjoin.Severity_num = Severity_num;
            clsProblemMiscDiagnosisjoin.PatientID = PatientID;
            clsProblemMiscDiagnosisjoin.DateEntered = DateTime.Now;
            clsProblemMiscDiagnosisjoin.Active_YN = true;
            clsProblemMiscDiagnosisjoin.BeingAddressed_YN = true;
            objAConsoleDAL.Create(clsProblemMiscDiagnosisjoin);
        }

        public List<PatientViewModel> GetPatientDetails(int PatientId)
        {
            var objPatient = new List<PatientViewModel>();
            var entityPatient = new List<Patient>();
            entityPatient = objAConsoleDAL.GetAll<Patient>(o => o.PatientID == PatientId).ToList();

            AutoMapper.Mapper.CreateMap<Patient, PatientViewModel>();
            objPatient = AutoMapper.Mapper.Map(entityPatient, objPatient);
            return objPatient;

        }

        public PatientViewModel GetPatientList(int PatientId)
        {

            var objPatient = new PatientViewModel();
            var entityPatient = new Patient();
            entityPatient = objAConsoleDAL.Get<Patient>(o => o.PatientID == PatientId);

            AutoMapper.Mapper.CreateMap<Patient, PatientViewModel>();
            objPatient = AutoMapper.Mapper.Map(entityPatient, objPatient);
            return objPatient;
            //PatientViewModel objPatient = objAConsoleDAL.GetPatientDetails().Where(o => o.PatientID == PatientId).FirstOrDefault();
           // return objPatient;
        }

        public PatientViewModel GetPatientListByCriteria(string FirstName, string LastName, string MiddleInitial, int patientId)
        {

            var objPatient = new PatientViewModel();
            var entityPatient = new Patient();
            if (patientId == 0)
            {
                entityPatient = objAConsoleDAL.Get<Patient>(o => o.FirstName == FirstName.Trim() && o.LastName == LastName.Trim() && (o.MiddleInitial == MiddleInitial));
            }
            else
            {
                entityPatient = objAConsoleDAL.Get<Patient>(o => o.FirstName == FirstName.Trim() && o.LastName == LastName.Trim() && (o.MiddleInitial == MiddleInitial) && o.PatientID !=patientId );
            }

            AutoMapper.Mapper.CreateMap<Patient, PatientViewModel>();
            objPatient = AutoMapper.Mapper.Map(entityPatient, objPatient);
            return objPatient;
            //PatientViewModel objPatient = objAConsoleDAL.GetPatientDetails().Where(o => o.FirstName == FirstName && o.LastName == LastName && (o.MiddleInitial == MiddleInitial || (o.MiddleInitial == null && MiddleInitial == ""))
            //                && o.Birthday == Birthday).FirstOrDefault();
            //return objPatient;
        }

        public List<AppConsole_InvoiceViewModal> GetInvoiceDetails(string ids, int PatientId)
        {
            List<AppConsole_InvoiceViewModal> lstInvoice = objAConsoleDAL.GetInvoiceDetails(ids, PatientId);
            return lstInvoice;
        }

        public List<SymptomViewModel> GetShowSymptomDetails(int AptId)
        {
            List<SymptomViewModel> lstSymptom = objAConsoleDAL.GetShowSymptomDetails(AptId);
            return lstSymptom;
        }

        public List<GoalItemViewModel> GetShowGoalDetails(int AptId)
        {
            List<GoalItemViewModel> lstGoal = objAConsoleDAL.GetShowGoalDetails(AptId);
            return lstGoal;
        }

        public QB_InvoicesViewModel GetQBInvoiceDetails(string Ids, int PatientId)
        {
            QB_InvoicesViewModel lstQBInvoice = objAConsoleDAL.GetQBInvoiceDetails(Ids, PatientId);
            return lstQBInvoice;
        }

        /// <summary>
        /// function for add the medical note details
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="EnteredBy"></param>
        /// <param name="MessageBody"></param>
        /// <param name="Apt_ID"></param>
        public void InsertMedicalNotes(int PatientID, int EnteredBy, string MessageBody, int Apt_ID)
        {


            //Contact_tblViewModel clsContact = new Contact_tblViewModel();
            //clsContact.PatientID = PatientID;
            //clsContact.AptType = 8;
            //clsContact.EnteredBy = EnteredBy;
            //clsContact.ContactDateEntered = DateTime.Now;
            //clsContact.MessageBody = MessageBody.Trim();
            //clsContact.Apt_ID = Apt_ID;
            //clsContact.FollowUP_Completed = false;

            //Contact_tbl cls = new Contact_tbl();
            //AutoMapper.Mapper.CreateMap<Contact_tblViewModel, Contact_tbl>();
            //cls = AutoMapper.Mapper.Map(clsContact, cls);

            //objAConsoleDAL.Create(cls);
            objAConsoleDAL.InsertMedicalNotes(PatientID, EnteredBy, MessageBody, Apt_ID);

        }

        public ProviderViewModel GetProviderList(int EmployeeID)
        {
            var objProvider = new ProviderViewModel();
            var entityobjProvider = new Provider();
            entityobjProvider = objAConsoleDAL.Get<Provider>(o => o.EmployeeID == EmployeeID);

            AutoMapper.Mapper.CreateMap<Provider, ProviderViewModel>();
            objProvider = AutoMapper.Mapper.Map(entityobjProvider, objProvider);
            return objProvider;
            //ProviderViewModel objProvider = objAConsoleDAL.GetProviderDetails().Where(o => o.EmployeeID == EmployeeID).FirstOrDefault();
            //return objProvider;
        }

        public apt_recViewModel GetAPTList(int AptId)
        {
            //apt_recViewModel objAPT = objAConsoleDAL.GetAPTDetails().Where(o => o.apt_id == AptId).FirstOrDefault();//objAConsoleDAL.Get<apt_recViewModel>(o => o.apt_id == AptId);
            //apt_recViewModel objAPT = objAConsoleDAL.GetAPTDetails().Where(o => o.apt_id == AptId).FirstOrDefault();//
            var objAptView = new apt_recViewModel();
            var AptEntity = new apt_rec();
            AptEntity = objAConsoleDAL.Get<apt_rec>(o => o.apt_id == AptId);

            AutoMapper.Mapper.CreateMap<apt_rec, apt_recViewModel>();
            objAptView = AutoMapper.Mapper.Map(AptEntity, objAptView);
            return objAptView;
           
        }

        public dynamic GetAptDateDetails(int PatientId)
        {
            var lstInvoice = objAConsoleDAL.GetAptDateDetails(PatientId);
            return lstInvoice;
        }

        public List<OvuAppointment> GetOVUDetails(int AptId)
        {
            return objAConsoleDAL.GetOVUDetails(AptId);
            
        }

        public List<OvuAppointment> GetOVUOldDetails(int AptId)
        {
            return objAConsoleDAL.GetOVUOldDetails(AptId);
            
        }

        public Patient_VitalsViewModel GetCVitalsList(int CVID)
        {
            var objVital = new Patient_VitalsViewModel();
            var VitalEntity = new Patient_Vitals();
            VitalEntity = objAConsoleDAL.Get<Patient_Vitals>(o => o.Vital_ID == CVID);// objAConsoleDAL.Get<Patient_VitalsViewModel>(o => o.Vital_ID == CVID);
            AutoMapper.Mapper.CreateMap<Patient_Vitals, Patient_VitalsViewModel>();
            objVital = AutoMapper.Mapper.Map(VitalEntity, objVital);
            return objVital;
        }

        public dynamic GetVindexDetails(int PatientID)
        {
            var obj = objAConsoleDAL.GetVindexDetails(PatientID);
            return obj;
        }

        public List<AppConsole_LabDetailsViewModal> GetLabDetails(int PatientID)
        {
            List<AppConsole_LabDetailsViewModal> lstInvoice = objAConsoleDAL.GetLabDetails(PatientID);
            return lstInvoice;
        }

        public dynamic GetTestLabDetails()
        {
            var obj = objAConsoleDAL.GetTestLabDetails();
            return obj;
        }



        public List<LabReportPanelViewModel> GetLabReportPanel()
        {
            List<LabReportPanelViewModel> obj = objAConsoleDAL.GetLabReportPanel();
            return obj;
        }

        public dynamic GetPatientDetailList(int PatientId)
        {
            var obj = objAConsoleDAL.GetPatientDetailList(PatientId);
            return obj;
        }

        public List<Patient_Details_ViewModel> GetPatientDetailListNew(int PatientId)
        {
            List<Patient_Details_ViewModel> lstPatinet = objAConsoleDAL.GetPatientDetailListNew(PatientId);
            return lstPatinet;
        }

        public List<apt_FollowUp_typesViewModel> GetFollowUpTypesList()
        {
                       
            var PatientVitals = new List<apt_FollowUp_types>();
            var objIList = new List<apt_FollowUp_typesViewModel>();
            PatientVitals = objAConsoleDAL.GetAll<apt_FollowUp_types>(o => o.ConsultType_YN == true).ToList();
            Mapper.CreateMap<apt_FollowUp_types, apt_FollowUp_typesViewModel>();
            objIList = Mapper.Map(PatientVitals, objIList);
            return objIList;
            //List<apt_FollowUp_typesViewModel> objAPT = objAConsoleDAL.GetFollowUpTypesDetails().Where(o => o.ConsultType_YN == true).ToList();
            //objAConsoleDAL.GetAll<apt_FollowUp_typesViewModel>(o => o.ConsultType_YN == true).ToList();
            //return objAPT;
        }

        public List<Contact_tblViewModel> GetContactTblDetails(int ContactID)
        {
            List<Contact_tblViewModel> objAPT = objAConsoleDAL.GetContactTblDetails(ContactID);
            return objAPT;
        }


        public void InsertAptFollowUp(string FollowUp_Body, DateTime? Range_Start, DateTime? Range_End, int FollowUp_Cat, int Entered_By, int? Apt_ID, int PatientID)
        {
            apt_FollowUpsViewModel clsViewModelFollowUp = new apt_FollowUpsViewModel();
            clsViewModelFollowUp.FollowUp_Body = FollowUp_Body;
            clsViewModelFollowUp.Range_Start = Range_Start;
            clsViewModelFollowUp.FirstCall = false;
            clsViewModelFollowUp.FirstCallNote = "";
            clsViewModelFollowUp.SecondCall = false;
            clsViewModelFollowUp.SeconCallNote = "";
            clsViewModelFollowUp.FinalCall = false;

            clsViewModelFollowUp.FinalCallNote = "";
            clsViewModelFollowUp.Letter = false;
            clsViewModelFollowUp.LetterNote = "";
            clsViewModelFollowUp.Range_End = Range_End;
            clsViewModelFollowUp.FollowUp_Cat = FollowUp_Cat;
            clsViewModelFollowUp.Entered_By = Entered_By;

            clsViewModelFollowUp.Apt_ID = Apt_ID;
            clsViewModelFollowUp.PatientID = PatientID;
            clsViewModelFollowUp.DateEntered = DateTime.Now;

            apt_FollowUps clsEntityFollowUp = new apt_FollowUps();
            AutoMapper.Mapper.CreateMap<apt_FollowUpsViewModel, apt_FollowUps>();
            clsEntityFollowUp = AutoMapper.Mapper.Map(clsViewModelFollowUp, clsEntityFollowUp);
            objAConsoleDAL.Create(clsEntityFollowUp);
        }

        /// <summary>
        /// this method is using in TicketUtil.cs class
        /// </summary>
        /// <param name="StaffID"></param>
        /// <param name="Message"></param>
        /// <param name="Category"></param>
        /// <param name="PatientID"></param>
        /// <param name="Severity"></param>
        /// <param name="AssignType"></param>
        /// <param name="AssignTo"></param>
        /// <param name="Subject"></param>
        /// <param name="DueOffset"></param>
        /// <returns></returns>
        public int InsertAptFollowUpByTicketUtilCls(int StaffID, string Message, int Category, int? PatientID, int Severity, string AssignType, int AssignTo, string Subject, int? DueOffset)
        {
            apt_FollowUpsViewModel clsViewModelFollowUp = new apt_FollowUpsViewModel();
            clsViewModelFollowUp.DateEntered = DateTime.Now;
            clsViewModelFollowUp.Entered_By = StaffID;
            clsViewModelFollowUp.FollowUp_Body = Message;
            clsViewModelFollowUp.FollowUp_Cat = Category;
            clsViewModelFollowUp.FollowUp_Completed_YN = false;
            clsViewModelFollowUp.PatientID = PatientID;
            clsViewModelFollowUp.Severity = Severity;
            if (AssignType == "i")
                clsViewModelFollowUp.Assigned = AssignTo;
            else
                clsViewModelFollowUp.DepartmentAssign = AssignTo;
            clsViewModelFollowUp.FollowUp_Subject = Subject;

            if (DueOffset == null)
            {
                clsViewModelFollowUp.DueDate = DateTime.Today;
            }
            else
            {
                clsViewModelFollowUp.DueDate = DateTime.Today.AddDays((int)DueOffset);
            }

            clsViewModelFollowUp.FirstCall = false;
            clsViewModelFollowUp.SecondCall = false;
            clsViewModelFollowUp.FinalCall = false;
            clsViewModelFollowUp.Letter = false;
            clsViewModelFollowUp.FinalCallNote = "";
            clsViewModelFollowUp.FirstCallNote = "";
            clsViewModelFollowUp.SeconCallNote = "";
            clsViewModelFollowUp.LetterNote = "";

            apt_FollowUps clsEntityFollowUp = new apt_FollowUps();
            AutoMapper.Mapper.CreateMap<apt_FollowUpsViewModel, apt_FollowUps>();
            clsEntityFollowUp = AutoMapper.Mapper.Map(clsViewModelFollowUp, clsEntityFollowUp);
            objAConsoleDAL.Create(clsEntityFollowUp);
            return clsEntityFollowUp.FollowUp_ID;
        }

        public List<PStaffUsersViewModel> GetStaffDetails()
        {
            List<PStaffUsersViewModel> objAPT = objAConsoleDAL.GetStaffDetails();
            return objAPT;
        }

        public List<ResellersViewModel> GetRellersDetails()
        {
            List<ResellersViewModel> lstObj = objAConsoleDAL.GetRellersDetails();
            return lstObj;
        }

        public List<AppointmentTypeViewModel> GetAppointmentTypeList()
        {
            List<AppointmentTypeViewModel> lstObj = objAConsoleDAL.GetAppointmentTypeList().OrderBy(p => p.TypeName).ToList();
            return lstObj;
        }

        public void UpdateMedicalNote(string MessageBody, int ContactID)
        {
            objAConsoleDAL.UpdateMedicalNote(MessageBody, ContactID);
        }

        public List<Contact_tblViewModel> GetContactTblByFollowupId(int FollowUp_ID)
        {
            List<Contact_tblViewModel> boxContactsOnly = objAConsoleDAL.GetContactTblByFollowupId(FollowUp_ID);
            return boxContactsOnly;
        }


        public List<Contact_tblViewModel> GetContactTblByPatientId(int PatientId, int cboAutoBox, int cboCalBox, int pageSize, int Contacttype, DateTime txtEventDate)
        {
            var objIList = new List<Contact_tblViewModel>();
            try
            {
                int RecordsPerPage = 20;
                if (cboCalBox == 1)
                {
                    var objResult = objAConsoleDAL.GetAll<Contact_tbl>(p => p.PatientID == PatientId && p.AptType == 57 && p.ContactDateEntered >= txtEventDate).OrderByDescending(p => p.ContactDateEntered).Skip((pageSize - 1) * RecordsPerPage).Take(RecordsPerPage).ToList(); ;
                    Mapper.CreateMap<Contact_tbl, Contact_tblViewModel>();
                    objIList = Mapper.Map(objResult, objIList);
                }
                if (cboCalBox == 2)
                {
                    var objResult = objAConsoleDAL.GetAll<Contact_tbl>(p => p.PatientID == PatientId && p.AptType == 59 && p.ContactDateEntered >= txtEventDate).OrderByDescending(p => p.ContactDateEntered).Skip((pageSize - 1) * RecordsPerPage).Take(RecordsPerPage).ToList(); ;
                    Mapper.CreateMap<Contact_tbl, Contact_tblViewModel>();
                    objIList = Mapper.Map(objResult, objIList);
                }
                if (cboCalBox == 3)
                {if (Contacttype > 0)
                    {
                        var objResult = objAConsoleDAL.GetAll<Contact_tbl>(p => p.PatientID == PatientId && p.AptType == Contacttype && p.ContactDateEntered >= txtEventDate).OrderByDescending(p => p.ContactDateEntered).Skip((pageSize - 1) * RecordsPerPage).Take(RecordsPerPage).ToList(); ;
                        Mapper.CreateMap<Contact_tbl, Contact_tblViewModel>();
                        objIList = Mapper.Map(objResult, objIList);
                    }
                    else {
                        var objResult = objAConsoleDAL.GetAll<Contact_tbl>(p => p.PatientID == PatientId  && p.ContactDateEntered >= txtEventDate).OrderByDescending(p => p.ContactDateEntered).Skip((pageSize - 1) * RecordsPerPage).Take(RecordsPerPage).ToList(); 
                        Mapper.CreateMap<Contact_tbl, Contact_tblViewModel>();
                        objIList = Mapper.Map(objResult, objIList);
                    }
                }

               
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return objIList;
        }

        public long CountContactRecords(int PatientId, int cboAutoBox, int cboCalBox, int pageSize, int Contacttype, DateTime txtEventDate)
           {
               long ContactCount=0;
            if(cboCalBox==1)
                ContactCount = objAConsoleDAL.Count<Contact_tbl>(p => p.PatientID == PatientId && p.AptType == 57  && p.ContactDateEntered >= txtEventDate);
            if (cboCalBox == 2)
                ContactCount = objAConsoleDAL.Count<Contact_tbl>(p => p.PatientID == PatientId && p.AptType == 59 && p.ContactDateEntered >= txtEventDate);
            if (cboCalBox == 3)
            {
                if (Contacttype > 0)
                { ContactCount = objAConsoleDAL.Count<Contact_tbl>(p => p.PatientID == PatientId && p.AptType == Contacttype && p.ContactDateEntered >= txtEventDate); }
                else
                {
                    ContactCount = objAConsoleDAL.Count<Contact_tbl>(p => p.PatientID == PatientId && p.ContactDateEntered >= txtEventDate);
                }
            }
            return ContactCount;
           }
        

        public apt_FollowUp_typesViewModel GetCustomInfoFromFollowUpType(int FollowUp_ID)
        {
            apt_FollowUp_typesViewModel clsFollowType = objAConsoleDAL.GetCustomInfoFromFollowUpType(FollowUp_ID);
            return clsFollowType;
        }

        public void InsertMedicalNotesByTicketForm(int PatientID, int EnteredBy, string MessageBody)
        {
            objAConsoleDAL.InsertMedicalNotesByTicketForm(PatientID, EnteredBy, MessageBody);
        }

        public List<Contact_Type_tblViewModel> GetContactTypeTblList()
        {
            List<Contact_Type_tblViewModel> lstObj = objAConsoleDAL.GetContactTypeTblList();
            return lstObj;
        }

        public void AddContactRecords(int AptType, int PatientID, string MessageBody, int EmployeeID, int Apt_ID)
        {
            objAConsoleDAL.AddContactRecords(AptType, PatientID, MessageBody, EmployeeID, Apt_ID);
        }

        public ContactStaffPatientTypeViewModel GetContactFromMultipleTableByContactId(int ContactId)
        {
            ContactStaffPatientTypeViewModel clsModel = objAConsoleDAL.GetContactFromMultipleTableByContactId(ContactId);
            return clsModel;
        }

        public int AssignApptsUtilities(int PatientID, int FolloupID)
        {
            return objAConsoleDAL.AssignApptsUtilities(PatientID, FolloupID);
        }

        public void AddContactFollowpRecords(int AptType, int PatientID, string MessageBody, int EmployeeID, int FollowupID)
        {
            objAConsoleDAL.AddContactFollowpRecords(AptType, PatientID, MessageBody, EmployeeID, FollowupID);
        }

        public aptDoctorconsoleViewModel GetAptFordoctorconsole(int aptID)
        {
            return objAConsoleDAL.GetAptFordoctorconsole(aptID);
        }

        public List<aptDoctorconsoleViewModel> GetFutureAppointments(int StaffID)
        {
            return objAConsoleDAL.GetFutureAppointments(StaffID);
        }
    }
}
