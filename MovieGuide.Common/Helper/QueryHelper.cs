using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace MovieGuide.Common.Helper
{
    public static class QueryHelper
    {
        public static string AddQueryString(this string uri, string name, string value)
        {
            if (String.IsNullOrEmpty(value))
                return uri;

            return QueryHelpers.AddQueryString(uri, name, value);
        }

        public static string AddQueryString(this string uri, string name, object value)
        {
            if (value == null)
                return uri;

            return QueryHelpers.AddQueryString(uri, name, value.ToString());
        }

        public static Dictionary<string, StringValues> ParseQuery(this string queryString)
        {
            return QueryHelpers.ParseQuery(queryString);
        }
    }
}
