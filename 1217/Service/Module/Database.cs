using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Service.Module
{
    public class Database
    {
        private SqlConnection conn;
        private bool status;
        
        public Database()
        {
            status = Connection();
        }

        private bool Connection()
        {
            try
            {
                conn = new SqlConnection();
                string server = "(localdb)\\ProjectsV13";
                string uid = "root";
                string password = "1234";
                string database = "Tes2";
                conn.ConnectionString = string.Format("server = {0}; uid = {1}; password = {2}; database = {3}", server, uid, password, database);
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Close()
        {
            if(status == true) conn.Close();
        }

           public SqlDataReader Reader(string sql)
        {
            if(status)  //연결된 상태일 때만
            {
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sql;
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    return comm.ExecuteReader();
                }
                catch
                {
                    return null;   
                }
            }
            else
            {
                return null;
            }
        }
   

       public bool ReaderClose(SqlDataReader sdr)
        {
            try
            {
                sdr.Close();
                return true;
            }
            catch 
            {
                return false;
            }
        }

         public bool NonQuery(string sql, Hashtable ht)    //NonQuery는 파라미터가 필요
        {
            if (status)  //연결된 상태일 때만
            {
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sql;
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    
                    //반복적이므로 for문사용
                    foreach (DictionaryEntry data in ht)
                    {
                        // Key : Value 형식 Hashtable or 멤버 객체
                        // 1) Hashtable 방식
                        // 키 와 값 형식으로 가져옴 ( DictionaryEntry )
                        comm.Parameters.AddWithValue(data.Key.ToString(), data.Value);
                    }
                  
                    comm.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}