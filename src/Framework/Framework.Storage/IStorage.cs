namespace Framework.Storage;

public interface IStorage
{
    string GetPublicUrl(string bucketName, string objectKey, string pathOfBucketFolder);
    Task<string?> GetUrl(string bucketName, string objectKey, string pathOfBucketFolder);
    Task UploadAsync(string bucketName, string objectKey, Stream fileStream, string pathOfBucketFolder,string fileType);
}
