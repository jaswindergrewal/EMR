using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class LabReportBAL
    {
        LabReportDAL objLabReportDAL = new LabReportDAL();

        public List<LabReportGroupViewModel> GetLabReportGroupDetails()
        {
            List<LabReportGroupViewModel> lstObj = objLabReportDAL.GetLabReportGroupDetails().OrderBy(p => p.GroupName).ToList();
            return lstObj;
        }

        public List<LabReportShortViewModel> GetLabReportShortDetails(int messageid)
        {
            List<LabReportShortViewModel>  lstObj = objLabReportDAL.GetLabReportShortDetails(messageid);
            return lstObj;
        }

        public List<LabObservationResultDetailSegmentsViewModel> GetlabObservationResultDetailSegments(int orderSegmentId)
        {
            List<LabObservationResultDetailSegmentsViewModel> lstObj = objLabReportDAL.GetlabObservationResultDetailSegments(orderSegmentId).ToList();
            return lstObj;
        }

        public List<LabNotesAndCommentsSegmentsDetailsViewModel> labNotesAndCommentsSegmentsDetails(string tableRowId)
        {
            List<LabNotesAndCommentsSegmentsDetailsViewModel> lstObj = objLabReportDAL.labNotesAndCommentsSegmentsDetails(tableRowId).ToList();
            return lstObj;
        }



        public List<LabDrilldownRecordsViewModel> GetLabDrilldownRecords(int patientID, string labName)
        {
            List<LabDrilldownRecordsViewModel> lstObj = objLabReportDAL.GetLabDrilldownRecords(patientID, labName).ToList();
            return lstObj;
        }

        public List<LabNotesAndCommentsSegmentsDetailsViewModel> labNotesAndCommentsSegmentsNTEPID(int MessageID)
        {
            var LabComments = objLabReportDAL.List<lab_NotesAndCommentsSegments>();
            var LabPatients = objLabReportDAL.List<lab_Patients>();
            List<LabNotesAndCommentsSegmentsDetailsViewModel> objComments = (from c in LabComments
                                                                             join p in LabPatients on c.TableRowID equals p.ID
                                                                             where p.MessageID == MessageID && c.TableName=="lab_Patients" 

                                                                             select new LabNotesAndCommentsSegmentsDetailsViewModel
                                                    {
                                                        Comment = c.Comment,
                                                        SetID = c.SetID
                                                    }).Distinct().OrderBy(c=>c.SetID).ToList();
            return objComments;
            
            //List<LabNotesAndCommentsSegmentsDetailsViewModel> lstObj = objLabReportDAL.labNotesAndCommentsSegmentsDetails(MessageID.ToString()).ToList();
            //return lstObj;
        }

        public List<LabObservationResultDetailSegmentsViewModel> GetLabAddress(int orderSegmentId, string Labcodes)
        {


            List<LabObservationResultDetailSegmentsViewModel> lstObj = objLabReportDAL.GetLabAddress(orderSegmentId, Labcodes).ToList();
            return lstObj; 

        }

        public List<ReportTypeViewModel> GetReportType()
        {
            return objLabReportDAL.GetReportType();
        }

        public List<ReportListViewModel> GetReportList(int ReportTypeId)
        {
            return objLabReportDAL.GetReportList(ReportTypeId);
        }

        public void InsertUpdateReportList(ReportListViewModel ReportListViewModel)
        {
            Emrdev.DataLayer.ReportList ReportList;
           
            if (ReportListViewModel.Id == 0)
            {
                ReportList = new Emrdev.DataLayer.ReportList();
                ReportList.IsActive = ReportListViewModel.IsActive;
                ReportList.ReportTypeId = ReportListViewModel.ReportTypeId;
                ReportList.ReportName = ReportListViewModel.ReportName;
                objLabReportDAL.Create(ReportList);
            }
            else
            {
                ReportList = objLabReportDAL.Get<Emrdev.DataLayer.ReportList>(o => o.Id == ReportListViewModel.Id);
                ReportList.IsActive = ReportListViewModel.IsActive;
                ReportList.ReportTypeId = ReportListViewModel.ReportTypeId;
                ReportList.ReportName = ReportListViewModel.ReportName;
                objLabReportDAL.Edit(ReportList);
            }
        }

        public bool CheckDuplicateReport(int Id, string Name,int ReportTypeId)
        {
            bool isExist = false;
            if (Id == 0)
            {
                var objfirst = objLabReportDAL.Get<Emrdev.DataLayer.ReportList>(o => o.ReportName == Name && o.ReportTypeId== ReportTypeId);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = objLabReportDAL.Get<Emrdev.DataLayer.ReportList>(o => o.ReportName == Name && o.ReportTypeId == ReportTypeId && o.Id != Id);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;
        }
    }
}
