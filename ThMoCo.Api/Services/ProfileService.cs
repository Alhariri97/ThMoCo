using Microsoft.AspNetCore.Http.HttpResults;
using ThMoCo.Api.Data;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Services;

public class ProfileService : IProfileService
{
    private readonly AppDbContext _dbContext;

    public ProfileService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }



    public AppUser AddUserAsync(AppUser user)
    {
        var doesUserExist = _dbContext.AppUsers
                  .FirstOrDefault(u => u.UserAuthId == user.UserAuthId);
        if (doesUserExist != null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        _dbContext.AppUsers.Add(user);
        _dbContext.SaveChanges();

        return user;
    }


    public  AppUser GetUserByAuthIdAsync(string userAuthId)
    {
        var user = _dbContext.AppUsers
                  .FirstOrDefault(u => u.UserAuthId == userAuthId);

        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        return user;

    }

    public AppUser GetUserByIdAsync(int userId)
    {
        var user = _dbContext.AppUsers
                  .FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        return user;
    }
    public AppUser? UpdateUserAsync(AppUser user)
    {
        // Find the existing user in the database by UserAuthId
        var existedUser = _dbContext.AppUsers
                      .FirstOrDefault(u => u.UserAuthId == user.UserAuthId);

        if (existedUser == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        // Update the user entity in the database
        _dbContext.AppUsers.Update(user);
        _dbContext.SaveChanges();

        // Map the updated AppUser to AppUserDTO
        return existedUser;
    }







    /// <summary>
    /// Gets the current user’s payment card info, if any.
    /// </summary>
    public PaymentCardDTO GetPaymentCard(string userId)
    {
        var card = _dbContext.PaymentCards
            .FirstOrDefault(pc => pc.UserId == userId);

        if (card == null) throw new KeyNotFoundException("User not found.");

        // Map entity to DTO
        return new PaymentCardDTO
        {
            CardNumber = card.CardNumber,
            CardHolderName = card.CardHolderName,
            ExpiryDate = card.ExpiryDate,
            Cvv = card.Cvv
        };
    }

    /// <summary>
    /// Saves or updates the current user’s payment card.
    /// </summary>
    public PaymentCardDTO SavePaymentCard(string userId, PaymentCardDTO cardDto)
    {
        // Check if user already has a PaymentCard record
        var existingCard = _dbContext.PaymentCards
            .FirstOrDefault(pc => pc.UserId == userId);

        if (existingCard == null)
        {
            // Create a new PaymentCard entity
            var newCard = new PaymentCard
            {
                UserId = userId,
                CardNumber = cardDto.CardNumber,
                CardHolderName = cardDto.CardHolderName,
                ExpiryDate = cardDto.ExpiryDate,
                Cvv = cardDto.Cvv
            };
            _dbContext.PaymentCards.Add(newCard);
        }
        else
        {
            // Update the existing one
            existingCard.CardNumber = cardDto.CardNumber;
            existingCard.CardHolderName = cardDto.CardHolderName;
            existingCard.ExpiryDate = cardDto.ExpiryDate;
            existingCard.Cvv = cardDto.Cvv;
        }

        // Save changes
        _dbContext.SaveChanges();
        return cardDto;
    }

    /// <summary>
    /// Gets the current user’s address, if any.
    /// </summary>
    public AddressDTO GetAddress(string userId)
    {
        var address = _dbContext.Addresses
            .FirstOrDefault(a => a.UserId == userId);

        if (address == null)
            throw new KeyNotFoundException("User not found.");

        // Map entity to DTO
        return new AddressDTO
        {
            Street = address.Street,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode
        };
    }

    /// <summary>
    /// Saves or updates the current user’s address.
    /// </summary>
    public AddressDTO SaveAddress(string userId, AddressDTO addressDto)
    {
        var existingAddress = _dbContext.Addresses
            .FirstOrDefault(a => a.UserId == userId);

        if (existingAddress == null)
        {
            // Create a new Address entity
            var newAddress = new Address
            {
                UserId = userId,
                Street = addressDto.Street,
                City = addressDto.City,
                State = addressDto.State,
                PostalCode = addressDto.PostalCode
            };
            _dbContext.Addresses.Add(newAddress);
        }
        else
        {
            // Update the existing one
            existingAddress.Street = addressDto.Street;
            existingAddress.City = addressDto.City;
            existingAddress.State = addressDto.State;
            existingAddress.PostalCode = addressDto.PostalCode;
        }

        _dbContext.SaveChanges();
        return addressDto;
    }


}
