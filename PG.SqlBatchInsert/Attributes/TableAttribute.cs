using System;

namespace PG.SqlBatchInsert.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }

        public string TableName { get; }
    }
}