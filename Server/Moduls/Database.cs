using System;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;

namespace Server.Moules
{
     public class Database
    {
        private MySqlConnection conn;
        private bool status;
        
        public Database()
        {
            status = Connection();
        }
        private bool Connection()
        {
            try
            {
                conn = new MySqlConnection();
                string host = "gudi.kr";
                string port = "5002";
                string user = "gdc3";
                string pwd = "gdc3";
                string db = "gdc3_1";
                conn.ConnectionString = string.Format("server={0};port={4};user={1};password={2};database={3}", host,  user, pwd, db,port);
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



        public MySqlDataReader Reader(string sql, Hashtable ht)
        {
            if(status)  //연결된 상태일 때만
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandText = sql;
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;

                    foreach (DictionaryEntry data in ht)
                    {
                        
                        comm.Parameters.AddWithValue(data.Key.ToString(), data.Value);
                    }

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

         public bool ReaderClose(MySqlDataReader sdr)
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

        public bool NonQuery(string sql, Hashtable ht)   
        {
            if (status) 
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandText = sql;
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    
                    foreach (DictionaryEntry data in ht)
                    {
                        
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

        public MySqlDataReader Readers(string sql,string ht)
        {
            if(status)  //연결된 상태일 때만
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand();
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
    }
}