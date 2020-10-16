using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Logging;
using DevDefined.OAuth.Storage.Basic;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using XeroApi;
using XeroApi.Model;
using XeroApi.OAuth;


namespace Emrdev.XeroAPI
{
    public class XeroApiGateway
    {

        IXeroAPIService objService;
        public string UserAgent;
        public string ConsumerKey;
        public string ConsumerSecret;

        public XeroApiGateway()
        {
            objService = new XeroAPIService();
            // XeroCredentialViewModel objXerocredentials = objService.GetXeroCredential();
            UserAgent = "Xero.API.ScreenCast v1.0 (Public App Testing)";
            UserAgent = "Xero.API.ScreenCast v1.0 (Public App Testing)";

            ConsumerKey = System.Configuration.ConfigurationManager.AppSettings["ConsumerKey"];// "JM8WNZYEA5RYEPH7OFFPEDHVPJXBX9";
            ConsumerSecret = System.Configuration.ConfigurationManager.AppSettings["ConsumerSecret"];// "CB9369F6453A5D20CEA7FEF713A741D1BBCCCC2A";

        }


        public ConsumerSessionData CreateRepository()
        {

            // private application
            IOAuthSession consumerSession = new XeroApiPrivateSession(UserAgent,
            ConsumerKey, CertificateRepository.GetOAuthSigningCertificate(optionalVar: ConsumerSecret));
            consumerSession.MessageLogger = new DebugMessageLogger();
            string userAgent = HttpContext.Current.Request.Browser.Browser;
            HttpContext.Current.Session["consumerSession"] = new Repository(consumerSession);
            //HttpContext.Current.Session["consumerSession"] = consumerSession;
            ConsumerSessionData _obj = new ConsumerSessionData();
            _obj.CunsumerSession = consumerSession;
            return _obj;
        }

        public void AccessToken(SessionData _SessionData, ConsumerSessionData _obj)
        {

            var verificationCode = _SessionData.oauth_verifier;
            AccessToken accessToken = new AccessToken();


            var consumerSession = _obj.CunsumerSession as IOAuthSession;
            try
            {
                accessToken = consumerSession.ExchangeRequestTokenForAccessToken(verificationCode);
            }
            catch (OAuthException ex)
            {
                Console.WriteLine("An OAuthException was caught:");
                Console.WriteLine(ex.Report);
            }

            // Wrap the authenticated consumerSession in the repository...
            //return new Repository(consumerSession);
            HttpContext.Current.Session["consumerSession"] = new Repository(consumerSession);
        }




        //create taxes

        public void CreatingTaxWithValidationErrors(AddTax _AddTax)
        {
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            TaxComponents _newTaxComponents = new TaxComponents();
            TaxComponent _newTaxComponent;
            foreach (var taxcomponent in _AddTax.TaxComponent)
            {
                _newTaxComponent = new TaxComponent();
                _newTaxComponent.Name = taxcomponent.Name;
                _newTaxComponent.Rate = taxcomponent.Rate;
                _newTaxComponents.Add(_newTaxComponent);
            }

            var TaxRate = new TaxRate
            {
                Name = _AddTax.Name,
                CanApplyToAssets = true,
                CanApplyToEquity = true,
                CanApplyToExpenses = true,
                CanApplyToLiabilities = true,
                Status = "ACTIVE",
                DisplayTaxRate = 0,
                EffectiveRate = 0,
                TaxType = "INPUT",
                TaxComponents = _newTaxComponents,
            };

            TaxRate createdTax = repository.Create(TaxRate);
        }

        //create taxes

        public void CreatingTaxListWithValidationErrors(List<AddTax> _AddTax)
        {
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            TaxComponents _newTaxComponents;
            TaxComponent _newTaxComponent;

            int pageNo = 0;
            //Logical Object

            List<TaxRate> taxList;
            _AddTax = _AddTax.OrderBy(x => x.Name).ToList();
            int totalPages = (_AddTax.Count() / 50) + 1;
            for (pageNo = 0; pageNo <= totalPages; pageNo++)
            {
                var temp = _AddTax.Skip(50 * pageNo).Take(50).ToList();

                taxList = new List<TaxRate>();
                foreach (var taxData in temp)
                {
                    _newTaxComponents = new TaxComponents();
                    foreach (var taxcomponent in taxData.TaxComponent)
                    {
                        _newTaxComponent = new TaxComponent();
                        _newTaxComponent.Name = taxcomponent.Name;
                        _newTaxComponent.Rate = taxcomponent.Rate;
                        _newTaxComponents.Add(_newTaxComponent);
                    }


                    var TaxRate = new TaxRate
                    {
                        Name = taxData.Name,
                        CanApplyToAssets = true,
                        CanApplyToEquity = true,
                        CanApplyToExpenses = true,
                        CanApplyToLiabilities = true,
                        Status = "ACTIVE",
                        DisplayTaxRate = 0,
                        EffectiveRate = 0,
                        TaxType = "INPUT",
                        TaxComponents = _newTaxComponents,
                    };

                    // TaxRate createdTax = repository.Create(TaxRate);

                    taxList.Add(TaxRate);

                }
                if (taxList.Count > 0)
                {
                    var a = repository.Create<TaxRate>(taxList);
                }
            }
        }


        /*

        /*
         * 
         * 
      
         * 
         * 
        public void CreateAnyPurchasesInvoice(AddInvoice _AddInvoice)
        {
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            LineItems _objLineItemList = new LineItems();
            foreach (var Item in _AddInvoice.ItemList)
            {
                LineItem _objLineItem = new LineItem();
                //_objLineItem.AccountCode = "200";
                _objLineItem.Description = Item.Description;
                _objLineItem.UnitAmount = Item.UnitAmount;
                //_objLineItem.TaxAmount = 2m;
                _objLineItem.LineAmount = Item.LineAmount;
                _objLineItem.Quantity = Item.Quantity;
                _objLineItemList.Add(_objLineItem);
            }

            var invoice = new Invoice
            {
                Type = "ACCREC",
                Contact = new Contact { Name = "KKSS" },
                Date = DateTime.Today,
                DueDate = _AddInvoice.DueDate,
                Status = "Draft",
                Total = _AddInvoice.Total,
                InvoiceID = _AddInvoice.InvoiceID,
                SubTotal = _AddInvoice.SubTotal,
                CurrencyCode = "AUD",
                InvoiceNumber = _AddInvoice.InvoiceNumber,
                TotalDiscount = _AddInvoice.TotalDiscount,
                LineItems = _objLineItemList,

            };

            var createdInvoice = repository.Create(invoice);
        }
        */


        //Create csv invoice as List
        public void CreatingCSVInvoiceWithValidationErrorsList(List<CSVInvoice> _csvin)
        {
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            int pageNo = 0;
            int invpageNo = 0;
            //Logical Object
            string ItemSubstr = string.Empty;
            string ItemDesc = string.Empty;
            IXeroAPIService _objService = new XeroAPIService();
            Insert_CSV _InserCSVModel = new Insert_CSV();
            Insert_CSVItem _csvItem = new Insert_CSVItem();

            List<Invoice> ListOfInvoice = new List<Invoice>();
            List<Payment> ListofPayment = new List<Payment>();
            List<CreditNote> ListofCreditNote = new List<CreditNote>();
            var ItemListcre = new List<Item>();
            var listinau = new List<Invoice>();
            var listiathCredit = new List<CreditNote>();
            var ItemList = new List<Item>();
            var CretedInvoiceList = new List<Invoice>();
            var CreatedCreditNoteList = new List<CreditNote>();
            var ItemTemp = new List<Item>();
            var _chkExistingCreditInv = new CreditNote();
            var _chkExistingInv = new Invoice();
            _csvin = _csvin.OrderBy(x => x.Num).ToList();
            int totalPages = (_csvin.Count() / 100) + 1;
            for (pageNo = 0; pageNo <= totalPages; pageNo++)
            {
                var temp = _csvin.Skip(100 * pageNo).Take(100).ToList();
                foreach (var Invoicedata in temp)
                {
                    //Create the LineItems
                    LineItems _objLineItemList = new LineItems();
                    if (Invoicedata.ItemType == "Credit")
                    {
                        _chkExistingCreditInv = new CreditNote();
                        _chkExistingCreditInv = ListofCreditNote.Where(x => x.CreditNoteNumber == Convert.ToString(Invoicedata.Num) && x.Contact.Name == Invoicedata.NameContact).FirstOrDefault();
                    }
                    else
                    {
                        _chkExistingInv = new Invoice();
                        _chkExistingInv = ListOfInvoice.Where(x => x.InvoiceNumber == Convert.ToString(Invoicedata.Num) && x.Contact.Name == Invoicedata.NameContact).FirstOrDefault();

                    }
                    foreach (var item in Invoicedata.ItemList)
                    {
                        string ItemCode;
                        XeroApi.Model.Item lstActualitems = new Item();
                        // string ItemSubstr;
                        if (item.Item.Length > 29)
                        {
                            ItemSubstr = item.Item.Substring(0, 25);


                        }
                        else
                        {


                            ItemSubstr = item.Item;

                        }



                        //lstActualitems = (from Actualitem in repository.Items   | these exceeding our 60 calls per 60 seconds rate limit
                        // where (Actualitem.Code == ItemSubstr)                  |
                        //select Actualitem).FirstOrDefault();                    |

                        //if (lstActualitems == null)
                        //{

                        Item _item = new Item

                        {

                            Code = ItemSubstr,
                            Name = item.Name,
                            UpdatedDateUTC = DateTime.UtcNow,
                            Description = item.Item,

                            SalesDetails = new ItemPrice
                            {
                                AccountCode = "440",
                                UnitPrice = Convert.ToDecimal(item.SalesPrice),
                            },

                        };
                        ItemTemp.Add(_item);
                        //var ITM = repository.UpdateOrCreate<Item>(_item);
                        //ItemCode = ITM.Code;
                        //}
                        //else
                        //{
                        // ItemCode = lstActualitems.Code;
                        //}
                        if (Invoicedata.ItemType == "Credit")
                        {
                            if (_chkExistingCreditInv != null)
                            {
                                LineItem _objLineItem = new LineItem();
                                _objLineItem.AccountCode = "440";
                                _objLineItem.Description = item.Item;
                                _objLineItem.UnitAmount = Convert.ToDecimal(item.SalesPrice);
                                _objLineItem.Quantity = Convert.ToDecimal(item.Qty);
                                //_objLineItem.ItemCode = ItemCode;
                                _objLineItem.ItemCode = ItemSubstr;
                                double yg = item.Discount;
                                _objLineItem.DiscountRate = (decimal)yg;
                                _chkExistingCreditInv.LineItems.Add(_objLineItem);
                            }
                            else
                            {

                                LineItem _objLineItem = new LineItem();
                                _objLineItem.AccountCode = "440";
                                _objLineItem.Description = item.Item;
                                _objLineItem.UnitAmount = Convert.ToDecimal(item.SalesPrice);
                                _objLineItem.Quantity = Convert.ToDecimal(item.Qty);
                                //_objLineItem.ItemCode = ItemCode;
                                _objLineItem.ItemCode = ItemSubstr;
                                double yg = item.Discount;
                                _objLineItem.DiscountRate = (decimal)yg; //(decimal)item.Discount;
                                _objLineItemList.Add(_objLineItem);
                            }
                        }
                        else
                        {

                            if (_chkExistingInv != null)
                            {
                                LineItem _objLineItem = new LineItem();
                                _objLineItem.AccountCode = "440";
                                _objLineItem.Description = item.Item;
                                _objLineItem.UnitAmount = Convert.ToDecimal(item.SalesPrice);
                                _objLineItem.Quantity = Convert.ToDecimal(item.Qty);
                                //_objLineItem.ItemCode = ItemCode;
                                _objLineItem.ItemCode = ItemSubstr;
                                double yg = item.Discount;
                                _objLineItem.DiscountRate = (decimal)yg;
                                _chkExistingInv.LineItems.Add(_objLineItem);
                            }
                            else
                            {

                                LineItem _objLineItem = new LineItem();
                                _objLineItem.AccountCode = "440";
                                _objLineItem.Description = item.Item;
                                _objLineItem.UnitAmount = Convert.ToDecimal(item.SalesPrice);
                                _objLineItem.Quantity = Convert.ToDecimal(item.Qty);
                                //_objLineItem.ItemCode = ItemCode;
                                _objLineItem.ItemCode = ItemSubstr;
                                double yg = item.Discount;
                                _objLineItem.DiscountRate = (decimal)yg; //(decimal)item.Discount;
                                _objLineItemList.Add(_objLineItem);
                            }
                        }
                    }

                    if (Invoicedata.ItemType == "Credit")//Add the  Item to the List when ItemType is Credit
                    {

                        var _crpaidsvin = temp.Where(x => x.Num == Invoicedata.Num).FirstOrDefault();

                        bool paidcrInvoice = true;

                        foreach (var paid in _crpaidsvin.ItemList)
                        {
                            if (paid.Paid == "Unpaid")
                            {
                                paidcrInvoice = false;


                            }
                        }
                        if (paidcrInvoice == true)
                        {

                            CreditNote _creditToCSV = new CreditNote
                            {
                                Type = "ACCRECCREDIT",
                                Contact = new Contact { Name = Invoicedata.NameContact },
                                Date = Invoicedata.Date,
                                DueDate = Invoicedata.Date,
                                //quant = Convert.ToDecimal(Invoicedata.Amount),
                                CreditNoteID = Invoicedata.InvoiceID,
                                CreditNoteNumber = Invoicedata.Num.ToString(),
                                Status = "AUTHORISED",
                                LineItems = _objLineItemList,
                            };
                            ListofCreditNote.Add(_creditToCSV);
                        }

                        else
                        {
                            CreditNote _creditToCSV = new CreditNote
                            {
                                Type = "ACCRECCREDIT",
                                Contact = new Contact { Name = Invoicedata.NameContact },
                                Date = Invoicedata.Date,
                                DueDate = Invoicedata.Date,
                                // Total = Convert.ToDecimal(Invoicedata.Amount),
                                CreditNoteID = Invoicedata.InvoiceID,
                                CreditNoteNumber = Invoicedata.Num.ToString(),
                                Status = "AUTHORISED",
                                LineItems = _objLineItemList,
                            };
                            ListofCreditNote.Add(_creditToCSV);
                        }

                    }
                    else// Sales Memo
                    {
                        if (_chkExistingInv == null)
                        {
                            Invoice invoiceToCSvCreate = new Invoice
                            {
                                Type = "ACCREC",
                                Contact = new Contact { Name = Invoicedata.NameContact },
                                Date = Invoicedata.Date,
                                DueDate = Invoicedata.Date,
                                Status = "DRAFT",
                                InvoiceID = Invoicedata.InvoiceID,
                                InvoiceNumber = Convert.ToString(Invoicedata.Num),
                                //InvoiceNumber = Convert.ToString(Invoicedata.Num),
                                LineItems = _objLineItemList,
                            };
                            ListOfInvoice.Add(invoiceToCSvCreate);
                        }
                    }

                }
                #region "Create the ItemList"
                if (ItemTemp.Count != 0)
                {
                    int invtotalPages = (ItemTemp.Count() / 100) + 1;
                    for (invpageNo = 0; invpageNo <= invtotalPages; invpageNo++)
                    {
                        List<Item> invItemList100 = new List<Item>();
                        var tempitem100 = ItemTemp.Skip(100 * invpageNo).Take(100).ToList();
                        if (tempitem100.Count > 0)
                        {
                            foreach (var data in tempitem100)
                            {
                                invItemList100.Add(data);
                            }
                            var ITM = repository.UpdateOrCreate<Item>(invItemList100).ToList();
                            for (int q = 0; q < ITM.Count; q++)
                            {
                                if (ITM[q].ValidationStatus == ValidationStatus.ERROR)
                                {
                                    //LogRecord(ITM[q].ValidationErrors.FirstOrDefault().Message, ITM[q].ItemID, "Invoice Item Error ", Guid.Empty, " ");
                                }
                            }
                        }

                    }
                }
                #endregion

                #region "Create the List Of CreditNode"

                if (ListofCreditNote.Count != 0)
                {
                    var credittocsv = repository.Create<CreditNote>(ListofCreditNote).ToList();
                    for (int i = 0; i < credittocsv.Count; i++)
                    {
                        //checking the Error
                        if (credittocsv[i].ValidationStatus == ValidationStatus.ERROR)
                        {
                            //Add the Error into Log File
                            LogRecord(credittocsv[i].ValidationErrors.FirstOrDefault().Message, Guid.Empty, " ", credittocsv[i].CreditNoteID, credittocsv[i].CreditNoteNumber);
                        }
                        else
                        {
                            CreatedCreditNoteList.Add(credittocsv[i]);
                        }
                    }
                    if (CreatedCreditNoteList.Count > 0)
                    {


                        foreach (var item in CreatedCreditNoteList)
                        {
                            #region "Comment Code by jaswinder on 19-03-2015"

                            var _crpaidsvin1 = temp.Where(x => x.Num == item.CreditNoteNumber).FirstOrDefault();
                            decimal CreditBalanceunpaid = 0;
                            bool paidcr1Invoice = true;


                            foreach (var paid in _crpaidsvin1.ItemList)
                            {
                                if (paid.Paid == "Unpaid")
                                {
                                    paidcr1Invoice = false;
                                    CreditBalanceunpaid = CreditBalanceunpaid + Convert.ToDecimal(paid.Balance);

                                }
                            }

                            if (paidcr1Invoice == true)
                            {

                                listiathCredit.Add(item);
                            }
                            else
                            {

                                if (CreditBalanceunpaid != 0)
                                {
                                    Payment _crepayunpaid = new Payment
                                    {
                                        Account = new Account { Code = "111" },
                                        CreditNote = new CreditNote { CreditNoteNumber = item.CreditNoteNumber, CreditNoteID = item.CreditNoteID },
                                        Date = (item.Date == null) ? DateTime.Now : item.Date.Value,
                                        PaymentType = item.Type,
                                        Amount = Convert.ToDecimal(item.Total) + CreditBalanceunpaid,
                                    };

                                    var _paidcreditunpaid = repository.Create<Payment>(_crepayunpaid);

                                    if (_paidcreditunpaid.ValidationStatus == ValidationStatus.ERROR)
                                    {

                                        //Add the Error into Log File
                                        //LogRecord(_paidcredit[k].ValidationErrors.FirstOrDefault().Message, Guid.Empty, " ", _paidcredit[k].CreditNote.CreditNoteID, _paidcredit[k].CreditNote.CreditNoteNumber);
                                    }

                                }
                            }
                            #endregion
                        }
                    }
                    //if (listiathCredit.Count > 0)
                    //{
                    //    var InvCreditinv = repository.UpdateOrCreate<CreditNote>(listiathCredit).ToList();
                    //    listiathCredit.Clear();
                    //    for (int m = 0; m < InvCreditinv.Count; m++)
                    //    {
                    //        if (InvCreditinv[m].ValidationStatus == ValidationStatus.ERROR)
                    //        {
                    //            LogRecord(InvCreditinv[m].ValidationErrors.FirstOrDefault().Message, InvCreditinv[m].CreditNoteID, InvCreditinv[m].CreditNoteNumber, Guid.Empty, " ");
                    //        }
                    //        else
                    //        {
                    //            listiathCredit.Add(InvCreditinv[m]);
                    //        }
                    //    }

                    //}


                    if (listiathCredit.Count > 0)
                    {

                        foreach (var Item in listiathCredit)
                        {
                            Payment _crepay = new Payment
                            {
                                Account = new Account { Code = "111" },
                                CreditNote = new CreditNote { CreditNoteNumber = Item.CreditNoteNumber, CreditNoteID = Item.CreditNoteID },
                                Date = (Item.Date == null) ? DateTime.Now : Item.Date.Value,
                                PaymentType = Item.Type,
                                Amount = Convert.ToDecimal(Item.Total),
                            };
                            ListofPayment.Add(_crepay);
                        }
                        #region " Item code comment by jaswinder on 19-03-2015"

                        #endregion
                        //Payment of CreditNote
                        var _paidcredit = repository.Create<Payment>(ListofPayment).ToList();
                        for (int k = 0; k < _paidcredit.Count; k++)
                        {
                            if (_paidcredit[k].ValidationStatus == ValidationStatus.ERROR)
                            {
                                k = _paidcredit.Count;
                                //Add the Error into Log File
                                //LogRecord(_paidcredit[k].ValidationErrors.FirstOrDefault().Message, Guid.Empty, " ", _paidcredit[k].CreditNote.CreditNoteID, _paidcredit[k].CreditNote.CreditNoteNumber);
                            }
                        }
                    }
                }

                #endregion

                #region "Create the List Of Invoice"

                if (ListOfInvoice.Count != 0)
                {
                    var InvoiceCreate = repository.Create<Invoice>(ListOfInvoice).ToList();
                    for (int l = 0; l < InvoiceCreate.Count; l++)
                    {
                        if (InvoiceCreate[l].ValidationStatus == ValidationStatus.ERROR)
                        {
                            //Add the Error into Log File
                            LogRecord(InvoiceCreate[l].ValidationErrors.FirstOrDefault().Message, InvoiceCreate[l].InvoiceID, InvoiceCreate[l].InvoiceNumber, Guid.Empty, " ");
                        }
                        else
                        {
                            CretedInvoiceList.Add(InvoiceCreate[l]);
                        }
                    }
                    if (CretedInvoiceList.Count > 0)
                    {

                        foreach (var _it in CretedInvoiceList)
                        {
                            var _cpaidsvin = temp.Where(x => x.Num == _it.InvoiceNumber).FirstOrDefault();
                            decimal Balanceunpaid = 0;
                            bool paidInvoice = true;

                            foreach (var paid in _cpaidsvin.ItemList)
                            {
                                if (paid.Paid == "Unpaid")
                                {
                                    paidInvoice = false;
                                    Balanceunpaid = Balanceunpaid + Convert.ToDecimal(paid.Balance);

                                }
                            }
                            if (paidInvoice == true)
                            {

                                Invoice _update = new Invoice
                                {

                                    InvoiceID = _it.InvoiceID,
                                    InvoiceNumber = _it.InvoiceNumber,
                                    Status = "AUTHORISED"
                                };
                                listinau.Add(_update);
                            }
                            else
                            {

                                if (Balanceunpaid > 0)
                                {
                                    Invoice _update = new Invoice
                                    {

                                        InvoiceID = _it.InvoiceID,
                                        InvoiceNumber = _it.InvoiceNumber,
                                        Status = "AUTHORISED"
                                    };
                                    var Inv = repository.UpdateOrCreate<Invoice>(_update);
                                    if (Inv.ValidationStatus == ValidationStatus.ERROR)
                                    {
                                        LogRecord(Inv.ValidationErrors.FirstOrDefault().Message, Inv.InvoiceID, Inv.InvoiceNumber, Guid.Empty, " ");
                                    }
                                    Payment _crepay = new Payment
                                    {
                                        Account = new Account { Code = "111" },
                                        Invoice = new Invoice { InvoiceNumber = Inv.InvoiceNumber, InvoiceID = Inv.InvoiceID },
                                        Date = (Inv.Date == null) ? DateTime.Now : Inv.Date.Value,
                                        PaymentType = Inv.Type,
                                        Amount = Convert.ToDecimal(Inv.Total) - Balanceunpaid,
                                    };
                                    var _paidinvoice = repository.Create<Payment>(_crepay);
                                    if (_paidinvoice.ValidationStatus == ValidationStatus.ERROR)
                                    {

                                    }
                                }
                            }

                        }
                        if (listinau.Count != 0)
                        {
                            var Inv = repository.UpdateOrCreate<Invoice>(listinau).ToList();
                            listinau.Clear();
                            for (int m = 0; m < Inv.Count; m++)
                            {
                                if (Inv[m].ValidationStatus == ValidationStatus.ERROR)
                                {
                                    LogRecord(Inv[m].ValidationErrors.FirstOrDefault().Message, Inv[m].InvoiceID, Inv[m].InvoiceNumber, Guid.Empty, " ");
                                }
                                else
                                {
                                    listinau.Add(Inv[m]);
                                }
                            }

                        }
                        if (listinau.Count != 0)
                        {
                            foreach (var item in listinau)
                            {
                                Payment _crepay = new Payment
                                {
                                    Account = new Account { Code = "111" },
                                    Invoice = new Invoice { InvoiceNumber = item.InvoiceNumber, InvoiceID = item.InvoiceID },
                                    Date = (item.Date == null) ? DateTime.Now : item.Date.Value,
                                    PaymentType = item.Type,
                                    Amount = Convert.ToDecimal(item.Total),
                                };
                                ListofPayment.Add(_crepay);
                            }

                            //Payment of Invoices
                            var _paidinvoice = repository.Create<Payment>(ListofPayment).ToList();
                            for (int n = 0; n < _paidinvoice.Count; n++)
                            {
                                if (_paidinvoice[n].ValidationStatus == ValidationStatus.ERROR)
                                {
                                    n = _paidinvoice.Count;
                                    //Add the Error into Log File
                                    //LogRecord(_paidinvoice[n].ValidationErrors.FirstOrDefault().Message, _paidinvoice[n].Invoice.InvoiceID, _paidinvoice[n].Invoice.InvoiceNumber, Guid.Empty, " ");//(Object reference not set to an instance of an object. error occured.)
                                }
                            }
                        }
                    }
                }

                #endregion

                ItemListcre.Clear();
                listinau.Clear();
                ItemList.Clear();
                CretedInvoiceList.Clear();
                CreatedCreditNoteList.Clear();
                ListOfInvoice.Clear();
                ListofPayment.Clear();
                ListofCreditNote.Clear();
                ItemTemp.Clear();
            }
        }




        //Creating the Log File
        public void LogRecord(string sMessage, Guid InvoiceID, String InvoiceNumber, Guid CreditNoteID, string CreditNoteNumber)
        {
            StreamWriter objSw = null;
            try
            {

                string sFolderName = HttpContext.Current.Server.MapPath("~/LogFile/");
                if (!Directory.Exists(sFolderName))
                    Directory.CreateDirectory(sFolderName);
                string sFilePath = sFolderName + "Transaction.log";

                objSw = new StreamWriter(sFilePath, true);

                objSw.WriteLine("Log Time :" + DateTime.Now.ToString() + "Error Message :" + sMessage + Environment.NewLine);
                if (InvoiceID != Guid.Empty)
                {
                    objSw.WriteLine(" InvoiceID :" + InvoiceID + " InvoiceNumber :" + InvoiceNumber + Environment.NewLine);
                }
                if (CreditNoteID != Guid.Empty)
                {
                    objSw.WriteLine("CreditNoteID :" + CreditNoteID + " CreditNoteNumber " + CreditNoteNumber + Environment.NewLine);
                }


            }
            catch (Exception ex)
            {
                //LogRecord("Error -" + ex.Message);
                throw;
            }
            finally
            {
                if (objSw != null)
                {
                    objSw.Flush();
                    objSw.Dispose();
                }
            }
        }


        //Creating Contact
        public void CreatingAndUpdatingContacts(Addcustomer _Addcustomer)
        {

            // Make a PUT call to the API - add a dummy contact
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;

            Addresses _newAddresses = new Addresses();
            Address _newAddress;

            foreach (var PatientAddress in _Addcustomer.Address)
            {
                _newAddress = new Address();
                _newAddress.AddressLine1 = PatientAddress.AddressLine1;
                _newAddress.AddressLine2 = PatientAddress.AddressLine2;
                _newAddress.City = PatientAddress.City;
                _newAddress.PostalCode = PatientAddress.PostalCode;
                _newAddress.Country = PatientAddress.Country;
                _newAddress.Region = PatientAddress.Region;
                _newAddress.AddressType = PatientAddress.AddressType;
                _newAddresses.Add(_newAddress);
            }

            Phones _newPhones = new Phones();
            Phone _newPhone;
            foreach (var PatientPhones in _Addcustomer.Phone)
            {
                _newPhone = new Phone();
                _newPhone.PhoneNumber = PatientPhones.PhoneNumber;
                _newPhone.PhoneType = PatientPhones.PhoneType;
                _newPhones.Add(_newPhone);
            }

            Contact contact = new Contact();

            contact.Name = _Addcustomer.Name;
            contact.ContactNumber = _Addcustomer.ContactNumber;
            contact.EmailAddress = _Addcustomer.EmailAddress;
            contact.IsCustomer = true;
            contact.IsSupplier = false;
            contact.FirstName = _Addcustomer.FirstName;
            contact.LastName = _Addcustomer.LastName;
            contact.ContactStatus = _Addcustomer.ContactStatus;
            contact.Addresses = _newAddresses;
            contact.DefaultCurrency = "USD";
            contact.Phones = _newPhones;
            contact.TrackingCategoryName = "Rep";
            contact.TrackingCategoryOption = _Addcustomer.ProviderName;
            if (!string.IsNullOrEmpty(contact.TrackingCategoryOption))
            {
                TrackingCategory Category = GetTrackingCategory(contact.TrackingCategoryName);
                foreach (var option in Category.Options)
                {
                    if (option.Name.Trim() == contact.TrackingCategoryOption.Trim())
                    {
                        contact.SalesTrackingCategories.Add(new SalesTrackingCategory()
                        {

                            TrackingCategoryName = contact.TrackingCategoryName,
                            TrackingOptionName = contact.TrackingCategoryOption
                        });
                    }
                }
            }

            if (_Addcustomer.ContactID != Guid.Empty)
            {
                contact.ContactID = _Addcustomer.ContactID;
            }
            Contact objCreatedContact = repository.UpdateOrCreate(contact);
            var MerchantID = objCreatedContact.ContactID;
            HttpContext.Current.Session["MerchantID"] = MerchantID;

            QB_MatchViewModel _QbMatchModel = new QB_MatchViewModel();
            IXeroAPIService objService = new XeroAPIService();
            if (_Addcustomer.ContactID == Guid.Empty)
            {
                _QbMatchModel.PatientID = Convert.ToInt16(_Addcustomer.ContactNumber);
                _QbMatchModel.QBid = MerchantID.ToString();
                objService.InsertXeroMatch(_QbMatchModel);
            }
            else
            {
                //objService.EditQbMatch(Convert.ToInt16(_Addcustomer.ContactNumber), 0);
            }
            contact = null;
            contact = null;
        }


        public void CreatingInvoiceWithValidationErrors(AddInvoice _AddInvoice)
        {
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            // Try and create an invoice - but using incorrect data. This should hopefully be rejected by the Xero API
            LineItems _objLineItemList = new LineItems();
            LineAmountType lineAmountType = new LineAmountType();
            if (_AddInvoice.Medical == false)
            {
                lineAmountType = LineAmountType.Exclusive;

            }
            else
            {
                lineAmountType = LineAmountType.NoTax;
            }
            string taxType = GetContactTaxType(_AddInvoice.ContactID);
            TrackingCategory Category1 = GetTrackingCategory("Rep");
            bool IsProvider = false;
            bool IsNA = false;

            XeroApi.Model.Contact lstContacts = new Contact();
            lstContacts = (from contacts in repository.Contacts where (contacts.ContactID == _AddInvoice.ContactID) select contacts).FirstOrDefault();

            string trackingcategoryname = string.Empty;
            if (lstContacts != null)
            {
                foreach (var item in lstContacts.SalesTrackingCategories)
                {
                    if (item.TrackingCategoryName == "Rep")
                    {
                        _AddInvoice.ProviderName = item.TrackingOptionName;
                        IsProvider = true;
                    }
                }

            }

            if (IsProvider == false)
            {
                foreach (var option in Category1.Options)
                {
                    //if (!string.IsNullOrEmpty(_AddInvoice.ProviderName))
                    //{
                    //    if (option.Name.Trim() == _AddInvoice.ProviderName)
                    //    {
                    //        IsProvider = true;

                    //    }
                    //}
                    if (option.Name.Trim() == "NA")
                    {
                        IsNA = true;
                    }

                }
            }

            if (IsProvider == false)
            {
                if (IsNA == true)
                {
                    _AddInvoice.ProviderName = "NA";
                    IsProvider = true;
                }

            }

            TrackingCategory Category2 = GetTrackingCategory("Location");




            foreach (var Item in _AddInvoice.ItemList)
            {
                XeroApi.Model.Item lstActualitems = new Item();
                LineItem _objLineItem = new LineItem();
                if (!string.IsNullOrEmpty(Item.ItemCode))
                {
                    lstActualitems = (from Actualitem in repository.Items where (Actualitem.Code.ToLower() == Item.ItemCode.ToLower()) select Actualitem).FirstOrDefault();

                    if (lstActualitems == null)
                    {

                        Item _item = new Item

                        {

                            Code = Item.ItemCode,
                            Name = Item.ItemCode,
                            UpdatedDateUTC = DateTime.UtcNow,
                            Description = Item.Description,

                            SalesDetails = new ItemPrice
                            {
                                AccountCode = _AddInvoice.SalesAccountCode.ToString(),
                                UnitPrice = Convert.ToDecimal(Item.UnitAmount),
                            },

                        };
                        var itemcode = repository.UpdateOrCreate<Item>(_item);
                        _objLineItem.ItemCode = Item.ItemCode;
                    }
                    else
                    {
                        _objLineItem.ItemCode = lstActualitems.Code;
                    }
                }

                _objLineItem.AccountCode = _AddInvoice.SalesAccountCode.ToString(); //"400";
                _objLineItem.Description = Item.Description;
                _objLineItem.UnitAmount = Item.UnitAmount;
                _objLineItem.TaxType = taxType;


                if (IsProvider == true)
                {
                    _objLineItem.Tracking.Add(new TrackingCategory()
                    {

                        Name = Category1.Name,
                        Option = _AddInvoice.ProviderName,
                    });
                }

                foreach (var option in Category2.Options)
                {
                    if (option.Name.Trim() == "Retail")
                    {
                        _objLineItem.Tracking.Add(new TrackingCategory()
                        {

                            Name = Category2.Name,
                            Option = option.Name,
                        });
                    }
                }

                _objLineItem.Quantity = Item.Quantity;
                _objLineItem.DiscountRate = Item.Discount;
                _objLineItemList.Add(_objLineItem);

            }

            /* LineItem _objLineItemDisc = new LineItem();
             _objLineItemDisc.AccountCode = _AddInvoice.SalesAccountCode.ToString();//"400";
             _objLineItemDisc.Description = ConfigurationSettings.AppSettings["XeroDiscountDiscription"];
             _objLineItemDisc.UnitAmount = 0;
             _objLineItemDisc.TaxType = taxType;
             if (IsProvider == true)
             {
                 _objLineItemDisc.Tracking.Add(new TrackingCategory()
                 {

                     Name = Category1.Name,
                     Option = _AddInvoice.ProviderName,
                 });
             }
             foreach (var option in Category2.Options)
             {
                 if (option.Name.Trim() == "Retail")
                 {
                     _objLineItemDisc.Tracking.Add(new TrackingCategory()
                     {

                         Name = Category2.Name,
                         Option = option.Name,
                     });
                 }
             }
             _objLineItemDisc.Quantity = 1;

             _objLineItemList.Add(_objLineItemDisc);*/

            if (_AddInvoice.Total <= _AddInvoice.OrderLimit)
            {
                if (_AddInvoice.ShippingFee > 0)
                {
                    LineItem _objLineItem = new LineItem();
                    _objLineItem.AccountCode = _AddInvoice.SalesAccountCode.ToString();//"400";
                    _objLineItem.Description = "Shipping Fee";
                    _objLineItem.UnitAmount = _AddInvoice.ShippingFee;
                    _objLineItem.TaxType = taxType;

                    if (IsProvider == true)
                    {
                        _objLineItem.Tracking.Add(new TrackingCategory()
                        {

                            Name = Category1.Name,
                            Option = _AddInvoice.ProviderName,
                        });
                    }

                    foreach (var option in Category2.Options)
                    {
                        if (option.Name.Trim() == "Retail")
                        {
                            _objLineItem.Tracking.Add(new TrackingCategory()
                            {

                                Name = Category2.Name,
                                Option = option.Name,
                            });
                        }
                    }
                    _objLineItem.Quantity = 1;

                    _objLineItemList.Add(_objLineItem);
                }
            }

            LineItem _objLineItemShippingaddress = new LineItem();
            _objLineItemShippingaddress.AccountCode = _AddInvoice.SalesAccountCode.ToString();//"400";
            _objLineItemShippingaddress.Description = _AddInvoice.ShippingAddress;
            _objLineItemShippingaddress.UnitAmount = 0;
            _objLineItemShippingaddress.TaxType = taxType;
            if (IsProvider == true)
            {
                _objLineItemShippingaddress.Tracking.Add(new TrackingCategory()
                {

                    Name = Category1.Name,
                    Option = _AddInvoice.ProviderName,
                });
            }
            foreach (var option in Category2.Options)
            {
                if (option.Name.Trim() == "Retail")
                {
                    _objLineItemShippingaddress.Tracking.Add(new TrackingCategory()
                    {

                        Name = Category2.Name,
                        Option = option.Name,
                    });
                }
            }
            _objLineItemShippingaddress.Quantity = 1;

            _objLineItemList.Add(_objLineItemShippingaddress);

            Invoice invoiceToCreate = new Invoice
            {
                Contact = new Contact
                {
                    ContactID = _AddInvoice.ContactID
                },
                Type = "ACCREC",
                Date = DateTime.Today,
                DueDate = _AddInvoice.DueDate,
                Status = "Draft",
                BrandingThemeID = GetBrandingThemeId(),
                // Total = _AddInvoice.Total,
                InvoiceID = _AddInvoice.InvoiceID,
                LineAmountTypes = lineAmountType,
                //SubTotal = _AddInvoice.SubTotal,
                //CurrencyCode = "AUD",
                InvoiceNumber = _AddInvoice.InvoiceNumber,
                Reference = "retail auto ship",
                //TotalDiscount = 0,
                LineItems = _objLineItemList,
            };

            var createdInvoice = repository.UpdateOrCreate(invoiceToCreate);

            if (createdInvoice.ValidationStatus == ValidationStatus.ERROR)
            {
                foreach (var message in createdInvoice.ValidationErrors)
                {
                    //return message.Message;
                }
            }
            else
            {
                IXeroAPIService objService = new XeroAPIService();
                objService.UpdateInvoiceXeroId(Convert.ToInt32(_AddInvoice.InvoiceNumber), createdInvoice.InvoiceID);
            }
        }

        //public void UpdateInvoiceXeroId(int InvoiceNumber,Guid InvoiceId)
        //{
        //    IXeroAPIService objService = new XeroAPIService();
        //    objService.UpdateInvoiceXeroId(InvoiceNumber, InvoiceId);
        //}

        public void CreatingInvoicAddPayment(AddInvoice _AddInvoice, string Amount, string PaymentDate, string AccountCode, string PaymentReference)
        {
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            Invoice _update = new Invoice
            {

                InvoiceID = _AddInvoice.InvoiceID,
                InvoiceNumber = _AddInvoice.InvoiceNumber,
                Status = "AUTHORISED"
            };
            var Inv = repository.UpdateOrCreate<Invoice>(_update);
            if (Inv.ValidationStatus == ValidationStatus.ERROR)
            {
                LogRecord(Inv.ValidationErrors.FirstOrDefault().Message, Inv.InvoiceID, Inv.InvoiceNumber, Guid.Empty, " ");
            }

            var listCreditNote = repository.CreditNotes.Where(x => x.Contact.ContactID == Inv.Contact.ContactID && x.RemainingCredit > 0).ToList();
            var listPayments = repository.Payments.ToList();
            DateTime myDate = DateTime.ParseExact(PaymentDate, "MM/dd/yyyy",
                                     CultureInfo.InvariantCulture);
            decimal BalanceAmount = Convert.ToDecimal(Amount);
            if (listCreditNote.Count > 0)
            {

                foreach (var creditNote in listCreditNote)
                {
                    if (creditNote.Status != "Authorised")
                    {
                        creditNote.Status = "Authorised";
                        var _CreditNote = repository.UpdateOrCreate<CreditNote>(creditNote);
                    }

                    if (BalanceAmount >= creditNote.RemainingCredit)
                    {
                        BalanceAmount = BalanceAmount - creditNote.RemainingCredit;

                        Allocation allocateAmount = new Allocation
                        {
                            Invoice = new Invoice { InvoiceNumber = Inv.InvoiceNumber, InvoiceID = Inv.InvoiceID },
                            AppliedAmount = creditNote.RemainingCredit
                        };



                        creditNote.Allocations.Add(allocateAmount);
                        //var _paidInvoiceAgainstCreditNote = repository.Create<Allocation>(allocateAmount);
                        var _updateCreditNote = repository.Create<CreditNote>(creditNote);

                    }
                    else
                    {
                        Allocation allocateAmount = new Allocation();
                        allocateAmount.Invoice = new Invoice { InvoiceNumber = Inv.InvoiceNumber, InvoiceID = Inv.InvoiceID };
                        allocateAmount.AppliedAmount = BalanceAmount;
                        creditNote.Allocations.Add(allocateAmount);
                        var _paidInvoiceAgainstCreditNote = repository.Create<Allocation>(allocateAmount);
                        var _updateCreditNote = repository.UpdateOrCreate<CreditNote>(creditNote);
                    }


                }
            }



            Payment _crepay = new Payment
            {
                Account = new Account { Code = AccountCode },
                Invoice = new Invoice { InvoiceNumber = Inv.InvoiceNumber, InvoiceID = Inv.InvoiceID },
                Date = myDate,
                PaymentType = Inv.Type,
                Amount = BalanceAmount,
                Reference = PaymentReference
            };
            var _paidinvoice = repository.Create<Payment>(_crepay);
            if (_paidinvoice.ValidationStatus == ValidationStatus.ERROR)
            {

            }
            else
            {
                IXeroAPIService objService = new XeroAPIService();
                objService.UpdateOrderPaid(Convert.ToInt32(Inv.InvoiceNumber));
            }
        }

        public void GetUser()
        {
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            var listOfUser = repository.Users.Where(x => x.FirstName != null).ToList();

        }

        public void GetAccounts()
        {
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            var listAccounts = repository.Accounts.Where(x => x.BankAccountNumber != null).ToList();
            if (listAccounts.Count > 0)
            {



                foreach (var account in listAccounts)//&& o.LastName.Contains(contacts.LastName)) 
                {
                    IXeroAPIService objService = new XeroAPIService();
                    objService.InsertXeroAccounts(account.Code, account.Name);

                }
            }
        }

        public void GetOrg()
        {
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            var listOfOrg = (from Orgs in repository.Organisations
                             where Orgs.IsDemoCompany != true
                             select Orgs).ToList();
        }

        public TrackingCategory GetTrackingCategory(string category)
        {
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            var BrandingTheme = (from brand in repository.TrackingCategories
                                 where brand.Name.Trim() == category.Trim()
                                 select brand).FirstOrDefault();
            return BrandingTheme;
        }

        public Guid GetBrandingThemeId()
        {
            Guid themeId = new Guid();
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            var BrandingTheme = (from brand in repository.BrandingThemes
                                 where brand.Name == ConfigurationSettings.AppSettings["XeroBrandName"]
                                 select brand).FirstOrDefault();
            if (BrandingTheme != null)
                return BrandingTheme.BrandingThemeID;
            else
                return themeId;
        }

        public string GetContactTaxType(Guid contactId)
        {

            var repository = HttpContext.Current.Session["consumerSession"] as Repository;

            var ContactDetails = (from contacts in repository.Contacts
                                  where contacts.ContactID == contactId
                                  select contacts).FirstOrDefault();

            if (ContactDetails != null)
            {
                if (!string.IsNullOrEmpty(ContactDetails.AccountsPayableTaxType))
                    return ContactDetails.AccountsPayableTaxType;
                else
                    return ContactDetails.AccountsReceivableTaxType;
            }
            else return null;
        }


        /// <summary>
        /// This method is using to get all contacts of Xero and then compare with application existing user and if not matched then save in XeroPatients ta
        /// </summary>
        public void GetContacts()
        {
            var dateval = GetLastContactFatchedDate();
            var repository = HttpContext.Current.Session["consumerSession"] as Repository;
            List<XeroApi.Model.Contact> lstContacts = new List<Contact>();
            if (dateval.HasValue)
            {

                lstContacts = (from contacts in repository.Contacts
                               where contacts.FirstName != "" && contacts.FirstName != null && contacts.UpdatedDateUTC >= dateval.Value
                               select contacts).ToList();
            }
            else
            {
                lstContacts = (from contacts in repository.Contacts
                               where contacts.FirstName != "" || contacts.FirstName != null
                               select contacts).ToList();
            }

            if (lstContacts.Count > 0)
            {
                QB_MatchViewModel _QbMatchModel = new QB_MatchViewModel();
                XeroPatientViewModel _XeroPatientModel = new XeroPatientViewModel();
                IXeroAPIService objService = new XeroAPIService();
                //Setting [XeroLog] table's [UpdatedDateUTC] field with current date time.
                objService.EditLastContactFatchedDate();
                //List<GetPatientDetailsViewModel> lstPatientDetails = objService.GetXeroPatientDetails();
                //lstPatientDetails = lstPatientDetails.Where(p => p.FirstName != null || p.FirstName != "").ToList();


                foreach (var contacts in lstContacts)//&& o.LastName.Contains(contacts.LastName)) 
                {

                    insertXeroPatients(contacts);

                }
            }

        }

        public DateTime? GetLastContactFatchedDate()
        {
            IXeroAPIService objService = new XeroAPIService();
            return objService.GetLastContactFatchedDate();
        }

        public void insertXeroPatients(Contact contacts)
        {
            QB_MatchViewModel _QbMatchModel = new QB_MatchViewModel();
            XeroPatientViewModel _XeroPatientModel = new XeroPatientViewModel();
            IXeroAPIService objService = new XeroAPIService();

            _XeroPatientModel.ContactId = contacts.ContactID;
            _XeroPatientModel.FirstName = contacts.FirstName;
            _XeroPatientModel.LastName = contacts.LastName;

            _XeroPatientModel.Email = contacts.EmailAddress;
            Phone phone1 = new Phone();
            phone1 = contacts.Phones.Where(x => x.PhoneType == "MOBILE").FirstOrDefault();
            _XeroPatientModel.CellPhone = phone1.PhoneCountryCode + phone1.PhoneAreaCode + phone1.PhoneNumber;
            phone1 = contacts.Phones.Where(x => x.PhoneType == "DEFAULT").FirstOrDefault();
            _XeroPatientModel.HomePhone = phone1.PhoneCountryCode + phone1.PhoneAreaCode + phone1.PhoneNumber;

            Address addresss = new Address();
            addresss = contacts.Addresses.Where(x => x.AddressType == "POBOX").FirstOrDefault();

            _XeroPatientModel.BillingStreet = addresss.AddressLine1 != null ? addresss.AddressLine1 : "" + addresss.AddressLine2 != null ? addresss.AddressLine2 : "" + addresss.AddressLine3 != null ? addresss.AddressLine3 : "" + addresss.AddressLine4 != null ? addresss.AddressLine4 : "";
            _XeroPatientModel.BillingCity = addresss.City;
            _XeroPatientModel.BillingState = addresss.Region;
            _XeroPatientModel.BillingZip = addresss.PostalCode;



            objService.InsertXeroNotMatch(_XeroPatientModel);
        }

    }
}