using ResourceManagement.DAL.ConnectionFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManagement.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction = null;
        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.GetConnection;
        }

        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        public void Begin()
        {
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_transaction != null)
                _transaction.Commit();

            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
            Dispose();
        }

        public void Rollback()
        {
            if (_transaction != null)
                _transaction.Rollback();

            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();

            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();

            _transaction = null;
        }

    }
}
