using SixLabors.ImageSharp;

namespace Framework.Picture;

public static class ImageHelper
{
    static ImageHelper()
    {
    }


    public static async Task<Stream> RemoveExifAsync(Stream imageStream)
    {
        imageStream.Seek
            (0, SeekOrigin.Begin);

        var image =
            await Image.LoadAsync(imageStream);

        image.Metadata.ExifProfile = null;
        image.Metadata.IptcProfile = null;
        image.Metadata.XmpProfile = null;
        image.Metadata.IccProfile = null;
        image.Metadata.CicpProfile = null;

        var newImageStream =
            new MemoryStream();

        await image.SaveAsync
            (newImageStream, image.Metadata.DecodedImageFormat!);

        return imageStream;
    }

    public static async Task<bool> CheckImageSizeAsync(Stream imageStream, int width, int height)
    {
        imageStream.Seek
            (0, SeekOrigin.Begin);

        var image =
            await Image.LoadAsync(imageStream);

        int imageWidth =
            image.Width;

        int imageHeight =
            image.Height;

        if (imageWidth != width || imageHeight != height)
        {
            return false;
        }

        return true;
    }
}
