using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class AppointmentConsoleDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members
        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).Count<T>();
        }
        #endregion

        // 
        public List<PatientViewModel> GetPatientDetails()
        {
            List<PatientViewModel> _PatientViewModel = new List<PatientViewModel>();
            var result = ObjectEntity1.Set<Patient>();
            _PatientViewModel = (from y in result
                                 select new PatientViewModel()
                                 {
                                     PatientID = y.PatientID,
                                     FirstName = y.FirstName,
                                     LastName = y.LastName,
                                     MiddleInitial = y.MiddleInitial,
                                     BillingStreet = y.BillingStreet,
                                     BillingCity = y.BillingCity,
                                     BillingState = y.BillingState,
                                     BillingZip = y.BillingZip,
                                     ShippingStreet = y.ShippingStreet,
                                     ShippingCity = y.ShippingCity,
                                     ShippingState = y.ShippingState,
                                     ShippingZip = y.ShippingZip,
                                     Work_Detailed_info = y.Work_Detailed_info,
                                     Work_CB_only = y.Work_CB_only,
                                     Work_NoMessage = y.Work_NoMessage,
                                     WorkPhone = y.WorkPhone,
                                     Cell_Detailed_info = y.Cell_Detailed_info,
                                     Cell_CB_Only = y.Cell_CB_Only,
                                     Cell_NoMessage = y.Cell_NoMessage,
                                     CellPhone = y.CellPhone,
                                     Home_detailed_info = y.Home_detailed_info,
                                     Home_CB_only = y.Home_CB_only,
                                     Home_NoMessage = y.Home_NoMessage,
                                     HomePhone = y.HomePhone,
                                     FaxPone = y.FaxPone,
                                     Fax_auth_detailed_info = y.Fax_auth_detailed_info,
                                     Email = y.Email,
                                     Email_auth_detailed_info = y.Email_auth_detailed_info,
                                     HIPPA_signed = y.HIPPA_signed,
                                     HIPPA_signed_date = y.HIPPA_signed_date,
                                     Prefered_Pharm = y.Prefered_Pharm,
                                     Pager = y.Pager,
                                     Birthday = y.Birthday,
                                     Sex = y.Sex,
                                     Clinic = y.Clinic,
                                     EmergencyFirstName = y.EmergencyFirstName,
                                     EmergencyLastName = y.EmergencyLastName,
                                     EmergencyPhone = y.EmergencyPhone,
                                     EmergencyRelationship = y.EmergencyRelationship,
                                     EmergencyState = y.EmergencyState,
                                     ContactPreference = y.ContactPreference,
                                     Inactive = y.Inactive,
                                     EntryDate = y.EntryDate,
                                     SpecialAttention = y.SpecialAttention,
                                     ActivityRating = y.ActivityRating,
                                     Notes = y.Notes,
                                     MedicalHistory = y.MedicalHistory,
                                     image = y.image,
                                     PCP = y.PCP,
                                     LMC_CP = y.LMC_CP,
                                     ProvID = y.ProvID,
                                     LastUpdated = y.LastUpdated,
                                     NameAlert = y.NameAlert,
                                     ConciergeID = y.ConciergeID,
                                     Aesthetic_YN = y.Aesthetic_YN,
                                     NoShowPol_Signed_YN = y.NoShowPol_Signed_YN,
                                     Cancel_NoShow_frm_signed = y.Cancel_NoShow_frm_signed,
                                     Allergies = y.Allergies,
                                     AllowApptReassign = y.AllowApptReassign,
                                     Medical = y.Medical,
                                     Aesthetics = y.Aesthetics,
                                     Autoship = y.Autoship,
                                     Retail = y.Retail,
                                     Affiliate = y.Affiliate,
                                     SOC = y.SOC,
                                     DiabetesSOC = y.DiabetesSOC,
                                     HeartSOC = y.HeartSOC,
                                     EmergencyContact = y.EmergencyContact,
                                     Marketing_source = y.Marketing_source,
                                     Seminar_attended = y.Seminar_attended,
                                     Seminar_status = y.Seminar_status,
                                     AutoshipNote = y.AutoshipNote,
                                     AutoshipDiscounts = y.AutoShipAlerts,
                                     MedicareOptOut_YN = y.MedicareOptOut_YN,
                                     MedicareOptOut_Date = y.MedicareOptOut_Date,
                                     EatingPlanReceived_YN = y.EatingPlanReceived_YN,
                                     Nickname = y.Nickname,
                                     RenewalMonth = y.RenewalMonth,
                                     Balance = y.Balance,
                                     BalanceDueDate = y.BalanceDueDate,
                                     AutoshipEmail = y.AutoshipEmail,
                                     AutoshipCancelReasonID = y.AutoshipCancelReasonID,
                                     AutoshipCancelOther = y.AutoshipCancelOther,
                                     PaymentDue = y.PaymentDue,
                                     TermsInMonths = y.TermsInMonths,
                                     StartMedical = y.StartMedical,
                                     EndMedical = y.EndMedical,
                                     InvoiceDueDate = y.InvoiceDueDate,
                                     InvoicePaid = y.InvoicePaid,
                                     InvoiceDue = y.InvoiceDue,
                                     ExpirationDate = y.ExpirationDate,
                                     MedicareB = y.MedicareB,
                                     RenewalException = y.RenewalException,
                                     RenewalExcExpire = y.RenewalExcExpire,
                                     AffiliateID = y.AffiliateID,
                                     AffiliateDate = y.AffiliateDate,
                                     IsAffiliate = y.IsAffiliate,
                                     LabsMailed = y.LabsMailed


                                 }).ToList();
            return _PatientViewModel;
        }

        public List<ProviderViewModel> GetProviderDetails()
        {
            List<ProviderViewModel> _ProviderViewModel = new List<ProviderViewModel>();
            var result = ObjectEntity1.Set<Provider>();
            _ProviderViewModel = (from objResult in result
                                  select new ProviderViewModel()
                                  {
                                      id = objResult.id,
                                      EmployeeID = objResult.EmployeeID,
                                      ProviderName = objResult.ProviderName,
                                      MondayStart = objResult.MondayStart,
                                      MondayEnd = objResult.MondayEnd,
                                      TuesdayStart = objResult.TuesdayStart,
                                      TuesdayEnd = objResult.TuesdayEnd,
                                      WednesdayStart = objResult.WednesdayStart,
                                      WednesdayEnd = objResult.WednesdayEnd,
                                      ThursdayStart = objResult.ThursdayStart,
                                      ThursdayEnd = objResult.ThursdayEnd,
                                      FridayStart = objResult.FridayStart,
                                      FridayEnd = objResult.FridayEnd,
                                      Active = objResult.Active,
                                      Category = objResult.Category,
                                      DEA = objResult.DEA,
                                      NPI = objResult.NPI,
                                      External = objResult.External

                                  }).ToList();
            return _ProviderViewModel;
        }

        public List<apt_recViewModel> GetAPTDetails()
        {
            List<apt_recViewModel> _AptViewModel = new List<apt_recViewModel>();
            var result = ObjectEntity1.Set<apt_rec>();
            _AptViewModel = (from objResult in result
                             select new apt_recViewModel()
                             {
                                 apt_id = objResult.apt_id,
                                 patient_id = objResult.patient_id,
                                 date_entered = objResult.date_entered,
                                 Arrived_time = objResult.Arrived_time,
                                 Seen_nurse_time = objResult.Seen_nurse_time,
                                 Seen_dr_time = objResult.Seen_dr_time,
                                 closed_time = objResult.closed_time,
                                 prescriptprint_time = objResult.prescriptprint_time,
                                 followUp_time = objResult.followUp_time,
                                 closed_yn = objResult.closed_yn,
                                 ApptStart = objResult.ApptStart,
                                 ApptEnd = objResult.ApptEnd,
                                 ProviderID = objResult.ProviderID,
                                 AppointmentTypeID = objResult.AppointmentTypeID,
                                 StatusID = objResult.StatusID,
                                 AllDay = objResult.AllDay,
                                 EmailOnChange = objResult.EmailOnChange,
                                 Results = objResult.Results,
                                 Notes = objResult.Notes,
                                 Email = objResult.Email,
                                 ActionNeeded = objResult.ActionNeeded,
                                 SaleMade_yn = objResult.SaleMade_yn,
                                 Note = objResult.Notes,
                                 LabsCheckedIn = objResult.LabsCheckedIn
                             }).ToList();
            return _AptViewModel;
        }

        //public List<Patient_VitalsViewModel> GetCVitalsDetails()
        //{
        //    var PatientVitals = GetDetails<Patient_Vitals>();
        //    var objIList = new List<Patient_VitalsViewModel>();
        //    Mapper.CreateMap<Patient_Vitals, Patient_VitalsViewModel>();
        //    objIList = Mapper.Map(PatientVitals, objIList);
        //    return objIList;
        //}

        public List<apt_FollowUp_typesViewModel> GetFollowUpTypesDetails()
        {
            var PatientVitals = GetDetails<apt_FollowUp_types>();
            var objIList = new List<apt_FollowUp_typesViewModel>();
            Mapper.CreateMap<apt_FollowUp_types, apt_FollowUp_typesViewModel>();
            objIList = Mapper.Map(PatientVitals, objIList);
            return objIList;

        }

        public List<Contact_tblViewModel> GetContactTblDetails(int ContactId)
        {
            var objResult = ObjectEntity1.ssp_GetContactTblDetails(ContactId).ToList();
            var objIList = new List<Contact_tblViewModel>();
            Mapper.CreateMap<ssp_GetContactTblDetails_Result, Contact_tblViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }



        public List<AppConsole_MedNotesViewModal> GetMedicalNotes(int AppointmentId)
        {
            List<AppConsole_MedNotesViewModal> objMedicalNotes = new List<AppConsole_MedNotesViewModal>();
            var obj = ObjectEntity1.ssp_GetMedNotes(AppointmentId);
            if (obj != null)
            {
                foreach (var objEntity in obj.ToList())
                {
                    AppConsole_MedNotesViewModal objModel = new AppConsole_MedNotesViewModal();
                    objModel.ContactId = objEntity.ContactID;
                    objModel.ContactDateEntered = Convert.ToDateTime(objEntity.ContactDateEntered);
                    objModel.PatientID = objEntity.PatientID;
                    objModel.EmployeeName = objEntity.EmployeeName;
                    objModel.MessageBody = objEntity.MessageBody;
                    objMedicalNotes.Add(objModel);
                }
            }
            return objMedicalNotes;
        }

        public List<AppConsole_ConsultViewModal> GetScheduleConsult(int AppointmentId)
        {
            List<AppConsole_ConsultViewModal> objConsult = new List<AppConsole_ConsultViewModal>();
            var objScheduleConsult = ObjectEntity1.ssp_GetScheduleConsult(AppointmentId);
            if (objScheduleConsult != null)
            {
                foreach (var objSEntity in objScheduleConsult.ToList())
                {
                    AppConsole_ConsultViewModal clsConsultViewModal = new AppConsole_ConsultViewModal();
                    clsConsultViewModal.DateEntered = objSEntity.DateEntered;
                    clsConsultViewModal.Range_Start = objSEntity.Range_Start;
                    clsConsultViewModal.Range_End = objSEntity.Range_End;
                    clsConsultViewModal.EmployeeName = objSEntity.EmployeeName;
                    clsConsultViewModal.FollowUp_Body = objSEntity.FollowUp_Body;
                    clsConsultViewModal.FollowUp_Type_Desc = objSEntity.FollowUp_Type_Desc;
                    objConsult.Add(clsConsultViewModal);
                }
            }
            return objConsult;
        }

        public List<AppConsole_FollowUpViewModal> GetScheduleFollowUp(int AppointmentId)
        {
            List<AppConsole_FollowUpViewModal> lstFollowUp = new List<AppConsole_FollowUpViewModal>();
            var objScheduleFollowUp = ObjectEntity1.ssp_GetScheduleFollowUp(AppointmentId);
            if (objScheduleFollowUp != null)
            {
                foreach (var followUpEntity in objScheduleFollowUp.ToList())
                {
                    AppConsole_FollowUpViewModal clsFollowUp = new AppConsole_FollowUpViewModal();
                    clsFollowUp.PatientID = followUpEntity.PatientID;
                    clsFollowUp.Range_Start = followUpEntity.Range_Start;
                    clsFollowUp.Range_End = followUpEntity.Range_End;
                    clsFollowUp.DateEntered = followUpEntity.DateEntered;
                    clsFollowUp.EmployeeName = followUpEntity.EmployeeName;
                    clsFollowUp.FollowUp_Body = followUpEntity.FollowUp_Body;
                    clsFollowUp.FollowUp_Type_Desc = followUpEntity.FollowUp_Type_Desc;
                    lstFollowUp.Add(clsFollowUp);
                }
            }
            return lstFollowUp;
        }

        public List<AppConsole_BloodDrawViewModal> GetScheduleBloodDraw(int AppointmentId)
        {
            List<AppConsole_BloodDrawViewModal> lstBloodDraw = new List<AppConsole_BloodDrawViewModal>();
            var objScheduleBloodDraw = ObjectEntity1.ssp_GetScheduleBloodDraw(AppointmentId);
            if (objScheduleBloodDraw != null)
            {
                foreach (var entityBloodDraw in objScheduleBloodDraw.ToList())
                {
                    AppConsole_BloodDrawViewModal clsBloodDraw = new AppConsole_BloodDrawViewModal();
                    clsBloodDraw.PatientID = entityBloodDraw.PatientID;
                    clsBloodDraw.DateEntered = entityBloodDraw.DateEntered;
                    clsBloodDraw.value = entityBloodDraw.value;
                    clsBloodDraw.EmployeeName = entityBloodDraw.EmployeeName;
                    clsBloodDraw.FollowUp_Body = entityBloodDraw.FollowUp_Body;
                    clsBloodDraw.FollowUp_Type_Desc = entityBloodDraw.FollowUp_Type_Desc;
                    clsBloodDraw.FollowupID = entityBloodDraw.FollowUp_ID;
                    lstBloodDraw.Add(clsBloodDraw);
                }
            }
            return lstBloodDraw;
        }

        public List<AppConsole_PrescriptionViewModal> GetPrescription(int AppointmentId)
        {
            List<AppConsole_PrescriptionViewModal> lstPrescription = new List<AppConsole_PrescriptionViewModal>();
            var objPrescription = ObjectEntity1.ssp_GetPrescription(AppointmentId);
            if (objPrescription != null)
            {
                foreach (var entityPres in objPrescription.ToList())
                {
                    AppConsole_PrescriptionViewModal clsPres = new AppConsole_PrescriptionViewModal();
                    clsPres.DrugName = entityPres.DrugName;
                    clsPres.Drug_Dose = entityPres.Drug_Dose;
                    clsPres.Drug_Dispenses = entityPres.Drug_Dispenses;
                    clsPres.Drug_NumbRefills = entityPres.Drug_NumbRefills;
                    clsPres.EmployeeName = entityPres.EmployeeName;
                    lstPrescription.Add(clsPres);
                }
            }
            return lstPrescription;
        }

        public dynamic GetDrugs(int PatinetId)
        {
            var objDrugs = ObjectEntity1.dr_console_rx_active(PatinetId).ToList();
            return objDrugs;
        }

        public dynamic GetPatientSupplement(int PatinetId)
        {
            var objDrugs = ObjectEntity1.Supplement_Patient_RX(PatinetId).ToList();
            return objDrugs;
        }

        public dynamic GetThirdPartyRX(int PatinetId)
        {
            var objDrugs = ObjectEntity1.dr_console_rx_3rdParty(PatinetId).ToList();
            return objDrugs;
        }

        /// <summary>
        /// Method for get the list of Med Notes tab details by PatientId
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public List<AppConsole_MedNoteDetailsViewModal> GetMedTabNoteDetails(int PatientId,int ContactType)
        {
            List<AppConsole_MedNoteDetailsViewModal> lstMedNoteTab = new List<AppConsole_MedNoteDetailsViewModal>();
            if (ContactType == 0)
            {
                var objMedNoteTab = ObjectEntity1.ssp_GetMedNotesDetails(PatientId);
                if (objMedNoteTab != null)
                {
                    foreach (var entityPres in objMedNoteTab.ToList())
                    {
                        AppConsole_MedNoteDetailsViewModal clsPres = new AppConsole_MedNoteDetailsViewModal();
                        clsPres.PatientID = entityPres.PatientID;
                        clsPres.value = entityPres.value;
                        clsPres.FirstName = entityPres.Firstname;
                        clsPres.LastName = entityPres.Lastname;
                        clsPres.EnteredBy = entityPres.EnteredBy;
                        clsPres.AptTypeDesc = entityPres.AptTypeDesc;
                        clsPres.ContactID = entityPres.ContactID;
                        clsPres.MessageBody = entityPres.MessageBody;
                        lstMedNoteTab.Add(clsPres);
                    }
                }
            }
            else
            {
                
                //Emr2017
                //var objMedNoteTab = ObjectEntityPart2.ssp_GetMedNotesDetailsByContactType(PatientId, ContactType);
                //if (objMedNoteTab != null)
                //{
                //    foreach (var entityPres in objMedNoteTab.ToList())
                //    {
                //        AppConsole_MedNoteDetailsViewModal clsPres = new AppConsole_MedNoteDetailsViewModal();
                //        clsPres.PatientID = entityPres.PatientID;
                //        clsPres.value = entityPres.value;
                //        clsPres.FirstName = entityPres.Firstname;
                //        clsPres.LastName = entityPres.Lastname;
                //        clsPres.EnteredBy = entityPres.EnteredBy;
                //        clsPres.AptTypeDesc = entityPres.AptTypeDesc;
                //        clsPres.ContactID = entityPres.ContactID;
                //        clsPres.MessageBody = entityPres.MessageBody;
                //        lstMedNoteTab.Add(clsPres);
                //    }
                //}
            }
            return lstMedNoteTab;
        }

        /// <summary>
        /// get the list of Lab Work tab details by PatientId
        /// </summary>
        /// <param name="PatinetId"></param>
        /// <returns></returns>
        public List<AppConsole_LabWorkViewModal> GetLabWorkDetails(int PatinetId)
        {
            List<AppConsole_LabWorkViewModal> lstLabWork = new List<AppConsole_LabWorkViewModal>();

            var objLabWork = ObjectEntity1.ssp_GetLabScheduleDetails(PatinetId);
            foreach (var entity in objLabWork.ToList())
            {
                AppConsole_LabWorkViewModal clsLabWork = new AppConsole_LabWorkViewModal();
                clsLabWork.GroupName = entity.GroupName;
                clsLabWork.InRange = Convert.ToBoolean(entity.InRange);
                clsLabWork.LastComplete = entity.LastComplete;
                clsLabWork.Days = entity.Days;
                lstLabWork.Add(clsLabWork);
            }
            return lstLabWork;
        }

        public List<AppConsole_LabListViewModal> GetLabListDetails(int PatinetId)
        {
            List<AppConsole_LabListViewModal> lstLabList = new List<AppConsole_LabListViewModal>();
            var objLabList = ObjectEntity1.Lab_LabList(PatinetId);
            if (objLabList != null)
            {
                foreach (var entity in objLabList.ToList())
                {
                    AppConsole_LabListViewModal clsLabList = new AppConsole_LabListViewModal();
                    clsLabList.MessageID = entity.MessageID;
                    clsLabList.ObservationDateTime = entity.ObservationDateTime;
                    clsLabList.PatientID = entity.PatientID;
                    clsLabList.FirstName = entity.FirstName;
                    clsLabList.LastName = entity.LastName;
                    clsLabList.LastChanged = entity.LastChanged;
                    clsLabList.PlacerOrderNumber = entity.PlacerOrderNumber;
                    lstLabList.Add(clsLabList);
                }
            }
            return lstLabList;
        }

        /// <summary>
        /// get the list of Vital tab details by PatientId
        /// </summary>
        /// <param name="PatinetId"></param>
        /// <returns></returns>
        public dynamic GetVitalDetails(int PatinetId)
        {
            var objLabWork = ObjectEntity1.ssp_GetVitalDetails(PatinetId).ToList();
            return objLabWork;
        }

        public List<Upload_tblViewModel> GetUploadDetails(int PatientId)
        {
            List<Upload_tblViewModel> lstUpload = new List<Upload_tblViewModel>();
            var objPrescription = ObjectEntity1.ssp_GetUploadDetails(PatientId);
            if (objPrescription != null)
            {
                foreach (var entityPres in objPrescription.ToList())
                {
                    Upload_tblViewModel clsUpload = new Upload_tblViewModel();
                    clsUpload.UploadID = entityPres.UploadID;
                    clsUpload.PatientID = entityPres.PatientID;
                    clsUpload.Upload_Path = entityPres.Upload_Path;
                    clsUpload.Upload_Title = entityPres.Upload_Title;
                    clsUpload.DateEntered = entityPres.DateEntered;
                    clsUpload.Category = entityPres.Category;
                    lstUpload.Add(clsUpload);
                }
            }

            return lstUpload;
        }

        public List<AppConsole_CriticalTaskViewModal> GetCriticalTaskDetails(int PatientId)
        {
            List<AppConsole_CriticalTaskViewModal> lstCritical = new List<AppConsole_CriticalTaskViewModal>();
            var objCritial = ObjectEntity1.ssp_CriticalTask(PatientId);
            if (objCritial != null)
            {
                foreach (var entityCritical in objCritial.ToList())
                {
                    AppConsole_CriticalTaskViewModal clsUpload = new AppConsole_CriticalTaskViewModal();
                    clsUpload.TaskName = entityCritical.TaskName;
                    clsUpload.Received = entityCritical.Received;
                    clsUpload.Requested = entityCritical.Requested;
                    clsUpload.Reviewed = entityCritical.Reviewed;
                    clsUpload.ReceivedDate = entityCritical.ReceivedDate;
                    clsUpload.RequestedDate = entityCritical.RequestedDate;
                    clsUpload.ReviewedDate = entityCritical.ReviewedDate;
                    clsUpload.TaskID = entityCritical.TaskID;
                    clsUpload.Upload_Title = entityCritical.Upload_Title;
                    clsUpload.Upload_Path = entityCritical.Upload_Path;
                    lstCritical.Add(clsUpload);
                }
            }
            return lstCritical;
        }

        public List<AppConsole_DiagnosisListViewModal> GetDiagnosisList(int PatientId)
        {
            List<AppConsole_DiagnosisListViewModal> lstDiagnosis = new List<AppConsole_DiagnosisListViewModal>();
            var objDiagnosis = ObjectEntity1.ssp_GetDiagnosisList(PatientId);
            if (objDiagnosis != null)
            {
                foreach (var entity in objDiagnosis.ToList())
                {
                    AppConsole_DiagnosisListViewModal clsUpload = new AppConsole_DiagnosisListViewModal();
                    clsUpload.PatientID = entity.PatientID;
                    clsUpload.ProbDiagID = entity.ProbDiagID;
                    clsUpload.Diag_Title = entity.Diag_Title;
                    clsUpload.ICD9_Code = entity.ICD9_Code;
                    clsUpload.DateEntered = entity.DateEntered;
                    clsUpload.Active_YN = entity.Active_YN;
                    clsUpload.Severity_num = entity.Severity_num;
                    clsUpload.Priority_num = entity.Priority_num;
                    clsUpload.inactive_date = entity.inactive_date;
                    clsUpload.beingaddressed_yn = entity.beingaddressed_yn;

                    lstDiagnosis.Add(clsUpload);
                }
            }
            return lstDiagnosis;
        }

        public dynamic GetDiagnosisDetails()
        {
            var objDrugs = ObjectEntity1.ssp_GetDiagnosisDetails().ToList();
            return objDrugs;
        }

        public List<AppConsole_DiagnosisListViewModal> GetPatientMiscDiag(int PatientId)
        {
            List<AppConsole_DiagnosisListViewModal> lstPatientMiscDiag = new List<AppConsole_DiagnosisListViewModal>();
            var objDiagnosis = ObjectEntity1.ssp_GetPatientMiscDiag(PatientId);
            if (objDiagnosis != null)
            {
                foreach (var entity in objDiagnosis.ToList())
                {
                    AppConsole_DiagnosisListViewModal clsPatientMiscDiag = new AppConsole_DiagnosisListViewModal();
                    clsPatientMiscDiag.PatientID = entity.PatientID;
                    clsPatientMiscDiag.ProbDiagID = entity.ProbDiagID;
                    clsPatientMiscDiag.Diag_Title = entity.Diag_Title;
                    clsPatientMiscDiag.ICD9_Code = entity.ICD9_Code;
                    clsPatientMiscDiag.DateEntered = entity.DateEntered;
                    clsPatientMiscDiag.Active_YN = entity.Active_YN;
                    clsPatientMiscDiag.Severity_num = entity.Severity_num;
                    clsPatientMiscDiag.Priority_num = entity.Priority_num;
                    clsPatientMiscDiag.inactive_date = entity.inactive_date;
                    clsPatientMiscDiag.beingaddressed_yn = entity.beingaddressed_yn;
                    lstPatientMiscDiag.Add(clsPatientMiscDiag);
                }
            }
            return lstPatientMiscDiag;
        }

        public List<AppConsole_PatientSymptViewModal> GetPatientSymptDetails(int PatientId)
        {
            List<AppConsole_PatientSymptViewModal> lstPatientSympt = new List<AppConsole_PatientSymptViewModal>();
            var objDiagnosis = ObjectEntity1.ssp_GetPatientSymptDetails(PatientId);
            if (objDiagnosis != null)
            {
                foreach (var entity in objDiagnosis.ToList())
                {
                    AppConsole_PatientSymptViewModal clsPatientSympt = new AppConsole_PatientSymptViewModal();
                    clsPatientSympt.ProbSymptID = entity.ProbSymptID;
                    clsPatientSympt.PatientID = entity.PatientID;
                    clsPatientSympt.DateEntered = entity.DateEntered;
                    clsPatientSympt.SymptomName = entity.SymptomName;
                    clsPatientSympt.Active_YN = Convert.ToBoolean(entity.Active_YN);
                    clsPatientSympt.FirstName = entity.Firstname;
                    clsPatientSympt.LastName = entity.Lastname;
                    clsPatientSympt.Severity_num = entity.Severity_num;
                    clsPatientSympt.Priority_num = entity.Priority_num;
                    clsPatientSympt.inactive_date = entity.inactive_date;
                    clsPatientSympt.beingaddressed_yn = Convert.ToBoolean(entity.beingaddressed_YN);
                    clsPatientSympt.Dir = entity.Dir;
                    lstPatientSympt.Add(clsPatientSympt);
                }
            }
            return lstPatientSympt;
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public List<AppConsole_InvoiceViewModal> GetInvoiceDetails(string ids, int PatientId)
        {
            List<AppConsole_InvoiceViewModal> lstInvoice = new List<AppConsole_InvoiceViewModal>();
            var objInvoice = ObjectEntity1.ssp_GetInvoiceDetails(ids, PatientId);
            if (objInvoice != null)
            {
                foreach (var entity in objInvoice.ToList())
                {
                    AppConsole_InvoiceViewModal clsInvoices = new AppConsole_InvoiceViewModal();
                    clsInvoices.InvoiceLineItemRefListID = entity.InvoiceLineItemRefListID;
                    clsInvoices.SalesPrice = entity.SalesPrice;
                    clsInvoices.DueDate = entity.DueDate;
                    clsInvoices.OpenBalance = entity.OpenBalance;
                    clsInvoices.IsPaid = entity.IsPaid;
                    lstInvoice.Add(clsInvoices);
                }
            }
            return lstInvoice;
        }
        public List<SymptomViewModel> GetShowSymptomDetails(int AptId)
        {
            List<SymptomViewModel> lstSymptom = new List<SymptomViewModel>();
            var objSymptom = ObjectEntity1.ssp_ShowSymtomDetails(AptId);
            if (objSymptom != null)
            {
                foreach (var entity in objSymptom.ToList())
                {
                    SymptomViewModel clsSymptom = new SymptomViewModel();
                    clsSymptom.SymptomID = entity.SymptomID;
                    clsSymptom.SymptomName = entity.Symptom;
                    clsSymptom.viewable_yn = entity.viewable_yn;
                    lstSymptom.Add(clsSymptom);
                }
            }
            //lstInvoice = ObjectEntity1.ssp_CriticalTask(1).ToList();
            return lstSymptom;
        }

        public List<GoalItemViewModel> GetShowGoalDetails(int AptId)
        {
            List<GoalItemViewModel> lstGoal = new List<GoalItemViewModel>();
            var objGoal = ObjectEntity1.ssp_ShowGoalDetails(AptId);
            if (objGoal != null)
            {
                foreach (var entity in objGoal.ToList())
                {
                    GoalItemViewModel clsGoalItem = new GoalItemViewModel();
                    clsGoalItem.GoalItemID = entity.GoalItemID;
                    clsGoalItem.DisplayName = entity.DisplayName;
                    lstGoal.Add(clsGoalItem);
                }
            }
            return lstGoal;
        }

        public QB_InvoicesViewModel GetQBInvoiceDetails(string Ids, int PatientId)
        {
            QB_InvoicesViewModel clsQBInvoice = new QB_InvoicesViewModel();
            //List<QB_Invoices> lstQBInvoice = new List<QB_Invoices>();
            var objQBInvoice = ObjectEntity1.ssp_ShowQBInvoiceDetails(Ids, PatientId);
            if (objQBInvoice != null)
            {
                foreach (var entity in objQBInvoice.ToList())
                {
                    clsQBInvoice.QBInvoiceID = entity.QBInvoiceID;
                    clsQBInvoice.TxnID = entity.TxnID;
                    clsQBInvoice.CustomerRefListID = entity.CustomerRefListID;
                    clsQBInvoice.CustomerRefFullName = entity.CustomerRefFullName;
                    clsQBInvoice.Date = entity.Date;
                    clsQBInvoice.Num = entity.Num;
                    clsQBInvoice.PONumber = entity.PONumber;
                    clsQBInvoice.Terms = entity.Terms;
                    clsQBInvoice.DueDate = entity.DueDate;
                    clsQBInvoice.OpenBalance = entity.OpenBalance;
                    clsQBInvoice.IsPaid = entity.IsPaid;
                    clsQBInvoice.InvoiceLineDesc = entity.InvoiceLineDesc;
                    clsQBInvoice.InvoiceLineItemRefFullName = entity.InvoiceLineItemRefFullName;
                    clsQBInvoice.SalesRepRefListID = entity.SalesRepRefListID;
                    clsQBInvoice.SalesRepRefFullName = entity.SalesRepRefFullName;
                    clsQBInvoice.Subtotal = entity.Subtotal;
                    clsQBInvoice.InvoiceLineQuantity = entity.InvoiceLineQuantity;
                    clsQBInvoice.InvoiceLineRate = entity.InvoiceLineRate;
                    clsQBInvoice.InvoiceLineAmount = entity.InvoiceLineAmount;
                    clsQBInvoice.InvoiceLineServiceDate = entity.InvoiceLineServiceDate;
                    clsQBInvoice.InvoiceLineItemRefListID = entity.InvoiceLineItemRefListID;

                    //lstQBInvoice.Add(clsQBInvoice);
                }
            }
            return clsQBInvoice;
        }

        public dynamic GetAptDateDetails(int PatientId)
        {
            //List<AppConsole_AptDateViewModal> lstAptDate = new List<AppConsole_AptDateViewModal>();
            var obj = ObjectEntity1.ssp_GetAptdatesList(PatientId);
            //if (obj != null)
            //{
            //    foreach (var objEntity in obj.ToList())
            //    {
            //        AppConsole_AptDateViewModal clsAptDate = new AppConsole_AptDateViewModal();
            //        clsAptDate.ApptStart = objEntity.ApptStart;
            //        clsAptDate.apt_id = objEntity.apt_id;
            //        clsAptDate.AppointmentTypeID = objEntity.AppointmentTypeID;
            //        clsAptDate.OVU = objEntity.OVU;
            //        lstAptDate.Add(clsAptDate);
            //    }
            //}
            return obj;
        }

        public List<OvuAppointment> GetOVUDetails(int AptId)
        {

            var objResult = ObjectEntity1.ssp_GetOVUList(AptId).ToList();
            var objIList = new List<OvuAppointment>();
            Mapper.CreateMap<ssp_GetOVUList_Result, OvuAppointment>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
            
            //var obj = ObjectEntity1.ssp_GetOVUList(AptId);
           
            //return obj;
        }

        public List<OvuAppointment> GetOVUOldDetails(int AptId)
        {
            //var obj = ObjectEntity1.ssp_GetOVUoldList(AptId);
            //return obj;

            var objResult = ObjectEntity1.ssp_GetOVUoldList(AptId).ToList();
            var objIList = new List<OvuAppointment>();
            Mapper.CreateMap<ssp_GetOVUoldList_Result, OvuAppointment>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public dynamic GetVindexDetails(int PatientID)
        {
            var obj = ObjectEntity1.ssp_GetVindex(PatientID);
            return obj;
        }

        public List<AppConsole_LabDetailsViewModal> GetLabDetails(int PatientID)
        {
            List<AppConsole_LabDetailsViewModal> lstLabDetails = new List<AppConsole_LabDetailsViewModal>();
            var obj = ObjectEntity1.ssp_GetLabDetails(PatientID);
            if (obj != null)
            {
                foreach (var entity in obj)
                {
                    AppConsole_LabDetailsViewModal clsLabDetails = new AppConsole_LabDetailsViewModal();
                    clsLabDetails.ObservationDateTime = entity.ObservationDateTime;
                    clsLabDetails.ID = entity.ID;
                    clsLabDetails.ObservationIdentifier = entity.ObservationIdentifier;
                    clsLabDetails.ObservationValue = entity.ObservationValue;
                    clsLabDetails.Units = entity.Units;
                    clsLabDetails.ReferencesRange = entity.ReferencesRange;
                    lstLabDetails.Add(clsLabDetails);
                }
            }
            return lstLabDetails;
        }

        public dynamic GetTestLabDetails()
        {
            var obj = ObjectEntity1.ssp_GetTestLabDetails();
            return obj;
        }

        public List<LabReportPanelViewModel> GetLabReportPanel()
        {
            
            var objResult = ObjectEntity1.ssp_GetLabReportPanel().ToList();
            var objIList = new List<LabReportPanelViewModel>();
            Mapper.CreateMap<ssp_GetLabReportPanel_Result, LabReportPanelViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public dynamic GetPatientDetailList(int PatientId)
        {
            var obj = ObjectEntity1.Patient_Details(PatientId);
            return obj;
        }

        public List<Patient_Details_ViewModel> GetPatientDetailListNew(int PatientId)
        {
            List<Patient_Details_ViewModel> lstPatient = new System.Collections.Generic.List<Patient_Details_ViewModel>();
            var obj = ObjectEntity1.Patient_Details(PatientId);
            if (obj != null)
            {
                foreach (var entity in obj.ToList())
                {
                    Patient_Details_ViewModel clsPatient = new Patient_Details_ViewModel();
                    clsPatient.FirstName = entity.FirstName;
                    clsPatient.LastName = entity.LastName;
                    lstPatient.Add(clsPatient);
                }
            }
            return lstPatient;
        }

        public List<PStaffUsersViewModel> GetStaffDetails()
        {
            var objResult = ObjectEntity1.Staff_Get().ToList();
            var objIList = new List<PStaffUsersViewModel>();
            Mapper.CreateMap<Staff_Get_Result, PStaffUsersViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void InsertMedicalNotes(int PatientID, int EnteredBy, string MessageBody, int Apt_ID)
        {
            ObjectEntity1.ssp_AddMedicalNoteDetails(PatientID, EnteredBy, MessageBody, Apt_ID);
        }

        public List<ResellersViewModel> GetRellersDetails()
        {
            var objResult = ObjectEntity1.ssp_GetRellersList().ToList();
            var objIList = new List<ResellersViewModel>();
            Mapper.CreateMap<ssp_GetRellersList_Result, ResellersViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<AppointmentTypeViewModel> GetAppointmentTypeList()
        {
            var objResult = GetDetails<AppointmentType>().ToList();
            var objIList = new List<AppointmentTypeViewModel>();
            Mapper.CreateMap<AppointmentType, AppointmentTypeViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public void UpdateMedicalNote(string MessageBody, int ContactID)
        {
            ObjectEntity1.ssp_UpdateMedicalNote(MessageBody, ContactID);
        }

        public List<Contact_tblViewModel> GetContactTblByFollowupId(int FollowUp_ID)
        {
            var objResult = ObjectEntity1.ssp_GetContactTblByFollowupId(FollowUp_ID).ToList();
            var objIList = new List<Contact_tblViewModel>();
            Mapper.CreateMap<ssp_GetContactTblByFollowupId_Result, Contact_tblViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        //public List<Contact_tblViewModel> GetContactTblByPatientId(int PatientId, bool cboAutoBox, bool cboCalBox,int pageSize)
        //{
        //    var objIList = new List<Contact_tblViewModel>();
        //    try
        //    {
        //        int RecordsPerPage = 20;
              
        //        if (cboAutoBox == true && cboCalBox == true)
        //        {
        //            var objResult = GetDetails<Contact_tbl>().Where(p => p.PatientID == PatientId).OrderByDescending(p => p.ContactDateEntered).Skip((pageSize - 1) * RecordsPerPage).Take(RecordsPerPage).ToList();
        //            Mapper.CreateMap<Contact_tbl, Contact_tblViewModel>();
        //            objIList = Mapper.Map(objResult, objIList);
        //        }
        //        else if (cboAutoBox == false && cboCalBox == true)
        //        {
        //            var objResult = GetDetails<Contact_tbl>().Where(p => p.PatientID == PatientId && p.AptType != 59).OrderByDescending(p => p.ContactDateEntered).Skip((pageSize - 1) * RecordsPerPage).Take(RecordsPerPage).ToList();
        //            Mapper.CreateMap<Contact_tbl, Contact_tblViewModel>();
        //            objIList = Mapper.Map(objResult, objIList);
        //        }
        //        else if (cboAutoBox == true && cboCalBox == false)
        //        {
        //            var objResult = Getall<Contact_tbl>().Where(p => p.PatientID == PatientId && p.AptType != 57).OrderByDescending(p => p.ContactDateEntered).Skip((pageSize - 1) * RecordsPerPage).Take(RecordsPerPage).ToList();
        //            Mapper.CreateMap<Contact_tbl, Contact_tblViewModel>();
        //            objIList = Mapper.Map(objResult, objIList);
        //        }
        //        else if (cboAutoBox == false && cboCalBox == false)
        //        {
        //            var objResult = GetDetails<Contact_tbl>().Where(p => p.PatientID == PatientId && p.AptType != 57 && p.AptType != 59).OrderByDescending(p => p.ContactDateEntered).Skip((pageSize - 1) * RecordsPerPage).Take(RecordsPerPage).ToList();
        //            Mapper.CreateMap<Contact_tbl, Contact_tblViewModel>();
        //            objIList = Mapper.Map(objResult, objIList);
        //        }
               
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw;
        //    }
        //    return objIList;
        //}

        public apt_FollowUp_typesViewModel GetCustomInfoFromFollowUpType(int FollowUp_ID)
        {
            var _objpatientList = new apt_FollowUp_typesViewModel();
            var PatientEntity = new apt_FollowUp_types();
            PatientEntity = Get<apt_FollowUp_types>(o => o.FollowUp_Type_ID == FollowUp_ID);
            Mapper.CreateMap<apt_FollowUp_types, apt_FollowUp_typesViewModel>();
            _objpatientList = Mapper.Map(PatientEntity, _objpatientList);
            return _objpatientList;
        }

        public void InsertMedicalNotesByTicketForm(int PatientID, int EnteredBy, string MessageBody)
        {
            ObjectEntity1.ssp_InsertMedicalNoteDetails(PatientID, EnteredBy, MessageBody);
        }

        public List<Contact_Type_tblViewModel> GetContactTypeTblList()
        {
            var drugDetails = GetDetails<Contact_Type_tbl>().ToList();
            var objIList = new List<Contact_Type_tblViewModel>();
            Mapper.CreateMap<Contact_Type_tbl, Contact_Type_tblViewModel>();
            objIList = Mapper.Map(drugDetails, objIList);
            return objIList;
        }

        public void AddContactRecords(int AptType, int PatientID, string MessageBody, int EmployeeID, int Apt_ID)
        {
            ObjectEntity1.contact_tbl_EMR_Insert(AptType, PatientID, MessageBody, EmployeeID, Apt_ID);
        }

        public ContactStaffPatientTypeViewModel GetContactFromMultipleTableByContactId(int ContactId)
        {
            var objResult = ObjectEntity1.ssp_GetContactDetailsFromMultipleTable(ContactId).FirstOrDefault();
            var objIList = new ContactStaffPatientTypeViewModel();
            Mapper.CreateMap<ssp_GetContactDetailsFromMultipleTable_Result, ContactStaffPatientTypeViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public int AssignApptsUtilities(int PatientID, int FolloupID)
        {
            int i = 0;
            i = Convert.ToInt32(ObjectEntity1.ssp_AssignAppts(PatientID, FolloupID));
            return i;
        }

        public void AddContactFollowpRecords(int AptType, int PatientID, string MessageBody, int EmployeeID, int FollowupID)
        {
            ObjectEntity1.contact_tbl_FollowUp_Insert(AptType, PatientID, MessageBody, EmployeeID, FollowupID);
        }

        public aptDoctorconsoleViewModel GetAptFordoctorconsole(int aptID)
        {
            var objResult = ObjectEntity1.ssp_GetAptWithAptIDforDoctorConsole(aptID).FirstOrDefault();
            var objIList = new aptDoctorconsoleViewModel();
            Mapper.CreateMap<ssp_GetAptWithAptIDforDoctorConsole_Result, aptDoctorconsoleViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<aptDoctorconsoleViewModel> GetFutureAppointments(int StaffID)
        {
            var objResult = ObjectEntity1.ssp_GetCurrentAptFordoctorConsole(StaffID).ToList();
            var objIList = new List<aptDoctorconsoleViewModel>();
            Mapper.CreateMap<ssp_GetCurrentAptFordoctorConsole_Result, aptDoctorconsoleViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

    }
}
