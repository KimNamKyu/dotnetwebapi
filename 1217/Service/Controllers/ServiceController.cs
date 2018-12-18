using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
        public ActionResult<string> update([FromForm] string name, [FromForm]string age, [FromForm] string no)
        {
            System.Console.WriteLine("update");
            string param = string.Format("{0} : {1}", name, age);
            System.Console.WriteLine("Insert");
            Hashtable ht = new Hashtable();
            ht.Add("@no",no);
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

         [Route("delete")]
        [HttpPost]
        public ActionResult<string> delete(string value)
        {
            System.Console.WriteLine("delete");
            return "";
        }

        [Route("imageUpload")]
        [HttpPost]
         public ActionResult<string> imageupload([FromForm] string fileName, [FromForm] string fileData)
        {
            //ystem.Console.WriteLine("fileName : {0} ", fileName);
            //System.Console.WriteLine("fileData : {0}" ,fileData);
            
            //고정값 string path = "C:\\Users\\GDC2\\Desktop\\github\\smartfactory-\\1214\\1218\\Image";
            string path = System.IO.Directory.GetCurrentDirectory();    //현재위치 경로 자동 탐색
            path += "\\wwwroot";
            if(!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            
            byte[] data = Convert.FromBase64String(fileData);
            try
            {
                string ext = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
                Guid saveNambe = Guid.NewGuid();
                string fullName = saveNambe+ext;    // 저장되는 파일명 만들기
                string fullPath = string.Format("{0}\\{1}",path,fullName);
                
                FileInfo fi = new FileInfo(fullPath);   //전체 경로 + 저장 파일명 (주소)
                FileStream fs = fi.Create();    //파일 껍데기를 만듬
                fs.Write(data, 0, data.Length);
                System.Console.WriteLine("파일 저장 완료");
                fs.Close();

                string url = string.Format("http://localhost:5000/{0}",fullName);

                Hashtable ht = new Hashtable();
                ht.Add("@fName",fileName);
                ht.Add("@fUrl", url);
                ht.Add("@fDesc","");
                Database db = new Database();
                if(db.NonQuery("sp_file_insert",ht))
                {
                    return url;
                }
                else
                {
                    return "0";
                }
            }
            catch 
            {
                System.Console.WriteLine("파일 저장 중 오류");
            }
            
            return fileName;
        }

    }
}
