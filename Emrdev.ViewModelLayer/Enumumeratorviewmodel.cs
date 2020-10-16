using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
   
    public enum ProvidersForCalendar
    {
        None = 0,
        KirklandMA = 17,
        LynnwoodMA = 18,
        TacomaMA = 19

    }

    public enum Clicnic
    {
        None = 0,
        Kirkland,
        Lynnwood,
        South
    }

    public enum crmstatus
    {
        None = 0,
        Attendant=5,
        Scheduled = 6,
        LrCompletedJoined = 8
    }

    public enum DepartmentsEnum
    {
        None = 0,
        Employees=1
    }

    public enum Followups
    {
        None=0,
        GeneralFollowUp = 1,
        LifestyleConsult=2,
        CalendarFollowup=3
    }

    public enum ContactType
    {
        None = 0,
        Followup=15
    
    }


}
