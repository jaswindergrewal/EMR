using Emrdev.GeneralClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
    public class IntakeHormoneService : IIntakeHormoneService
    {
        #region Global

        IntakeHormoneBAL objBAL;

        #endregion


        #region Save Intake Form Hormone

        public void SaveIntakeFormHormone(ViewModelLayer.IntakeHormoneViewModel objModel)
        {
            objBAL = new IntakeHormoneBAL();
            objBAL.SaveIntakeFormHormone(objModel);
        }

        #endregion
    }
}
