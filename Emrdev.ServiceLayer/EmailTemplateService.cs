using Emrdev.GeneralClasses;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmailTemplateService" in both code and config file together.
    public class EmailTemplateService : IEmailTemplateService
    {
        EmailTemplateBAL objBAL = new EmailTemplateBAL();
        public void SaveEmailTemplate(string EmailContent, int AppointmentTypeId)
        {
            objBAL.SaveEmailTemplate(EmailContent, AppointmentTypeId);

        }


        public EmailTemplateViewModel GetEmailTemplate(int AppointmentTypeId)
        {
            return objBAL.GetEmailTemplate(AppointmentTypeId);
        }

        public void SaveIVRTemplate(string EmailContent, int AppointmentTypeId)
        {
            objBAL.SaveIVRTemplate(EmailContent, AppointmentTypeId);

        }


        public EmailTemplateViewModel GetIVRTemplate(int AppointmentTypeId)
        {
            return objBAL.GetIVRTemplate(AppointmentTypeId);
        }


        public void SaveCRMEmailTemplate(string EmailContent, string WufooLink, bool IsActive)
        {
            objBAL.SaveCRMEmailTemplate(EmailContent, WufooLink, IsActive);

        }


        public EmailTemplateViewModel GetAutoShipEmailTemplate()
        {
            return objBAL.GetAutoShipEmailTemplate();
        }

        public void SaveAutoShipEmailTemplate(string EmailContent)
        {
            objBAL.SaveAutoShipEmailTemplate(EmailContent);

        }

        public CRMEmailTemplateViewModel GetCRMEmailTemplate()
        {
            return objBAL.GetCRMEmailTemplate();
        }

        public int insertSurveyQuestions(string Title, string Type, string FieldName)
        {
            return objBAL.insertSurveyQuestions(Title, Type, FieldName);
        }

        public void insertSurveyQuestionsAnswer(string FieldName, string Answer, int patientID, DateTime createdBy, string EntryID, int ApptID)
        {
            objBAL.insertSurveyQuestionsAnswer(FieldName, Answer, patientID, createdBy, EntryID, ApptID);
        }

        public void UpdateQuestionAsActive(string ActiveQuestions)
        {
            objBAL.UpdateQuestionAsActive(ActiveQuestions);
        }

        public List<MergedPatientViewModel> GetMergedPatientRecord(int page, int rows, string sord, string sidx,int IsSearch, string SearchColumn, string SearchText)
        {
            return objBAL.GetMergedPatientRecord(page, rows, sord, sidx, IsSearch, SearchColumn, SearchText);
        }

        public bool UndoMergedPatient(int MergedPatientID)
        {
            return objBAL.UndoMergedPatient(MergedPatientID);
        }


        public List<MergedPatientViewModel> GetPatienstLisTotMerge(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText)
        {
            return objBAL.GetPatienstLisTotMerge(page, rows, sord, sidx,IsSearch,  SearchColumn, SearchText);
        }

        public bool MergedPatientData(int existingPatientID, int NewPatientID, int StaffID)
        {
            return objBAL.MergedPatientData(existingPatientID, NewPatientID, StaffID);
        }

        public void SaveSalesAccountCode(int salesAccountCode)
        {
             objBAL.SaveSalesAccountCode(salesAccountCode);
        }

        public SalesAccountCodeViewModel GetSalesAccountCode( )
        {

            return objBAL.GetSalesAccountCode();


        }
    }
}
