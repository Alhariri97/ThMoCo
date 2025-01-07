//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.Extensions.DependencyInjection;
//using System.Net.Http.Headers;
//using System.Text.Json;
//using ThMoCo.Api.DTO;

//namespace ThMoCo.Tests.IntegrationTests
//{
//    public class AdminControllerTests : IClassFixture<WebApplicationFactory<ThMoCo.Api.Program>>
//    {
//        private readonly HttpClient _client;

//        public AdminControllerTests(WebApplicationFactory<ThMoCo.Api.Program> factory)
//        {
//            _client = factory.WithWebHostBuilder(builder =>
//            {
//                builder.ConfigureServices(services =>
//                {
//                    services.AddAuthentication(options =>
//                    {
//                        options.DefaultAuthenticateScheme = "Test";
//                        options.DefaultChallengeScheme = "Test";
//                    })
//                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
//                });
//            }).CreateClient();
//        }

//        [Fact]
//        public async Task GetOrdersToDispatch_ShouldReturnSuccessAndOrders()
//        {
//            MockAuthenticateClient("admin");

//            var response = await _client.GetAsync("/api/admin/orders/dispatch");
//            response.EnsureSuccessStatusCode();

//            var json = await response.Content.ReadAsStringAsync();
//            var orders = JsonSerializer.Deserialize<List<Order>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

//            Assert.NotNull(orders);
//            Assert.NotEmpty(orders);
//        }

//        [Fact]
//        public async Task MarkOrderAsDispatched_ShouldUpdateOrder()
//        {
//            MockAuthenticateClient("admin");

//            var response = await _client.PutAsync("/api/admin/orders/1/dispatch", null);
//            response.EnsureSuccessStatusCode();

//            var json = await response.Content.ReadAsStringAsync();
//            Assert.Contains("Order 1 marked as dispatched", json);
//        }

//        [Fact]
//        public async Task GetCustomerProfile_ShouldReturnUserDetails()
//        {
//            MockAuthenticateClient("admin");

//            var response = await _client.GetAsync("/api/admin/customers/1");
//            response.EnsureSuccessStatusCode();

//            var json = await response.Content.ReadAsStringAsync();
//            var user = JsonSerializer.Deserialize<AppUser>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

//            Assert.NotNull(user);
//            Assert.Equal("John Doe", user.Name);
//        }

//        [Fact]
//        public async Task DeleteCustomerAccount_ShouldReturnSuccessMessage()
//        {
//            MockAuthenticateClient("admin");

//            var response = await _client.DeleteAsync("/api/admin/customers/1");
//            response.EnsureSuccessStatusCode();

//            var json = await response.Content.ReadAsStringAsync();
//            Assert.Contains("Customer 1 account deleted successfully", json);
//        }

//        //[Fact]
//        //public async Task GetOrdersToDispatch_WithoutAuth_ShouldReturnUnauthorized()
//        //{
//        //    _client.DefaultRequestHeaders.Authorization = null;

//        //    var response = await _client.GetAsync("/api/admin/orders/dispatch");

//        //    Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
//        //}

//        private void MockAuthenticateClient(string role)
//        {
//            var fakeToken = GenerateFakeJwtToken(role);
//            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", fakeToken);
//        }

//        private string GenerateFakeJwtToken(string role)
//        {
//            return $"mock-jwt-token-{role}";
//        }
//    }


//}
