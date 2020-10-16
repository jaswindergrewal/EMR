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
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;




namespace Emrdev.AmazonService
{
    public class CAmazon
    {
        IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.USWest2);
        string base64Key = "mTlkCEcbyVahjr1+e6zGnjixCzNnKIOJCQyAJTBL7qc=";
        string accessKeyID = System.Configuration.ConfigurationManager.AppSettings["AWSAccessKey"];
        string secretAccessKeyID = System.Configuration.ConfigurationManager.AppSettings["AWSSecretKey"];

        public bool UploadFile(string localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            bool b = false;
            // client = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKeyID,secretAccessKeyID);
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
            request.ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256;
            if(bucketName== "lmc-sharefiles")
            {
                request.CannedACL= S3CannedACL.PublicRead;
            }
            //// Create encryption key.
            //Aes aesEncryption = Aes.Create();
            //aesEncryption.KeySize = 256;
            //aesEncryption.GenerateKey();

            //request.ServerSideEncryptionCustomerProvidedKey = base64Key;
            utility.Upload(request); //commensing the transfer





            return true;
        }

        //public static void DownloadObject(string keyName)
        //{
        //    string[] keySplit = keyName.Split('/');
        //    string fileName = keySplit[keySplit.Length - 1];
        //    string dest = Path.Combine(HttpRuntime.CodegenDir, fileName);

        //    using (client = Amazon.AWSClientFactory.CreateAmazonS3Client())
        //    {
        //        GetObjectRequest request = new GetObjectRequest().WithBucketName(bucketName).WithKey(keyName);

        //        using (GetObjectResponse response = client.GetObject(request))
        //        {
        //            response.WriteResponseStreamToFile(dest, false);
        //        }

        //        HttpContext.Current.Response.Clear();
        //        HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + fileName);
        //        HttpContext.Current.Response.ContentType = "application/octet-stream";
        //        HttpContext.Current.Response.TransmitFile(dest);
        //        HttpContext.Current.Response.Flush();
        //        HttpContext.Current.Response.End();

        //        // Clean up temporary file.
        //        System.IO.File.Delete(dest);
        //    }
        //}

        public void DownloadFile(string subDirectoryInBucket, string file, string bucketName )
        {
            bool b = false;
            string AwsBucketName;

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                AwsBucketName = bucketName; //no subdirectory just bucket name
            }
            else
            {   // subdirectory and bucket name
                AwsBucketName = bucketName + @"/" + subDirectoryInBucket;
            }

            IAmazonS3 client2;
            using (client2 = new AmazonS3Client(Amazon.RegionEndpoint.USWest2))
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = AwsBucketName,
                    Key = file
                    //// Provide encryption information of the object stored in S3.
                    // ServerSideEncryptionCustomerMethod = ServerSideEncryptionCustomerMethod.AES256,

                    // ServerSideEncryptionCustomerProvidedKey = base64Key

                };
                // request.ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256;
                //string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), file);
                string path = HttpContext.Current.Server.MapPath("uploads");
                Directory.CreateDirectory(path);
                string dest = path + "/"+file;
                using (GetObjectResponse response = client2.GetObject(request))
                {
                    // Clean up temporary file.
                    System.IO.File.Delete(dest);
                    if (!File.Exists(dest))
                    {

                        response.WriteResponseStreamToFile(dest);

                    }


                }


            }



            //return true;
        }
        public bool DeleteFile(string subDirectoryInBucket, string file, string bucketName)
        {
            bool b = false;
            string AwsBucketName;

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                AwsBucketName = bucketName; //no subdirectory just bucket name
            }
            else
            {   // subdirectory and bucket name
                AwsBucketName = bucketName + @"/" + subDirectoryInBucket;
            }


            IAmazonS3 client3;
            client3 = new AmazonS3Client(Amazon.RegionEndpoint.USWest2);

            DeleteObjectRequest deleteObjectRequest =
            new DeleteObjectRequest
            {
                BucketName = AwsBucketName,
                Key = file
            };

            using (client3 = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKeyID, secretAccessKeyID, Amazon.RegionEndpoint.USEast1))
            {
                client3.DeleteObject(deleteObjectRequest);
            }

            return true;
        }

    }
}
