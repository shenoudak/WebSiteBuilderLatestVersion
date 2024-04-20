using iTech.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace iTech
{

    public class RoutingRequestMiddleware
    {

        private readonly RequestDelegate _next;

        public RoutingRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, CRMDBContext dbcontext)
        {
            var co = context.Request.Host.Value;
            var cr = context.Request.Path;

            if (Regex.IsMatch(context.Request.Host.Value, @"^[^.]+\.localhost:44352"))
            {
                string subdomain = context.Request.Host.Value.Split('.')[0];
                var SiteObj = await dbcontext.Sites.Where(e => e.SiteTitle.Contains(subdomain)).FirstOrDefaultAsync();
                if (SiteObj != null)
                {
                    var siteSub = await dbcontext.SiteSubscriptions.Where(e => e.SiteId == SiteObj.SiteId).OrderByDescending(e => e.SiteSubscriptionId).FirstOrDefaultAsync();
                    if (siteSub.EndDate < DateTime.Now)
                    {
                        context.Request.Path = $"{context.Request.Host.Value}";
                    }
                    else
                    {
                        context.Request.Path = $"/{subdomain}{context.Request.Path}";
                    }

                }
               
            }
            await _next(context);



        }

    }

    public static class RoutingRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestRoutingRequest(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RoutingRequestMiddleware>();
        }
    }

}
