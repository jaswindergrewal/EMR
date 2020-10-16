using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILabReportService" in both code and config file together.
    [ServiceContract]
    public interface ILabReportService
    {
        [OperationContract]
        List<LabReportGroupViewModel> GetLabReportGroupDetails();

        [OperationContract]
        List<LabReportShortViewModel> GetLabReportShortDetails(int messageid);

        List<LabObservationResultDetailSegmentsViewModel> GetlabObservationResultDetailSegments(int orderSegmentId);

        List<LabNotesAndCommentsSegmentsDetailsViewModel> labNotesAndCommentsSegmentsDetails(string tableRowId);


        List<LabDrilldownRecordsViewModel> GetLabDrilldownRecords(int patientID, string labName);

        List<LabNotesAndCommentsSegmentsDetailsViewModel> labNotesAndCommentsSegmentsNTEPID(int MessageID);

        List<LabObservationResultDetailSegmentsViewModel> GetLabAddress(int orderSegmentId,string Labcodes);

        List<ReportTypeViewModel> GetReportType();

        List<ReportListViewModel> GetReportList(int ReportTypeId);

        void InsertUpdateReportList(ReportListViewModel ReportListViewModel);

        bool CheckDuplicateReport(int Id, string Name,int ReportTypeId);
        [OperationContract]
        void DoWork();
    }
}
