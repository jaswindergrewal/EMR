using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;
using Emrdev.BusinessLayer.GeneralClasses;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LabReportService" in both code and config file together.
    public class LabReportService : ILabReportService
    {
        LabReportBAL objLabReportBAL = new LabReportBAL();
        public void DoWork()
        {
        }

        public List<LabReportGroupViewModel> GetLabReportGroupDetails()
        {
            List<LabReportGroupViewModel> lstObj = objLabReportBAL.GetLabReportGroupDetails();
            return lstObj;
        }

        public List<LabReportShortViewModel> GetLabReportShortDetails(int messageid)
        {
            List<LabReportShortViewModel> lstObj = objLabReportBAL.GetLabReportShortDetails(messageid);
            return lstObj;
        }

        public List<LabObservationResultDetailSegmentsViewModel> GetlabObservationResultDetailSegments(int orderSegmentId)
        {
            List<LabObservationResultDetailSegmentsViewModel> lstObj = objLabReportBAL.GetlabObservationResultDetailSegments(orderSegmentId).ToList();
            return lstObj;
        }

        public List<LabNotesAndCommentsSegmentsDetailsViewModel> labNotesAndCommentsSegmentsDetails(string tableRowId)
        {
            List<LabNotesAndCommentsSegmentsDetailsViewModel> lstObj = objLabReportBAL.labNotesAndCommentsSegmentsDetails(tableRowId).ToList();
            return lstObj;
        }


        public List<LabDrilldownRecordsViewModel> GetLabDrilldownRecords(int patientID, string labName)
        {
            List<LabDrilldownRecordsViewModel> lstObj = objLabReportBAL.GetLabDrilldownRecords(patientID, labName).ToList();
            return lstObj;
        }

        public List<LabNotesAndCommentsSegmentsDetailsViewModel> labNotesAndCommentsSegmentsNTEPID(int MessageID)
        {
            List<LabNotesAndCommentsSegmentsDetailsViewModel> lstObj = objLabReportBAL.labNotesAndCommentsSegmentsNTEPID(MessageID).ToList();
            return lstObj;
        }

        public List<LabObservationResultDetailSegmentsViewModel> GetLabAddress(int orderSegmentId, string Labcodes)
        {
            List<LabObservationResultDetailSegmentsViewModel> lstObj = objLabReportBAL.GetLabAddress(orderSegmentId, Labcodes).ToList();
            return lstObj;
        
        }

        public List<ReportTypeViewModel> GetReportType()
        {
            return objLabReportBAL.GetReportType();
        }

        public List<ReportListViewModel> GetReportList(int ReportTypeId)
        {
            return objLabReportBAL.GetReportList(ReportTypeId);
        }

        public void InsertUpdateReportList(ReportListViewModel ReportListViewModel)
        {
             objLabReportBAL.InsertUpdateReportList(ReportListViewModel);
        }

        public bool CheckDuplicateReport(int Id, string Name,int ReportTypeId)
        {
            return objLabReportBAL.CheckDuplicateReport(Id, Name, ReportTypeId);
        }
    }
}
