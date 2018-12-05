using System;
using MySql.Data.MySqlClient;
using System.Collections;
namespace dotnetwebapi.Modules
{
    public class Test
    {
        public string id{set;get;}
        public string name{set;get;}
        public string passwd{set;get;}
    }

    public class Query
    {

        public static ArrayList GetSelect()
        {
            MYsql my = new MYsql();
            string sql = string.Format("select * from test;");
            MySqlDataReader sdr = my.Reader(sql);
            //string result = "";
            ArrayList list = new ArrayList();   //string 이 아닌 배열 형식으로 
            while(sdr.Read())
            {
                Hashtable ht = new Hashtable();
                for(int i = 0; i< sdr.FieldCount; i++)
                {
                    //result += string.Format("{0} : {1} ",sdr.GetName(i), sdr.GetValue(i)); //열 , 값
                    ht.Add(sdr.GetName(i),sdr.GetValue(i));
                }
                //행바뀜
                //result += "\n";
                list.Add(ht);
            }
            return list;
        }
        public static ArrayList Getinsert(Test test)
        {
            MYsql my = new MYsql();
            string sql = string.Format("insert into test values ('{0}','{1}','{2}');",test.id,test.name,test.passwd);
            if(my.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }

        public static ArrayList GetUpdate(Test test)
        {
            MYsql my = new MYsql();
            string sql = string.Format("update test set name = '{1}', passwd = '{2}' where id = '{0}';",test.id,test.name,test.passwd);
            if(my.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
    }
        public static ArrayList GetDelete(Test test)
        {
            MYsql my = new MYsql();
            string sql = string.Format("delete from test where id = '{0}';",test.id);
            if(my.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }
    
    }
}