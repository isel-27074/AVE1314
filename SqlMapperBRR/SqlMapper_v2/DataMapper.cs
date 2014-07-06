using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace SqlMapper_v2
{
    public class DataMapper<T> : IDataMapper<T> where T : class, new()
    {
        private SqlConnection _connnection;
        private SqlCommand _command;
        private SqlDataReader _dr;
        private string[] _columns;
        private string _table;
        private bool _persistant;
        private bool _commitable;
        private string prepStateLastInsertedRecord = "Select @@Identity";
        private int lastInsertedRecordID;

        private string prepStateGetAll = "SELECT * from {0}";
        private string prepStateInsert = "INSERT INTO {0} ({1}) VALUES ({2})";
        private string prepStateUpdate = "UPDATE {0} SET {2} WHERE {1}";
        private string prepStateDelete = "DELETE FROM {0} WHERE {1} = {2}";
        
        public DataMapper(SqlConnection con, bool persistant, string table, string[] columns, bool commitable)
        {
            _connnection = con;
            _command = new SqlCommand(null, con);
            _table = table;
            _columns = columns;
            _persistant = persistant;
            _commitable = commitable;
        }        

        #region GetAll
        public ISqlEnumerable<T> GetAll()
        {
            PreparedStatement(String.Format(prepStateGetAll, _table));
            return new SqlEnumerable<T>(_connnection, _command, _columns, _persistant);
        }
        #endregion

        #region Update
        public void Update(T val)
        {
            if (!_persistant) _connnection.Open();
            if (_connnection.State != ConnectionState.Open)
                _connnection.Open(); //abre se não estava aberta
            SqlTransaction trans = _connnection.BeginTransaction("Update Transaction");
            PreparedStatement(FormatStringUpdate(val));
            _command.Transaction = trans;
            _command.ExecuteNonQuery();
            if (_commitable)
                trans.Commit();
            else
                trans.Rollback();
            if (!_persistant) _connnection.Close();
        }

        //Dado um T, formatamos a string de Insert
        public string FormatStringUpdate(T val)
        {
            object[] args = FormatParameterUpdate(val);
            return String.Format(prepStateUpdate, args);
        }

        //Dado um T, devolvemos um array com o nome da tabela e os dados do T
        //"UPDATE {0} SET {2} WHERE {1}";
        public object[] FormatParameterUpdate(T val)
        {
            string conditionProperties = "";
            string conditionFields = "";
            string valuesProperties = "";
            string valuesFields = "";
            object[] newobj = new object[3];
            newobj[0] = _table;
            Type t = val.GetType();
            PropertyInfo[] properties = t.GetProperties();
            int numberOfProperties = properties.Length;
            FieldInfo[] fields = t.GetFields();
            int numberOfFields = fields.Length;

            //Percorrer a lista de propriedades
            for (int i = 0; i < numberOfProperties; i++)
            {
                KeyAttribute attr = (KeyAttribute)properties[i].GetCustomAttribute(typeof(KeyAttribute));
                if (attr != null)
                {
                    if (properties[i].PropertyType.Name.Equals("String"))
                        conditionProperties = properties[i].Name + " = " + "\'" + properties[i].GetValue(val) + "\'";
                    else
                        conditionProperties = properties[i].Name + " = " + properties[i].GetValue(val);
                }
                else
                {
                    if (properties[i].GetValue(val).GetType() == typeof(String) || properties[i].GetValue(val).GetType() == typeof(Char))
                    {
                        valuesProperties = valuesProperties + properties[i].Name + " = " + "\'" + properties[i].GetValue(val) + "\'";
                    }
                    else
                    {
                        valuesProperties = valuesProperties + properties[i].Name + " = " + properties[i].GetValue(val);
                    }
                    if (i != numberOfProperties - 1)
                    {
                        valuesProperties += ",";
                    }
                }
            }
            //Percorrer a lista de campos
            for (int i = 0; i < numberOfFields; i++)
            {
                KeyAttribute attr = (KeyAttribute)fields[i].GetCustomAttribute(typeof(KeyAttribute));
                if (attr != null)
                {
                    if (fields[i].FieldType.Name.Equals("String"))
                        conditionFields = fields[i].Name + " = " + "\'" + fields[i].GetValue(val) + "\'";
                    else
                        conditionFields = fields[i].Name + " = " + fields[i].GetValue(val);
                }
                else
                {
                    if (fields[i].GetValue(val).GetType() == typeof(String) || fields[i].GetValue(val).GetType() == typeof(Char))
                    {
                        valuesFields = valuesFields + fields[i].Name + " = " + "\'" + fields[i].GetValue(val) + "\'";
                    }
                    else
                    {
                        valuesFields = valuesFields + fields[i].Name + " = " + fields[i].GetValue(val);
                    }
                    if (i != numberOfFields - 1)
                    {
                        valuesFields += ",";
                    }
                }
            }
            //Valida se existe propriedades e campos anotados com Key e junta-os na condição
            if (conditionProperties != "" && conditionFields != "")
            {
                conditionProperties = conditionProperties + "," + conditionFields;
            }
            else
            {
                conditionProperties = conditionProperties + conditionFields;
            }
            //Junta os values de ambas as listas a fazer update
            if (valuesProperties != "" && valuesFields != "")
            {
                valuesProperties = valuesProperties + "," + valuesFields;
            }
            else
            {
                valuesProperties = valuesProperties + valuesFields;
            }

            newobj[1] = conditionProperties;
            newobj[2] = valuesProperties;
            return newobj;
        }
        #endregion

        #region Delete
        public void Delete(T val)
        {
            if (!_persistant) _connnection.Open();
            if (_connnection.State != ConnectionState.Open)
                _connnection.Open(); //abre se não estava aberta
            SqlTransaction trans = _connnection.BeginTransaction("Delete Transaction");
            PreparedStatement(FormatStringDelete(val));
            _command.Transaction = trans;
            _command.ExecuteNonQuery();
            if (_commitable)
                trans.Commit();
            else
                trans.Rollback();
            if (!_persistant) _connnection.Close();
        }

        //Dado um T, formatamos a string de Insert
        public string FormatStringDelete(T val)
        {
            object[] args = FormatParameterDelete(val);
            return String.Format(prepStateDelete, args);
        }

        //Dado um T, devolvemos um array com o nome da tabela e os dados do T
        public object[] FormatParameterDelete(T val)
        {
            object[] newobj = new object[3];
            newobj[0] = _table;
            Type t = val.GetType();
            MemberInfo[] mi = t.GetMembers();
            string columnKey = null;
            foreach (MemberInfo m in mi)
            {
                KeyAttribute attr = (KeyAttribute)m.GetCustomAttribute(typeof(KeyAttribute));
                if (attr != null)
                {
                    columnKey = m.Name;
                    break;// assumimos que cada tabela tem apenas um Key
                }
            }
            newobj[1] = columnKey;
            object value = val.GetType().GetProperty(columnKey).GetValue(val);
            newobj[2] = value.ToString();
            return newobj;
        }
        #endregion

        #region Insert
        public void Insert(T val)
        {
            if (!_persistant) _connnection.Open();
            if (_connnection.State != ConnectionState.Open)
                _connnection.Open(); //abre se não estava aberta
            SqlTransaction trans = _connnection.BeginTransaction("Insert Transaction");
            PreparedStatement(FormatStringInsert(val));
            _command.Transaction = trans;
            _command.ExecuteNonQuery();
            if (_commitable)
            {
                trans.Commit();
                //Obter o ID do ultimo item inserido
                _command.CommandText = prepStateLastInsertedRecord;
                var lastInsertedID = (decimal)_command.ExecuteScalar();
                lastInsertedRecordID = Decimal.ToInt32(lastInsertedID);
            }
            else
                trans.Rollback();
            //mFmt.format(pattern, values)
            //MessageFormat mFmt = new MessageFormat(pattern);
            ////SqlTransaction
            ////http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqltransaction.aspx
            //throw new NotImplementedException();
            if (!_persistant) _connnection.Close();
        }

        //Dado um T, formatamos a string de Insert
        private string FormatStringInsert(T val)
        {
            object[] args = FormatParameterInsert(val);
            return String.Format(prepStateInsert, args);
        }

        //Dado um T, devolvemos um array com o nome da tabela e os dados do T
        private object[] FormatParameterInsert(T val)
        {
            //insert tem 3 argumentos, tabela, nome de colunas e valores de colunas
            object[] newobj = new object[3];
            string columnProperties = "";
            string valuesProperties = "";
            string columnFields = "";
            string valuesFields = "";
            newobj[0] = _table;
            Type t = val.GetType();
            PropertyInfo[] properties = t.GetProperties();
            int numberOfProperties = properties.Length;
            FieldInfo[] fields = t.GetFields();
            int numberOfFields = fields.Length;
            string columnKey = "";
            string valueKey = "";
            //Percorrer a lista de propriedades
            for (int i = 0; i < numberOfProperties; i++)
            {
                KeyAttribute attr = (KeyAttribute)properties[i].GetCustomAttribute(typeof(KeyAttribute));
                if ((attr != null) && (properties[i].PropertyType.Name.Equals("String")))
                {
                    columnKey = columnKey + properties[i].Name + ",";
                    valueKey = valueKey + "\'" + val.GetType().GetProperty(columnKey).GetValue(val).ToString() + "\'" + ",";
                }
                if (attr == null)
                {
                    columnProperties += properties[i].Name;
                    if (properties[i].GetValue(val).GetType() == typeof(String) || properties[i].GetValue(val).GetType() == typeof(Char))
                    {
                        valuesProperties = valuesProperties + "\'" + properties[i].GetValue(val) + "\'";
                    }
                    else
                    {
                        valuesProperties += properties[i].GetValue(val);
                    }
                    if (i != numberOfProperties - 1)
                    {
                        columnProperties += ",";
                        valuesProperties += ",";
                    }
                }
            }
            //Percorrer a lista de campos
            for (int i = 0; i < numberOfFields; i++)
            {
                KeyAttribute attr = (KeyAttribute)fields[i].GetCustomAttribute(typeof(KeyAttribute));
                if ((attr != null) && fields[i].FieldType.Name.Equals("String"))
                {
                    columnKey = columnKey + fields[i].Name + ",";
                    valueKey = valueKey + "\'" + val.GetType().GetField(columnKey).GetValue(val).ToString() + "\'" + ",";
                }
                if (attr == null)
                {
                    columnFields += fields[i].Name;
                    if (fields[i].GetValue(val).GetType() == typeof(String) || fields[i].GetValue(val).GetType() == typeof(Char))
                    {
                        valuesFields = valuesFields + "\'" + fields[i].GetValue(val) + "\'";
                    }
                    else
                    {
                        valuesFields += fields[i].GetValue(val);
                    }
                    if (i != numberOfFields - 1)
                    {
                        columnFields += ",";
                        valuesFields += ",";
                    }
                }
            }
            if (columnProperties != "" && columnFields != "")
            {
                columnProperties = columnProperties + "," + columnFields;
                valuesProperties = valuesProperties + "," + valuesFields;
            }
            else
            {
                columnProperties = columnProperties + columnFields;
                valuesProperties = valuesProperties + valuesFields;
            }
            newobj[1] = columnKey + columnProperties;
            newobj[2] = valueKey + valuesProperties;
            return newobj;
        }
        #endregion

        private void PreparedStatement(string instruction)
        {
            _command.CommandText = instruction;
        }
        public int GetLastInsertedRecord() { return lastInsertedRecordID; }

        #region toCheck
        /*
         *             //int total = properties.Length + fields.Length;
            //MemberInfo[] members = t.GetMembers();
            //MemberInfo[] membersDefined = new MemberInfo[total];
            //int idx = 0;
            ////MemberInfo[] members = t.GetFields(BindingFlags.Public | BindingFlags.Instance).Cast<MemberInfo>().Concat(t.GetProperties(BindingFlags.Public | BindingFlags.Instance)).ToArray();
            //foreach (var m in members) {
            //    if (m.MemberType.Equals(MemberTypes.Field))
            //    {
            //        membersDefined[idx] = m;
            //        idx++;
            //    }
            //    if (m.MemberType.Equals(MemberTypes.Property))
            //    {
            //        membersDefined[idx] = m;
            //        idx++;
            //    }
            //}

            //foreach (var md in membersDefined) Console.WriteLine(md.Name);


         * 
        public IEnumerable<T> GetAll()
        {
            int count = 0;
            Console.WriteLine("chguei ao getall");
            while (_dr.Read())
            {
                T t = new T();
                Type tp = t.GetType();
                FieldInfo[] fi = tp.GetFields();
                foreach (FieldInfo fii in fi)
                    Console.WriteLine(fii.ToString());
                PropertyInfo[] pi = tp.GetProperties();
                foreach (PropertyInfo pii in pi)
                    Console.WriteLine(pii.ToString());

                yield return (T)_dr[count++];
            }
            //return (ienumerable<t>)datareader;
            Console.WriteLine("bo dia");
            //throw new NotImplemente
         * dException();
        }
        public void GetAll2()
        {
            int count = 0;
            Console.WriteLine("chguei ao getall");
            while (_dr.Read())
            {
                T t = new T();

                Type tp = t.GetType();
                Console.WriteLine("Fields");
                FieldInfo[] fi = tp.GetFields();
                foreach (FieldInfo fii in fi)
                {
                    Console.WriteLine(fii.ToString());
                }
                Console.WriteLine("Properties");

                PropertyInfo[] pi = tp.GetProperties();
                foreach (PropertyInfo pii in pi)
                {
                    Console.WriteLine(pii.MemberType);
                    Console.WriteLine(pii.GetType());
                    Console.WriteLine(pii.ToString());
                }
                // yield return (T)_dr[count++];
            }
            //return (ienumerable<t>)datareader;
            Console.WriteLine("bo dia");
            //throw new NotImplementedException();
        }
        */
        #endregion toCheck


    }
}











