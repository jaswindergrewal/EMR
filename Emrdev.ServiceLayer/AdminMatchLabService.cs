using System.Collections.Generic;
using Emrdev.BusinessLayer.GeneralClasses;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminMatchLabService" in both code and config file together.
    public class AdminMatchLabService : IAdminMatchLabService
    {
        AdminMatchLabBAL objBAL = new AdminMatchLabBAL();

        public List<AdminMatchLabViewModel> GetMatchLabsList(string year)
        {
            return objBAL.GetMatchLabsList(year);

        }

        public void EditMactLab(int FollowupID, int AppointmentID)
        {
             objBAL.EditMactLab(FollowupID, AppointmentID);
        }
    }
}
