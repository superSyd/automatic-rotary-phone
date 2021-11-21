using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using turtleCommand.Models;

namespace turtleCommand.Controllers
{
    public class TurtleController : Controller
    {
        
        [HttpGet]
        public JsonResult ReadCommands(string commandString, bool isPlaced)
        {
            TurtleModel myTurtle = new TurtleModel();
            
            string turtleResponse = "";
            List<string> turtlePosition = new List<string>();

            if (commandString != null)
            {
                List<string> commandListString = JsonConvert.DeserializeObject<List<string>>(commandString);
                myTurtle.IsPlaced = isPlaced;
                var commandList = commandListString[0].Split('\n');
                foreach (string item in commandList)
                {
                    if (item.Length >= 11)
                    {
                        if (item.ToLower().Trim().Substring(0, 5) == "place")
                        {
                            var coordDirection = item.ToLower().Trim().Substring(6);
                            var coordList = coordDirection.Split(',').ToList();
                            var x = "";
                            var y = "";
                            var f = "";
                            if (coordList.Count() == 3)
                            {
                                x = coordList[0];
                                y = coordList[1];
                                f = coordList[2];

                                turtleResponse = myTurtle.place(int.Parse(x), int.Parse(y), f);
                                turtlePosition.Add(myTurtle.x + "," + myTurtle.y + "," + myTurtle.f);
                            }
                            else
                            {
                                turtleResponse = "No Way Ma'am!";
                                turtlePosition.Add(myTurtle.x + "," + myTurtle.y + "," + myTurtle.f);

                            }


                            
                        }
                        else
                        {
                            turtleResponse = "Cant do it Ma'am!";
                            turtlePosition.Add(myTurtle.x + "," + myTurtle.y + "," + myTurtle.f);

                        }

                    }
                    else if (item.ToLower().Trim() == "move")
                    {
                        turtleResponse = myTurtle.move();
                        turtlePosition.Add(myTurtle.x + "," + myTurtle.y + "," + myTurtle.f);


                    }
                    else if (item.ToLower().Trim() == "left")
                    {
                        turtleResponse = myTurtle.left();
                        turtlePosition.Add(myTurtle.x + "," + myTurtle.y + "," + myTurtle.f);


                    }
                    else if (item.ToLower().Trim() == "right")
                    {
                        turtleResponse = myTurtle.right();
                        turtlePosition.Add(myTurtle.x + "," + myTurtle.y + "," + myTurtle.f);


                    }
                    else if (item.ToLower().Trim() == "report")
                    {
                        turtleResponse = myTurtle.report();
                        turtlePosition.Add(myTurtle.x + "," + myTurtle.y + "," + myTurtle.f);


                    }
                    else
                    {
                        turtleResponse = "Impossible Ma'am!";
                        turtlePosition.Add(myTurtle.x + "," + myTurtle.y + "," + myTurtle.f);

                    }
                }
            }

            var jsonItems = JsonConvert.SerializeObject(turtlePosition);


            return Json(new { response = turtleResponse, position = jsonItems, placed = myTurtle.IsPlaced }, JsonRequestBehavior.AllowGet);

        }


    }
}
