using Emrdev.DataLayer.GeneralClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Emrdev.GeneralClasses
{

    public class LabLaunchBAL
    {
        #region Global

        LabLaunchDAL objDAL;

        #endregion


        #region Get By PatientId

        public Emrdev.ViewModelLayer.LabLaunchViewModel GetByPatientId(int patientId)
        {
            objDAL = new LabLaunchDAL();
            return objDAL.GetByPatientId(patientId);
        }


        #endregion


    }
}
