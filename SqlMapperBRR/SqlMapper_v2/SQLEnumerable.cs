using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlMapper_v2
{
    /* class SQLEnumerable <T> : ISqlEnumerable<T> 
   {
       string _cmd;

       public SQLEnumerable(String cmd){
           _cmd = cmd;
       }

       public ISqlEnumerable<T> where(string clause) {
          return  new SQLEnumerable<T>(_cmd = _cmd + clause);
       }



      if (!_persistant) _connnection.Open();
    if (_connnection.State != ConnectionState.Open)
        _connnection.Open(); //abre se não estava aberta
    PreparedGetAll(FormatStringGetAll(_table));
    _dr = _command.ExecuteReader();
            
    //int numberOfColumns = 0; //to remove
    foreach (var dr in _dr) {
        //Console.WriteLine(_dr.GetFieldType(numberOfColumns).Name); //to remove
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
        
   */
}
