using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace turtleCommand.Models
{
    public class CommandModel
    {
        public string outputMessage { get; set; }
        public string readFile(string files)
        {
            try
            {
                if(HttpContext.Current != null)
                {
                    string[] filecontent = System.IO.File.ReadAllLines(HttpContext.Current.Server.MapPath("~/Uploads/" + files));
                    outputMessage = string.Join("|", filecontent);

                } else
                {
                    string[] filecontent = System.IO.File.ReadAllLines(Path.Combine(@"Uploads/", files));
                    outputMessage = string.Join("|", filecontent);
                }
                
            }
            catch
            {
                outputMessage = "Where is my file?";
            }

            return outputMessage;

        }
        public string readInput(string textboxCommand)
        {
            if (textboxCommand.Length > 0)
            {
                outputMessage = string.Join("|", textboxCommand);

            }
            else
            {
                outputMessage = "I dont have the commands yet!";

            }
            return outputMessage;
        }
    }
}