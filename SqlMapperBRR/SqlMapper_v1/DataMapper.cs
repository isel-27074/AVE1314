﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;

namespace SqlMapper_v1
{
    public class DataMapper<T> : IDataMapper<T> where T : class, new()
    {
        private SqlDataReader _dr;
        private string[] _columnlist;
        private string _tabela;

        private string prepstategetall;
        private string prepstateinsert;
        private string prepstateupdate;
        private string prepstatedelete;
 

        public DataMapper()
        {
            //SqlCommand cmd = _builderConnection.CreateCommand();
            //cmd.CommandText = "SELECT * from " + _table;
            //_builderConnection.Open();
            //Console.WriteLine("Builder - Openning connection...");
            //_dr = cmd.ExecuteReader();
            //DataMapper<T> dm = new DataMapper<T>(_dr);
            ////return (IDataMapper<T>)dm;
            //return dm;
            //_dr = dr;
        }
        
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
        public void Update(T val)
        {
            throw new NotImplementedException();
        }

        public void Delete(T val)
        {
            throw new NotImplementedException();
        }

        //INSERT INTO table_name (column1,column2,column3,...)
        //VALUES (value1,value2,value3,...);
        public void Insert(T val)
        {
            throw new NotImplementedException();
        }

    }
}
