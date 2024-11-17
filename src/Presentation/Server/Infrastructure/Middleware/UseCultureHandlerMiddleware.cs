namespace Server.Infrastructure.Middleware
{
    public class UseCultureHandlerMiddleware : object
    {
        #region Static Member(s)
        public readonly static string CookieName = "Culture.Cookie";

        public static void SetCulture(string? cultureName)
        {
            if (string.IsNullOrWhiteSpace(cultureName) == false)
            {
                var cultureInfo =
                    new System.Globalization.CultureInfo(name: cultureName);

                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
        }

        #endregion /Static Member(s)

        public UseCultureHandlerMiddleware
            (RequestDelegate next) : base()
        {
            Next = next;
        }

        private RequestDelegate Next { get; }

        public async Task InvokeAsync
            (HttpContext httpContext)
        {

            //var userLangs = httpContext.Request.Headers["Accept-Language"].ToString();
            //var firstLang = userLangs.Split(',').FirstOrDefault();

            //var defaultLang = string.IsNullOrEmpty(firstLang) ? "fa-IR" : firstLang;
            SetCulture(cultureName: "fa-IR");

            await Next(context: httpContext);
        }

    }
    public static class UseCultureHandlerExtensions
    {
        public static IApplicationBuilder UseCultureHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UseCultureHandlerMiddleware>();
        }
    }




}
