<%@ WebHandler Language="C#" Class="XeroImplementation" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Emrdev.XeroAPI;
using System.Web.SessionState;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using Microsoft.Office.Interop.Excel;
using System.IO;

//Created by jaswinder 

public class XeroImplementation : IHttpHandler, IRequiresSessionState
{
    //private static Application xlApp;
    //private static Workbook xlWorkbook = null;
    //comment on jan 2018 private static _Worksheet xlWorksheet = null;

    public void ProcessRequest(HttpContext context)
    {

        //For CSV file
        if (HttpContext.Current.Session["FilenameList"] != null)
        {
            //
            //The varibles required for logical purpose
            #region Logical variables

            //int prevInvoice = 0;
            string prevInvoice = string.Empty;
            string ItemType = string.Empty;
            int first = 0;
            bool discountUsed = false;

            #endregion
            //
            //Object creation for Logical Purpose
            #region Logical Objects


            CSVInvoice _csvin = new CSVInvoice();
            List<CSVInvoice> CSVItems = new List<CSVInvoice>();
            List<AddCsvItem> _AddItemList = new List<AddCsvItem>();

            XeroCustomerData _XeroCustomerData = new XeroCustomerData();
            AddCsvItem InvoiceData = new AddCsvItem();
            #endregion

            string FileName = HttpContext.Current.Session["FilenameList"] as string;
            // var _csvdata = System.IO.File.ReadAllText(FileName);

            // string _csvdata = HttpContext.Current.Session["FilenameList"].ToString();
            try
            {
                //
                //Collecting itemlist from CSV from which invoice and credit is being create
                #region collecting possible item list

                foreach (var row in File.ReadLines(FileName))
                // foreach (string row in _csvdata.Split('\n'))
                {
                    if (first == 0)
                    {
                        first++;
                        continue;
                    }
                    if (!string.IsNullOrEmpty(row))
                    {
                        if ((!row.Equals("\r")) && row.Length > 60)
                        {
                            //string data = null;
                            int actualIndex = 0;

                            //
                            //Formaing data if contain one or more comma
                            #region Comma

                            string[] cellData = row.Split(',');
                            for (int index = 0; index < cellData.Length; index++, actualIndex++)
                            {
                                if (cellData[index].StartsWith("\"") && !cellData[index].EndsWith("\""))
                                {
                                    cellData[actualIndex] = cellData[index].Replace("\"", "");
                                    while (!cellData[index].EndsWith("\""))
                                    {
                                        index++;
                                        cellData[actualIndex] = cellData[actualIndex] + " " + cellData[index].Replace("\"", "");
                                    }
                                }
                                else
                                {
                                    cellData[actualIndex] = cellData[index].Replace("\"", "");
                                }
                            }

                            #endregion
                            if (!string.IsNullOrEmpty(cellData[0].ToString()))
                            { continue; }
                            //
                            //Filtering data to differentiate sales, credit and discount
                            #region Filtering Data
                            //XeroApiGateway abc = new XeroApiGateway();
                            //abc.LogRecord("error " + cellData[4].ToString(), Guid.Empty, "Invoice Item Error ", Guid.Empty, " ");
                            string celldata8 = cellData[8];
                            string celldata9 = cellData[9];
                            string celldata10 = cellData[10];
                            string celldata12 = cellData[12];

                            //adding data to invoice
                            InvoiceData = new AddCsvItem();
                            if (!string.IsNullOrEmpty(cellData[1].ToString()))
                            {
                                InvoiceData.Type = cellData[1];
                            }
                            if (!string.IsNullOrEmpty(cellData[2].ToString()))
                            {
                                InvoiceData.NameContact = cellData[2];
                            }

                            if (!string.IsNullOrEmpty(cellData[6].ToString()))
                            {
                                InvoiceData.Name = cellData[6].ToString();
                                ////InvoiceData.Name = cellData[5];
                                //string[] namec = cellData[6].Split(' ');
                                //if (namec.Length >= 3)
                                //{
                                //    InvoiceData.Name = namec[0] + ", " + namec[2];
                                //}
                                //else
                                //{
                                //    if (namec.Length >= 2)
                                //    {
                                //        InvoiceData.Name = namec[0] + ", " + namec[1];
                                //    }
                                //    else
                                //    {
                                //        InvoiceData.Name = namec[0];
                                //    }
                                //}
                            }


                            if (!string.IsNullOrEmpty(cellData[3].ToString()))
                            {
                                //InvoiceData.Date = DateTime.ParseExact(cellData[3], "M/dd/yyyy", null);
                                InvoiceData.Date = Convert.ToDateTime(cellData[3]);

                            }
                            if (!string.IsNullOrEmpty(cellData[4].ToString()))
                            {
                                InvoiceData.Num = "D16 " + cellData[4].ToString();
                            }

                            InvoiceData.Item = cellData[7];
                            InvoiceData.Paid = cellData[11];
                            if (!string.IsNullOrEmpty(cellData[12]))
                            {
                                celldata12 = celldata12.Replace(",", "");
                                celldata12 = celldata12.Replace(" ", "");
                                InvoiceData.Balance = Convert.ToDouble(celldata12);

                            }


                            //Code for Credit Memo
                            if (cellData[1].Equals("Credit Memo"))
                            {
                                InvoiceData.ItemType = "Credit";
                                if (!string.IsNullOrEmpty(cellData[10]))
                                {
                                    celldata10 = celldata10.Replace(",", "");
                                    celldata10 = celldata10.Replace(" ", "");

                                }


                                if (cellData[8] != "")
                                {
                                    celldata8 = celldata8.Replace(",", "");
                                    celldata8 = celldata8.Replace(" ", "");
                                    if (celldata8.Contains("-"))
                                    {
                                        InvoiceData.Qty = -(Convert.ToDouble(celldata8));
                                    }
                                    else
                                    {
                                        InvoiceData.Qty = (Convert.ToDouble(celldata8));
                                    }
                                    if (!string.IsNullOrEmpty(cellData[9].ToString()))
                                    {
                                        celldata9 = celldata9.Replace(",", "");
                                        celldata9 = celldata9.Replace(" ", "");
                                        if (celldata9.Contains("-"))
                                        {
                                            InvoiceData.SalesPrice = (Convert.ToDouble(celldata9));
                                        }
                                        else
                                        {
                                            InvoiceData.SalesPrice = (Convert.ToDouble(celldata9));
                                        }
                                    }


                                }
                                else
                                {
                                    if (cellData[9].Contains('%'))
                                    {


                                        InvoiceData.SalesPrice = -(Convert.ToDouble(celldata10));
                                        InvoiceData.Qty = 1;
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(celldata10))
                                        {
                                            InvoiceData.SalesPrice = -(Convert.ToDouble(celldata10));
                                            InvoiceData.Qty = 1;
                                        }
                                    }
                                }


                            }
                            else //Code for Sales Recipt
                            {

                                InvoiceData.ItemType = "Sales Item";
                                if (!string.IsNullOrEmpty(cellData[10]))
                                {
                                    celldata10 = celldata10.Replace(",", "");
                                    celldata10 = celldata10.Replace(" ", "");

                                }
                                if (!string.IsNullOrEmpty(cellData[8].ToString()))
                                {
                                    //InvoiceData.Qty = Convert.ToInt32(cellData[7].ToString());  
                                    celldata8 = celldata8.Replace(",", "");
                                    celldata8 = celldata8.Replace(" ", "");

                                }
                                if (cellData[8] != "" && Convert.ToDouble(celldata8) > 0)
                                {
                                    InvoiceData.Qty = Convert.ToDouble(celldata8);
                                    if (!string.IsNullOrEmpty(cellData[9].ToString()))
                                    {
                                        celldata9 = celldata9.Replace(",", "");
                                        celldata9 = celldata9.Replace(" ", "");
                                        InvoiceData.SalesPrice = Convert.ToDouble(celldata9);
                                    }


                                }
                                else
                                {
                                    if (cellData[9].Contains('%'))
                                    {
                                        if (celldata10.Contains("-"))
                                        {
                                            InvoiceData.SalesPrice = Convert.ToDouble(celldata10);
                                            InvoiceData.Qty = 1;
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(celldata10))
                                            {
                                                InvoiceData.SalesPrice = Convert.ToDouble(celldata10);
                                                InvoiceData.Qty = 1;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (celldata10.Contains("-"))
                                        {
                                            InvoiceData.SalesPrice = Convert.ToDouble(celldata10);
                                            InvoiceData.Qty = 1;
                                        }
                                        if (!string.IsNullOrEmpty(celldata10))
                                        {
                                            InvoiceData.SalesPrice = Convert.ToDouble(celldata10);
                                            InvoiceData.Qty = 1;
                                        }




                                    }
                                }
                            }

                            _AddItemList.Add(InvoiceData);

                            InvoiceData = null;
                        }
                            #endregion
                    }
                }
                #endregion

                foreach (var InvoiceItem in _AddItemList)
                {
                    //if (prevInvoice != InvoiceItem.Num && prevInvoice != 0)
                    if (prevInvoice != InvoiceItem.Num && !string.IsNullOrEmpty(prevInvoice))
                    {
                        CSVItems.Add(_csvin);
                        _csvin = new CSVInvoice();
                    }
                    else if (ItemType != InvoiceItem.ItemType && prevInvoice == InvoiceItem.Num)
                    {
                        CSVItems.Add(_csvin);
                        _csvin = new CSVInvoice();

                    }


                    _csvin.Type = InvoiceItem.ItemType;
                    if (!string.IsNullOrEmpty(InvoiceItem.NameContact))
                    {
                        _csvin.NameContact = InvoiceItem.NameContact;
                    }
                    else
                    {
                        _csvin.NameContact = InvoiceItem.Name;
                    }
                    _csvin.Name = InvoiceItem.Name;
                    _csvin.ItemType = InvoiceItem.ItemType;
                    _csvin.Date = InvoiceItem.Date;
                    _csvin.Amount += InvoiceItem.SalesPrice;

                    _csvin.Num = InvoiceItem.Num;

                    _csvin.Name = InvoiceItem.NameContact;

                    _csvin.InvoiceID = Guid.NewGuid();

                    //if (InvoiceItem.ItemType == "Credit")
                    //{

                    //}
                    //else
                    //{
                    //    if (InvoiceItem.Balance < 0)
                    //    { InvoiceItem.Balance = 0; }
                    //}
                    _csvin.ItemList.Add(new AddCsvItem
                    {
                        ItemType = InvoiceItem.ItemType,
                        Discount = InvoiceItem.Discount,
                        Item = InvoiceItem.Item,
                        SalesPrice = InvoiceItem.SalesPrice,
                        Qty = InvoiceItem.Qty,
                        Num = InvoiceItem.Num,
                        Amount = (InvoiceItem.Qty) * (InvoiceItem.SalesPrice),
                        Paid = InvoiceItem.Paid,
                        Balance = InvoiceItem.Balance,
                    });
                    prevInvoice = InvoiceItem.Num;
                    ItemType = InvoiceItem.ItemType;
                }
                CSVItems.Add(_csvin);

                _XeroCustomerData.SaveCSVData(CSVItems);
                //prevInvoice = 0;
                prevInvoice = string.Empty;
                _AddItemList.Clear();
                _csvin = null;
                CSVItems.Clear();

                _XeroCustomerData = null;
                HttpContext.Current.Session["FilenameList"] = null;
                HttpContext.Current.Response.Redirect("./PatientFormXERO.aspx", false);
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                HttpContext.Current.Response.Redirect("./admin_main.aspx", false);
            }
        }

        //Exporting Tax rate to Xero
        if (HttpContext.Current.Session["TaxFilenameList"] != null)
        {

            try
            {
               /* string FileName = HttpContext.Current.Session["TaxFilenameList"] as string;
                IXeroAPIService objIXeroAPIService = new XeroAPIService();
                XeroCustomerData _XeroCustomerData = new XeroCustomerData();
                List<AddTax> _AddTaxList = new List<AddTax>();
                if (System.IO.File.Exists(FileName))
                {
                    // then go and load this into excel
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlWorkbook = xlApp.Workbooks.Open(FileName, true, true);
                    //xlWorksheet = (_Worksheet)xlApp.ActiveWorkbook.ActiveSheet;
                    xlWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlApp.Sheets[1]; // Explicit cast is not required here
                    Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                    int rCnt = 0;
                    int cCnt = 0;
                    int strcount = 0;
                    int repoCount = 0;
                    for (rCnt = 1; rCnt <= xlRange.Rows.Count; rCnt++)
                    {
                        for (cCnt = 1; cCnt <= xlRange.Columns.Count; cCnt++)
                        {
                            string str = (string)(xlRange.Cells[rCnt, cCnt] as Microsoft.Office.Interop.Excel.Range).Value2;

                            if (strcount == 0)
                            {
                                if (!string.IsNullOrEmpty(str))
                                {
                                    strcount = 1;
                                    cCnt = xlRange.Columns.Count + 1;
                                }
                            }
                            else
                            {
                                AddTax _AddTax = new AddTax();
                                string TaxName = (string)(xlRange.Cells[rCnt, cCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                                if (TaxName.Length > 25)
                                {
                                    TaxName = TaxName.Substring(0, 25);

                                }
                                _AddTax.Name = TaxName + " " + (string)(xlRange.Cells[rCnt, cCnt + 1] as Microsoft.Office.Interop.Excel.Range).Value2 + " " + (string)(xlRange.Cells[rCnt, cCnt + 2] as Microsoft.Office.Interop.Excel.Range).Value2.ToString();
                                List<AddTaxComponent> _AddTaxComponentList = new List<AddTaxComponent>();
                                AddTaxComponent _AddTaxComponent;
                                _AddTaxComponent = new AddTaxComponent();
                                _AddTaxComponent.Name = "Local Rate";
                                _AddTaxComponent.Rate = Convert.ToDecimal((xlRange.Cells[rCnt, cCnt + 3] as Microsoft.Office.Interop.Excel.Range).Value2);
                                _AddTaxComponent.Rate = _AddTaxComponent.Rate * 100;
                                _AddTaxComponent.IsCompound = false;
                                _AddTaxComponentList.Add(_AddTaxComponent);

                                _AddTaxComponent = new AddTaxComponent();
                                _AddTaxComponent.Name = "State Rate";
                                _AddTaxComponent.Rate = Convert.ToDecimal((xlRange.Cells[rCnt, cCnt + 4] as Microsoft.Office.Interop.Excel.Range).Value2);
                                _AddTaxComponent.Rate = _AddTaxComponent.Rate * 100;
                                _AddTaxComponent.IsCompound = false;
                                _AddTaxComponentList.Add(_AddTaxComponent);

                                _AddTax.TaxComponent = _AddTaxComponentList;

                                _XeroCustomerData.AddTaxes(_AddTax);
                                //_AddTaxList.Add(_AddTax);
                                cCnt = xlRange.Columns.Count + 1;
                                repoCount = repoCount + 1;
                                if (repoCount > 50)
                                {
                                    repoCount = 0;
                                    _XeroCustomerData.CreateRepository();
                                }
                            }

                        }
                    }
                    //_XeroCustomerData.SaveTaxData(_AddTaxList);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                    xlApp = null;
                    System.Windows.Forms.Application.Exit();
                }

                else
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                    xlApp = null;
                    System.Windows.Forms.Application.Exit();
                }
                HttpContext.Current.Session["TaxFilenameList"] = null;
                HttpContext.Current.Response.Redirect("./PatientFormXERO.aspx", false);*/
            }
            catch (SystemException ex)
            {
                HttpContext.Current.Response.Redirect("./admin_main.aspx", false);
            }
        }

        //End tax rates        


        //Send patients data to Xero
        if (HttpContext.Current.Session["CostomersList"] != null)
        {
            try
            {

                string _Customerid = HttpContext.Current.Session["CostomersList"] as string;
                IXeroAPIService objIXeroAPIService = new XeroAPIService();
                IQBCustMatchPatientService objIQBCustMatchPatientService = new QBCustMatchPatientService();
                int patientId = Convert.ToInt16(_Customerid);

                PatientViewModel pat = objIQBCustMatchPatientService.GetPatientDetailById(patientId);
                if (pat != null)
                {
                    XeroCustomerData _XeroCustomerData = new XeroCustomerData();

                    List<AddPhone> _AddPhoneList = new List<AddPhone>();
                    AddPhone _AddPhone = null;

                    _AddPhone = new AddPhone();
                    _AddPhone.PhoneType = "DEFAULT";
                    _AddPhone.PhoneNumber = pat.HomePhone;
                    _AddPhoneList.Add(_AddPhone);



                    _AddPhone = new AddPhone();
                    _AddPhone.PhoneType = "MOBILE";
                    _AddPhone.PhoneNumber = pat.CellPhone;
                    _AddPhoneList.Add(_AddPhone);


                    _AddPhone = new AddPhone();
                    _AddPhone.PhoneType = "FAX";
                    _AddPhone.PhoneNumber = pat.FaxPone;
                    _AddPhoneList.Add(_AddPhone);


                    _AddPhone = new AddPhone();
                    _AddPhone.PhoneType = "DDI";
                    _AddPhone.PhoneNumber = pat.WorkPhone;
                    _AddPhoneList.Add(_AddPhone);


                    Addcustomer _AddCustomer = new Addcustomer();

                    List<AddAddress> _AddAddressesList = new List<AddAddress>();
                    AddAddress _AddAddAddres = null;

                    _AddAddAddres = new AddAddress();
                    _AddAddAddres.AddressType = "POBOX";
                    _AddAddAddres.AddressLine1 = pat.BillingStreet;
                    _AddAddAddres.City = pat.BillingCity;
                    _AddAddAddres.Country = "USA";
                    _AddAddAddres.PostalCode = pat.BillingZip;
                    _AddAddAddres.Region = pat.BillingState;
                    _AddAddressesList.Add(_AddAddAddres);

                    _AddAddAddres = new AddAddress();
                    _AddAddAddres.AddressType = "STREET";
                    _AddAddAddres.AddressLine1 = pat.ShippingStreet;
                    _AddAddAddres.City = pat.ShippingCity;
                    _AddAddAddres.Country = "USA";
                    _AddAddAddres.PostalCode = pat.ShippingZip;
                    _AddAddAddres.Region = pat.ShippingState;

                    _AddAddressesList.Add(_AddAddAddres);

                    _AddCustomer.Address = _AddAddressesList;
                    _AddCustomer.ContactNumber = pat.PatientID.ToString();
                    if (!string.IsNullOrEmpty(pat.XeropatientId.ToString()))
                    {
                        Guid ContactIDGuid = Guid.Empty;
                        string ContactID = pat.XeropatientId.ToString();
                        ContactIDGuid = new Guid(ContactID);
                        _AddCustomer.ContactID = ContactIDGuid;
                    }

                    _AddCustomer.EmailAddress = pat.Email;
                    _AddCustomer.FirstName = pat.FirstName;
                    _AddCustomer.LastName = pat.LastName;
                    _AddCustomer.Phone = _AddPhoneList;
                    _AddCustomer.ProviderName = pat.LMC_CP;
                    if (string.IsNullOrEmpty(pat.MiddleInitial))
                    {
                        _AddCustomer.Name = pat.FirstName + " " + pat.LastName;
                    }
                    else
                    {
                        _AddCustomer.Name = pat.FirstName + " " + pat.MiddleInitial + " " + pat.LastName;
                    }
                    _AddCustomer.ContactPerson = pat.EmergencyLastName + " " + pat.EmergencyFirstName + " ( " + pat.EmergencyRelationship + " ) -" + pat.EmergencyPhone.ToString();

                    _XeroCustomerData.AddCustomers(_AddCustomer);
                }

                HttpContext.Current.Session["CostomersList"] = null;
                if (!string.IsNullOrEmpty(pat.XeropatientId.ToString()))
                {
                    HttpContext.Current.Response.Redirect("./PatientInfo.aspx?PatientID=" + pat.PatientID, false);
                }
                else
                { HttpContext.Current.Response.Redirect("./LandingPAge.aspx", false); }

            }
            catch (SystemException ex)
            {

                HttpContext.Current.Response.Redirect("./admin_main.aspx", false);
            }
        }

        //end of add contacts

        //For impoting the contacts and matching with the database
        if (HttpContext.Current.Session["ImportContactsList"] != null)
        {
            try
            {
                XeroCustomerData _XeroCustomerData = new XeroCustomerData();

                _XeroCustomerData.GetContacts();
                HttpContext.Current.Session["ImportContactsList"] = null;

                HttpContext.Current.Response.Redirect("./PatientFormXERO.aspx", false);

            }
            catch (SystemException ex)
            {
                HttpContext.Current.Session["ImportContactsList"] = null;
                HttpContext.Current.Response.Redirect("./admin_main.aspx", false);
            }

        }


        //For impoting the Accounts and matching with the database
        if (HttpContext.Current.Session["ImportAccountList"] != null)
        {
            try
            {
                XeroCustomerData _XeroCustomerData = new XeroCustomerData();

                _XeroCustomerData.GetAccounts();
                HttpContext.Current.Session["ImportAccountList"] = null;

                HttpContext.Current.Response.Redirect("./PatientFormXERO.aspx", false);

            }
            catch (SystemException ex)
            {
                HttpContext.Current.Session["ImportContactsList"] = null;
                HttpContext.Current.Response.Redirect("./admin_main.aspx", false);
            }

        }

        //For updating Invoice Payments 
        if (HttpContext.Current.Session["AccountCode"] != null)
        {
            try
            {
                string OrderId = HttpContext.Current.Session["OrderId"] as string;
              
                IXeroAPIService objIXeroAPIService = new XeroAPIService();
                List<XeroOrders> _ListInvoice = objIXeroAPIService.GetXeroInvoiceByID(OrderId).ToList();
                
                AddInvoice _AddInvoice = new AddInvoice();
                foreach (var Invoice in _ListInvoice)
                {
                   
                    string QBInvID = Invoice.QBInvID.ToString();
                    if (!string.IsNullOrEmpty(QBInvID))
                    {
                        _AddInvoice.InvoiceNumber = Invoice.orderid.ToString();
                        _AddInvoice.DueDate = Convert.ToDateTime(Invoice.DatePrep);
                        _AddInvoice.Status = "Authorised";
                        _AddInvoice.InvoiceID = new Guid(QBInvID);
                       
                    }
                }

                XeroCustomerData _XeroCustomerData = new XeroCustomerData();
                _XeroCustomerData.AddPayment(_AddInvoice, HttpContext.Current.Session["Amount"].ToString(), HttpContext.Current.Session["PaymentDate"].ToString(), HttpContext.Current.Session["AccountCode"].ToString(), HttpContext.Current.Session["PaymentRefrence"].ToString());
                HttpContext.Current.Session["AccountCode"] = null;
                HttpContext.Current.Response.Redirect("./PatientFormXERO.aspx", false);

            }
            catch (SystemException ex)
            {
                HttpContext.Current.Session["ImportContactsList"] = null;
                HttpContext.Current.Response.Redirect("./admin_main.aspx", false);
            }

        }

        //For Orders
        if (HttpContext.Current.Session["Invoicelist"] != null)
        {
            try
            {

                string _Invoiceid = HttpContext.Current.Session["Invoicelist"] as string;
                IXeroAPIService objIXeroAPIService = new XeroAPIService();

                //Get the orders details
                List<XeroOrders> _ListInvoice = objIXeroAPIService.GetXeroInvoiceByID(_Invoiceid).ToList();

                XeroCustomerData _XeroCustomerData = new XeroCustomerData();
                var Invoicelist = HttpContext.Current.Session["Invoicelist"];
                foreach (var Invoice in _ListInvoice)
                {
                    AddInvoice _AddInvoice = new AddInvoice();
                    objIXeroAPIService = new XeroAPIService();
                    IQBCustMatchPatientService objIQBCustMatchPatientService = new QBCustMatchPatientService();

                    //Add patient to xero if QBID is null
                    PatientViewModel pat = objIQBCustMatchPatientService.GetPatientDetailById(Invoice.PatientID);
                    string QBID = Invoice.QBid.ToString();
                    if (!string.IsNullOrEmpty(QBID))
                    {
                        string _Customerid = Invoice.PatientID.ToString();
                       
                        if (pat != null)
                        {
                            
                                _XeroCustomerData = new XeroCustomerData();

                                List<AddPhone> _AddPhoneList = new List<AddPhone>();
                                AddPhone _AddPhone = null;

                                _AddPhone = new AddPhone();
                                _AddPhone.PhoneType = "DEFAULT";
                                _AddPhone.PhoneNumber = pat.HomePhone;
                                _AddPhoneList.Add(_AddPhone);



                                _AddPhone = new AddPhone();
                                _AddPhone.PhoneType = "MOBILE";
                                _AddPhone.PhoneNumber = pat.CellPhone;
                                _AddPhoneList.Add(_AddPhone);


                                _AddPhone = new AddPhone();
                                _AddPhone.PhoneType = "FAX";
                                _AddPhone.PhoneNumber = pat.FaxPone;
                                _AddPhoneList.Add(_AddPhone);


                                _AddPhone = new AddPhone();
                                _AddPhone.PhoneType = "DDI";
                                _AddPhone.PhoneNumber = pat.WorkPhone;
                                _AddPhoneList.Add(_AddPhone);


                                Addcustomer _AddCustomer = new Addcustomer();



                                List<AddAddress> _AddAddressesList = new List<AddAddress>();
                                AddAddress _AddAddAddres = null;

                                _AddAddAddres = new AddAddress();
                                _AddAddAddres.AddressType = "POBOX";
                                _AddAddAddres.AddressLine1 = pat.BillingStreet;
                                _AddAddAddres.City = pat.BillingCity;
                                _AddAddAddres.Country = "USA";
                                _AddAddAddres.PostalCode = pat.BillingZip;
                                _AddAddAddres.Region = pat.BillingState;
                                _AddAddressesList.Add(_AddAddAddres);

                                _AddAddAddres = new AddAddress();
                                _AddAddAddres.AddressType = "STREET";
                                _AddAddAddres.AddressLine1 = pat.ShippingStreet;
                                _AddAddAddres.City = pat.ShippingCity;
                                _AddAddAddres.Country = "USA";
                                _AddAddAddres.PostalCode =pat.ShippingZip;
                                _AddAddAddres.Region = pat.ShippingState;

                                _AddAddressesList.Add(_AddAddAddres);

                                _AddCustomer.Address = _AddAddressesList;
                                _AddCustomer.ContactNumber = pat.PatientID.ToString();
                                if (!string.IsNullOrEmpty(pat.XeropatientId.ToString()))
                                {
                                    Guid ContactIDGuid = Guid.Empty;
                                    string ContactID = pat.XeropatientId.ToString();
                                    ContactIDGuid = new Guid(ContactID);
                                    _AddCustomer.ContactID = ContactIDGuid;
                                }

                                _AddCustomer.EmailAddress = pat.Email;
                                _AddCustomer.FirstName = pat.FirstName;
                                _AddCustomer.LastName = pat.LastName;
                                _AddCustomer.Phone = _AddPhoneList;
                                _AddCustomer.ProviderName = pat.LMC_CP;
                                if (string.IsNullOrEmpty(pat.MiddleInitial))
                                {
                                    _AddCustomer.Name = pat.FirstName + " " + pat.LastName;
                                }
                                else
                                {
                                    _AddCustomer.Name = pat.FirstName + " " + pat.MiddleInitial + " " + pat.LastName;
                                }
                                _AddCustomer.ContactPerson = pat.EmergencyLastName + " " + pat.EmergencyFirstName + " ( " + pat.EmergencyRelationship + " ) -" + pat.EmergencyPhone.ToString();
                                if (string.IsNullOrEmpty(pat.XeropatientId.ToString()))
                                {
                                    _XeroCustomerData.AddCustomers(_AddCustomer);

                                    if (HttpContext.Current.Session["MerchantID"] != null)
                                        _AddInvoice.ContactID = Guid.Parse(HttpContext.Current.Session["MerchantID"].ToString());
                                }
                                else
                                {
                                    _AddInvoice.ContactID = Guid.Parse(pat.XeropatientId.ToString());   
                                }
                            }
                        }

                   

                    else
                    {
                        _AddInvoice.ContactID = Invoice.QBid;
                    }
                    _AddInvoice.Medical = pat.Medical;
                    _AddInvoice.ProviderName = pat.LMC_CP;
                    _AddInvoice.InvoiceNumber = Invoice.orderid.ToString();
                    _AddInvoice.DueDate = Convert.ToDateTime(Invoice.DatePrep);
                    _AddInvoice.Status = "DRAFT";
                    if (!string.IsNullOrEmpty(Invoice.QBInvID.ToString()))
                    {
                        Guid InvoiceIDGuid = Guid.Empty;
                        string InvoiceId = Invoice.QBInvID.ToString();
                        InvoiceIDGuid = new Guid(InvoiceId);
                        _AddInvoice.InvoiceID = InvoiceIDGuid;
                    }

                    //Get order items details
                    List<XeroInvoiceItems> _InvoiceItemList = objIXeroAPIService.GetXeroInvoiceItemsByID(Invoice.orderid).ToList();
                    List<AddItem> _AddItemList = new List<AddItem>();
                    decimal Total = 0;
                    bool autoshipDisc = false;
                    foreach (var Items in _InvoiceItemList)
                    {
                        AddItem _newAddItem = new AddItem();
                        _newAddItem.ItemCode = Items.Sku;
                        _newAddItem.Description = Items.ProductName;
                        _newAddItem.LineAmount = Convert.ToDecimal(Convert.ToDecimal(Items.Price) * Convert.ToDecimal(Items.Quantity));
                        _newAddItem.Quantity = Convert.ToDecimal(Items.Quantity);
                        _newAddItem.UnitAmount = Convert.ToDecimal(Items.Price);
                        AddItem _newAddDiscountItem = new AddItem();
                        if (Items.DiscountID > 0)
                        {
                           // _newAddItem.Description = _newAddItem.Description + "( " + Items.DiscountName + " )";
                            if (Items.Percent > 0)
                            {
                                _newAddItem.Discount = Convert.ToDecimal(Items.Percent);
                            }
                            else if (Items.Dollar > 0)
                            {
                                decimal percent = Convert.ToDecimal(Items.Dollar) *100;
                                 percent = Convert.ToDecimal(percent)/_newAddItem.LineAmount;
                                _newAddItem.Discount = percent;
                            }
                            _newAddDiscountItem.Description ="Autoship Discount: "+ Items.DiscountName;
                            _newAddDiscountItem.LineAmount = 0;
                            _newAddDiscountItem.Quantity = 1;
                            _newAddDiscountItem.UnitAmount = 0;
                             
                        }
                        Total = Total + (_newAddItem.LineAmount - ((_newAddItem.LineAmount * _newAddItem.Discount)/100));
                        _AddItemList.Add(_newAddItem);
                        if (Items.DiscountID != 1)
                        {
                            if (_newAddDiscountItem != null)
                            {
                                _AddItemList.Add(_newAddDiscountItem);
                            }
                        }

                        else if (Items.DiscountID == 1)
                        {
                            autoshipDisc = true;
                        }

                    }
                    if (autoshipDisc == true)
                    {
                        AddItem _AddItem = new AddItem();
                        _AddItem.Description = System.Configuration.ConfigurationManager.AppSettings["XeroDiscountDiscription"];
                        _AddItem.Quantity = 1;
                        _AddItem.UnitAmount = 0;
                        _AddItem.LineAmount = 0;
                        _AddItemList.Add(_AddItem);
                    }
                                                           
                    _AddInvoice.ItemList = _AddItemList;
                    _AddInvoice.Total = Total;
                    _AddInvoice.ShippingFee = Invoice.ShippingFee;
                    _AddInvoice.OrderLimit = Invoice.OrderLimit;
                    _AddInvoice.SalesAccountCode = Invoice.SalesAccountCode;
                    _AddInvoice.ShippingAddress = "Shipping Address: " + _ListInvoice[0].ShipAddress1 + "  " + _ListInvoice[0].ShipCity + " , " + _ListInvoice[0].ShipState + "  " + _ListInvoice[0].ShipZip;
                    _XeroCustomerData.AddInvoices(_AddInvoice);


                    HttpContext.Current.Session["Invoicelist"] = null;
                    HttpContext.Current.Session["TabName"] = "AutoShip";
                    HttpContext.Current.Response.Redirect("./Autoship/AutoShip.aspx", false);

                }
                HttpContext.Current.Session["TabName"] = "AutoShip";
                HttpContext.Current.Response.Redirect("./Autoship/AutoShip.aspx", false);
                //HttpContext.Current.Response.Redirect("/MerchantReports/UpdateNotificationReckon?Success=Yes&ReportTypeFromAPI=Invoice", false);
            }
            catch
            {
                HttpContext.Current.Session["TabName"] = "";
                HttpContext.Current.Response.Redirect("./admin_main.aspx", false);
                //HttpContext.Current.Response.Redirect("/MerchantReports/UpdateNotificationReckon?Success=No&ReportTypeFromAPI=Invoice");
            }

        }
        //End of orders

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}