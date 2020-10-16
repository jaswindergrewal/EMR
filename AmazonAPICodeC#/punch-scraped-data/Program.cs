using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace punch_scraped_data
{
    class Program
    {
        static void Main(string[] args)
        {
            //Upload a file 

            string fileToUpload = @"D:\TestFile.txt"; // test file
            string myBucketName = "testbucketjaswinder2";//"punch-scraped-data"; //your s3 bucket name goes here
            string s3DirectoryName = "pat12";
            string s3FileName = @"TestFile.txt";

            CAmazon myUploader = new CAmazon();
            bool b = myUploader.UploadFile(fileToUpload, myBucketName, s3DirectoryName, s3FileName);
            if (b == true)
            {
                Console.WriteLine("Uploaded Successfully");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Not Uploded");
                Console.ReadLine();
            }



            //Download a file

            //CAmazon myUploader = new CAmazon();
            //bool b = myUploader.DownloadFile("TestFile.txt");
            //if (b == true)
            //{
            //    Console.WriteLine("downloaded Successfully");
            //    Console.ReadLine();
            //}
            //else
            //{
            //    Console.WriteLine("Not Downloaded");
            //    Console.ReadLine();
            //}



            //Delete a File

            //CAmazon myUploader = new CAmazon();
            //bool b = myUploader.DeleteFile("TestFile.txt");
            //if (b == true)
            //{
            //    Console.WriteLine("deleted Successfully");
            //    Console.ReadLine();
            //}
            //else
            //{
            //    Console.WriteLine("Not Deleted");
            //    Console.ReadLine();
            //}


        }
    }
}
