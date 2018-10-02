using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PG.SqlBatchInsert.Attributes;

namespace PG.SqlBatchInsert
{
    public partial class SqlMetadata<T>
    {
        public SqlMetadata()
        {
            TableName = GetTableName();
            Properties = GetColumns<ColumnAttribute>().ToList();
        }

        public string TableName { get; set; }
        public List<ColumnProperty> Properties { get; }

        private static string GetTableName()
        {
            var tableAttributes = typeof(T).GetCustomAttributes(typeof(TableAttribute), true);

            if (!tableAttributes.Any())
            {
                return typeof(T).Name;
            }

            return ((TableAttribute) tableAttributes[0]).TableName;
        }

        private static List<ColumnProperty> GetColumns<TColumnType>()
            where TColumnType : Attribute, IColumnAttribute
        {
            return typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .Select(CreateColumnProperty<TColumnType>)
                .Where(x => x != null)
                .ToList();
        }

        private static ColumnProperty CreateColumnProperty<TColumnType>(PropertyInfo property)
            where TColumnType : Attribute, IColumnAttribute
        {
            var columnAttr =
                (IColumnAttribute) property.GetCustomAttributes(typeof(TColumnType), true).FirstOrDefault();
            return columnAttr == null ? null : new ColumnProperty(property, columnAttr);
        }

        private static ColumnProperty GetColumn<TColumnType>()
            where TColumnType : Attribute, IColumnAttribute
        {
            return GetColumns<TColumnType>().FirstOrDefault();
        }
    }
}