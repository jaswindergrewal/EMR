<%@ WebHandler Language="C#" Class="XeroAuthonticationCall" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Emrdev.XeroAPI;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

//Created by Jaswinder 
//To authorise the credentials of Xero

public class XeroAuthonticationCall : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["checkedfile"] != null && context.Request.QueryString["checkedfile"] != "")
        {
            string _FileName = context.Request.QueryString["checkedfile"];
            HttpContext.Current.Session["FilenameList"] = _FileName;
        }

        if (context.Request.QueryString["checkedTaxfile"] != null && context.Request.QueryString["checkedTaxfile"] != "")
        {
            string _FileName = context.Request.QueryString["checkedTaxfile"];
            HttpContext.Current.Session["TaxFilenameList"] = _FileName;
        }

        if (context.Request.QueryString["checkedMerchantiDS"] != null && context.Request.QueryString["checkedMerchantiDS"] != "")
        {
            var CustomerIds = context.Request.QueryString["checkedMerchantiDS"];
            HttpContext.Current.Session["CostomersList"] = CustomerIds;
        }

        if (context.Request.QueryString["ImportContactsList"] != null && context.Request.QueryString["ImportContactsList"] != "")
        {
            string contacts = context.Request.QueryString["ImportContactsList"];
            HttpContext.Current.Session["ImportContactsList"] = contacts;
        }

        if (context.Request.QueryString["ImportAccountList"] != null && context.Request.QueryString["ImportAccountList"] != "")
        {
            string Accounts = context.Request.QueryString["ImportAccountList"];
            HttpContext.Current.Session["ImportAccountList"] = Accounts;
        }
        

        if (context.Request.QueryString["checkedInvoiceiDS"] != null && context.Request.QueryString["checkedInvoiceiDS"] != "")
        {
            var InvoiceIds = context.Request.QueryString["checkedInvoiceiDS"];
            HttpContext.Current.Session["Invoicelist"] = InvoiceIds;
        }

        if (context.Request.QueryString["AccountCode"] != null && context.Request.QueryString["AccountCode"] != "")
        {
            string AccountCode = context.Request.QueryString["AccountCode"];
            HttpContext.Current.Session["AccountCode"] = AccountCode;

            string Amount = context.Request.QueryString["Amount"];
            HttpContext.Current.Session["Amount"] = Amount;

            string PaymentDate = context.Request.QueryString["PaymentDate"];
            HttpContext.Current.Session["PaymentDate"] = PaymentDate;

            string PaymentRefrence = context.Request.QueryString["PaymentRefrence"];
            HttpContext.Current.Session["PaymentRefrence"] = PaymentRefrence;

            string OrderId = context.Request.QueryString["OrderId"];
            HttpContext.Current.Session["OrderId"] = OrderId;
        }
       
        XeroCustomerData _XeroCustomerData = new XeroCustomerData();
        _XeroCustomerData.CreateRepository();
        HttpContext.Current.Response.Redirect("http://" + System.Configuration.ConfigurationManager.AppSettings["WebUrl"] + "//XeroImplementation.ashx");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}