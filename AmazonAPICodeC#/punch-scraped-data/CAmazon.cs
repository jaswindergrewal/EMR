using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace punch_scraped_data
{
    class CAmazon
    {
        IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.USEast1);
        public bool UploadFile(string localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            bool b = false;
            //IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.USEast1);
            TransferUtility utility = new TransferUtility(client);
            // making a TransferUtilityUploadRequest instance
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName; //no subdirectory just bucket name
            }
            else
            {   // subdirectory and bucket name
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.Key = fileNameInS3; //file name up in S3
            request.FilePath = localFilePath; //local file name
            utility.Upload(request); //commensing the transfer





            return true;
        }
        public bool DownloadFile(string file)
        {
            bool b = false;

             IAmazonS3 client2;
             using (client2 = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
             {
                 GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = "punch-scraped-data",
                    Key = file
                };

                 using (GetObjectResponse response = client2.GetObject(request))
                 {
                     string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), file);
                     if (!File.Exists(dest))
                     {
                         response.WriteResponseStreamToFile(dest);
                     }
                 }
             }



            return true;
        }
        public bool DeleteFile(string file)
        {
            bool b = false;
            string accessKeyID = System.Configuration.ConfigurationManager.AppSettings["AWSAccessKey"];
            string secretAccessKeyID = System.Configuration.ConfigurationManager.AppSettings["AWSSecretKey"];

            IAmazonS3 client3;
            client3 = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            DeleteObjectRequest deleteObjectRequest =
            new DeleteObjectRequest
            {
                BucketName = "punch-scraped-data",
                Key = file
            };

            using (client3 = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKeyID, secretAccessKeyID,Amazon.RegionEndpoint.USEast1))
            {
                 client3.DeleteObject(deleteObjectRequest);
            }

            return true;
        }

    }
}
