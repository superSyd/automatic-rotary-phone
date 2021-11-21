using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using turtleCommand.Controllers;

namespace turtleCommand.Tests.Controllers
{
    [TestClass]
    public class UnitTest
    {
        //Command
        [TestMethod]
        public void Index()
        {
            // Arrange
            CommandController commandController = new CommandController();

            // Act
            ViewResult commandResult = commandController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(commandResult);
        }

        [TestMethod]
        public void ReadFile()
        {
            // Arrange
            CommandController commandController = new CommandController();

            string fullPath = @"UploadsFrom\testFile.txt";

            string fileValid = "testFile.txt";


            string filePath = Path.GetFullPath(fullPath);
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            FileHandlerTest fileUpload = new FileHandlerTest(fileStream, "text/*", fileValid);

            FileController myFile = new FileController();
            myFile.UploadFile(fileUpload);

            //Valid
            JsonResult resultValid = commandController.ReadFile(fileValid) as JsonResult;
            JObject jObjectValid = JObject.Parse(JsonConvert.SerializeObject(resultValid.Data));
            var commandString = jObjectValid.SelectToken("commandString");

            Assert.AreEqual("Item1|Item2", commandString);

            //Invalid
            string fileInvalid = "";
            JsonResult resultInvalidDataType = commandController.ReadFile(fileInvalid) as JsonResult;
            JObject jObjectInvalid = JObject.Parse(JsonConvert.SerializeObject(resultInvalidDataType.Data));
            var commandStringInvalid = jObjectInvalid.SelectToken("commandString");

            Assert.AreEqual("Where is my file?", commandStringInvalid);


        }

        [TestMethod]
        public void ReadInput()
        {
            // Arrange
            CommandController commandController = new CommandController();

            // Valid
            string commandInputValid = "PLACE 0,0,NORTH" + Environment.NewLine + "MOVE" + Environment.NewLine + "REPORT";

            JsonResult resultValid = commandController.ReadInput(commandInputValid) as JsonResult;
            JObject jObjectValid = JObject.Parse(JsonConvert.SerializeObject(resultValid.Data));
            var commandStringValid = jObjectValid.SelectToken("commandString");
            Assert.AreEqual("PLACE 0,0,NORTH" + Environment.NewLine + "MOVE" + Environment.NewLine + "REPORT", commandStringValid);

            //Invalid
            string commandInputInvalid = " ";
            JsonResult resultInvalid = commandController.ReadInput(commandInputInvalid) as JsonResult;
            JObject jObjectInvalid = JObject.Parse(JsonConvert.SerializeObject(resultInvalid.Data));
            var commandStringInvalid = jObjectInvalid.SelectToken("commandString");
            Assert.AreEqual("I dont have the commands yet!", commandStringInvalid);
        }

        //File
        [TestMethod]
        public void GetFiles()
        {
            // Arrange
            FileController fileController = new FileController();

            //Valid
            JsonResult resultValid = fileController.GetFiles() as JsonResult;
            JObject jObjectValid = JObject.Parse(JsonConvert.SerializeObject(resultValid.Data));
            var files = jObjectValid.SelectToken("files");

            Assert.IsNotNull(files);
        }

        [TestMethod]
        public void UploadFile()
        {
            // Arrange
            FileController fileController = new FileController();

            string fullPath = @"UploadsFrom\testFileUpload.txt";

            string fileValid = "testFileUpload.txt";


            string filePath = Path.GetFullPath(fullPath);
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            FileHandlerTest fileUpload = new FileHandlerTest(fileStream, "text/*", fileValid);



            //Valid
            JsonResult resultValid = fileController.UploadFile(fileUpload) as JsonResult;
            JObject jObjectValid = JObject.Parse(JsonConvert.SerializeObject(resultValid.Data));
            var feedbackString = jObjectValid.SelectToken("feedback");

            Assert.AreEqual(fileValid + " Uploaded Successfully", feedbackString);

            //Invalid file type
            string fileInvalid = "testInvalidUploadFormat.docx";

            FileHandlerTest fileUploadInvalid = new FileHandlerTest(fileStream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileInvalid);

            JsonResult resultInvalid = fileController.UploadFile(fileUploadInvalid) as JsonResult;
            JObject jObjectInvalid = JObject.Parse(JsonConvert.SerializeObject(resultInvalid.Data));
            var feedbackInvalidString = jObjectInvalid.SelectToken("feedback");

            Assert.AreEqual("Please upload a text file", feedbackInvalidString);



        }

        [TestMethod]
        public void RemoveFile()
        {
            FileController fileController = new FileController();

            string fileValid = "testFileUpload.txt";


            //Valid
            JsonResult resultValid = fileController.RemoveFile(fileValid) as JsonResult;
            JObject jObjectValid = JObject.Parse(JsonConvert.SerializeObject(resultValid.Data));
            var feedbackString = jObjectValid.SelectToken("feedback");

            Assert.AreEqual(fileValid+ " successfully deleted", feedbackString);

            //Invalid
            JsonResult resultInvalid = fileController.RemoveFile("") as JsonResult;
            JObject jObjectInvalid = JObject.Parse(JsonConvert.SerializeObject(resultInvalid.Data));
            var feedbackStringInvalid = jObjectInvalid.SelectToken("feedback");

            Assert.AreEqual("Please select a file", feedbackStringInvalid);


        }

        //Turtle
        [TestMethod]
        public void ReadCommands()
        {
            TurtleController turtleController = new TurtleController();

            //valid commands
            //place
            
            
        JsonResult resultValid = turtleController.ReadCommands("[\"PLACE 2,2,NORTH\\nREPORT\"]", false) as JsonResult;
            JObject jObjectValid = JObject.Parse(JsonConvert.SerializeObject(resultValid.Data));
            var feedbackString = jObjectValid.SelectToken("response");

            Assert.AreEqual("2,2,NORTH!", feedbackString);

            //direction sense
            //north

            JsonResult resultValidNorth = turtleController.ReadCommands("[\"PLACE 2,2,EAST\\nLEFT\\nMOVE\\nREPORT\"]", false) as JsonResult;
            JObject jObjectValidNorth = JObject.Parse(JsonConvert.SerializeObject(resultValidNorth.Data));
            var feedbackStringNorth = jObjectValidNorth.SelectToken("response");

            Assert.AreEqual("2,3,NORTH!", feedbackStringNorth);

            //south
            JsonResult resultValidSouth = turtleController.ReadCommands("[\"PLACE 2,2,WEST\\nLEFT\\nMOVE\\nREPORT\"]", false) as JsonResult;
            JObject jObjectValidSouth = JObject.Parse(JsonConvert.SerializeObject(resultValidSouth.Data));
            var feedbackStringSouth = jObjectValidSouth.SelectToken("response");

            Assert.AreEqual("2,1,SOUTH!", feedbackStringSouth);

            //east
            JsonResult resultValidEast = turtleController.ReadCommands("[\"PLACE 2,2,NORTH\\nRIGHT\\nMOVE\\nREPORT\"]", false) as JsonResult;
            JObject jObjectValidEast = JObject.Parse(JsonConvert.SerializeObject(resultValidEast.Data));
            var feedbackStringEast = jObjectValidEast.SelectToken("response");

            Assert.AreEqual("3,2,EAST!", feedbackStringEast);

            //west
            JsonResult resultValidWest = turtleController.ReadCommands("[\"PLACE 2,2,SOUTH\\nRIGHT\\nMOVE\\nREPORT\"]", false) as JsonResult;
            JObject jObjectValidWest = JObject.Parse(JsonConvert.SerializeObject(resultValidWest.Data));
            var feedbackStringWest = jObjectValidWest.SelectToken("response");

            Assert.AreEqual("1,2,WEST!", feedbackStringWest);

            //invalid commands
            //without place
            var commandStringPlace = "MOVE|REPORT";
            var commandListPlace = commandStringPlace.Split('|');
            JsonResult resultInvalidPlace = turtleController.ReadCommands("[\"MOVE\\nREPORT\"]", false) as JsonResult;
            JObject jObjectInvalid = JObject.Parse(JsonConvert.SerializeObject(resultInvalidPlace.Data));
            var feedbackStringInvalid = jObjectInvalid.SelectToken("response");

            Assert.AreEqual("Am not there yet Ma'am!", feedbackStringInvalid);

            //fall off table
            JsonResult resultInvalidFall = turtleController.ReadCommands("[\"PLACE 2,2,NORTH\\nMOVE\\nMOVE\\nMOVE\\nMOVE\\nMOVE\\nMOVE\\nREPORT\"]", false) as JsonResult;
            JObject jObjectInvalidFall = JObject.Parse(JsonConvert.SerializeObject(resultInvalidFall.Data));
            var feedbackStringInvalidFall = jObjectInvalidFall.SelectToken("response");

            Assert.AreEqual("2,5,NORTH!", feedbackStringInvalidFall);

        }


    }
}
