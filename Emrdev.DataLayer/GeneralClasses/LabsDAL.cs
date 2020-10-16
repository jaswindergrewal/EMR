using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Emrdev.ViewModelLayer;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Configuration;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class LabsDAL : ObjectEntity, IRepositary
    {
        #region IRepositary Members

        public void Create<T>(T entityToCreate) where T : class
        {
            ObjectEntity1.Set<T>().Add(entityToCreate);
            ObjectEntity1.SaveChanges();
        }

        public void Edit<T>(T entityToEdit) where T : class
        {
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToEdit).State = EntityState.Modified;
            ObjectEntity1.SaveChanges();
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> List<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public T GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            return ObjectEntity1.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public long Count<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition) where T : class
        {
            throw new NotImplementedException();
        }

        public IList<T> GetDetails<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Select ExternalLabLists By PanelId

        public List<List<Emrdev.ViewModelLayer.LabsViewModel>> GetExternalLabListByPanelId(int? panelId)
        {
            List<List<Emrdev.ViewModelLayer.LabsViewModel>> lst=new List<List<LabsViewModel>>();
            lst.Add(ObjectEntity1.ExternalLabLists.Where(i => i.PanelID == panelId).Select(i => new Emrdev.ViewModelLayer.LabsViewModel { ExternalLabListID = i.ExternalLabListID, ExternaLabName = i.ExternaLabName, PanelID = i.PanelID }).ToList());
            lst.Add(ObjectEntity1.ExternalLabLists.Where(i => i.PanelID == null).Select(i => new Emrdev.ViewModelLayer.LabsViewModel { ExternalLabListID = i.ExternalLabListID, ExternaLabName = i.ExternaLabName, PanelID = i.PanelID }).ToList());
            return lst;
        }

        #endregion


        #region Update Panel for ExternalLabList By Id

        public void UpdatePanelForLabList(string labListId, int panelId)
        {
            ObjectEntity1.ssp_SetPanelIdForExternalList(panelId, labListId);
        }

        #endregion


        #region Set PanelId Null

        public void SetPanelIdNull(int ExternalListId)
        {
            ExternalLabList objLabList=ObjectEntity1.ExternalLabLists.SingleOrDefault(i => i.ExternalLabListID == ExternalListId);
            objLabList.PanelID = null;
            ObjectEntity1.SaveChanges();
        }

        #endregion


        #region Select External Panel List

        public List<Emrdev.ViewModelLayer.ExternalPanelViewModel> SelectAllExternalPanel()
        {
            return ObjectEntity1.ExternalPanels.OrderBy(i => i.PanelName).Select(i => new Emrdev.ViewModelLayer.ExternalPanelViewModel { ExternalPanelsID = i.ExternalPanelsID, PanelName = i.PanelName }).ToList();
        }

        #endregion

        /// <summary>
        /// Get the details for lab old chart details for appointment console page
        /// Jaswinder 8th oct 2013
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public DataTable GetLabOldChartDetails(int patientId)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                conn.Open();
                DataTable labTable = new DataTable();
                using (SqlCommand cmd = new SqlCommand("ssp_GetLabOldTableData", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@PatientID", patientId));
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(labTable);
                }
                return labTable;
            }
       
        }
    }
}
