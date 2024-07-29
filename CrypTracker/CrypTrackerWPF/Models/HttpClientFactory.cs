using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CrypTrackerWPF.Models;

public static class HttpClientFactory
{
    public static HttpClient CreateClient()
    {
        return new HttpClient()
        {

            DefaultRequestHeaders =
            {
                { "Accept", "text/json" },
                { "Accept-Encoding", "deflate" }
            }
        };
    }
}
