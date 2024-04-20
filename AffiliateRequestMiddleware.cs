using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace iTech
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AffiliateRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public AffiliateRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Query["affiliateId"].ToString() !="")
            {
                httpContext.Session.SetString("AffiliateId", httpContext.Request.Query["affiliateId"].ToString());
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AffiliateRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseAffiliateRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AffiliateRequestMiddleware>();
        }
    }
}
