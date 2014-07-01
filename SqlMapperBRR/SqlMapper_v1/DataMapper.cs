using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;

namespace SqlMapper_v1
{
    public class DataMapper<T> : IDataMapper<T> where T : class, new()
    {
        private SqlConnection _connnection;
        private SqlCommand _command;
        private SqlDataReader _dr;
        private string[] _columns;
        private string _table;
        private bool _persistant;
        
        private string prepStateLastInsertedRecord = "Select @@Identity";
        private int lastInsertedRecordID;

        private string prepStateGetAll = "SELECT * from {0}";
        private string prepStateInsert = "INSERT INTO {0} VALUES ('{1}', '{2}', {3}, {4}, {5})";
        private string prepStateUpdate = "UPDATE {0} SET {3}='{4}', {5}='{6}', {7}={8}, {9}={10}, {11}={12} WHERE {1}={2}";
        private string prepStateDelete; //= "DELETE FROM {0} WHERE {1} = {2}";

        public DataMapper(SqlConnection con, bool persistant, string table, string[] columns)
        {
            _connnection = con;
            _command = new SqlCommand(null, con);
            _table = table;
            _columns = columns;
            _persistant = persistant;

            if (_persistant && (_connnection.State != ConnectionState.Open))
            {
                Console.WriteLine("Builder - Starting connection...");
                _connnection.Open();
            }

            //SqlCommand cmd = _builderConnection.CreateCommand();

        }

        //SELECT column_name,column_name
        //FROM table_name;
        // or
        //SELECT * FROM table_name;
        public IEnumerable<T> GetAll()
        {
            if (!_persistant) _connnection.Open();
            if (_connnection.State != ConnectionState.Open)
                _connnection.Open(); //abre se não estava aberta
            PreparedGetAll(FormatStringGetAll(_table));
            _dr = _command.ExecuteReader();
            
            int numberOfColumns = 0; //to remove
            foreach (var dr in _dr) {
                Console.WriteLine(_dr.GetFieldType(numberOfColumns).Name); //to remove
                object[] o = new object[_columns.Length];
                for (int i= 0; i<_columns.Length; i++){
                    //Console.WriteLine(_dr[i]); //to remove
                    o[i] = _dr[i];
                }
                T newT = (T) Activator.CreateInstance(typeof(T), o);
                yield return newT;
            }

            if (!_persistant) _connnection.Close();
            _dr.Close();
        }

        //Preparação de Statement para o GetALL
        private void PreparedGetAll(string instruction)
        {
            //_command.CommandText = "SELECT * from " + _table;
            _command.CommandText = instruction;
        }

        //dado um T, formatamos a string de Select
        public string FormatStringGetAll(string tableName)
        {
            return String.Format(prepStateGetAll, tableName);
        }

        //UPDATE table_name
        //SET column1=value1,column2=value2,...
        //WHERE some_column=some_value;
        public void Update(T val)
        {
            if (!_persistant) _connnection.Open();
            if (_connnection.State != ConnectionState.Open)
                _connnection.Open(); //abre se não estava aberta

            PreparedUpdate(FormatStringUpdate(val));
            _dr = _command.ExecuteReader();
            _dr.Close();
        }

        private void PreparedUpdate(string instruction)
        {
            _command.CommandText = instruction;
        }

        //Dado um T, formatamos a string de Insert
        //public string FormatStringDelete(string column, string value)
        public string FormatStringUpdate(T val)
        {
            object[] args = FormatParameterUpdate(val);
            return String.Format(prepStateUpdate, args);
        }

        //Dado um T, devolvemos um array com o nome da tabela e os dados do T
        public object[] FormatParameterUpdate(T val)
        {
            int count = _columns.Length * 2 + 1;//colunas valores + nome tabela
            object[] newobj = new object[count];
            newobj[0] = _table;
            Type t = val.GetType();
            PropertyInfo[] props = t.GetProperties();

            int i = 0;
            for (int j = 1; j < count;)
            {
                
                newobj[j] = props[i].Name ;
                Console.WriteLine(newobj[j]);
                j++;
                newobj[j] = props[i].GetValue(val);
                Console.WriteLine(newobj[j]);
                j++;
                i++;
            }

            return newobj;
        }

        //_____________________________




        //DELETE FROM table_name
        //WHERE some_column = some_value;
        public void Delete(T val)
        {
            if (!_persistant) _connnection.Open();
            if (_connnection.State != ConnectionState.Open)
                _connnection.Open(); //abre se não estava aberta
            PreparedDelete(FormatStringDelete(val));
            _command.ExecuteNonQuery();
        }

        //Preparação de Statement para o Insert
        private void PreparedDelete(string instruction)
        {
            _command.CommandText = instruction;
        }

        //Dado um T, formatamos a string de Insert
        //public string FormatStringDelete(string column, string value)
        public string FormatStringDelete(T val)
        {
            object[] args = FormatParameterDelete(val);
            return String.Format(prepStateInsert, args);
        }

        //Dado um T, devolvemos um array com o nome da tabela e os dados do T
        public object[] FormatParameterDelete(T val)
        {
            object[] newobj = new object[_columns.Length];
            newobj[0] = _table;
            Type t = val.GetType();
            PropertyInfo[] props = t.GetProperties();

            for (int j = 1; j < props.Length; j++)
            {
                Console.WriteLine(props[j].GetValue(val));
                newobj[j] = props[j].GetValue(val);
            }

            return newobj;
        }

        //INSERT INTO table_name (column1,column2,column3,...)
        //VALUES (value1,value2,value3,...);
        public void Insert(T val)
        {
            if (!_persistant) _connnection.Open();
            if (_connnection.State != ConnectionState.Open)
                _connnection.Open(); //abre se não estava aberta
            PreparedInsert(FormatStringInsert(val));
            _command.ExecuteNonQuery();

            //Obter o ID do ultimo item inserido
            _command.CommandText = prepStateLastInsertedRecord;
            var lastInsertedID = (decimal)_command.ExecuteScalar();
            lastInsertedRecordID = Decimal.ToInt32(lastInsertedID);

            //mFmt.format(pattern, values)
            //MessageFormat mFmt = new MessageFormat(pattern);
            ////SqlTransaction
            ////http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqltransaction.aspx
            //throw new NotImplementedException();
        }

        //Preparação de Statement para o Insert
        private void PreparedInsert(string instruction)
        {
            _command.CommandText = instruction;
            //if (_command.ExecuteNonQuery() == 1) Console.WriteLine("inserido");
            //else Console.WriteLine("NAO inserido");
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
            object[] newobj = new object[_columns.Length];
            newobj[0] = _table;
            Type t = val.GetType();
            PropertyInfo[] props = t.GetProperties();

            for (int j = 1; j < props.Length ; j++)
            {
                    Console.WriteLine(props[j].GetValue(val));
                    newobj[j] = props[j].GetValue(val);
            }

            return newobj;
        }

        public int GetLastInsertedRecord() { return lastInsertedRecordID; }

        #region toCheck
        /*
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
