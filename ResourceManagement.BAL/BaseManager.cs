using ResourceManagement.DAL;
using ResourceManagement.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagement.BAL
{
    public class BaseManager
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public IDynamicRepository DynamicRepository { get; set; }

        public void BeginTransaction()
        {
            UnitOfWork.Begin();
        }
        public void CommitTransaction()
        {
            UnitOfWork.Commit();
        }
        public void RollBackTransaction()
        {
            UnitOfWork.Rollback();
        }
    }
}
