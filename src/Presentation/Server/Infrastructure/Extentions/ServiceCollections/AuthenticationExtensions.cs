using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Server.Infrastructure.Extentions.ServiceCollections;

public static class AuthenticationExtensions
{
    private const string LOGIN_PAGE_PATH = "/account/login";
    private const string REGISTER_PAGE_PATH = "/account/register";

    private const string LOGOUT_PAGE_PATH = "/account/logout";
    private const string ACCESS_DENIED_PATH = "/account/access-denied";

    public static IServiceCollection AddAuthenticationCookie(this IServiceCollection services)
    {
        services.AddAuthentication("Cookies")
            .AddCookie("Cookies", options =>
            {
                options.LoginPath = LOGIN_PAGE_PATH;
                options.LogoutPath = LOGOUT_PAGE_PATH;
                options.AccessDeniedPath = ACCESS_DENIED_PATH;
            });

        services.AddAuthorization();
        return services;
    }

    public static IServiceCollection AddRazorPagesWithAuth(this IServiceCollection services)
    {
        services.AddRazorPages(opt =>
        {
            opt.Conventions.AuthorizeAreaFolder("Admin", "/");
            opt.Conventions.AuthorizeAreaFolder("Pos", "/");

            opt.Conventions.AllowAnonymousToPage(LOGIN_PAGE_PATH);
            opt.Conventions.AllowAnonymousToPage(ACCESS_DENIED_PATH);
            opt.Conventions.AllowAnonymousToPage(REGISTER_PAGE_PATH);

            

        });
        return services;
    }
}
