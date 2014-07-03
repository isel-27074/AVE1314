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


        /*
         
         public IEnumerable<T> GetAll()
        {
            if (!_persistant) _connnection.Open();
            if (_connnection.State != ConnectionState.Open)
                _connnection.Open(); //abre se não estava aberta
            PreparedStatement(FormatStringGetAll(_table));
            _dr = _command.ExecuteReader();

            foreach (var dr in _dr)
            {
                object[] o = new object[_columns.Length];
                for (int i = 0; i < _columns.Length; i++)
                {
                    o[i] = _dr[i];
                }
                T newT = (T)Activator.CreateInstance(typeof(T), o);
                yield return newT;
            }

            if (!_persistant) _connnection.Close();
            _dr.Close();
        }
         
         */

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
