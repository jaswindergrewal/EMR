using System;
namespace Calendar
{
	[Serializable]
	public class Appointment
	{
		public string Patient { get; set; }
		public int PatientID 
		{
			get
			{
				if (patient != null)
					return patient.ID;
				else
					return -1;
			}
			set 
			{
				patient = new Patient(value); 
			}
		}
		public DateTime ApptStart { get; set; }
		public DateTime ApptEnd { get; set; }
		public int ProviderID { get; set; }
		public int ApptTypeID { get; set; }
		public int StatusID { get; set; }
		public int EventID { get; set; }
		public bool AllDay { get; set; }
		public bool EmailOnChange { get; set; }
		public string Email { get; set; }
		public int Results { get; set; }
		public string Notes { get; set; }
		public bool IsRecurring { get; set; }
		public string Recur { get; set; }
		public Patient patient { get; set; }
		public string ActionNeeded { get; set; }
		public int FollowupID { get; set; }
		public bool LabsCheckedIn { get; set; }
        public int SaleMadeYn { get; set; }
        public string WufooFormKey { get; set; }
        public string clinic { get; set; }
        public string category { get; set; }
        public int HARep { get; set; }
	}
}
