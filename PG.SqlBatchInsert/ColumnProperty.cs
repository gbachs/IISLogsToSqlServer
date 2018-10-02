using System;
using System.Reflection;
using PG.SqlBatchInsert.Attributes;

namespace PG.SqlBatchInsert
{
    public partial class SqlMetadata<T>
    {
        public class ColumnProperty
        {
            public ColumnProperty(PropertyInfo property, IColumnAttribute columnAttribute)
            {
                Property = property;
                PropertyName = property.Name;
                ColumnName = columnAttribute.ColumnName;
                IsSingleValue = columnAttribute.IsSingleValue;
            }

            private PropertyInfo Property { get; }
            public string PropertyName { get; }
            public string ColumnName { get; }
            public bool IsSingleValue { get; }

            public TType GetValue<TType>(T obj)
            {
                return (TType) Property.GetValue(obj, null);
            }

            public object GetValue(T obj)
            {
                return Property.GetValue(obj, null);
            }

            public void SetValue(T obj, object value)
            {
                if (Convert.IsDBNull(value))
                    value = null;
                if (Property.PropertyType.IsEnum)
                {
                    var stringValue = value as string;
                    if (stringValue != null)
                        value = Enum.Parse(Property.PropertyType, stringValue);
                }

                Property.SetValue(obj, value, null);
            }
        }
    }
}