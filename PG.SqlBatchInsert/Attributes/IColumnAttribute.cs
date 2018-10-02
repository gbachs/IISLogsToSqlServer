namespace PG.SqlBatchInsert.Attributes
{
    public interface IColumnAttribute
    {
        string ColumnName { get; }
        bool IsSingleValue { get; }
    }
}