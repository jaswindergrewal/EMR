using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using AutoMapper;
using System.Data.Objects;
using System.Globalization;
using System.Data.Entity.Validation;
using System.Data;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class PrescriptionDAL : ObjectEntity, IRepositary
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
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntity1.SaveChanges();
        }

        public IQueryable<T> List<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }
        #endregion

        public List<PrescriptionDrugViewModel> GetPrescriptionDrugDetails(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetPrescriptionDrugsDetails(PatientId).ToList();

            var objIList = new List<PrescriptionDrugViewModel>();           
           
            Mapper.CreateMap<ssp_GetPrescriptionDrugsDetails_Result, PrescriptionDrugViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<DrugViewModel> GetDrugDetails()
        {
            var drugDetails = GetDetails<Drug>().ToList();
            var objIList = new List<DrugViewModel>();
            Mapper.CreateMap<Drug, DrugViewModel>();
            objIList = Mapper.Map(drugDetails, objIList);
            return objIList;
        }

        public List<PrescripDrugStaffViewModel> GetPrescriptionDrugStaffDetails(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetClosePrescriptionDetails(PatientId).ToList();
            var objIList = new List<PrescripDrugStaffViewModel>();
            Mapper.CreateMap<ssp_GetClosePrescriptionDetails_Result, PrescripDrugStaffViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PrescriptionSupplierViewModel> GetSupplementsDetails(int PatientId)        {
            var objResult = ObjectEntity1.ssp_GetSupplementsDetails(PatientId).ToList();
            var objIList = new List<PrescriptionSupplierViewModel>();
            Mapper.CreateMap<ssp_GetSupplementsDetails_Result, PrescriptionSupplierViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<AutoshipProductsViewModel> GetNewSupplementDetails(string PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetNewSupplementDetails(PatientId).ToList();
            var objIList = new List<AutoshipProductsViewModel>();
            Mapper.CreateMap<ssp_GetNewSupplementDetails_Result, AutoshipProductsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PresCripSuppAutoshipProductStaffViewModel> GetClosedSupplementsDetails(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetClosedSupplementsDetails(PatientId).ToList();
            var objIList = new List<PresCripSuppAutoshipProductStaffViewModel>();
            Mapper.CreateMap<ssp_GetClosedSupplementsDetails_Result, PresCripSuppAutoshipProductStaffViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<ModifiedPrescribedAutoshipViewModel> GetModifiedPrescribedAutoshipList(int PatientId)
        {
            var objResult = ObjectEntity1.ssp_GetModifiedPrescribedAutoshipList(PatientId).ToList();
            var objIList = new List<ModifiedPrescribedAutoshipViewModel>();
            Mapper.CreateMap<ssp_GetModifiedPrescribedAutoshipList_Result, ModifiedPrescribedAutoshipViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PendingPrescriptionViewModel> GetPendingPrescriptionList()
        {
            var objResult = ObjectEntity1.ssp_GetPendingPrescriptionList().ToList();
            var objIList = new List<PendingPrescriptionViewModel>();
            Mapper.CreateMap<ssp_GetPendingPrescriptionList_Result, PendingPrescriptionViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PendingSupplementViewModel> GetPendingSupplementList()
        {
            var objResult = ObjectEntity1.ssp_GetPendingSupplementList().ToList();
            var objIList = new List<PendingSupplementViewModel>();
            Mapper.CreateMap<ssp_GetPendingSupplementList_Result, PendingSupplementViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PendingConsultRequestViewModel> GetPendingConsultList(string ClinicName)
        {
            var objResult = ObjectEntity1.ssp_GetPendingConsultList(ClinicName).ToList();
            var objIList = new List<PendingConsultRequestViewModel>();
            Mapper.CreateMap<ssp_GetPendingConsultList_Result, PendingConsultRequestViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PendingConsultRequestViewModel> GetPendingConsults(string ClinicName)
        {
            var objResult = ObjectEntity1.ssp_GetPendingConsults(ClinicName).ToList();
            var objIList = new List<PendingConsultRequestViewModel>();
            Mapper.CreateMap<ssp_GetPendingConsults_Result, PendingConsultRequestViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PendingBloodDrawsViewModel> GetPendingBloodDrawsList()
        {
            var objResult = ObjectEntity1.ssp_GetPendingBloodDrawsList().ToList();
            var objIList = new List<PendingBloodDrawsViewModel>();
            Mapper.CreateMap<ssp_GetPendingBloodDrawsList_Result, PendingBloodDrawsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<PendingBloodDrawsByClinicViewModel> GetPendingBloodDrawsListByClinic(string ClinicName)
        {
            var objResult = ObjectEntity1.ssp_GetPendingBloodDrawsListByClinic(ClinicName).ToList();
            var objIList = new List<PendingBloodDrawsByClinicViewModel>();
            Mapper.CreateMap<ssp_GetPendingBloodDrawsListByClinic_Result, PendingBloodDrawsByClinicViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public int InsertPrescriptionDrug(string txtDrugNameLocal, string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, bool chkThirdPartyAdd)
        {
            DateTime StartDate = DateTime.Parse(txtStartDate);
            DateTime? EndDate = !string.IsNullOrEmpty(txtEndDate) ? DateTime.Parse(txtEndDate) : (DateTime?)null;


            ObjectParameter result = new ObjectParameter("result", typeof(global::System.Int32));
            result.Value = DBNull.Value;
            ObjectEntity1.ssp_InsertPrescriptionDrugs(txtDrugNameLocal, chkThirdPartyAdd, StartDate, EndDate, txtPatientID, txtNotes, txtStaffID, txtSig, txtRefill, txtDisp, int.Parse(txtAptID), result);
           int results = Convert.ToInt32(result.Value);
           return results;
            
        }

        public int InsertPrescriptionRefill(string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, int DrugID, int PrescriptionID)
        {
            DateTime StartDate = DateTime.Parse(txtStartDate);
            DateTime? EndDate = !string.IsNullOrEmpty(txtEndDate)? DateTime.Parse(txtEndDate) : (DateTime?)null;

            ObjectParameter result = new ObjectParameter("result", typeof(global::System.Int32));
            result.Value = DBNull.Value;
            ObjectEntity1.ssp_InsertPrescriptionRefill(StartDate, EndDate, txtPatientID, txtNotes, txtStaffID, txtSig, txtRefill, txtDisp, int.Parse(txtAptID),DrugID,PrescriptionID, result);
            int results = Convert.ToInt32(result.Value);
            return results;
           
        }

        public void ClosePrescription(int PrescriptionID, int ElementId)
        {
            if (ElementId == 1)
            {
                ObjectEntity1.ssp_ClosePrescription(PrescriptionID);
            }
            else {
                ObjectEntity1.ssp_ClosePresscriptionSupp(PrescriptionID);
            }
        }

        public void DeletePrescription(int PrescriptionID,int ElementId)
        {
            if (ElementId == 1)
            {
                ObjectEntity1.ssp_DeletePrescription(PrescriptionID);
            }
            else
            {
                ObjectEntity1.ssp_DeletePrescriptionSupp(PrescriptionID);
            }
        }

        public int InsertPrescriptionSupp(string txtDrugNameLocal, string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID)
        {
            DateTime StartDate = DateTime.Parse(txtStartDate);

            DateTime ? EndDate = !string.IsNullOrEmpty(txtEndDate)? DateTime.Parse(txtEndDate) : (DateTime?)null;

            ObjectParameter result = new ObjectParameter("result", typeof(global::System.Int32));
            result.Value = DBNull.Value;
            ObjectEntity1.ssp_InsertPrescriptionSupp(txtDrugNameLocal, StartDate, EndDate, txtPatientID, txtNotes, txtStaffID, txtSig, txtRefill, txtDisp, int.Parse(txtAptID), result);
            int results = Convert.ToInt32(result.Value);
            return results;
        }

        public int InsertPrescriptionSuppRefill(string txtSig, string txtDisp, string txtRefill, string txtStartDate, string txtEndDate, string txtNotes, int txtPatientID, int txtStaffID, string txtAptID, int DrugID, int PrescriptionID)
        {
            DateTime StartDate = DateTime.Parse(txtStartDate);
            DateTime? EndDate = !string.IsNullOrEmpty(txtEndDate) ? DateTime.Parse(txtEndDate) : (DateTime?)null;
            ObjectParameter result = new ObjectParameter("result", typeof(global::System.Int32));
            result.Value = DBNull.Value;
            ObjectEntity1.ssp_InsertPrescriptionSuppRefill(StartDate, EndDate, txtPatientID, txtNotes, txtStaffID, txtSig, txtRefill, txtDisp, int.Parse(txtAptID), DrugID, PrescriptionID, result);
            int results = Convert.ToInt32(result.Value);
            return results;
        }

        public List<prescriptionHistoryViewModel> GetPrescriptionHistory(int PatientID)
        {
            var objResult = ObjectEntity1.ssp_PrescriptionHistory(PatientID).ToList();
            var objIList = new List<prescriptionHistoryViewModel>();
            Mapper.CreateMap<ssp_PrescriptionHistory_Result, prescriptionHistoryViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public int AutoshipFollowupPrescription(string Data)
        {
            try
            {
                string[] dataArray = Data.Split(',');

                int[] scripsArray = new int[dataArray.Length];
                for (int i = 2; i < dataArray.Length; i++)
                {
                    scripsArray[i] = int.Parse(dataArray[i]);
                }

                int patientID = int.Parse(dataArray[0]);
                Patient pat = (from p in ObjectEntity1.Patients
                               where p.PatientID == patientID
                               select p).First();

                List<PresscriptionSupp> allChanges = (from m in ObjectEntity1.ModifiedSupps
                                                      join ps in ObjectEntity1.PresscriptionSupps on m.PrescriptionSuppID equals ps.PresscriptionSuppID
                                                      join a in ObjectEntity1.AutoshipProducts on ps.ProductID equals a.ProductID
                                                      join p in ObjectEntity1.Patients on ps.PatientID equals p.PatientID
                                                      where p.PatientID == patientID
                                                      && scripsArray.Contains(ps.PresscriptionSuppID)
                                                      select ps).ToList();

              

                if (allChanges.Count > 0)
                {
                    apt_FollowUps theFollow = new apt_FollowUps();
                    theFollow.DateEntered = DateTime.Now;
                    theFollow.Entered_By = int.Parse(dataArray[1]);
                    foreach (PresscriptionSupp supp in allChanges)
                    {
                        theFollow.FollowUp_Body += "\r\n<br/><b>Product: </b>" + (from a in ObjectEntity1.AutoshipProducts where a.ProductID == supp.ProductID select a.ProductName).First();
                        theFollow.FollowUp_Body += "\r\n<br/><b>Prescription: </b>" + supp.SuppDose;
                        theFollow.FollowUp_Body += "\r\n<br/><b>Prescription Start Date: </b>" + ((DateTime)supp.SuppDatePrescibed).ToShortDateString();
                        theFollow.FollowUp_Body += "\r\n<br/>";
                    }
                    theFollow.FollowUp_Cat = 16;
                    theFollow.FollowUp_Completed_YN = false;
                    theFollow.PatientID = patientID;

                    theFollow.FollowUp_Subject = "Auto ship change for " + pat.FirstName + " " + pat.LastName;
                    theFollow.DueDate = DateTime.Now;
                    theFollow.FirstCallNote = "";
                    theFollow.SeconCallNote = "";
                    theFollow.LetterNote = "";
                    theFollow.FinalCallNote = "";
                    Create(theFollow);
                }

               
                List<ModifiedSupp> DeleteModifiedSupp=((from m in ObjectEntity1.ModifiedSupps
                                                       join ps in ObjectEntity1.PresscriptionSupps on m.PrescriptionSuppID equals ps.PresscriptionSuppID
                                                       join a in ObjectEntity1.AutoshipProducts on ps.ProductID equals a.ProductID
                                                       join p in ObjectEntity1.Patients on ps.PatientID equals p.PatientID
                                                       where p.PatientID == patientID
                                                       select m).ToList());
                foreach (ModifiedSupp supp in DeleteModifiedSupp)
                {
                    Delete(supp);
                }

               
                return 1;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }


        /// <summary>
        /// Call the procedure and fetch supplement history by patient Id
        /// Added by surabhi 8 oct 2013
        /// </summary>
        /// <param name="PatientID"></param>      
        /// <returns></returns>
        public List<prescriptionHistoryViewModel> GetSupplementHistory(int PatientID)
        {
            var objResult = ObjectEntity1.ssp_SupplementHistory(PatientID).ToList();
            var objIList = new List<prescriptionHistoryViewModel>();
            Mapper.CreateMap<ssp_SupplementHistory_Result, prescriptionHistoryViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }
    }
}
