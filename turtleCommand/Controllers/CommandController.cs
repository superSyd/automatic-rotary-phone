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
    public class CommandController : Controller
    {
        // GET: Command
        public ActionResult Index()
        {

            return View();
        }
        

        //GET: Command/ReadFile/files
        [HttpPost]
        public JsonResult ReadFile(string files)
        {
            string response = "";

            if (files != null)
            {
                CommandModel myCommand = new CommandModel();
                response = myCommand.readFile(files);

            }
            
            return Json(new { commandString = response.Trim(), fileTabOn = false, standardInputTabOn = true }, JsonRequestBehavior.AllowGet);

        }

        //GET: Command/ReadInput/textboxCommand
        [HttpPost]
        public JsonResult ReadInput(string textboxCommand)
        {
            string response = "";
            if (textboxCommand != null)
            {
                CommandModel myCommand = new CommandModel();
                response = myCommand.readInput(textboxCommand.Trim());

            }
            
            return Json(new { commandString = response.Trim(), fileTabOn = false, standardInputTabOn = true }, JsonRequestBehavior.AllowGet);
            

        }
        
    }
}
