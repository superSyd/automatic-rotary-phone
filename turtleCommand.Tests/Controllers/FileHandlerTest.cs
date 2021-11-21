using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

// No class available to create an instance of HttpPostedFileBase
// derived from https://stackoverflow.com/questions/7466687/how-to-create-an-instance-of-httppostedfilebaseor-its-inherited-type

namespace turtleCommand.Tests.Controllers
{

    class FileHandlerTest : HttpPostedFileBase
    {
        Stream stream;
        string contentType;
        string fileName;

        public FileHandlerTest(Stream stream, string contentType, string fileName)
        {
            this.stream = stream;
            this.contentType = contentType;
            this.fileName = fileName;
        }

        public override int ContentLength
        {
            get { return (int)stream.Length; }
        }

        public override string ContentType
        {
            get { return contentType; }
        }

        public override string FileName
        {
            get { return fileName; }
        }

        public override Stream InputStream
        {
            get { return stream; }
        }

        public override void SaveAs(string filename)
        {
            using (var file = File.Open(filename, FileMode.CreateNew))
                stream.CopyTo(file);
        }
    }
}
