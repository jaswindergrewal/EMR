using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.GeneralClasses
{
    public class SpecialAttentionBAL
    {
        SpecialAttentionDAL objDAL = new SpecialAttentionDAL();
        
        /// <summary>
        /// Get the details of the special attentions given to the patient
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
        public List<SpecialAttentionViewModel> GetSpecialAttentionByPatientId(int PatientID)
        {
            //var _objAttentionList = new List<SpecialAttentionViewModel>();
            //var AttentionEntity = new List<SpecialAttentionFlag>();
            //AttentionEntity = objDAL.GetAll<SpecialAttentionFlag>(o => o.PatientID == PatientID).ToList();
            //Mapper.CreateMap<SpecialAttentionFlag, SpecialAttentionViewModel>();
            //_objAttentionList = Mapper.Map(AttentionEntity, _objAttentionList);
            //return _objAttentionList;
            return objDAL.GetSpecialAttentionByPatientId( PatientID);
        }

        /// <summary>
        /// Delete the paticular special attentions given to patient
        /// </summary>
        /// <param name="SpecialAttentionID"></param>
        public void DeleteSpecialAttentionFlag(int SpecialAttentionID)
        {
            SpecialAttentionFlag _objAttentionList = new SpecialAttentionFlag();
            _objAttentionList = objDAL.Get<SpecialAttentionFlag>(o => o.SpecialAttentionID == SpecialAttentionID);
            objDAL.Delete(_objAttentionList);
        }

        
        /// <summary>
        /// Insert the Special attentiondata to tables
        /// 14th aug 2013 jaswinder
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="Content"></param>
        public void AddSpecialAttentionByPatientId(int PatientID, string Content,int StaffId)
        {
            var AttentionEntity = new SpecialAttentionFlag();
            AttentionEntity.DateEntered = DateTime.Now;
            AttentionEntity.PatientID = PatientID;
            AttentionEntity.FlagNotes = Content;
            AttentionEntity.FlagYN = true;
            AttentionEntity.StaffId = StaffId;
            objDAL.Create(AttentionEntity);
        }

        /// <summary>
        /// Get the count for special attentions
        /// Jaswinder 29th aug 2013
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns></returns>
               
        public long GetSpecialAttentionCount(int PatientID)
        {
            long SpecialAttCount;
            SpecialAttCount = objDAL.Count<SpecialAttentionFlag>(o => o.PatientID == PatientID);
            return SpecialAttCount;
        }
       
    
    }
}
