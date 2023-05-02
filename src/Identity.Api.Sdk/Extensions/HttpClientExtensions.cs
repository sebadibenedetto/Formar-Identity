using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Newtonsoft.Json;

namespace Identity.Api.Sdk.Extensions
{
    internal static class HttpClientExtensions
    {
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string requestUri, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            }

            using (response)
            {
                return await response.Content.ReadAsAsync<T>();
            }
        }

        public static async Task PutAsync<T>(this HttpClient httpClient, string requestUri, T value, CancellationToken cancellationToken)
        {
            await SendAsync(httpClient, HttpMethod.Put, requestUri, value, cancellationToken);
        }

        public static async Task PatchAsync<T>(this HttpClient httpClient, string requestUri, T value, CancellationToken cancellationToken)
        {
            await SendAsync(httpClient, new HttpMethod("PATCH"), requestUri, value, cancellationToken);
        }

        public static async Task DeleteAsync<T>(this HttpClient httpClient, string requestUri, T value, CancellationToken cancellationToken)
        {
            await SendAsync(httpClient, HttpMethod.Delete, requestUri, value, cancellationToken);
        }

        public static async Task PostAsync<T>(this HttpClient httpClient, string requestUri, T value, CancellationToken cancellationToken)
        {
            await SendAsync(httpClient, HttpMethod.Post, requestUri, value, cancellationToken);
        }

        public static async Task<TResponse> PostAsync<TRequest, TResponse>(this HttpClient httpClient, string requestUri, TRequest value, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await SendAsync(httpClient, HttpMethod.Post, requestUri, value, cancellationToken);
            using (response)
            {
                return await response.Content.ReadAsAsync<TResponse>();
            }
        }

        private static async Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpMethod httpMethod, string requestUri, object value, CancellationToken cancellationToken)
        {
            using MemoryStream stream = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), 1024, leaveOpen: true))
            {
                using JsonTextWriter jsonWriter = new JsonTextWriter(writer);
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jsonWriter, value);
                jsonWriter.Flush();
            }

            stream.Seek(0L, SeekOrigin.Begin);
            HttpContent content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using HttpRequestMessage request = new HttpRequestMessage(httpMethod, requestUri);
            request.Content = content;
            return await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        }
    }

    internal static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent httpContent)
        {
            using Stream stream = await httpContent.ReadAsStreamAsync();
            using StreamReader sr = new StreamReader(stream);
            using JsonTextReader reader = new JsonTextReader(sr);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<T>(reader);
        }
    }
}