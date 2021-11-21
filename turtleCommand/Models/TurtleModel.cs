using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace turtleCommand.Models
{
    public class TurtleModel
    {
        public string feedback;
        public bool IsPlaced = false;
        public int x { get; set; }
        public int y { get; set; }
        public string f { get; set; }
        public string place(int x, int y, string f)
        {
            if (x < 0 || x > 5)
            {
                feedback = "Impossible Ma'am!";
            }
            else if (y < 0 || y > 5)
            {
                feedback = "No Way Ma'am!";
            }
            else if (f.Trim().ToUpper() == "NORTH" 
                || f.Trim().ToUpper() == "SOUTH" 
                || f.Trim().ToUpper() == "EAST" 
                || f.Trim().ToUpper() == "WEST")
            {
                this.x = x;
                this.y = y;
                this.f = f.Trim().ToUpper();
                this.IsPlaced = true;

                feedback = "Yes Ma'am!";
            }
            else {

                feedback = "Cant do it Ma'am!";

            }

            return feedback;
            
        }

        public string move()
        {
            if (IsPlaced)
            {
                switch (this.f)
                {
                    case "NORTH":
                        if ((this.y + 1) > 5)
                        {
                            feedback = "Impossible Ma'am!";
                        }
                        else
                        {
                            this.y++;

                        }
                        break;

                    case "SOUTH":
                        if ((this.y - 1) < 0)
                        {
                            feedback = "No way Ma'am!";
                        }
                        else
                        {
                            this.y--;
                        }
                        break;
                    case "EAST":
                        if ((this.x + 1) > 5)
                        {
                            feedback = "Cant do it Ma'am!";
                        }
                        else
                        {
                            this.x++;
                        }

                        break;

                    case "WEST":
                        if ((this.x - 1) < 0)
                        {
                            feedback = "Impossible Ma'am!";
                        }
                        else
                        {
                            this.x--;

                        }
                        break;
                }

            } else
            {
                feedback = "Am not there yet Ma'am!";

            }

            return feedback;
        }

        public string left()
        {
            if (IsPlaced)
            {
                switch (this.f)
                {
                    case "NORTH":
                        this.f = "WEST";
                        break;

                    case "WEST":
                        this.f = "SOUTH";
                        break;

                    case "SOUTH":
                        this.f = "EAST";
                        break;

                    case "EAST":
                        this.f = "NORTH";
                        break;
                }

            } else
            {
                feedback = "Am not there yet Ma'am!";

            }


            return "Yes Ma'am!";
            

        }
        public string right()
        {
            if (IsPlaced)
            {
                switch (this.f)
                {
                    case "NORTH":
                        this.f = "EAST";
                        break;

                    case "EAST":
                        this.f = "SOUTH";
                        break;

                    case "SOUTH":
                        this.f = "WEST";
                        break;

                    case "WEST":
                        this.f = "NORTH";
                        break;
                }

            } else
            {
                feedback = "Am not there yet Ma'am!";

            }


            return "Yes Ma'am!";
        }

        public string report()
        {
            if (IsPlaced)
            {
                feedback = this.x + "," + this.y + "," + this.f + "!";
            } else
            {
                feedback = "Am not there yet Ma'am!";
            }
            return feedback;
        }
    }
}