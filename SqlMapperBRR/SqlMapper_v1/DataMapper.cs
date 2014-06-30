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

        private string prepstategetall = "SELECT * from ";
        private string prepstateinsert;
        private string prepstateupdate;
        private string prepstatedelete;
        //private List<T> _tmp;
 

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
            PreparedSelectAll();

          //  _tmp = new List<T>();
            
            int numberOfColumns = 0;
            foreach (var dr in _dr) {
                Console.WriteLine(_dr.GetFieldType(numberOfColumns).Name);
                object[] o = new object[_columns.Length];
                for (int i= 0; i<_columns.Length; i++){
                    //Console.WriteLine(_dr[i]);
                    o[i] = _dr[i];
                }
                T newT = (T) Activator.CreateInstance(typeof(T), o);
                //_tmp.Add(newT);
                yield return newT;
            }

            if (!_persistant) _connnection.Close();
            //return _tmp.ToList();
        }

        private void PreparedSelect() {
            _command.CommandText = "SELECT * from " + _table;
            _dr = _command.ExecuteReader();
        }

        //Usado no getALL
        private void PreparedSelectAll()
        {
            _command.CommandText = prepstategetall + _table;
            _dr = _command.ExecuteReader();
        }

        private void PreparedInsert()
        {
            _command.CommandText = prepstateinsert + _table;
            if (_command.ExecuteNonQuery()==1)
                Console.WriteLine("inserido");
            else
                Console.WriteLine("NAO inserido");
            
        }


        //UPDATE table_name
        //SET column1=value1,column2=value2,...
        //WHERE some_column=some_value;
        public void Update(T val)
        {
            throw new NotImplementedException();
        }

        //DELETE FROM table_name
        //WHERE some_column = some_value;
        public void Delete(T val)
        {
            throw new NotImplementedException();
        }

        //INSERT INTO table_name (column1,column2,column3,...)
        //VALUES (value1,value2,value3,...);
        public void Insert(T val)
        {


            //SqlTransaction
            //http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqltransaction.aspx
            throw new NotImplementedException();
        }


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
            //throw new NotImplementedException();
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
