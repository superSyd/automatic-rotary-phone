using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace turtleCommand.Models
{
    public class FileModel
    {
        public decimal fileSize { get; set; }
        public string outputMessage { get; set; }
        public List<FileInfo> getFiles()
        {
            List<FileInfo> items = new List<FileInfo>();

            if (HttpContext.Current != null)
            {
                DirectoryInfo myDirectory = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Uploads"));
                items = myDirectory.GetFiles().ToList();

            } else
            {
                DirectoryInfo myDirectory = new DirectoryInfo(Path.Combine(@"Uploads/"));
                items = myDirectory.GetFiles().ToList();
            }
            

            return items;
        }
        public string uploadFile(HttpPostedFileBase file)
        {
            try
            {
                var extension = "txt";
                var fileExtension = Path.GetExtension(file.FileName).Substring(1);

                if(fileExtension != extension)
                {
                    outputMessage = "Please upload a text file";
                } else if (file.ContentLength > (fileSize * 1024))
                {
                    outputMessage = "Please upload a text file of less than " + fileSize + "KB";
                }
                else
                {
                    List<FileInfo> items = new List<FileInfo>();

                    if (HttpContext.Current != null)
                    {
                        DirectoryInfo myDirectory;
                        myDirectory = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Uploads"));

                        items = myDirectory.GetFiles().ToList();

                    } else
                    {
                        DirectoryInfo myDirectory;
                        myDirectory = new DirectoryInfo(Path.Combine(@"Uploads/"));

                        items = myDirectory.GetFiles().ToList();

                    }
                   
                    

                    if (file != null)
                    {
                        List<string> fileNames = new List<string>();
                        foreach (FileInfo item in items)
                        {
                            fileNames.Add(item.Name);
                        }
                        if (!fileNames.Contains(file.FileName))
                        {

                            string fileName = Path.GetFileName(file.FileName);
                            if(HttpContext.Current != null)
                            {
                                string destination = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads"), fileName);
                                file.SaveAs(destination);


                            } else
                            {
                                string destination = Path.Combine(@"Uploads/", fileName);
                                file.SaveAs(destination);

                            }

                            

                            outputMessage = fileName + " Uploaded Successfully";


                        }
                        else
                        {
                            outputMessage = "File Exists";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                outputMessage = ex.ToString();
            }

            return outputMessage;
        }
        public string removeFile(string fileName)
        {
            DirectoryInfo myDirectory;
            List<FileInfo> items;
            if(HttpContext.Current != null)
            {
                myDirectory = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Uploads"));
                items = myDirectory.GetFiles().ToList();

            } else
            {
                myDirectory = new DirectoryInfo(Path.Combine(@"Uploads/"));
                items = myDirectory.GetFiles().ToList();
            }

            


            if (fileName != null)
            {
                if (fileName.Trim().Length != 0)
                {
                    if (HttpContext.Current != null)
                    {
                        FileInfo myFile = new FileInfo(HttpContext.Current.Server.MapPath("~/Uploads/" + fileName));
                        if (myFile.Exists)
                        {
                            myFile.Delete();
                            outputMessage = (fileName + " successfully deleted");
                        }

                    }
                    else
                    {
                        FileInfo myFile = new FileInfo(Path.Combine(@"Uploads/", fileName));
                        if (myFile.Exists)
                        {
                            myFile.Delete();
                            outputMessage = (fileName + " successfully deleted");
                        }
                    }
                } else
                {
                    outputMessage = "Please select a file";

                }




            }
            else
            {
                outputMessage = "Please select a file";
            }

            return outputMessage;

        }
    }
}