using System;
using MySql.Data.MySqlClient;
using System.Collections;
namespace dotnetwebapi.Modules
{
    public class Test
    {
        // public string id{set;get;}
        // public string name{set;get;}
        // public string passwd{set;get;}
        public string mNo{set; get;}
        public string nTitle{set; get;}
        public string nContents{set; get;}
    }

    public class Query
    {

        public static ArrayList GetSelect()
        {
            MYsql my = new MYsql();
            string sql = string.Format("select n.nNo, n.nTitle, n.nContents, m.mName, DATE_FORMAT(n.regDate, '%Y-%m-%d') as regDate, DATE_FORMAT(n.modDate, '%Y-%m-%d') as modDate from Notice as n inner join Member as m on (n.mNo = m.mNo and m.delYn = 'N') where n.delYn = 'N';");
            MySqlDataReader sdr = my.Reader(sql);

            ArrayList list = new ArrayList();   //string 이 아닌 배열 형식으로 
            while(sdr.Read())
            {
                Hashtable ht = new Hashtable();
                for(int i = 0; i< sdr.FieldCount; i++)
                {
                    ht.Add(sdr.GetName(i),sdr.GetValue(i));
                }
                list.Add(ht);
            }
            return list;
        }
        public static ArrayList Getinsert(Test test)
        {
            MYsql my = new MYsql();
            string sql = string.Format("insert into Notice (mNo,nTitle,nContents) values ({0},'{1}','{2}');",test.mNo,test.nTitle,test.nContents);
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
            string sql = string.Format("update Notice set nTitle = '{1}', nContents = '{2}' where mNo = {0};",test.mNo,test.nTitle,test.nContents);
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
            string sql = string.Format("update Notice set delYn = 'Y' where mNo = {0};",test.mNo);
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