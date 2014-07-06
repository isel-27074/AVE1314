using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlMapper_v3
{
    public class SqlEnumerable<T> : ISqlEnumerable<T>
    {
        private SqlConnection _connnection;
        private SqlCommand _command;
        private SqlDataReader _dr;
        private string[] _columns;
        private bool _persistant;
        private bool _commitable;

        public SqlEnumerable(SqlConnection con, bool persistant, string table, string[] columns, bool commitable, SqlCommand cmd)
        {
            _connnection = con;
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
            if (!_persistant) _connnection.Open();
            if (_connnection.State != ConnectionState.Open)
                _connnection.Open(); //abre se não estava aberta
            _dr = _command.ExecuteReader();

            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();
            int numberOfProperties = properties.Length;
            FieldInfo[] fields = t.GetFields();
            int numberOfFields = fields.Length;
            var listOfFKs = new Dictionary<string, Type>();

            for (int i = 0; i < numberOfProperties; i++)
            {
                ForeignKeyAttribute fkattr = (ForeignKeyAttribute)properties[i].GetCustomAttribute(typeof(ForeignKeyAttribute));
                if ((fkattr != null) && (properties[i].PropertyType.IsClass)) listOfFKs.Add(properties[i].Name, properties[i].PropertyType);
            }
            for (int i = 0; i < numberOfFields; i++)
            {
                ForeignKeyAttribute fkattr = (ForeignKeyAttribute)fields[i].GetCustomAttribute(typeof(ForeignKeyAttribute));
                if ((fkattr != null) && (fields[i].FieldType.IsClass)) listOfFKs.Add(fields[i].Name, fields[i].FieldType);
            }
                        

            foreach (var dr in _dr)
            {
                object[] o = new object[_columns.Length];
                for (int i = 0; i < _columns.Length; i++)
                {
                    foreach(var loFKs in listOfFKs)
                    {
                        if(loFKs.Key.Equals(_dr.GetName(i)))
                        {
                            Type tfk = loFKs.Value;
                            //IDataMapper dmfk = new DataMapper<Type.GetType(tfk.Name)>(_connnection, _persistant,  _commitable);
                            var newtfk = Activator.CreateInstance(Type.GetType(tfk.Name));
                        }
                        else
                         o[i] = _dr[i];
                    }
                }
                T newT = (T)Activator.CreateInstance(typeof(T), o);
                yield return newT;
            }
            _dr.Close();
            if (!_persistant) _connnection.Close();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        ISqlEnumerable ISqlEnumerable.Where(string clause)
        {
            return Where(clause);
        }
    }

}
