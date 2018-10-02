using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.Parser
{
    public class W3CFieldsParser
	{
	    public W3CFieldMap Parse(string line)
	    {
	        var fieldMap = new W3CFieldMap();
	        var w3CFields = typeof(W3CEvent).GetProperties();
	        var lineFields = line.Split(' ');
	        var lineFieldsIndex = 0;

	        foreach (var lineField in lineFields)
	        {
	            var index = lineFieldsIndex;
	            GetFieldAttributeByName(lineField, w3CFields, (fieldAttribute, fieldInfo) =>
	            {
	                fieldMap.Add(index, fieldAttribute.Convertor, fieldInfo);
	            });
	            lineFieldsIndex += 1;
	        }
	        return fieldMap;
	    }

        private static void GetFieldAttributeByName(string name, IEnumerable<PropertyInfo> w3CFields, Action<W3CFieldBaseAttribute, PropertyInfo> foundBlock)
		{
		    foreach (var w3CField in w3CFields)
		    {
		        var fieldAttribute = (W3CFieldBaseAttribute)w3CField.GetCustomAttributes(typeof(W3CFieldBaseAttribute), false).FirstOrDefault();
		        if (fieldAttribute != null && fieldAttribute.FieldName == name)
		        {
		            foundBlock(fieldAttribute, w3CField);
		            break;
		        }
		    }
		}
	}
}
