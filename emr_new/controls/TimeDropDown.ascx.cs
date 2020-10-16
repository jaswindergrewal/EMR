using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_TimeDropDown : System.Web.UI.UserControl
{
	DateTime _startTime = DateTime.MinValue;
	public DateTime StartTime
	{
		set
		{
			_startTime = value;
			Populate(_startTime);
			
		}
	}


	public string CssClass
	{
		get
		{
			return MainDrop.CssClass;
		}
		set
		{
			MainDrop.CssClass = value;
		}
	}

	public string SelectedValue
	{
		get
		{
			return MainDrop.SelectedValue;
		}
		set
		{
			MainDrop.SelectedValue = value;
		}
	}

	public string Text
	{
		get
		{
			if (MainDrop.Text != "")
			{
				Populate(DateTime.Parse(DateTime.Now.ToShortDateString() + " " + MainDrop.Text));
				return MainDrop.Text;
			}
			else
				return MainDrop.SelectedValue;
		}
		set
		{
			MainDrop.Text = value;
		}
	}

	public string ToolTip
	{
		get
		{
			return MainDrop.ToolTip;
		}
		set
		{
			MainDrop.ToolTip = value;
		}
	}


	protected void Page_Load(object sender, EventArgs e)
	{

	}

	private void Populate(DateTime beginTime)
	{
		int newInterval = (int)Session["Interval"];
		string minsAfterHour = beginTime.ToShortTimeString().Split(':')[1];
		DateTime currentTime = DateTime.Parse(beginTime.ToShortDateString() + " 12:" + minsAfterHour.Replace("PM","AM")).AddMinutes(newInterval);
		bool selectedOne = false;
		MainDrop.Items.Clear();
		while (currentTime < beginTime.AddDays(1))
		{
			ListItem itm = new ListItem(currentTime.ToShortTimeString());

			if (currentTime.ToShortTimeString() == beginTime.ToShortTimeString() && !selectedOne)
			{
				itm.Selected = true;
				selectedOne=true;
			}
			MainDrop.Items.Add(itm);
			currentTime = currentTime.AddMinutes(newInterval);
		}
		MainDrop.Text = beginTime.ToShortTimeString();
	}

}