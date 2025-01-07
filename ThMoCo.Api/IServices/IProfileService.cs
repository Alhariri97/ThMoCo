using ThMoCo.Api.DTO;
using ThMoCo.Api.Models;

namespace ThMoCo.Api.IServices;

public interface IProfileService
{
    List<AppUser> GetAllUsers();
    AppUser AddUserAsync(AppUser user);
    AppUser GetUserByAuthIdAsync(string userAuthId);
    AppUser GetUserByIdAsync(int userId);

    AppUser UpdateUserAsync(AppUser user);


    PaymentCardDTO GetPaymentCard(string userAuthId);
    PaymentCardDTO SavePaymentCard(string userAuthId, PaymentCardDTO cardDto);

    AddressDTO GetAddress(string userAuthId);
    AddressDTO SaveAddress(string userAuthId, AddressDTO addressDto);
    bool AnonymiseCustomerDataAsync(int userId);
}
