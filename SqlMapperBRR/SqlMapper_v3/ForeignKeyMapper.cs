using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v3
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public class ForeignKeyMapperAttribute : Attribute
    {
        public ForeignKeyMapperAttribute(string toTableName, Type typeOfTable)
        {
            ToTableName = toTableName;
            TypeOfTable = typeOfTable;
        }
        public string ToTableName { get; internal set; }
        public Type TypeOfTable { get; internal set; }
    }
}
