using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using ThMoCo.WebApp.IServices;
using ThMoCo.WebApp.Models;

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

    }
}
