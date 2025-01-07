using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using ThMoCo.WebApp.DTO;
using ThMoCo.WebApp.IServices;

namespace ThMoCo.WebApp.Services
{
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        private async Task<HttpClient> CreateAuthenticatedClientAsync()
        {
            // Retrieve the access token from the current HTTP context
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            return _httpClient;
        }





        public async Task<AppUserDTO> GetUser()
        {
            try
            {
                var httpClient = await CreateAuthenticatedClientAsync();
                var response = await httpClient.GetAsync("/api/Profile/user");
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received JSON: {json}"); // Debugging log

                // Parse the returned JSON into an AppUserDTO
                var user = await response.Content.ReadFromJsonAsync<AppUserDTO>();
                return user;
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                throw new Exception();
            }
        }

        public async Task<AppUserDTO> SaveUser(AppUserDTO userDTO)
        {
            var httpClient = await CreateAuthenticatedClientAsync();

            // Use PUT instead of POST
            var response = await httpClient.PutAsJsonAsync("/api/Profile/user", userDTO);
            response.EnsureSuccessStatusCode();

            // Parse the returned JSON into an AppUserDTO
            return await response.Content.ReadFromJsonAsync<AppUserDTO>();
        }





        public async Task<PaymentCardDTO> GetPaymentCard()
        {
            try
            {
                var httpClient = await CreateAuthenticatedClientAsync();
                var response = await httpClient.GetAsync($"/api/Profile/paymentcard");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<PaymentCardDTO>();
            }
            catch (Exception ex)
            {
                return new PaymentCardDTO();
            }
        }

        public async Task<PaymentCardDTO> SavePaymentCard(PaymentCardDTO paymentCardDTO)
        {
            var httpClient = await CreateAuthenticatedClientAsync();

            var response = await httpClient.PostAsJsonAsync("/api/Profile/paymentcard", paymentCardDTO);
            response.EnsureSuccessStatusCode();

            // Read back the updated PaymentCardDTO
            var updatedCard = await response.Content.ReadFromJsonAsync<PaymentCardDTO>();

            return updatedCard;
        }



        public async Task<AddressDTO> GetAddress()
        {
            try
            {
                var httpClient = await CreateAuthenticatedClientAsync();

                var response = await httpClient.GetAsync("/api/Profile/address");

                response.EnsureSuccessStatusCode();
                var addressDto = await response.Content.ReadFromJsonAsync<AddressDTO>();

                return addressDto;
            }
            catch (Exception ex)
            {
                return new AddressDTO();
            }
        }

        public async Task<AddressDTO> SaveAddress(AddressDTO addressDTO)
        {
            var httpClient = await CreateAuthenticatedClientAsync();

            var response = await httpClient.PostAsJsonAsync("/api/Profile/address", addressDTO);
            response.EnsureSuccessStatusCode();

            // Parse the returned JSON into an AddressDTO
            var updatedAddressDto = await response.Content.ReadFromJsonAsync<AddressDTO>();

            return updatedAddressDto;
        }

        public async Task<AppUserDTO> AddFund(AddFundsDTO addFundsDTO)
        {
            try
            {
                var httpClient = await CreateAuthenticatedClientAsync();

                var response = await httpClient.PostAsJsonAsync("/api/Profile/addfunds", addFundsDTO);
                if (response.IsSuccessStatusCode)
                {
                    var updatedAddressDto = await response.Content.ReadFromJsonAsync<AppUserDTO>();

                    return updatedAddressDto;
                }
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorMessage}");
                throw new Exception(errorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
