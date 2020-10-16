using AutoMapper;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.GeneralClasses
{
    public class AptFollowUpsBAL
    {
        #region Global Variables/Objects
        AptFollowUpsDAL objDAL = new AptFollowUpsDAL();
        #endregion

        #region InsertAptFollowUps
        /// <summary>
        /// this function used for insert aptfollowups details in to database apt_FollowUps table
        /// </summary>
        /// <param name="objAptFollowUpsViewModel"></param>
        /// Created By : Rakesh Kumar
        /// Created Date : 3-Sep-2013
        public void InsertAptFollowUps(AptFollowUpsViewModel objAptFollowUpsViewModel)
        {
            apt_FollowUps objApt_FollowUps = new apt_FollowUps();
            Mapper.CreateMap<AptFollowUpsViewModel, apt_FollowUps>();
            objApt_FollowUps = AutoMapper.Mapper.Map(objAptFollowUpsViewModel, objApt_FollowUps);
            objDAL.Create(objApt_FollowUps);
        }
        #endregion
    }
}
