using BuildingBlocks.Persistence;
using Framework.DataType;
using Framework.Storage;

namespace Server.Infrastructure.Services;

public class StorageService(
    IExecutionContextAccessor executionContextAccessor,
    IStorage storage)
{
    public async Task<ResultContract<string>> UploadImageAsync(IFormFile formFile, string prefix, string path)
    {
        var size =
            formFile.Length;

        // Check Image Length
        if (size == 0)
        {
            var message =
                "file is too large.";

            return (ErrorType.FileWriteError, message);
        }

        using var memoryStream = new MemoryStream();

        await formFile.CopyToAsync
            (memoryStream).ConfigureAwait(false);

        var fileType =
            formFile.ContentType;

        string objectKey =
            $"{prefix}_IMG_{executionContextAccessor.StoreId}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";

        var folder =
            $"{executionContextAccessor.StoreId}/{path}";

        await storage.UploadAsync
            ("unibazzar", objectKey, memoryStream, folder, fileType);

        var link = storage.GetPublicUrl
            ("unibazzar", objectKey, folder);

        return link;
    }
}
