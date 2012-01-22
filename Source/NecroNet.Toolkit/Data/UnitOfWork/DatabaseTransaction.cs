using System;
using System.Data;
using System.Data.Common;
using NecroNet.Toolkit.Configuration;

namespace NecroNet.Toolkit.Data
{
    public class DatabaseTransaction : ITransaction
    {
        private readonly DbTransaction _transaction;
        private bool _wasCommitted;

        public DatabaseTransaction(DbConnection connection, IsolationLevel? isolationLevel)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            _transaction = connection.BeginTransaction(isolationLevel ?? NecroNetToolkitConfigurationManager.GetOption(c => c.UnitOfWork.Transaction.IsolationLevel));
        }

        public void Dispose()
        {
            if (!_wasCommitted)
            {
                _transaction.Rollback();
            }

            _transaction.Dispose();
        }

        public void Commit()
        {
            _transaction.Commit();
            _wasCommitted = true;
        }
    }
}