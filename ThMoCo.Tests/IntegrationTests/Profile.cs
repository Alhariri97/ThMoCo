using System.Net.Http.Headers;
using System.Net;
using System.Net.Http.Json;
using ThMoCo.Api.DTO;

namespace ThMoCo.Tests.IntegrationTests;

public class Profile : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client;
    private string _authToken;

    public Profile(CustomWebApplicationFactory<Api.Program> factory)
    {
        _client = factory.CreateClient();
    }

    private async Task AuthenticateClient()
    {
        _authToken = await Auth0TokenHelper.GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _authToken);
    }

    [Fact]
    public async Task RegisterUser_ReturnsConflict_WhenUserAlreadyExists()
    {
        //var any = new AppUser
        //{
        //    Id = 2,
        //    Name = "John Doe",
        //    Email = "john.doe@example.com",
        //    UserAuthId = "TestUser",
        //    PaymentCard = new PaymentCard
        //    {
        //        CardNumber = "4111111111111111",
        //        CardHolderName = "John Doe",
        //        ExpiryDate = "12/24",
        //        Cvv = "123"
        //    },
        //    Address = new Address
        //    {
        //        Street = "123 Main Street",
        //        City = "New York",
        //        State = "NY",
        //        PostalCode = "10001"
        //    }
        //};
        var userDto = new RegisterUserDTO
        {
            created_at = DateTime.Now,
            email = "test@gm.com",
            email_verified = false,
            family_name = "Test_family_name",
            given_name = "Test_given_name",
            name = "Test_name",
            nickname = "nickname",
            phone_number = "000",
            picture = "",
            tenant = "dev-wg8ow1pequtk5eia",
            updated_at = DateTime.Now,
            phone_verified = false,
            user_id = "TestUser",
            username = "t+test"
        }; var response = await _client.PostAsJsonAsync("/api/profile/register", userDto);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task RegisterUser_ReturnsOk_WhenSuccessful()
    {

        var userDto = new RegisterUserDTO
        {
            created_at = DateTime.Now,
            email = "test@gm.com",
            email_verified = false,
            family_name = "Test_family_name",
            given_name = "Test_given_name",
            name = "Test_name",
            nickname = "nickname",
            phone_number= "000",
            picture = "",
            tenant = "dev-wg8ow1pequtk5eia",
            updated_at = DateTime.Now,
            phone_verified = false,
            user_id = "newUser",
            username = "t+test"
        };

        var response = await _client.PostAsJsonAsync("/api/profile/register", userDto);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task GetUser_ReturnsOk_WhenUserExists()
    {
        await AuthenticateClient();  // ✅ Authenticate before making request

        var response = await _client.GetAsync("/api/profile/user");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddFunds_ReturnsBadRequest_WhenAmountIsInvalid()
    {
        await AuthenticateClient();  // ✅ Ensure request has valid auth

        var response = await _client.PostAsJsonAsync("/api/profile/addfunds", new AddFundsDTO { Amount = -50 });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task AddFunds_ReturnsOk_WhenSuccessful()
    {
        await AuthenticateClient();  // ✅ Authenticate before making request

        var requestBody = new AddFundsDTO { Amount = 50, Cvv = "123" };
        var response = await _client.PostAsJsonAsync("/api/profile/addfunds", requestBody);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
