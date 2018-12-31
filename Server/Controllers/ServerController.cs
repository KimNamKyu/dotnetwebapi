using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Server.Moules;

namespace Server.Controllers
{
    [ApiController]
    public class ServerController : ControllerBase
    {
        [Route("select")]
        [HttpPost]
        public ActionResult<ArrayList> select([FromForm] string spName, [FromForm] string param)
        {
            Console.WriteLine("spName : {0}, param : {1}",spName, param);
            Hashtable ht = new Hashtable();
            if(!String.IsNullOrEmpty(param)){
                string[] str = param.Split(":");
                ht.Add(str[0], str[1]);
               
                Console.WriteLine("11111");
            }
            
            Database db = new Database(); 
            MySqlDataReader sdr = db.Reader(spName, ht);
            ArrayList list = new ArrayList();
            while(sdr.Read())
            {
                string[] arr = new string[sdr.FieldCount];
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    Console.WriteLine(sdr.GetValue(i).ToString());
                    arr[i] = sdr.GetValue(i).ToString();
                }
                list.Add(arr);
            }
            db.ReaderClose(sdr);
            Console.WriteLine("asd : {0}",list.Count.ToString());
            return list;
        }

        [Route("sp_update")]
        [HttpPost]
        public ActionResult<string> update([FromForm] string spName,[FromForm] string tNo, [FromForm] string mNo,[FromForm] string oCount)
        {
           
            Console.WriteLine("spName : {0}, mNo : {1}, tNo : {2}, oCount : {3}",spName, mNo, tNo,oCount);
            Hashtable ht = new Hashtable();
            ht.Add("_mNo",mNo);
            ht.Add("_tNo",tNo);
            ht.Add("_oCount",oCount);
            Database db = new Database();

            System.Console.WriteLine(db.NonQuery("sp_update",ht));
            if(db.NonQuery(spName,ht))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        [Route("sp_delete")]
        [HttpPost]
        public ActionResult<string> delete([FromForm] string spName,[FromForm] string tNo)
        {
            Console.WriteLine("spName : {0}, tNo : {1}",spName, tNo);
            
            Hashtable ht = new Hashtable();
            ht.Add("_tNo",tNo);
           
            Database db = new Database();
            if(db.NonQuery(spName,ht))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        [Route("sp_insert")]
        [HttpPost]
        public ActionResult<string> sp_insert([FromForm] string spName, [FromForm] string mNo, [FromForm] string tNo,[FromForm] string oCount)
        {
            Console.WriteLine("spName : {0}, mNo : {1}, tNo : {2}, oCount : {3}",spName, mNo, tNo,oCount);
            Hashtable ht = new Hashtable();
           
            ht.Add("_mNo",mNo);
            ht.Add("_tNo",tNo);
            ht.Add("_oCount",oCount);
            Database db = new Database(); 
           if(db.NonQuery(spName,ht))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        [Route("sp_list_delete")]
        [HttpPost]
        public ActionResult<string> sp_list_delete([FromForm] string spName,[FromForm] string tNo,[FromForm] string mNo)
        {
            Console.WriteLine("spName : {0}, tNo : {1}, mName = {2}",spName, tNo, mNo);
            
            Hashtable ht = new Hashtable();
            ht.Add("_tNo",tNo);
            ht.Add("_mNo",mNo);
            Database db = new Database();
            if(db.NonQuery(spName,ht))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
    }  
}