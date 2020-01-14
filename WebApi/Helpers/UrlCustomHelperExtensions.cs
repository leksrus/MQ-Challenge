using Microsoft.AspNetCore.Http;

namespace WebApi.Helpers
{
    public class UrlCustomHelperExtensions
    {
        public static string AbsoluteUrl(IHttpContextAccessor httpcontextaccessor)
        {
            var request = httpcontextaccessor.HttpContext.Request;

            var absoluteUri = string.Concat(
                        request.Scheme,
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.ToUriComponent(),
                        request.Path.ToUriComponent(),
                        request.QueryString.ToUriComponent());

            return absoluteUri;
        }
    }
}
