using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Profile
/// </summary>
public class PatientProfile
{
	public int PatientID { get; set; }
	public string PatientName { get; set; }
	public string ShippingStreet { get; set; }
	public string ShippingCity { get; set; }
	public string ShippingState { get; set; }
	public string ShippingZip { get; set; }
	public string StartDate { get; set; }
	public string EndDate { get; set; }
	public string Exception { get; set; }
}