using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using AutoMapper;


namespace Emrdev.GeneralClasses
{
    public class EmailTemplateBAL
    {
        CampaignTypeDAL objDAL = new CampaignTypeDAL();
        public void SaveEmailTemplate(string EmailContent,int AppointmentTypeId)
        {
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
            EmailTemplate objTemplate=null;
            objTemplate = objDAL.Get<EmailTemplate>(o => o.AppointmentId == AppointmentTypeId);
            
            if (objTemplate != null)
            {
                objTemplate.TemplateDesc = EmailContent;
                objDAL.Edit(objTemplate);
            }
            else
            {
                objTemplate = new Emrdev.DataLayer.EmailTemplate();
                objTemplate.TemplateDesc = EmailContent;
                objTemplate.AppointmentId = AppointmentTypeId;
                objDAL.Create(objTemplate);
            }
        }


        public EmailTemplateViewModel GetEmailTemplate(int AppointmentTypeId)
        {
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
            var objTemplate = new EmailTemplateViewModel();
            EmailTemplate objTemplateEntity = objDAL.Get<EmailTemplate>(o => o.AppointmentId == AppointmentTypeId);
            Mapper.CreateMap<EmailTemplate, EmailTemplateViewModel>();
            objTemplate = Mapper.Map(objTemplateEntity, objTemplate);
            return objTemplate;
           
            
        }

        public void SaveIVRTemplate(string EmailContent, int AppointmentTypeId)
        {
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
            IVRTemplate objTemplate = null;
            objTemplate = objDAL.Get<IVRTemplate>(o => o.AppointmentId == AppointmentTypeId);

            if (objTemplate != null)
            {
                objTemplate.TemplateDesc = EmailContent;
                objDAL.Edit(objTemplate);
            }
            else
            {
                objTemplate = new Emrdev.DataLayer.IVRTemplate();
                objTemplate.TemplateDesc = EmailContent;
                objTemplate.AppointmentId = AppointmentTypeId;
                objDAL.Create(objTemplate);
            }
        }


        public EmailTemplateViewModel GetIVRTemplate(int AppointmentTypeId)
        {
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
            var objTemplate = new EmailTemplateViewModel();
            IVRTemplate objTemplateEntity = objDAL.Get<IVRTemplate>(o => o.AppointmentId == AppointmentTypeId);
            Mapper.CreateMap<IVRTemplate, EmailTemplateViewModel>();
            objTemplate = Mapper.Map(objTemplateEntity, objTemplate);
            return objTemplate;


        }


        public void SaveCRMEmailTemplate(string EmailContent, string WufooLink,bool IsActive)
        {
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
            CRMWufooLink objTemplate = null;
            objTemplate = objDAL.Get<CRMWufooLink>(o => o.Id>0);

            if (objTemplate != null)
            {
                objTemplate.EmailDescription = EmailContent;
                objTemplate.IsActive = IsActive;
                objTemplate.WufooFormLink = WufooLink;
                objDAL.Edit(objTemplate);
            }
            else
            {
                objTemplate = new Emrdev.DataLayer.CRMWufooLink();
                objTemplate.EmailDescription = EmailContent;
                objTemplate.IsActive = IsActive;
                objTemplate.WufooFormLink = WufooLink;
                objDAL.Create(objTemplate);
            }
        }

        public EmailTemplateViewModel GetAutoShipEmailTemplate()
        {
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
            var objTemplate = new EmailTemplateViewModel();
            AutoShipEmailTEmplate objTemplateEntity = objDAL.Get<AutoShipEmailTEmplate>(o => o.Id >= 0);
            Mapper.CreateMap<AutoShipEmailTEmplate, EmailTemplateViewModel>();
            objTemplate = Mapper.Map(objTemplateEntity, objTemplate);
            return objTemplate;
        }

        public void SaveAutoShipEmailTemplate(string EmailContent)
        {
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
            AutoShipEmailTEmplate objTemplate = null;
            objTemplate = objDAL.Get<AutoShipEmailTEmplate>(o => o.Id > 0);

            if (objTemplate != null)
            {
                objTemplate.TemplateDesc = EmailContent;
                objDAL.Edit(objTemplate);
            }
            else
            {
                objTemplate = new Emrdev.DataLayer.AutoShipEmailTEmplate();
                objTemplate.TemplateDesc = EmailContent;
                objDAL.Create(objTemplate);
            }

        }

        public CRMEmailTemplateViewModel GetCRMEmailTemplate()
        {
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
            var objTemplate = new CRMEmailTemplateViewModel();
            CRMWufooLink objTemplateEntity = objDAL.Get<CRMWufooLink>(o=>o.Id>0);
            Mapper.CreateMap<CRMWufooLink, CRMEmailTemplateViewModel>();
            objTemplate = Mapper.Map(objTemplateEntity, objTemplate);
            return objTemplate;


        }
        public int insertSurveyQuestions(string Title, string Type, string FieldName)
        {
            return objDAL.insertSurveyQuestions(Title, Type, FieldName);
        }

        public void insertSurveyQuestionsAnswer(string FieldName, string Answer, int patientID, DateTime createdBy, string EntryID, int ApptID)
        {
            objDAL.insertSurveyQuestionsAnswer(FieldName, Answer, patientID, createdBy, EntryID, ApptID);
        }

        public void UpdateQuestionAsActive(string ActiveQuestions)
        {
            objDAL.UpdateQuestionAsActive(ActiveQuestions);
        }

        public List<MergedPatientViewModel> GetMergedPatientRecord(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText)
        {
            return objDAL.GetMergedPatientRecord(page, rows, sord, sidx, IsSearch, SearchColumn, SearchText);
        }

        public bool UndoMergedPatient(int MergedPatientID)
        {
            return objDAL.UndoMergedPatient(MergedPatientID);
        }

        public List<MergedPatientViewModel> GetPatienstLisTotMerge(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText)
        {
            return objDAL.GetPatienstLisTotMerge(
                page, rows, sord, sidx, IsSearch, SearchColumn, SearchText);
        }


        public bool MergedPatientData(int existingPatientID, int NewPatientID, int StaffID)
        {
            return objDAL.MergedPatientData(existingPatientID, NewPatientID, StaffID);
        }

        public void SaveSalesAccountCode(int salesAccountCode)
        {
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
             objDAL.SaveSalesAccountCode(salesAccountCode);
        }

        public SalesAccountCodeViewModel GetSalesAccountCode()
        {
            EmailTemplateDAL objDAL = new EmailTemplateDAL();
            return objDAL.GetSalesAccountCode();


        }
    }
}
