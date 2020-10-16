using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmailTemplateService" in both code and config file together.
    [ServiceContract]
    public interface IEmailTemplateService
    {
        [OperationContract]
        void SaveEmailTemplate(string EmailContent, int AppointmentTypeId);

        [OperationContract]
        EmailTemplateViewModel GetEmailTemplate(int AppointmentTypeId);

        [OperationContract]
        void SaveIVRTemplate(string EmailContent, int AppointmentTypeId);

        [OperationContract]
        EmailTemplateViewModel GetIVRTemplate(int AppointmentTypeId);

        [OperationContract]
        void SaveCRMEmailTemplate(string EmailContent, string WufooLink, bool IsActive);

        [OperationContract]
        EmailTemplateViewModel GetAutoShipEmailTemplate();

        [OperationContract]
        void SaveAutoShipEmailTemplate(string EmailContent);

        [OperationContract]
        CRMEmailTemplateViewModel GetCRMEmailTemplate();

        [OperationContract]
        int insertSurveyQuestions(string Title, string Type,string FieldName);

        [OperationContract]
        void insertSurveyQuestionsAnswer(string FieldName, string Answer, int patientID, DateTime createdBy, string EntryID,int ApptID);

        [OperationContract]
        void UpdateQuestionAsActive(string ActiveQuestions);
       
        [OperationContract]
        List<MergedPatientViewModel> GetMergedPatientRecord(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText);

        [OperationContract]
        bool UndoMergedPatient(int MergedPatientID);

        [OperationContract]
        List<MergedPatientViewModel> GetPatienstLisTotMerge(int page, int rows, string sord, string sidx, int IsSearch, string SearchColumn, string SearchText);

        [OperationContract]
        bool MergedPatientData(int existingPatientID, int NewPatientID, int StaffID);

        [OperationContract]
        void SaveSalesAccountCode(int salesAccountCode);

        [OperationContract]
        SalesAccountCodeViewModel GetSalesAccountCode();
      
    }
}
