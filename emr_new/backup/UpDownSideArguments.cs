using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UpDownSideArguments
/// </summary>
public class UpDownSideArguments : EventArgs
{
	public int SysmptomID { get; set; }
	public string dir { get; set; }
	public string SymptomName { get; set; }
}