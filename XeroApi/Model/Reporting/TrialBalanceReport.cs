using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace XeroApi.Model.Reporting
{
    public class TrialBalanceReport : DynamicReportBase
    {
        private DateTime? _date;
        private bool? _paymentsOnly;


        // User-accesible constructor
        public TrialBalanceReport(DateTime? date = null, bool? paymentsOnly = null)
        {
            _date = date;
            _paymentsOnly = paymentsOnly;
        }

        /// <summary>
        /// Generates the querystring params.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        internal override void GenerateQuerystringParams(NameValueCollection queryStringParams)
        {
            if (_date.HasValue)
                queryStringParams.Add("date", _date.Value.ToString(ReportDateFormatString));

            if (_paymentsOnly.HasValue)
                queryStringParams.Add("paymentsOnly", _paymentsOnly.Value ? "true" : "false");
        }
    }
}
