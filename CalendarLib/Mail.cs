using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace Calendar
{
    public class Mail
    {
        public static void SendMessage(string fromAddress, string fromName, string toAddress, string toName, string MessageBody, string Attachment, string Subject)
        {
            System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress(fromAddress, fromName);
            //Below line is commented by jaswinder 
            //var ItoAddress = new MailAddress(toAddress, toName);
            const string fromPassword = "LMCC0ntacts";
            System.Net.Mail.MailMessage msg1 = new System.Net.Mail.MailMessage();
            msg1.From = from;
            msg1.To.Add(new System.Net.Mail.MailAddress(toAddress, toName));
            msg1.Subject = Subject;
            msg1.Body = MessageBody;
            //Commented by jaswinder to show add string.null
            //if (Attachment != "")
            if (!string.IsNullOrEmpty(Attachment))
            {
                msg1.Attachments.Add(new System.Net.Mail.Attachment(Attachment));
            }
            var Client = new SmtpClient
            {
                Host = "pod51019.outlook.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, fromPassword)
            };
            Client.Send(msg1);
        }

        public static void NoMail(string toName, string MessageBody, string Attachment, string Subject)
		{

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {

                string sql = "INSERT INTO NoMail (PatientName,MessageText,Attachment,Subject)VALUES('";
                sql += toName + "','" + MessageBody + "','" + Attachment + "','" + Subject + "')";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }

                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
		}
    }
}
