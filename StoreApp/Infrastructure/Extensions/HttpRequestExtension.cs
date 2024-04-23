namespace StoreApp.Infrastructure.Extensions
{
    public static class HttpRequestExtension
    {
        public static string PathAndQuery(this HttpRequest request) //It extends the HttpRequest class with a method named PathAndQuery
        {
            return request.QueryString.HasValue //we check if there's value in queryString.
              ?  $"{request.Path}{request.QueryString}" //if there is queryString, return this
              : request.Path.ToString(); //if there isnt any queryString, return the path as string.
        }
    }
}