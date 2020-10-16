using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.ViewModelLayer;
using System.Data;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class ScanUploadsDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntityPart2.Set<T>().Add(entityToCreate);
            ObjectEntityPart2.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {

            ObjectEntityPart2.Set<T>();
            ObjectEntityPart2.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntityPart2.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
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
            return ObjectEntityPart2.Set<T>().Where(whereCondition).ToList<T>();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntityPart2.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Update the upload files details
        /// Jaswinder 16th aug 2013
        /// </summary>
        /// <param name="UploadId"></param>
        /// <param name="FileName"></param>
        /// <param name="Category"></param>
        public void UpdateUploadDocument(int UploadId, string FileName, string Category)
        {
            ObjectEntity1.ssp_UpdateUploadFileDetails(UploadId, FileName, Category);
        }

        public void DeleteUpload(int UploadID)
        {
            ObjectEntity1.ssp_DeleteUpload(UploadID);
        }

        //Emr2017
        //public List<ReportScanUploadViewModel> ReportScanupload(int PatientID, int All, DateTime FromDate, DateTime ToDate)
        //{
        //    Mapper.CreateMap<ssp_ReportScanUpload_Result, ReportScanUploadViewModel>();
        //    var objResult = ObjectEntityPart1.ssp_ReportScanUpload(PatientID, All, FromDate, ToDate).ToList();
        //    var objIList = new List<ReportScanUploadViewModel>();
        //    objIList = Mapper.Map(objResult, objIList);
        //    return objIList;
            
           
        //}
        #endregion
    }
}
