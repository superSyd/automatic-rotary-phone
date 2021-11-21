using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using turtleCommand.Models;

namespace turtleCommand.Controllers
{
    public class FileController : Controller
    {
      public JsonResult GetFiles()
        {
            FileModel myFile = new FileModel();
            List<FileInfo> files = myFile.getFiles();

            List<string> items = new List<string>();


            foreach (FileInfo item in files)
            {
                items.Add(item.Name);
            }

            var jsonItems = JsonConvert.SerializeObject(items);
            return Json(new { files = jsonItems }, JsonRequestBehavior.AllowGet);
        }

        // Post file upload
        [HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase uploadFile)
        {
            FileModel myFile = new FileModel();
            myFile.fileSize = 200;
            string response = myFile.uploadFile(uploadFile);

            List<FileInfo> files = myFile.getFiles();

            List<string> items = new List<string>();


            foreach (FileInfo item in files)
            {
                items.Add(item.Name);
            }

            var jsonItems = JsonConvert.SerializeObject(items);
            return Json(new { feedback = response, files = jsonItems }, JsonRequestBehavior.AllowGet);


        }

        // file delete
        public JsonResult RemoveFile(string fileName)
        {
            FileModel myFile = new FileModel();
            string response = myFile.removeFile(fileName);

            List<FileInfo> files = myFile.getFiles();

            List<string> items = new List<string>();


            foreach (FileInfo item in files)
            {
                items.Add(item.Name);
            }
            var jsonItems = JsonConvert.SerializeObject(items);

            return Json(new { feedback = response, files = jsonItems }, JsonRequestBehavior.AllowGet);

        }
        
    }
}
