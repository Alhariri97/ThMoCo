using ThMoCo.WebApp.Models;

namespace ThMoCo.WebApp.IServices;

public interface IProfileService
{
    Task<PaymentCardDTO> GetPaymentCard();
    Task<PaymentCardDTO> SavePaymentCard(PaymentCardDTO paymentCardDTO);

    Task<AddressDTO> GetAddress();
    Task<AddressDTO> SaveAddress(AddressDTO addressDTO);


    Task<AppUserDTO> GetUser();
    Task<AppUserDTO> SaveUser(AppUserDTO userDTO);


    Task<AppUserDTO> AddFund(AddFundsDTO addFundsDTO);

}
