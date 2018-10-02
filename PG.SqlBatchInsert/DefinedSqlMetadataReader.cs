using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PG.SqlBatchInsert
{
    public class DefinedSqlMetadataReader : IDataReader
    {
        private readonly List<Column> _columns;
        private readonly Dictionary<string, int> _ordinalLookup;
        private IEnumerator<object[]> _enumerator;

        public DefinedSqlMetadataReader(IList<Column> columns, List<object[]> items)
        {
            _columns = columns.ToList();
            var idx = 0;
            _ordinalLookup = _columns.ToDictionary(x => x.Name, x => idx++);
            _enumerator = items.GetEnumerator();
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsClosed => _enumerator == null;

        public bool Read()
        {
            if (_enumerator == null)
                throw new ObjectDisposedException("ObjectDataReader");
            return _enumerator.MoveNext();
        }

        public int FieldCount => _columns.Count;

        public int GetOrdinal(string name)
        {
            int ordinal;
            if (!_ordinalLookup.TryGetValue(name, out ordinal))
                throw new InvalidOperationException("Unknown parameter name " + name);
            return ordinal;
        }

        public object GetValue(int i)
        {
            if (_enumerator == null)
                throw new ObjectDisposedException("ObjectDataReader");
            return _enumerator.Current[i];
        }

        public int Depth => 1;

        public DataTable GetSchemaTable()
        {
            return null;
        }

        public int RecordsAffected => -1;

        public bool NextResult()
        {
            return false;
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[string name] => throw new NotImplementedException();

        public object this[int i] => throw new NotImplementedException();

        protected void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_enumerator == null)
                return;

            _enumerator.Dispose();
            _enumerator = null;
        }
    }
}