using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SqlMapper_v2
{
    class SQLEnumerable<T> : ISqlEnumerable<T>
    {
        private SqlCommand _command;
        private String whereclauses;

        public SQLEnumerable(String cmd)
        {
            _command.CommandText = cmd;
           
        }

        public ISqlEnumerable<T> where(string clause)
        {
            _command.CommandText = (!_command.CommandText.Contains("where") ? "+ where" : "");
            _command.CommandText += "and" + clause.ToString();
            return new SQLEnumerable<T>(_command.CommandText);
            //return this;
        }


        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
