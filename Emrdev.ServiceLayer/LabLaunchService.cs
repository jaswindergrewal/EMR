using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public class LabLaunchService:ILabLaunchService
    {
        #region Global

        Emrdev.GeneralClasses.LabLaunchBAL objDAL;

        #endregion


        #region Get By Patient Id

        public ViewModelLayer.LabLaunchViewModel GetByPatientId(int patientId)
        {
            objDAL = new GeneralClasses.LabLaunchBAL();
            return objDAL.GetByPatientId(patientId);
        }

        #endregion
    }
}
