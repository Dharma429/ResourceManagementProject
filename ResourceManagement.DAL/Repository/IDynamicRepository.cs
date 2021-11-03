using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagement.DAL.Repository
{
    public interface IDynamicRepository
    {
        IUnitOfWork UnitOfWork { get; set; }
        int Add<T>(string uspName, T item);
        int AddOrUpdateDynamic(string uspName, dynamic entity);
        List<T> All<T>(string uspName, object param);
        void Delete(int id);
        int DeleteMultiple(string uspName, string ids);
        int DeleteMultiple<T>(string uspName, T entity);
        void Delete<T>(T entity);
        T Find<T>(string uspName, int id);
        T FindBy<T>(string uspName, object entityParam);
        T FindByName<T>(string name);
        int Update<T>(string uspName, T entity);
        //void BulkSave(DataTable item, string[] param);
        //void BulkSaveWithNewIdentity(DataTable item, string[] param);
    }
}
