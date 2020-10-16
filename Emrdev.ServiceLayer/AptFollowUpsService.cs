using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AptFollowUpsService" in both code and config file together.
    public class AptFollowUpsService : IAptFollowUpsService
    {
        #region Global Variables/Objects
        AptFollowUpsBAL objBAL = new AptFollowUpsBAL();
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
            objBAL.InsertAptFollowUps(objAptFollowUpsViewModel);
        }
        #endregion

    }
}
