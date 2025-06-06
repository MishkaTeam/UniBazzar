using Amazon.S3;
using Amazon.S3.Model;

namespace Framework.Storage;

internal class AmazonS3 : IStorage
{
    private readonly StorageConfig _storageConfig;

    public AmazonS3(StorageConfig storageConfig)
    {
        _storageConfig = storageConfig;
    }

    public string GetPublicUrl(string bucketName, string objectKey, string pathOfBucketFolder)
    {
       return $"{_storageConfig.Endpoint.TrimEnd('/')}/{bucketName}/{pathOfBucketFolder}/{objectKey}";
    }

    public async Task<string?> GetUrl(string bucketName, string objectKey, string pathOfBucketFolder = "")
    {
        var config = new AmazonS3Config
        {
            ServiceURL = _storageConfig.Endpoint,
            ForcePathStyle = true
        };
        var credentials = new Amazon.Runtime.BasicAWSCredentials(_storageConfig.AccessKey, _storageConfig.SecretKey);
        using var client = new AmazonS3Client(credentials, config);


        if (!string.IsNullOrWhiteSpace(pathOfBucketFolder))
        {
            objectKey = $"{pathOfBucketFolder}/{objectKey}";
        }

        GetPreSignedUrlRequest request = new()
        {
            BucketName = bucketName,
            Key = objectKey,
            Expires = null
        };

        var res = await client.GetPreSignedURLAsync(request);
        return res;
    }

    public async Task UploadAsync(string bucketName, string objectKey, Stream fileStream,
        string pathOfBucketFolder, string fileType)
    {
        try
        {
            var config = new AmazonS3Config
            {
                ServiceURL = _storageConfig.Endpoint,
                ForcePathStyle = true
            };
            var credentials = new Amazon.Runtime.BasicAWSCredentials(_storageConfig.AccessKey, _storageConfig.SecretKey);
            using var client = new AmazonS3Client(credentials, config);

            if (!string.IsNullOrWhiteSpace(pathOfBucketFolder))
            {
                objectKey = $"{pathOfBucketFolder}/{objectKey}";
            }

            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = objectKey,
                InputStream = fileStream,
                ContentType = fileType
            };

            await client.PutObjectAsync(request);

        }
        catch (Exception ex)
        {

            throw new InvalidOperationException(ex.Message, ex);
        }
    }
}
