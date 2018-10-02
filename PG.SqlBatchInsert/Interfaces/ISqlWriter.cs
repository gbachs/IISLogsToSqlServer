using System;
using System.Collections.Generic;
using System.Data;

namespace PG.SqlBatchInsert.Interfaces
{
    public interface ISqlWriter<T> : IDisposable
    {
        void Write(IDbTransaction transaction, List<T> items);
    }
}