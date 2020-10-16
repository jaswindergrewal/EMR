using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.ViewModelLayer
{
    public class SeminarScheduleViewModel
    {
    }

    public class SeminarDateRangeViewModel
    {
        public string Name { get; set; }
        public DateTime Id { get; set; }

    }

    public class PostSeminarAppointment
    {
        public int apt_id { get; set; }
        public DateTime ApptStart { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProviderName { get; set; }
        public string WeekdayName { get; set; }
        public string WeekDayColor { get; set; }
        public string ApptText { get; set; }
        
    }
}
