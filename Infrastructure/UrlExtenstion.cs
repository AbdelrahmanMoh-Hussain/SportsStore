namespace SportsStore.Infrastructure
{
    public static class UrlExtenstion
    {
        public static string PathAndQuery(this HttpRequest request) 
        {
            return request.QueryString.HasValue 
                ? $"{request.Path}{request.Query}"
                : request.Path.ToString();
        }
    }
}
