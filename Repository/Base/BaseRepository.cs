using DataAccess.CustomConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Repositoriy.Base
{
    public abstract class BaseRepository
    {
        protected readonly ICustomConnection mConnection;

        protected const StringComparison IgnoreCase = StringComparison.OrdinalIgnoreCase;
        protected const bool ACTIVO = true;

        public BaseRepository(ICustomConnection connection)
        {
            mConnection = connection;
        }

        protected object IsNull(object value)
        {
            if (value == null)
                return DBNull.Value;
            else if (value.GetType() == typeof(String) && string.IsNullOrEmpty(value.ToString()))
                return DBNull.Value;
            else
                return value;
        }

        //protected TransactionScope GetTransaction() => new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

    }
}
