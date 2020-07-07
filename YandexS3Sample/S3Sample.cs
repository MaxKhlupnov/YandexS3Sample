
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.AccessControl;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.Auth;

using Amazon.S3.Model;

namespace GettingStartedGuide
{
    class YandexS3Sample
    {

        static IAmazonS3 client;

        public static void Main(string[] args)
        {
                string accessKey = "put your access key here!";
                string secretKey = "put your secret key here!";

                AmazonS3Config config = new AmazonS3Config();
                config.ServiceURL = "https://storage.yandexcloud.net";

                AWSCredentials credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);

                using (client = new AmazonS3Client(credentials, config))
                {
                    Console.WriteLine("Listing buckets");
                    ListingBuckets();
                }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void ListingBuckets()
        {
            try
            {
                ListBucketsResponse response = client.ListBuckets();
                foreach (S3Bucket bucket in response.Buckets)
                {
                    Console.WriteLine("You own Bucket with name: {0}", bucket.BucketName);
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An Error, number {0}, occurred when listing buckets with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
                }
            }
        }
    }
}