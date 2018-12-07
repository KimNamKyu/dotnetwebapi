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
    [ApiController]
    public class TestController : ControllerBase
    {
        
        [Route("api/Insert")]
        [HttpPost]
        public ActionResult<ArrayList> Insert([FromForm] Test test)
        {
            // Query query = new Query();
            // return query.Getinsert(test);
            return Query.Getinsert(test);
        }

        [Route("api/Update")]
        [HttpPost]
        public ActionResult<ArrayList> Update([FromForm] Test test)
        {
            return Query.GetUpdate(test);
        }

        [Route("api/Delete")]
        [HttpPost]
        public ActionResult<ArrayList> Delete([FromForm] Test test)
        {
            return Query.GetDelete(test);
        }
        [Route("api/Select")]
        [HttpGet]
        public ActionResult<ArrayList> Select([FromForm] Test test)
        {
            return Query.GetSelect();
        }
    }
}
