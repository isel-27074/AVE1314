using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace SqlMapper_v3
{
    public class Builder
    {
        //uma connection string por instância
        private readonly SqlConnection _builderConnection;
        private Dictionary<string, string[]> _tableColumnPair;
        private string _table; //nome da tabela obtido no Build 
        private string[] _columnlist; //nomes das colunas obtido no Build
        private bool _commitable;
        private bool _connectionState = false;
        public Dictionary<Type, IDataMapper> listOfMappers;

        //public Builder(ConnectionPolicy cp, QueryData qd)
        public Builder(ConnectionPolicy cp)
        {
            string connectionString = "";
            connectionString = @"Data Source=" + cp.dataSource + "; ";
            connectionString = connectionString + "Initial Catalog=" + cp.initialCatalog + "; ";
            connectionString = connectionString + "Integrated Security=" + cp.integratedSecurity + "; ";
            connectionString = connectionString + "Connection Timeout=" + cp.connectionTimeout + "; ";
            connectionString = connectionString + "Pooling=" + cp.pooling + ";";
            _builderConnection = new SqlConnection(connectionString);
            if (cp.pooling.ToLower().Equals("true"))
                _connectionState = true;
            //_tableColumnPair = qd.GetQueryData();
            _commitable = cp.commitable;
            listOfMappers = new Dictionary<Type, IDataMapper>();
        }

        public IDataMapper<T> Build<T>() where T : class, new()
        {
            Type t = typeof(T);

            Attribute[] attribs = Attribute.GetCustomAttributes(t);
            foreach (Attribute attr in attribs)
            {
                if (attr is TableAttribute)
                {
                    TableAttribute act = (TableAttribute)attr;
                    _table = act.Name;
                }
            }

            //Gets all properties in class T
            PropertyInfo[] properties = t.GetProperties();
            //Gets all fields in class T
            FieldInfo[] fields = t.GetFields();

            _columnlist = new String[properties.Length + fields.Length];
            int idx = 0;
            foreach (PropertyInfo property in properties)
            {
                _columnlist[idx] = property.Name;
                idx++;
            }
            foreach (FieldInfo field in fields)
            {
                _columnlist[idx] = field.Name;
                idx++;
            }

            DataMapper<T> dm = new DataMapper<T>(_builderConnection, true, _table, _columnlist, listOfMappers, _commitable);

            if (!_connectionState)
                _builderConnection.Close();
            if (_builderConnection.State != ConnectionState.Closed)
                _builderConnection.Dispose();

            return dm;
        }
    }
}