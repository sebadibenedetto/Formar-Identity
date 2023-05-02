using System.Net;

using Moq.Language.Flow;
using Moq.Protected;
using Newtonsoft.Json;

namespace Identity.Api.Sdk.Test.Extensions
{
    public static class HttpMessageHandlerMockExtensions
    {
        public static IReturnsResult<HttpMessageHandler> SetupGetReturnValue(this Mock<HttpMessageHandler> mock, object expected)
        {
            return
                mock.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(expected))
                    });
        }

        public static IReturnsResult<HttpMessageHandler> SetupHttpStatusCode(this Mock<HttpMessageHandler> mock, HttpStatusCode httpStatusCode)
        {
            return
                mock.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage(httpStatusCode));
        }

        public static IReturnsResult<HttpMessageHandler> SetupPostReturnCodeWithValue(this Mock<HttpMessageHandler> mock, object expected)
        {
            return
                mock.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(expected))
                    });
        }

        public static void VerifyCalledWithGetMethod(this Mock<HttpMessageHandler> mock, string expectedUri)
        {
            mock.VerifySendAsyncWasCalledWithMethod(HttpMethod.Get, expectedUri);
        }

        public static void VerifyCalledWithPutMethod(this Mock<HttpMessageHandler> mock, string expectedUri)
        {
            mock.VerifySendAsyncWasCalledWithMethod(HttpMethod.Put, expectedUri);
        }

        public static void VerifyCalledWithPostMethod(this Mock<HttpMessageHandler> mock, string expectedUri)
        {
            mock.VerifySendAsyncWasCalledWithMethod(HttpMethod.Post, expectedUri);
        }

        public static void VerifyCalledWithPatchMethod(this Mock<HttpMessageHandler> mock, string expectedUri)
        {
            mock.VerifySendAsyncWasCalledWithMethod(HttpMethod.Patch, expectedUri);
        }

        public static void VerifyCalledWithDeleteMethod(this Mock<HttpMessageHandler> mock, string expectedUri)
        {
            mock.VerifySendAsyncWasCalledWithMethod(HttpMethod.Delete, expectedUri);
        }

        private static void VerifySendAsyncWasCalledWithMethod(this Mock<HttpMessageHandler> mock, HttpMethod expectedMethod, string expectedUri)
        {
            var expectedRequestUri = new Uri(expectedUri);

            mock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == expectedMethod
                    &&
                    req.RequestUri == expectedRequestUri),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
