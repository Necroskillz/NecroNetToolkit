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
            IsolationLevel isolationLevelActual;

            if (NecroNetToolkitConfigurationManager.Configuration != null)
            {
                var transactionConfig = NecroNetToolkitConfigurationManager.Configuration.UnitOfWork.Transaction;
                isolationLevelActual = isolationLevel ?? transactionConfig.IsolationLevel;
            }
            else
            {
                isolationLevelActual = isolationLevel ?? default(IsolationLevel);
            }

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            _transaction = connection.BeginTransaction(isolationLevelActual);
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