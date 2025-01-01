using ThMoCo.Api.DTO;

namespace ThMoCo.Api.IServices;

public interface IProfileService
{
    AppUser GetUserByAuthIdAsync(string userAuthId);
    AppUser AddUserAsync(AppUser user);
    PaymentCardDTO GetPaymentCard(string userId);
    PaymentCardDTO SavePaymentCard(string userId, PaymentCardDTO cardDto);

    AddressDTO GetAddress(string userId);
    AddressDTO SaveAddress(string userId, AddressDTO addressDto);
}
