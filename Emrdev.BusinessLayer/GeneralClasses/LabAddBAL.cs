using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class LabAddBAL
    {
        LabAddDAL objDAL = new LabAddDAL();
        public int InsertintoAptFollowup(apt_FollowUpsViewModel ViewModelfup, int PatientID)
        {
            apt_FollowUps FupEntity = new apt_FollowUps();
            Mapper.CreateMap<apt_FollowUpsViewModel, apt_FollowUps>();
            FupEntity = Mapper.Map(ViewModelfup, FupEntity);
            objDAL.Create(FupEntity);

            int RetID=AssignAppts(PatientID, FupEntity.FollowUp_ID, true);
            return RetID;
        }

        public int AssignAppts(int PatientID, int FolloupID, bool SaveChanges)
        {
            return objDAL.AssignAppts(PatientID, FolloupID, SaveChanges);
        }

        public void UpdateLabAdd(int LabID, DateTime RangeStart, DateTime RangeEnd, int StaffID, string Content)
        {
            objDAL.UpdateLabAdd(LabID, RangeStart, RangeEnd, StaffID, Content);
        }

        public apt_FollowUpsViewModel GetFollowupDetails(int FollowupId)
        {
            var _objFollowupList = new apt_FollowUpsViewModel();
            var FollowUpEntity = new apt_FollowUps();
            FollowUpEntity = objDAL.Get<apt_FollowUps>(o => o.FollowUp_ID == FollowupId);

            Mapper.CreateMap<apt_FollowUps, apt_FollowUpsViewModel>();
            _objFollowupList = Mapper.Map(FollowUpEntity, _objFollowupList);
            return _objFollowupList;
        }

        public int InsertAptFollowup(apt_FollowUpsViewModel ViewModelfup)
        {
            apt_FollowUps FupEntity = new apt_FollowUps();
            Mapper.CreateMap<apt_FollowUpsViewModel, apt_FollowUps>();
            FupEntity = Mapper.Map(ViewModelfup, FupEntity);
            objDAL.Create(FupEntity);
            int AptID =0;
            if (FupEntity.Apt_ID != null)
                AptID = FupEntity.Apt_ID.Value;
            return AptID;
        }
    }
}
