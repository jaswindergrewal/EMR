using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GeneralClasses;
using AutoMapper;

namespace Emrdev.GeneralClasses
{
    public class LabLogBAL
    {
        LabLogDAL objDAL = new LabLogDAL();
        
        /// <summary>
        /// Get the detail list for labs
        /// </summary>
        /// <returns></returns>
        public List<LabLogViewModel> GetLabLogList()
        {
            var _objLabLogList = new List<LabLogViewModel>();
            var LabLogEntity = new List<LabLog>();
            LabLogEntity = objDAL.GetAll<LabLog>(o => o.LabLogID > 0).OrderByDescending(o => o.DateImported).ToList();
            Mapper.CreateMap<LabLog, LabLogViewModel>();
            _objLabLogList = Mapper.Map(LabLogEntity, _objLabLogList);
            return _objLabLogList;
        }

        /// <summary>
        /// Get the details list of Lab panels
        /// </summary>
        /// <returns></returns>
        public List<LabRequestPanelViewModel> GetLabPanelList()
        {
            var _objLabPanelList = new List<LabRequestPanelViewModel>();
            var LabPanelEntity = new List<LabRequest_Panels>();
            LabPanelEntity = objDAL.GetAll<LabRequest_Panels>(o => o.LabRequest_PanelID > 0).OrderBy(o => o.PanelName).ToList();
            Mapper.CreateMap<LabRequest_Panels, LabRequestPanelViewModel>();
            _objLabPanelList = Mapper.Map(LabPanelEntity, _objLabPanelList);
            return _objLabPanelList;
        }

        public List<LabRequestTestViewModel> GetLabTestList()
        {
            var _objLabTestList = new List<LabRequestTestViewModel>();
            var LabTestEntity = new List<LabRequest_Tests>();
            LabTestEntity = objDAL.GetAll<LabRequest_Tests>(o => o.LabRequest_TestID > 0).OrderBy(o => o.TestName).ToList();
            Mapper.CreateMap<LabRequest_Tests, LabRequestTestViewModel>();
            _objLabTestList = Mapper.Map(LabTestEntity, _objLabTestList);
            return _objLabTestList;
        }

        public void InsertUpdateLabRequests(string gridName, int RequestID, bool Active, string RequestLabName)
        {
            objDAL.InsertUpdateLabRequests(gridName, RequestID, Active, RequestLabName);
        }

        /// <summary>
        /// method for check the duplicate records in LabRequest_Panels table
        /// during add/update the data
        /// used admin_LabRequest.aspx.cs
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public bool CheckDuplicatePanel(int PanelID, string PanelName)
        {
            bool isExist = false;
            if (PanelID == 0)
            {
                var objfirst = objDAL.Get<Emrdev.DataLayer.LabRequest_Panels>(o => o.PanelName == PanelName);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = objDAL.Get<Emrdev.DataLayer.LabRequest_Panels>(o => o.PanelName == PanelName && o.LabRequest_PanelID != PanelID);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;

        }

        /// <summary>
        /// method for check the duplicate records in Labrequest_Tests table
        /// during add/update the data
        /// used admin_LabRequest.aspx.cs
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="EventName"></param>
        /// <returns></returns>
        public bool CheckDuplicateTest(int testID, string testName)
        {
            bool isExist = false;
            if (testID == 0)
            {
                var objfirst = objDAL.Get<Emrdev.DataLayer.LabRequest_Tests>(o => o.TestName == testName);
                if (objfirst != null)
                    isExist = true;
            }
            else
            {
                var objfirst = objDAL.Get<Emrdev.DataLayer.LabRequest_Tests>(o => o.TestName == testName && o.LabRequest_TestID != testID);
                if (objfirst != null)
                    isExist = true;
            }
            return isExist;

        }

        public void DeleteLabRequestPanels(int Id)
        {
            objDAL.DeleteLabRequestPanels(Id);
        }

        public void DeleteLabRequestTests(int Id)
        {
            objDAL.DeleteLabRequestTests(Id);
        }
    }
}
