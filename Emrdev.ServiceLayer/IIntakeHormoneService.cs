using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ServiceLayer
{
  public  interface IIntakeHormoneService
    {
         void SaveIntakeFormHormone(Emrdev.ViewModelLayer.IntakeHormoneViewModel objModel);

    }
}
