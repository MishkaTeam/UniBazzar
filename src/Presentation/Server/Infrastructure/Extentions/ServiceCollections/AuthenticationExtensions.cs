using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Server.Infrastructure.Extentions.ServiceCollections;

public class AuthenticationConstant
{
    public const string AUTHENTICATION_SCHEME = "UniCookie";
    public const string LOGIN_PAGE_PATH = "/account/login";
    public const string REGISTER_PAGE_PATH = "/account/register";
    public const string LOGOUT_PAGE_PATH = "/account/logout";
    public const string ACCESS_DENIED_PATH = "/account/access-denied";

}
public static class AuthenticationExtensions
{

    public static IServiceCollection AddAuthenticationCookie(this IServiceCollection services)
    {
        services.AddAuthentication(AuthenticationConstant.AUTHENTICATION_SCHEME)
            .AddCookie(AuthenticationConstant.AUTHENTICATION_SCHEME, options =>
            {
                options.LoginPath = AuthenticationConstant.LOGIN_PAGE_PATH;
                options.LogoutPath = AuthenticationConstant.LOGOUT_PAGE_PATH;
                options.AccessDeniedPath = AuthenticationConstant.ACCESS_DENIED_PATH;
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

            opt.Conventions.AllowAnonymousToPage(AuthenticationConstant.LOGIN_PAGE_PATH);
            opt.Conventions.AllowAnonymousToPage(AuthenticationConstant.ACCESS_DENIED_PATH);
            opt.Conventions.AllowAnonymousToPage(AuthenticationConstant.REGISTER_PAGE_PATH);

            

        });
        return services;
    }
}
