using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace SqlMapper_v2
{
    public class SqlEnumerable<T> : ISqlEnumerable<T>
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _dr;
        private string[] _columns;
        private bool _persistant;
        private bool _commitable;

        public SqlEnumerable(SqlConnection con, bool persistant, string table, string[] columns, bool commitable, SqlCommand cmd)
        {
            _connection = con;
            _command = cmd;
            _columns = columns;
            _persistant = persistant;
            _commitable = commitable;
        }

        public ISqlEnumerable<T> Where(string clause)
        {
            bool wh = false;
            if (_command.CommandText.Contains("where"))
                wh  = true;
             if (wh)   
                 _command.CommandText  = _command.CommandText + " and " + clause;
             else
                _command.CommandText += " where " + clause;
            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!_persistant) _connection.Open();
            if (_connection.State != ConnectionState.Open)
                _connection.Open(); //abre se não estava aberta
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
            _dr.Close();
            if (!_persistant) _connection.Close();            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
