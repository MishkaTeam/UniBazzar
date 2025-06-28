namespace Server.Infrastructure.Extentions;

public static class HttpContextExtensions
{
    public static string GetUrl(this HttpContext context)
    {
        var request = context.Request;
        var path = request.Path; // "/mypage"
        var queryString = request.QueryString; // "?id=123"

        return $"{path}{queryString}";

    }

    public static string PartialDecodeSlashes(this string url)
    {
        return url.Replace("%2F", "/").Replace("%2f", "/");
    }

}
