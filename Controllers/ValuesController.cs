using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetwebapi.Modules;
using MySql.Data.MySqlClient;
using System.Collections;


namespace dotnetwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<ArrayList> Get()
        {
            MYsql my = new MYsql();
            string sql = "select * from test;";
            ArrayList list = new ArrayList();
            MySqlDataReader sdr = my.Reader(sql);
            while(sdr.Read()) //행 반복
            {
                Hashtable ht = new Hashtable(); //열 담기
                for( int i = 0; i<sdr.FieldCount; i++) 
                {
                    ht.Add(sdr.GetName(i),sdr.GetValue(i));
                }
                //어딘가에 담아야된다.
                list.Add(ht);
            }
             my.ReaderClose(sdr);
            my.ConnectionClose();
            return list;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
