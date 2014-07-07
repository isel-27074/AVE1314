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
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _dr;
        private string[] _columns;
        private bool _persistant;
        private Dictionary<Type, IDataMapper> _listOfMappers;
        private bool _commitable;

        public SqlEnumerable(SqlConnection con, bool persistant, string[] columns, Dictionary<Type, IDataMapper> listOfMappers, bool commitable, SqlCommand cmd)
        {
            _connection = con;
            _persistant = persistant;
            _columns = columns;
            _listOfMappers = listOfMappers;
            _commitable = commitable;
            _command = cmd;
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

            //percorro o data reader
            foreach (var dr in _dr)
            {
                object[] o = new object[_columns.Length];
                for (int i = 0; i < _columns.Length; i++)
                {
                    if (listOfFKs.Count != 0)
                    {
                        foreach (var loFKs in listOfFKs)//percorro a lista de FKs
                        {
                            if (loFKs.Key.Equals(_dr.GetName(i)))//vejo se a chave da lista de FKs é igual ao nome da coluna actual
                            {
                                #region REGIAOemTESTES
                                foreach (var mapper in _listOfMappers)//percorro a lista de mappers
                                {
                                    if (mapper.Key.Equals(loFKs.Value))//vejo se o tipo do mapper é do tipo da FK
                                    {
                                        IDataMapper localmapper = mapper.Value;
                                        //string text = _command.CommandText;
                                        //Console.WriteLine("-------------->" + text);
                                        
                                        string condition = _dr.GetName(i);
                                        Console.WriteLine(_dr.GetName(i));
                                        Console.WriteLine(_dr[i]);
                                        var value = _dr[i];
                                        if (_dr.GetName(i).GetType() == typeof(String))
                                            value = "\'" + value.ToString() + "\'";
                                        _dr.Close();//liberto o DataReader

                                        Console.WriteLine("mapper.Value = " + mapper.Value + "\n" + condition + " = " + value);
                                        var obj = localmapper.GetAll().Where(condition + " = " + value);
                                        
                                        object[] sol = null;

                                        foreach (var o1 in obj)
                                        {
                                            sol = o1.ToString().Split('-');
                                            foreach(var s in sol)
                                                Console.WriteLine(s);
                                        }

                                        var l = Activator.CreateInstance(mapper.Key, sol);
                                        o[i] = l;
                                        //string text2 = _command.CommandText;
                                        //Console.WriteLine("-------------->" + text2);
                                        _dr = _command.ExecuteReader();//reactivo o DataReader
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                o[i] = _dr[i];
                            }
                        }
                    }
                    else
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

        ISqlEnumerable ISqlEnumerable.Where(string clause)
        {
            return Where(clause);
        }
    }

}
