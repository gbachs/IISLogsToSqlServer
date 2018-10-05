using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastMember;
using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.Parser.Parsers
{
    public class FieldsParser
	{
	    public FieldMapping Parse(string line)
	    {
	        var fieldMap = new FieldMapping();
	        var w3CFields = typeof(LogEvent).GetProperties();
	        var lineFields = line.Split(' ');
	        var lineFieldsIndex = 0;
	        var accessors = TypeAccessor.Create(typeof(LogEvent));
	        foreach (var lineField in lineFields)
	        {
	            var index = lineFieldsIndex;
	            GetFieldAttributeByName(lineField, w3CFields, (fieldAttribute, fieldInfo) =>
	            {
	                fieldMap.Add(index, new FieldMapInfo(fieldAttribute.Convertor, accessors, fieldInfo.Name));
	            });
	            lineFieldsIndex += 1;
	        }
	        return fieldMap;
	    }

        private static void GetFieldAttributeByName(string name, IEnumerable<PropertyInfo> w3CFields, Action<FieldBaseAttribute, PropertyInfo> foundBlock)
		{
		    foreach (var w3CField in w3CFields)
		    {
		        var fieldAttribute = (FieldBaseAttribute)w3CField.GetCustomAttributes(typeof(FieldBaseAttribute), false).FirstOrDefault();
		        if (fieldAttribute != null && fieldAttribute.FieldName == name)
		        {
		            foundBlock(fieldAttribute, w3CField);
		            break;
		        }
		    }
		}
	}
}
