using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;

namespace Emrdev.BusinessLayer.GeneralClasses
{

    public class ProviderBAL
    {
        ProviderDAL objProviderDAL = new ProviderDAL();

        public List<ProviderViewModel> GetProviderDetails()
        {
            List<ProviderViewModel> lstObj = objProviderDAL.GetProviderDetails();
            return lstObj;
        }

        /// <summary>
        /// method for check the duplicate records in tables(Providers)
        /// during add/update the data
        /// used admin_reseller_data.aspx.cs
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public bool CheckDuplicateData(string Text, int ID, string Name)
        {
            bool isExist = false;
            switch (Text)
            {
                case "Providers":
                    if (ID == 0)
                    {
                        var objfirst = objProviderDAL.Get<Emrdev.DataLayer.Provider>(o => o.ProviderName == Name && o.Active == true);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objProviderDAL.Get<Emrdev.DataLayer.Provider>(o => o.ProviderName == Name && o.id != ID && o.Active == true);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
                case "AppointmentType":
                    if (ID == 0)
                    {
                        var objfirst = objProviderDAL.Get<Emrdev.DataLayer.AppointmentType>(o => o.TypeName == Name && o.Active == true);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objProviderDAL.Get<Emrdev.DataLayer.AppointmentType>(o => o.TypeName == Name && o.ID != ID && o.Active == true);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
                case "Results":
                    if (ID == 0)
                    {
                        var objfirst = objProviderDAL.Get<Emrdev.DataLayer.AppointmentResult>(o => o.ResultName == Name && o.Active == true);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objProviderDAL.Get<Emrdev.DataLayer.AppointmentResult>(o => o.ResultName == Name && o.ResultID != ID && o.Active == true);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
                case "Status":
                    if (ID == 0)
                    {
                        var objfirst = objProviderDAL.Get<Emrdev.DataLayer.Status>(o => o.StatusName == Name && o.Active == true);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objProviderDAL.Get<Emrdev.DataLayer.Status>(o => o.StatusName == Name && o.ID != ID && o.Active == true);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
            }

            return isExist;

        }
    }
}
