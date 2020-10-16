using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.DataLayer;
using Emrdev.ViewModelLayer;
using System.Data;

namespace Emrdev.BusinessLayer.GeneralClasses
{

    public class LabScehduleGroupsBAL
    {
        LabScehduleGroupsDAL objLabScehduleGroupsDAL = new LabScehduleGroupsDAL();
        public List<LabScehduleGroupsViewModel> GetLabScehduleGroupsDetails()
        {
            List<LabScehduleGroupsViewModel> objLst = objLabScehduleGroupsDAL.GetLabScehduleGroupsDetails().OrderBy(p => p.GroupName).ToList();
            return objLst;
        }

        public List<LabScheduleTestsViewModel> GetLabScehduleTestDetails()
        {
            List<LabScheduleTestsViewModel> objLst = objLabScehduleGroupsDAL.GetLabScehduleTestDetails().OrderBy(p => p.TestName).ToList();
            return objLst;
        }

        public LabScehduleGroupsViewModel GetLabScehduleGroupListByGroupId(int groupId)
        {
            LabScehduleGroupsViewModel clsObj = objLabScehduleGroupsDAL.GetLabScehduleGroupListByGroupId(groupId);
            return clsObj;
        }

        public LabScheduleTestsViewModel GetLabScehduleTestListByTestId(int testId)
        {
            LabScheduleTestsViewModel clsObj = objLabScehduleGroupsDAL.GetLabScehduleTestListByTestId(testId);
            return clsObj;
        }

        public void InsertLabScehduleGroups(LabScehduleGroupsViewModel viewModelQB)
        {
            LabScehdule_Groups cls = new LabScehdule_Groups();
            AutoMapper.Mapper.CreateMap<LabScehduleGroupsViewModel, LabScehdule_Groups>();
            cls = AutoMapper.Mapper.Map(viewModelQB, cls);
            objLabScehduleGroupsDAL.Create(cls);
        }

        public void UpdateLabScehduleGroups(LabScehduleGroupsViewModel viewModelGroup)
        {
            LabScehdule_Groups clsGroup = new LabScehdule_Groups();
            AutoMapper.Mapper.CreateMap<LabScehduleGroupsViewModel, LabScehdule_Groups>();
            clsGroup = AutoMapper.Mapper.Map(viewModelGroup, clsGroup);
            objLabScehduleGroupsDAL.Edit(clsGroup);
        }

        public void InsertLabScehduleTest(LabScheduleTestsViewModel viewModelQB)
        {
            LabSchedule_Tests cls = new LabSchedule_Tests();
            AutoMapper.Mapper.CreateMap<LabScheduleTestsViewModel, LabSchedule_Tests>();
            cls = AutoMapper.Mapper.Map(viewModelQB, cls);
            objLabScehduleGroupsDAL.Create(cls);
        }

        public void UpdateLabScehduleTest(LabScheduleTestsViewModel viewModelTest)
        {
            LabSchedule_Tests clsLabTest = new LabSchedule_Tests();
            AutoMapper.Mapper.CreateMap<LabScheduleTestsViewModel, LabSchedule_Tests>();
            clsLabTest = AutoMapper.Mapper.Map(viewModelTest, clsLabTest);
            objLabScehduleGroupsDAL.Edit(clsLabTest);
        }

        public void DeleteLabScehduleGroups(int Id)
        {
            objLabScehduleGroupsDAL.DeleteLabScehduleGroups(Id);
        }

        public void DeleteLabScheduleTests(int Id)
        {
            objLabScehduleGroupsDAL.DeleteLabScheduleTests(Id);
        }

        /// <summary>
        /// method for check the duplicate records in LabScehdule_Groups and LabSchedule_Tests tables
        /// during add/update the data
        /// used admin_LabSchedule.aspx.cs
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool CheckDuplicateRecords(int ID, string Name, string tableName)
        {
            bool isExist = false;
            switch (tableName)
            {
                case "LabScehdule_Groups":
                    if (ID == 0)
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.LabScehdule_Groups>(o => o.GroupName == Name);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.LabScehdule_Groups>(o => o.GroupName == Name && o.GroupID != ID);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
                case "LabSchedule_Tests":
                    if (ID == 0)
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.LabSchedule_Tests>(o => o.TestName == Name);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.LabSchedule_Tests>(o => o.TestName == Name && o.TestID != ID);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
                case "ExternalPanels": // used in External/Panels.aspx.cs
                    if (ID == 0)
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.ExternalPanel>(o => o.PanelName == Name);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.ExternalPanel>(o => o.PanelName == Name && o.ExternalPanelsID != ID);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
                case "Tickets_Manage": // used in Tickats_Manage.aspx.cs
                    if (ID == 0)
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.Tickets_Manage>(o => o.ProcessName == Name);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.Tickets_Manage>(o => o.ProcessName == Name && o.ProcessID != ID);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
                case "apt_FollowUp_types": // used in Tickats_Manage.aspx.cs
                    if (ID == 0)
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.apt_FollowUp_types>(o => o.FollowUp_Type_Desc == Name);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.apt_FollowUp_types>(o => o.FollowUp_Type_Desc == Name && o.FollowUp_Type_ID != ID);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
                case "AutoshipCancelReasons": // used in Autoship/Autoship_CancelReason.aspx.cs
                    if (ID == 0)
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.AutoshipCancelReason>(o => o.ReasonName == Name);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.AutoshipCancelReason>(o => o.ReasonName == Name && o.ReasonID != ID);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
                case "AutoshipProducts": // used in Autoship/AutoShip.aspx.cs
                    if (ID == 0)
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.AutoshipProduct>(o => o.ProductName == Name);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.AutoshipProduct>(o => o.ProductName == Name && o.ProductID != ID);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
                case "Drugs": // used in Admin_Drug_List.aspx.cs
                    if (ID == 0)
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.Drug>(o => o.DrugName == Name);
                        if (objfirst != null)
                            isExist = true;
                    }
                    else
                    {
                        var objfirst = objLabScehduleGroupsDAL.Get<Emrdev.DataLayer.Drug>(o => o.DrugName == Name && o.DrugID != ID);
                        if (objfirst != null)
                            isExist = true;
                    }
                    break;
              
            }

            return isExist;

        }
    }
}
