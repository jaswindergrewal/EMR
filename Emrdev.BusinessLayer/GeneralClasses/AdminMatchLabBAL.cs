using System.Collections.Generic;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class AdminMatchLabBAL
    {
        AdminMatchLabDAL objDAL = new AdminMatchLabDAL();

        public List<AdminMatchLabViewModel> GetMatchLabsList(string year)
        {
            return objDAL.GetMatchLabsList(year);
          
        }

        public void EditMactLab(int FollowupID, int AppointmentID)
        {
             objDAL.EditMactLab(FollowupID, AppointmentID);
        }
       
        
    }
}
