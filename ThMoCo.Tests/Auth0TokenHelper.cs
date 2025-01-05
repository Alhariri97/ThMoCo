using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThMoCo.Tests;

public class Auth0TokenHelper
{

    private const string Auth0Domain = "https://dev-wg8ow1pequtk5eia.uk.auth0.com";
    private const string ClientId = "5gbJNYfyURNS9ZiyxeV48PgTgieI6M50";
    private const string ClientSecret = "UwztAnJodlDnEtio-6fFYUlxIb3OeoSWEh2RpRo8Ree_wmkhvCyRvJhp9Orbd-P3";
    private const string Audience = "https://thamcodevah-app.azurewebsites.net/";

    public static async Task<string> GetAuthTokenAsync()
    {
        using var client = new HttpClient();

        var requestBody = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" }, // Use client_credentials grant type
            { "client_id", ClientId },
            { "client_secret", ClientSecret },
            { "audience", Audience }
        };

        var requestContent = new FormUrlEncodedContent(requestBody);

        var response = await client.PostAsync($"{Auth0Domain}/oauth/token", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to retrieve Auth0 token: {errorMessage}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<Auth0TokenResponse>(responseContent);

        return tokenResponse.AccessToken;
    }

    private class Auth0TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }

}
