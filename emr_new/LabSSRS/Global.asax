<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
			Exception ex = Server.GetLastError().GetBaseException();
			System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress("IT@LongevityMedicalClinic.com", "IT");
			System.Net.Mail.MailMessage msg1 = new System.Net.Mail.MailMessage();
			msg1.From = from;
			msg1.To.Add(new System.Net.Mail.MailAddress("IT@LongevityMedicalClinic.com", "IT"));
			msg1.Subject = "An error occurred in LabReportUI";
			msg1.Body = "MESSAGE: " + ex.Message +
			  "\nSOURCE: " + ex.Source +
			  "\nFILE: " + Request.FilePath +
			  "\nQUERYSTRING: " + Request.QueryString.ToString() +
			  "\nTARGETSITE: " + ex.TargetSite +
			  "\nSTACKTRACE: " + ex.StackTrace +
			  "\nREFERRER: " + Request.ServerVariables["HTTP_REFERER"] +
			  "\nREMOTE ADDRESS: " + Request.ServerVariables["REMOTE_ADDR"];


			System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient("longevity-1.LongevityMedicalClinic.local");
			Client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
			Client.Send(msg1);
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
