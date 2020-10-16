using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emrdev.DataLayer.GeneralClasses
{
    public class PanelDAL:ObjectEntity,IRepositary
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
            ObjectEntity1.Set<T>();
            ObjectEntity1.Entry(entityToDelete).State = EntityState.Deleted;
            ObjectEntity1.SaveChanges();
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


        #region Select All External Panel(s)

        public List<Emrdev.ViewModelLayer.ExternalPanelViewModel> SelectAllPanel()
        {
            return ObjectEntity1.ExternalPanels.OrderBy(i => i.PanelName).Select(i => new Emrdev.ViewModelLayer.ExternalPanelViewModel {ExternalPanelsID=i.ExternalPanelsID,PanelName=i.PanelName }).ToList();
        }

        #endregion


        #region Delete Selected Panel

        public void DeletePanel(int panelId)
        {
            ExternalPanel objPanel=ObjectEntity1.ExternalPanels.SingleOrDefault(i => i.ExternalPanelsID == panelId);
            this.Delete<ExternalPanel>(objPanel);                      
        }

        #endregion


        #region Update Panel

        public void UpdatePanel(int panelId, string panelName)
        {
            ExternalPanel objPanel=ObjectEntity1.ExternalPanels.SingleOrDefault(i => i.ExternalPanelsID == panelId);
            objPanel.PanelName = panelName;
            ObjectEntity1.SaveChanges();
        }

        #endregion


        #region Insert Panel

        public void InsertPanel(string panelName)
        {
            ExternalPanel objPanel=new ExternalPanel();
            objPanel.PanelName = panelName;
            Create<ExternalPanel>(objPanel);
        }


        #endregion
    }
}
