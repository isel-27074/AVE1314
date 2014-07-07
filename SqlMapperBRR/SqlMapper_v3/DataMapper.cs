using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlMapper_v3
{
    public class DataMapper<T> : IDataMapper<T> where T : class, new()
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _dr;
        private string[] _columns;
        private string _table;
        private bool _persistant;
        private bool _commitable;
        private string prepStateLastInsertedRecord = "Select @@Identity";
        private int lastInsertedRecordID;
        private Dictionary<Type, IDataMapper> _listOfMappers;

        private string prepStateGetAll = "SELECT * from {0}";
        private string prepStateInsert = "INSERT INTO {0} ({1}) VALUES ({2})";
        private string prepStateUpdate = "UPDATE {0} SET {2} WHERE {1}";
        private string prepStateDelete = "DELETE FROM {0} WHERE {1} = {2}";

        public DataMapper(SqlConnection con, bool persistant, string table, string[] columns, Dictionary<Type, IDataMapper> listOfMappers, bool commitable)
        {
            _connection = con;
            _command = new SqlCommand(null, con);
            _table = table;
            _columns = columns;
            _persistant = persistant;
            _listOfMappers = listOfMappers;
            _commitable = commitable;
        }        

        #region GetAll
        public ISqlEnumerable<T> GetAll()
        {
            PreparedStatement(String.Format(prepStateGetAll, _table));
            return new SqlEnumerable<T>(_connection, _persistant, _columns, _listOfMappers, _commitable, _command);
        }
        #endregion

        #region Update
        public void Update(T val)
        {
            if (!_persistant) _connection.Open();
            if (_connection.State != ConnectionState.Open)
                _connection.Open(); //abre se não estava aberta
            SqlTransaction trans = _connection.BeginTransaction("Update Transaction");
            PreparedStatement(FormatStringUpdate(val));
            _command.Transaction = trans;
            _command.ExecuteNonQuery();
            if (_commitable)
                trans.Commit();
            else
                trans.Rollback();
            if (!_persistant) _connection.Close();
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
                    ForeignKeyAttribute fkattr = (ForeignKeyAttribute)properties[i].GetCustomAttribute(typeof(ForeignKeyAttribute));
                    if (fkattr == null)
                    {
                        if ((properties[i].GetValue(val)) == null)
                        {
                            valuesProperties = valuesProperties + properties[i].Name + " = " + "\' \'";
                        }
                        else
                        {
                            if ((properties[i].GetValue(val).GetType() == typeof(String))
                                || (properties[i].GetValue(val).GetType() == typeof(DateTime)))
                            {
                                valuesProperties = valuesProperties + properties[i].Name + " = " + "\'" + properties[i].GetValue(val) + "\'";
                            }
                            else
                            {
                                valuesProperties = valuesProperties + properties[i].Name + " = " + properties[i].GetValue(val);
                            }
                        }
                    }
                    else
                    {
                        Type typeFK = properties[i].GetValue(val).GetType();
                        PropertyInfo[] fkproperties = typeFK.GetProperties();
                        int fknumberOfProperties = fkproperties.Length;
                        FieldInfo[] fkfields = typeFK.GetFields();
                        int fknumberOfFields = fkfields.Length;
                        //Percorrer a lista de propriedades da FK para propriedade em T
                        for (int j = 0; j < fknumberOfProperties; j++)
                        {
                            KeyAttribute attrFK = (KeyAttribute)fkproperties[j].GetCustomAttribute(typeof(KeyAttribute));
                            string aux = fkproperties[j].Name;
                            if (attrFK != null)
                                if (fkproperties[j].PropertyType.Name.Equals("String"))
                                {
                                    valuesProperties = valuesProperties + properties[i].Name + " = " + "\'" +
                                                       typeFK.GetProperty(aux)
                                                       .GetValue(properties[i].GetValue(val))
                                                       .ToString() + "\'";
                                    break;
                                }
                                else
                                {
                                    valuesProperties = valuesProperties + properties[i].Name + " = " +
                                        typeFK.GetProperty(aux).GetValue(properties[i].GetValue(val)).ToString();
                                    break;
                                }
                        }
                        //Percorrer a lista de campos da FK para propriedade em T
                        for (int j = 0; j < fknumberOfFields; j++)
                        {
                            KeyAttribute attrFK = (KeyAttribute)fkfields[j].GetCustomAttribute(typeof(KeyAttribute));
                            string aux = fkfields[j].Name;
                            if (attrFK != null)
                                if (fkfields[j].FieldType.Name.Equals("String"))
                                {
                                    valuesProperties = valuesProperties + properties[i].Name + " = " + "\'" +
                                                    typeFK.GetField(aux)
                                                    .GetValue(properties[i].GetValue(val))
                                                    .ToString() + "\'";
                                    break;
                                }
                                else
                                {
                                    valuesProperties = valuesProperties + properties[i].Name + " = " +
                                        typeFK.GetField(aux).GetValue(properties[i].GetValue(val)).ToString();
                                    break;
                                }
                        }
                        if(!properties[i].PropertyType.IsClass)
                            if ((properties[i].GetValue(val).GetType() == typeof(String))
                            || (properties[i].GetValue(val).GetType() == typeof(DateTime)))
                            {
                                valuesProperties = valuesProperties + properties[i].Name + " = " +
                                    "\'" + properties[i].GetValue(val) + "\'";
                            }
                            else
                            {
                                valuesProperties = valuesProperties + properties[i].Name + " = " + properties[i].GetValue(val);
                            }
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
                    ForeignKeyAttribute fkattr = (ForeignKeyAttribute)fields[i].GetCustomAttribute(typeof(ForeignKeyAttribute));
                    if (fkattr == null)
                    {
                        if ((fields[i].GetValue(val)) == null)
                        {
                            valuesFields = valuesFields + fields[i].Name + " = " + "\' \'";
                        }
                        else
                        {
                            if ((fields[i].GetValue(val).GetType() == typeof(String))
                               || (fields[i].GetValue(val).GetType() == typeof(DateTime)))
                            {
                                valuesFields = valuesFields + fields[i].Name + " = " + "\'" + fields[i].GetValue(val) + "\'";
                            }
                            else
                            {
                                valuesFields = valuesFields + fields[i].Name + " = " + fields[i].GetValue(val);
                            }
                        }
                    }
                    else
                    {
                        Type typeFK = (fields[i].GetValue(val)).GetType();
                        PropertyInfo[] fkproperties = typeFK.GetProperties();
                        int fknumberOfProperties = fkproperties.Length;
                        FieldInfo[] fkfields = typeFK.GetFields();
                        int fknumberOfFields = fkfields.Length;
                        //Percorrer a lista de propriedades da FK para campo em T
                        for (int j = 0; j < fknumberOfProperties; j++)
                        {
                            KeyAttribute attrFK = (KeyAttribute)fkproperties[j].GetCustomAttribute(typeof(KeyAttribute));
                            string aux = fkproperties[j].Name;
                            if (attrFK != null)
                                if (fkproperties[j].PropertyType.Name.Equals("String"))
                                {
                                    valuesFields = valuesFields + fields[i].Name + " = " + "\'" +
                                                    typeFK.GetProperty(aux)
                                                    .GetValue(fields[i].GetValue(val))
                                                    .ToString() + "\'";
                                    break;
                                }
                                else
                                {
                                    valuesFields = valuesFields + fields[i].Name + " = " +
                                        typeFK.GetProperty(aux).GetValue(fields[i].GetValue(val)).ToString();
                                    break;
                                }
                        }
                        //Percorrer a lista de campos da FK para campo em T
                        for (int j = 0; j < fknumberOfFields; j++)
                        {
                            KeyAttribute attrFK = (KeyAttribute)fkfields[j].GetCustomAttribute(typeof(KeyAttribute));
                            string aux = fkfields[j].Name;
                            if (attrFK != null)
                                if (fkfields[j].FieldType.Name.Equals("String"))
                                {
                                    valuesFields = valuesFields + fields[i].Name + " = " + "\'" +
                                                    typeFK.GetField(aux)
                                                    .GetValue(fields[i].GetValue(val))
                                                    .ToString() + "\'";
                                    break;
                                }
                                else
                                {
                                    valuesFields = valuesFields + fields[i].Name + " = " +
                                        typeFK.GetField(aux).GetValue(fields[i].GetValue(val)).ToString();
                                    break;
                                }
                        }
                        if (!fields[i].FieldType.IsClass)
                            if ((fields[i].GetValue(val).GetType() == typeof(String))
                            || (fields[i].GetValue(val).GetType() == typeof(DateTime)))
                            {
                                valuesFields = valuesFields + fields[i].Name + " = " +
                                    "\'" + fields[i].GetValue(val) + "\'";
                            }
                            else
                            {
                                valuesFields = valuesFields + fields[i].Name + " = " + fields[i].GetValue(val);
                            }
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
            if (!_persistant) _connection.Open();
            if (_connection.State != ConnectionState.Open)
                _connection.Open(); //abre se não estava aberta
            SqlTransaction trans = _connection.BeginTransaction("Delete Transaction");
            PreparedStatement(FormatStringDelete(val));
            _command.Transaction = trans;
            _command.ExecuteNonQuery();
            if (_commitable)
                trans.Commit();
            else
                trans.Rollback();
            if (!_persistant) _connection.Close();
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
            object value = null;
            if (val.GetType().GetProperty(columnKey) != null)
                value = val.GetType().GetProperty(columnKey).GetValue(val);
            else
                value = val.GetType().GetField(columnKey).GetValue(val);
            newobj[2] = value.ToString();
            return newobj;
        }
        #endregion

        #region Insert
        public void Insert(T val)
        {
            if (!_persistant) _connection.Open();
            if (_connection.State != ConnectionState.Open)
                _connection.Open(); //abre se não estava aberta
            SqlTransaction trans = _connection.BeginTransaction("Insert Transaction");
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
            if (!_persistant) _connection.Close();
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
            //Percorrer a lista de propriedades de T
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
                    ForeignKeyAttribute fkattr = (ForeignKeyAttribute)properties[i].GetCustomAttribute(typeof(ForeignKeyAttribute));
                    if (fkattr == null)
                    {
                        if ((properties[i].GetValue(val).GetType() == typeof(String)) 
                            || (properties[i].GetValue(val).GetType() == typeof(DateTime)))
                        {
                            valuesProperties = valuesProperties + "\'" + properties[i].GetValue(val) + "\'";
                        }
                        else
                        {
                            valuesProperties += properties[i].GetValue(val);
                        }
                    }
                    else
                    {
                        Type typeFK = (properties[i].GetValue(val)).GetType();
                        PropertyInfo[] fkproperties = typeFK.GetProperties();
                        int fknumberOfProperties = fkproperties.Length;
                        FieldInfo[] fkfields = typeFK.GetFields();
                        int fknumberOfFields = fkfields.Length;
                        //Percorrer a lista de propriedades da FK para propriedade em T
                        for (int j = 0; j < fknumberOfProperties; j++)
                        {
                            KeyAttribute attrFK = (KeyAttribute)fkproperties[j].GetCustomAttribute(typeof(KeyAttribute));
                            string aux = fkproperties[j].Name;
                            if (attrFK != null)
                                if (fkproperties[j].PropertyType.Name.Equals("String"))
                                {
                                    valuesProperties = valuesProperties + "\'" +
                                                       typeFK.GetProperty(aux)
                                                       .GetValue(properties[i].GetValue(val))
                                                       .ToString() + "\'";
                                    break;
                                }
                                else
                                {
                                    valuesProperties +=
                                        typeFK.GetProperty(aux).GetValue(properties[i].GetValue(val)).ToString();
                                    break;
                                }
                        }
                        //Percorrer a lista de campos da FK para propriedade em T
                        for (int j = 0; j < fknumberOfFields; j++)
                        {
                            KeyAttribute attrFK = (KeyAttribute)fkfields[j].GetCustomAttribute(typeof(KeyAttribute));
                            string aux = fkfields[j].Name;
                            if (attrFK != null)
                                if (fkfields[j].FieldType.Name.Equals("String"))
                                {
                                    valuesProperties = valuesProperties + "\'" +
                                                    typeFK.GetField(aux)
                                                    .GetValue(properties[i].GetValue(val))
                                                    .ToString() + "\'";
                                    break;
                                }
                                else
                                {
                                    valuesProperties +=
                                        typeFK.GetField(aux).GetValue(properties[i].GetValue(val)).ToString();
                                    break;
                                }
                        }
                    }
                    if (i != numberOfProperties - 1)
                    {
                        columnProperties += ",";
                        valuesProperties += ",";
                    }
                }
            }
            //Percorrer a lista de campos de T
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
                    ForeignKeyAttribute fkattr = (ForeignKeyAttribute)fields[i].GetCustomAttribute(typeof(ForeignKeyAttribute));
                    if (fkattr == null)
                    {
                        if ((fields[i].GetValue(val).GetType() == typeof(String))
                            || (fields[i].GetValue(val).GetType() == typeof(DateTime)))
                        {
                            valuesFields = valuesFields + "\'" + fields[i].GetValue(val) + "\'";
                        }
                        else
                        {
                            valuesFields += fields[i].GetValue(val);
                        }
                    }
                    else
                    {
                        Type typeFK = (fields[i].GetValue(val)).GetType();
                        PropertyInfo[] fkproperties = typeFK.GetProperties();
                        int fknumberOfProperties = fkproperties.Length;
                        FieldInfo[] fkfields = typeFK.GetFields();
                        int fknumberOfFields = fkfields.Length;
                        //Percorrer a lista de propriedades da FK para campo em T
                        for (int j = 0; j < fknumberOfProperties; j++)
                        {
                            KeyAttribute attrFK = (KeyAttribute)fkproperties[j].GetCustomAttribute(typeof(KeyAttribute));
                            string aux = fkproperties[j].Name;
                            if (attrFK != null)
                                if (fkproperties[j].PropertyType.Name.Equals("String"))
                                {
                                    valuesFields = valuesFields + "\'" +
                                                    typeFK.GetProperty(aux)
                                                    .GetValue(fields[i].GetValue(val))
                                                    .ToString() + "\'";
                                    break;
                                }
                                else
                                {
                                    valuesFields +=
                                        typeFK.GetProperty(aux).GetValue(fields[i].GetValue(val)).ToString();
                                    break;
                                }
                        }
                        //Percorrer a lista de campos da FK para campo em T
                        for (int j = 0; j < fknumberOfFields; j++)
                        {
                            KeyAttribute attrFK = (KeyAttribute)fkfields[j].GetCustomAttribute(typeof(KeyAttribute));
                            string aux = fkfields[j].Name;
                            if (attrFK != null)
                                if (fkfields[j].FieldType.Name.Equals("String"))
                                {
                                    valuesFields = valuesFields + "\'" +
                                                    typeFK.GetField(aux)
                                                    .GetValue(fields[i].GetValue(val))
                                                    .ToString() + "\'";
                                    break;
                                }
                                else
                                {
                                    valuesFields += typeFK.GetField(aux).GetValue(fields[i].GetValue(val)).ToString();
                                    break;
                                }
                        }
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


        #region Parte 2
        ISqlEnumerable IDataMapper.GetAll()
        {
            return GetAll();
        }

        public void Update(object val)
        {

            Update((T)val);
        }

        public void Delete(object val)
        {
            Delete((T)val);
        }

        public void Insert(object val)
        {
            Insert((T) val);
        }
        #endregion
    }
}











