using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Module;

namespace Service.Controllers
{

    [ApiController]
    public class ServiceController : ControllerBase
    {

        [Route("select")]
        [HttpGet]
        public ActionResult<ArrayList> select()
        {
            Database db = new Database();
            SqlDataReader sdr = db.Reader("sp_select");
            ArrayList list = new ArrayList();
            while(sdr.Read())
            {
                string[] arr = new string[6];
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    arr[i] = sdr.GetValue(i).ToString();
                }
                list.Add(arr);
            }
            db.ReaderClose(sdr);
            
            return list;
        }

        [Route("insert")]
        [HttpPost]
        public ActionResult<string> insert([FromForm] string name, [FromForm]string age)
        {
            string param = string.Format("{0} : {1}", name, age);
            System.Console.WriteLine("Insert");
            Hashtable ht = new Hashtable();
            ht.Add("@name",name);
            ht.Add("@age",age);
            Database db = new Database();
            if(db.NonQuery("sp_Insert",ht))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
         [Route("update")]
        [HttpPost]
        public ActionResult<string> update(string value)
        {
            System.Console.WriteLine("update");
            return "";
        }

         [Route("delete")]
        [HttpPost]
        public ActionResult<string> delete(string value)
        {
            System.Console.WriteLine("delete");
            return "";
        }
    }
}
