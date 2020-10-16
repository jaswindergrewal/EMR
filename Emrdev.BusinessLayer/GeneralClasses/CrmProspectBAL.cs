using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emrdev.DataLayer;
using Emrdev.DataLayer.GenaralClasses;

namespace Emrdev.BusinessLayer.GeneralClasses
{
    public class CrmProspectBAL //: ObjectEntity, IRepositary 
      {
        CrmProspectDAL objProspectsrevice = new CrmProspectDAL();


        public List<CRM_Prospects> GetAllCrmProspect()
             {
                 List<CRM_Prospects> objProspect = objProspectsrevice.List<CRM_Prospects>().ToList();
                 return objProspect; 
             }
             
             //public void CreateAlbum( Album  ObjAlbum)
             //{
             //    objAlbumesrevice.Create(ObjAlbum);
             //}
          
             //public void DeleteAlbum(Album ObjAlbum)
             //{
             //    objAlbumesrevice.Delete(ObjAlbum);
             //}

             //public Album  GetAlbumForEdit(int id)
             //{
             //    //List<Album> objAlbum = objAlbumesrevice.Edit<Album>(whereCondition);
             //    Album objAlbum= objAlbumesrevice.Get<Album>(e => e.AlbumId== id);
             //    return objAlbum; 
             //}
             //public void UpdateAlbumAfterEdit(Album ObjAlbum)
             //{
             //    objAlbumesrevice.Edit(ObjAlbum);  
             //}
           

      }
}
