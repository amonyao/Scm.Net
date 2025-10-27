using Com.Scm.Utils;

namespace Com.Scm.Quartz.Extensions
{
    public static class HttpClientFactoryExtension
    {
        public static async Task<string> HttpSendAsync(this IHttpClientFactory httpClientFactory, HttpMethod method, string url, Dictionary<string, string> headers = null)
        {
            var client = httpClientFactory.CreateClient();
            var content = new StringContent("");
            // content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            var request = new HttpRequestMessage(method, url)
            {
                Content = content
            };

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            try
            {
                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                LogUtils.Info(ex.Message);
                return ex.Message;
            }
        }
    }
}
