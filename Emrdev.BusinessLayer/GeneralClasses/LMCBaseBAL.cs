using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using AutoMapper;


namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class LMCBaseBAL
    {
        LMCBaseDAL objDAL = new LMCBaseDAL();
        public StaffViewModel Get(string UserName)
        {
            StaffViewModel _objStaffList=new StaffViewModel() ;
            Staff StaffEntity ;
            StaffEntity = objDAL.Get<Staff>(o => o.username == UserName
                                  && o.Active_YN == true
                               );

            Mapper.CreateMap<Staff, StaffViewModel>();
            _objStaffList = Mapper.Map(StaffEntity, _objStaffList);
            return _objStaffList;
           
        }

        public long Count(int StaffID)
        {
            long ApptFollowUpCount;
            ApptFollowUpCount = objDAL.Count<apt_FollowUps>(o => o.Assigned == StaffID
                                                         && o.FollowUp_Completed_YN == false
                                                         && o.DueDate <= DateTime.Now);
            return ApptFollowUpCount;
        }
    }
}
