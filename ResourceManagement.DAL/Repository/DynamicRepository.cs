using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagement.DAL.Repository
{
    public class DynamicRepository : IDynamicRepository
    {
        //  [SimpleIoCPropertyInject]
        public IUnitOfWork UnitOfWork { get; set; }

        public DynamicRepository( IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;/*Single ton Pattern*/
        }
        public List<T> All<T>(string uspName, object param)
        {
            return UnitOfWork.Connection.Query<T>(
               uspName, param, commandType: CommandType.StoredProcedure
            ).ToList();
        }

        public T Find<T>(string uspName, int id)
        {
            return UnitOfWork.Connection.Query<T>(
              uspName, new { Id = id }, commandType: CommandType.StoredProcedure
           ).FirstOrDefault();
        }

        public T FindBy<T>(string uspName, object entityParam)
        {
            return UnitOfWork.Connection.Query<T>(
               uspName, entityParam, commandType: CommandType.StoredProcedure
            ).FirstOrDefault();
        }

        public int Add<T>(string uspName, T entity)
        {
            return AddorUpdate(uspName, entity, true);
        }

        public int Update<T>(string uspName, T entity)
        {
            return AddorUpdate(uspName, entity);
        }

        public int DeleteMultiple(string uspName, string ids)
        {
            return UnitOfWork.Connection.Execute(uspName, new { Id = ids }, transaction: UnitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        }

        public int DeleteMultiple<T>(string uspName, T entity)
        {
            int result = -1;
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(entity);
                dynamicParameters.Add("@res", dbType: DbType.Int32, direction: ParameterDirection.Output);
                UnitOfWork.Connection.Execute(uspName, dynamicParameters, commandType: CommandType.StoredProcedure);
                result = dynamicParameters.Get<int>("@res");
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public void Delete(int id)
        {
            UnitOfWork.Connection.Execute(
                "DELETE FROM T WHERE TId = @TId",
                param: new { TId = id }
            );
        }

        public void Delete<T>(T entity)
        {
            //Delete(entity.TId);
        }

        public T FindByName<T>(string name)
        {
            return UnitOfWork.Connection.Query<T>(
                "SELECT * FROM T WHERE Name = @Name",
                param: new { Name = name }
            ).FirstOrDefault();
        }

        private int AddorUpdate<T>(string uspName, T entity, bool isInsert = false)
        {
            int result = -1;
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(entity);

                if (isInsert)
                    dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                if (UnitOfWork.Connection.State == ConnectionState.Closed)
                    UnitOfWork.Connection.Open();

                if (UnitOfWork.Transaction != null)
                    result = UnitOfWork.Connection.Execute(uspName, dynamicParameters, transaction: UnitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                else
                    result = UnitOfWork.Connection.Execute(uspName, dynamicParameters, commandType: CommandType.StoredProcedure);

                result = dynamicParameters.Get<int>("@Id");

                if (UnitOfWork.Transaction == null && UnitOfWork.Connection.State == ConnectionState.Open)
                    UnitOfWork.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public int AddOrUpdateDynamic(string uspName, dynamic entity)
        {
            int result = -1;
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(entity);


                dynamicParameters.Add("@res", dbType: DbType.Int32, direction: ParameterDirection.Output);

                if (UnitOfWork.Connection.State == ConnectionState.Closed)
                    UnitOfWork.Connection.Open();

                if (UnitOfWork.Transaction != null)
                    result = UnitOfWork.Connection.Execute(uspName, dynamicParameters, transaction: UnitOfWork.Transaction, commandType: CommandType.StoredProcedure);
                else
                    result = UnitOfWork.Connection.Execute(uspName, dynamicParameters, commandType: CommandType.StoredProcedure);

                result = dynamicParameters.Get<int>("@res");

                if (UnitOfWork.Transaction == null && UnitOfWork.Connection.State == ConnectionState.Open)
                    UnitOfWork.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        //public void BulkSave(DataTable item, string[] param)
        //{

        //    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString(), SqlBulkCopyOptions.KeepIdentity))
        //    {
        //        bulkCopy.BatchSize = 100;
        //        bulkCopy.DestinationTableName = item.TableName;
        //        bulkCopy.WriteToServer(item);
        //    }
        //}

        //public void BulkSaveWithNewIdentity(DataTable item, string[] param)
        //{

        //    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
        //    {
        //        bulkCopy.BatchSize = 100;
        //        bulkCopy.DestinationTableName = item.TableName;
        //        bulkCopy.WriteToServer(item);
        //    }
        //}
    }
}
