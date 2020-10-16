using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Emrdev.ViewModelLayer;

namespace Emrdev.ServiceLayer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILabScehduleService" in both code and config file together.
    [ServiceContract]
    public interface ILabScehduleService
    {
        [OperationContract]
        List<LabScehduleGroupsViewModel> GetLabScehduleGroupsDetails();

        [OperationContract]
        List<LabScheduleTestsViewModel> GetLabScehduleTestDetails();

        [OperationContract]
        LabScehduleGroupsViewModel GetLabScehduleGroupListByGroupId(int groupId);

        [OperationContract]
        LabScheduleTestsViewModel GetLabScehduleTestListByTestId(int testId);

        [OperationContract]
        void InsertLabScehduleGroups(LabScehduleGroupsViewModel viewModelQB);

        [OperationContract]
        void UpdateLabScehduleGroups(LabScehduleGroupsViewModel viewModelQB);

        [OperationContract]
        void InsertLabScehduleTest(LabScheduleTestsViewModel viewModelQB);

        [OperationContract]
        void UpdateLabScehduleTest(LabScheduleTestsViewModel viewModelQB);

        [OperationContract]
        bool CheckDuplicateRecords(int ID, string Name, string tableName);

        [OperationContract]
        void DeleteLabScehduleGroups(int Id);

        [OperationContract]
        void DeleteLabScheduleTests(int Id);

        [OperationContract]
        void DoWork();
    }
}
