using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThMoCo.Tests;

public class Auth0TokenHelper
{
    private static readonly string Auth0Domain = Environment.GetEnvironmentVariable("AUTH0_DOMAIN");
    private static readonly string ClientId = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID");
    private static readonly string ClientSecret = Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET");
    private static readonly string Audience = Environment.GetEnvironmentVariable("AUTH0_AUDIENCE");
    /// <summary>
    /// To run the tests locally you need to set these vars in your local env useing the following comands: 
    /// owherie most of the tests will failes
    /// Run the following commands to add to your windows locall env:
    /// [System.Environment]::SetEnvironmentVariable("AUTH0_DOMAIN", "https://dev-VALUE.uk.auth0.com", "User")
    /// [System.Environment]::SetEnvironmentVariable("AUTH0_CLIENT_ID", "VALUE", "User")
    /// [System.Environment]::SetEnvironmentVariable("AUTH0_CLIENT_SECRET", "VALUE-P3", "User")
    /// [System.Environment]::SetEnvironmentVariable("AUTH0_AUDIENCE", "https://VALUE.net/", "User")
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<string> GetAuthTokenAsync()
    {
        Console.WriteLine("AUTH0_DOMAIN: " + Environment.GetEnvironmentVariable("AUTH0_DOMAIN"));
        Console.WriteLine("AUTH0_CLIENT_ID: " + Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID"));
        Console.WriteLine("AUTH0_CLIENT_SECRET: " + Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET"));
        Console.WriteLine("AUTH0_AUDIENCE: " + Environment.GetEnvironmentVariable("AUTH0_AUDIENCE"));

        if (string.IsNullOrEmpty(Auth0Domain) || string.IsNullOrEmpty(ClientId) ||
            string.IsNullOrEmpty(ClientSecret) || string.IsNullOrEmpty(Audience))
        {
            throw new Exception("Missing Auth0 environment variables.");
        }

    public static async Task<string> GetAuthTokenAsync()
    {
        using var client = new HttpClient();

        var requestBody = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
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
