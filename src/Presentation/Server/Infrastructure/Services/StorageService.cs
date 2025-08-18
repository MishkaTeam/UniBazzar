using BuildingBlocks.Domain.Context;
using Framework.DataType;
using Framework.Picture;
using Framework.Storage;

namespace Server.Infrastructure.Services;

public class StorageService(
    IExecutionContextAccessor executionContextAccessor,
    IStorage storage)
{
    public readonly string _bucket = "unibazzar";

    public async Task<ResultContract<string>> UploadImageAsync(IFormFile imageFile, string prefix, string path)
    {
        if (imageFile.ContentType != "image/jpeg"
            && imageFile.ContentType != "image/png")
        {
            var message =
                "image format available: jpg, png.";

            return (ErrorType.UnknownError, message);
        }

        long maxFileSize =
            5 * 1024 * 1024;

        var size =
            imageFile.Length;

        if (imageFile == null || size == 0)
        {
            var message =
                "image is empty.";

            return (ErrorType.UnknownError, message);
        }
        else if (size > maxFileSize)
        {
            var message =
                "image is too large.";

            return (ErrorType.UnknownError, message);
        }

        using var memoryStream =
            new MemoryStream();

        await imageFile.CopyToAsync(memoryStream);

        var newImage = await ImageHelper
            .RemoveExifAsync(memoryStream);

        var fileType =
            imageFile.ContentType;

        string objectKey =
            $"{prefix}_IMG_{executionContextAccessor.StoreId}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";

        var folder =
            $"{executionContextAccessor.StoreId}/{path}";

        await storage.UploadAsync
            (_bucket, objectKey, newImage, folder, fileType);

        var link = storage.GetPublicUrl
            (_bucket, objectKey, folder);

        return link;
    }

    public async Task<ResultContract<bool>> DeleteImageAsync(string link, string path)
    {
        if (string.IsNullOrWhiteSpace(link))
        {
            return false;
        }

        var objectKey =
            link.Split('/').Last();

        var folder =
            $"{executionContextAccessor.StoreId}/{path}";

        await storage.DeleteAsync
            (_bucket, objectKey, folder);

        return true;
    }
}
