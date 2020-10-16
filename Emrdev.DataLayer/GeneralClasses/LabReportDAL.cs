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
    public class LabReportDAL : ObjectEntity, IRepositary
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
            return ObjectEntity1.Set<T>();
        }

        public T GetAll<T>() where T : class
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

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            return ObjectEntity1.Set<T>().ToList<T>();
        }

      
        #endregion

        public List<LabReportGroupViewModel> GetLabReportGroupDetails()
        {
            var objResult = GetDetails<LabReports_Groups>();
            var objIList = new List<LabReportGroupViewModel>();
            Mapper.CreateMap<LabReports_Groups, LabReportGroupViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }


        public List<LabReportShortViewModel>  GetLabReportShortDetails(int messageid)
        {
            //List<LabReportShortViewModel> objLabReportShortViewModel = new List<LabReportShortViewModel>();
            //objLabReportShortViewModel = ObjectEntity1.Get_Lab_Report_Short(messageid);

            var objResult =  ObjectEntity1.Get_Lab_Report_Short(messageid).ToList();
            var objIList = new List<LabReportShortViewModel>();
            Mapper.CreateMap<Get_Lab_Report_Short_Result, LabReportShortViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
            //if (objLabReportShortViewModel != null)
            //{
                
            //    objLabReportShortViewModel.ClientID = objResult.ClientID;
            //    objLabReportShortViewModel.LastName = objResult.LastName;
            //    objLabReportShortViewModel.FirstName = objResult.FirstName;
            //    objLabReportShortViewModel.MiddleName = objResult.MiddleName;
            //    objLabReportShortViewModel.DOB = objResult.DOB;
            //    objLabReportShortViewModel.Sex = objResult.Sex;
            //    objLabReportShortViewModel.PhoneNumber = objResult.PhoneNumber;
            //    objLabReportShortViewModel.SSN = objResult.SSN;
            //    objLabReportShortViewModel.RequisitionID = objResult.RequisitionID;
            //    objLabReportShortViewModel.SpecimenID = objResult.SpecimenID;
            //    objLabReportShortViewModel.ProviderLastName = objResult.ProviderLastName;
            //    objLabReportShortViewModel.ProviderFirstName = objResult.ProviderFirstName;
            //    objLabReportShortViewModel.ProviderMiddleName = objResult.ProviderMiddleName;
            //    objLabReportShortViewModel.CollectedDateTime = objResult.CollectedDateTime;
            //    objLabReportShortViewModel.ReceivedDateTime = objResult.ReceivedDateTime;
            //    objLabReportShortViewModel.ReportedDateTime = objResult.ReportedDateTime;
            //    objLabReportShortViewModel.Result = objResult.Result;
            //    objLabReportShortViewModel.OrderSegmentID = objResult.OrderSegmentID;
            //    objLabReportShortViewModel.SetID = objResult.SetID;
            //    objLabReportShortViewModel.ServiceID = objResult.ServiceID;
            //    objLabReportShortViewModel.LabID = objResult.LabID;
            //    objLabReportShortViewModel.LabName = objResult.LabName;
            //    objLabReportShortViewModel.LabAddress = objResult.LabAddress;
            //    objLabReportShortViewModel.LabCity = objResult.LabCity;
            //    objLabReportShortViewModel.LabState = objResult.LabState;
            //    objLabReportShortViewModel.LabZip = objResult.LabZip;
            //    objLabReportShortViewModel.LabDirector = objResult.LabDirector;
            //    objLabReportShortViewModel.TotalAge = objResult.TotalAge!=null?Convert.ToInt32(objResult.TotalAge):0;
            //}
            //return objLabReportShortViewModel;
        }

        public List<LabObservationResultDetailSegmentsViewModel> GetlabObservationResultDetailSegments(int orderSegmentId)
        {
            var objResult = ObjectEntity1.Get_lab_ObservationResultDetailSegments(orderSegmentId).ToList();
            var objIList = new List<LabObservationResultDetailSegmentsViewModel>();
            Mapper.CreateMap<Get_lab_ObservationResultDetailSegments_Result, LabObservationResultDetailSegmentsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<LabNotesAndCommentsSegmentsDetailsViewModel> labNotesAndCommentsSegmentsDetails(string tableRowId)
        {
            var objResult = ObjectEntity1.labNotesAndCommentsSegmentsDetails(tableRowId).ToList();
            var objIList = new List<LabNotesAndCommentsSegmentsDetailsViewModel>();
            Mapper.CreateMap<labNotesAndCommentsSegmentsDetails_Result, LabNotesAndCommentsSegmentsDetailsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }


        public List<LabDrilldownRecordsViewModel> GetLabDrilldownRecords(int patientID, string labName)
        {
            var objResult = ObjectEntity1.Get_Lab_Drilldown_Record(patientID, labName).ToList();
            var objIList = new List<LabDrilldownRecordsViewModel>();
            Mapper.CreateMap<Get_Lab_Drilldown_Record_Result, LabDrilldownRecordsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;
        }

        public List<LabObservationResultDetailSegmentsViewModel> GetLabAddress(int orderSegmentId, string Labcodes)
        {

            var objResult = ObjectEntityPart1.ssp_GetLabAddress(orderSegmentId, Labcodes).ToList();
            var objIList = new List<LabObservationResultDetailSegmentsViewModel>();
            Mapper.CreateMap<ssp_GetLabAddress_Result, LabObservationResultDetailSegmentsViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;

        }

        public List<ReportTypeViewModel> GetReportType()
        {

            var objResult = ObjectEntityPart1.ssp_GetReportType().ToList();
            var objIList = new List<ReportTypeViewModel>();
            Mapper.CreateMap<ssp_GetReportType_Result, ReportTypeViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;

        }

        public List<ReportListViewModel> GetReportList(int ReportTypeId)
        {

            var objResult = ObjectEntityPart1.ssp_GetReportList(ReportTypeId).ToList();
            var objIList = new List<ReportListViewModel>();
            Mapper.CreateMap<ssp_GetReportList_Result, ReportListViewModel>();
            objIList = Mapper.Map(objResult, objIList);
            return objIList;

        }


        
    }
}
