using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.API.Data
{
    public class StorageService
    {
        public async Task<string> UploadFileAsync(S3Object s3Obj, AwsCredentials awsCredentials, bool makePublic)
        {
            var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecretKey);
               
            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.EUCentral1
            };

            //var response = new S3ResponseDto();
            var aclOption = makePublic ? S3CannedACL.PublicRead : S3CannedACL.Private;
            try
            {
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = s3Obj.InputStream,
                    Key = s3Obj.Name,
                    BucketName = s3Obj.BucketName,
                    CannedACL = aclOption
                };

                // Create S3 client
                using var client = new AmazonS3Client(credentials, config);

                // Upload utility to S3
                var transferUtility = new TransferUtility(client);

                // Upload file to S3
                await transferUtility.UploadAsync(uploadRequest);

                //response.StatusCode = 200;
                //response.Message = $"File has been uploaded successfully!";
            }
            catch (AmazonS3Exception ex)
            {
                //response.StatusCode = (int)ex.StatusCode;
                //response.Message = ex.Message;
                return ex.ErrorCode;
            }
            catch (Exception ex)
            {
                //response.StatusCode = 500;
                //response.Message = ex.Message;
                return ex.Message;
            }

            return $"https://{s3Obj.BucketName}.s3.{config.RegionEndpoint.SystemName}.amazonaws.com/{s3Obj.Name}";
        }

        public async Task<Stream> DownloadFileAsync(AwsCredentials cred, string objName, string bucketName)
        {
            
            var s3Client = new AmazonS3Client(cred.AwsKey, cred.AwsSecretKey, Amazon.RegionEndpoint.EUCentral1);

            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = objName
            };

            GetObjectResponse response = await s3Client.GetObjectAsync(request);
            return response.ResponseStream;
        }

        public async Task DeleteFileAsync(AwsCredentials cred, string objName, string bucketName)
        {
            

            var s3Client = new AmazonS3Client(cred.AwsKey, cred.AwsSecretKey, Amazon.RegionEndpoint.EUCentral1);
            

            DeleteObjectRequest request = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = objName,
                
            };

            

            await s3Client.DeleteObjectAsync(request);
        }
    }
}
