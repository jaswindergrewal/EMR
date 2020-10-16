using Emrdev.DataLayer.GeneralClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.GeneralClasses
{
    public class IntakeHormoneBAL
    {
        #region Global

        IntakeHormoneDAL objDAL;

        #endregion




        #region Save Intake Form Hormone

        public void SaveIntakeFormHormone(Emrdev.ViewModelLayer.IntakeHormoneViewModel objModel)
        {
            objDAL = new IntakeHormoneDAL();
            objDAL.SaveIntakeHormone(objModel);
        }

        #endregion

    }
}
