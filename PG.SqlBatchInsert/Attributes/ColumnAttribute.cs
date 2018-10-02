using System;
using System.Runtime.CompilerServices;

namespace PG.SqlBatchInsert.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute, IColumnAttribute
    {
        public ColumnAttribute([CallerMemberName] string columnName = null, bool isSingleValue = false)
        {
            ColumnName = columnName;
            IsSingleValue = isSingleValue;
        }

        public string ColumnName { get; }

        public bool IsSingleValue { get; }
    }
}