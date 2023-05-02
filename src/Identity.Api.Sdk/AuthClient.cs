using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

using Identity.Dto.Response;
using Identity.Dto.Request.Query;
using Identity.Api.Sdk.Extensions;
using Identity.Api.Sdk.Interfaces;

namespace Identity.Api.Sdk
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient httpClient;

        public AuthClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<JwtResponse> LoginAsync(UserCredentialsQuery credentials, CancellationToken cancellationToken = default)
        {
            var requestUri = $"Auth/login";
            return await this.httpClient.PostAsync<UserCredentialsQuery, JwtResponse>(requestUri, credentials, cancellationToken);
        }
    }
}
