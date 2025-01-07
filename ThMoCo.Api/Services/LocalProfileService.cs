using Microsoft.EntityFrameworkCore;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Api.Services
{
    public class LocalProfileService : IProfileService
    {
        // In-memory store for payment cards (for demo)
        private readonly List<PaymentCard> _paymentCards = new List<PaymentCard>
        {
            new PaymentCard
            {
                UserId = "wMO0z3baPdNSN7edWYKKc1M39gd3g9XE@clients",
                CardNumber = "1234 5678 9012 3456",
                CardHolderName = "John Doe",
                ExpiryDate = "12/25",
                Cvv = "123"
            },
            new PaymentCard
            {
                UserId = "TestUser",
                CardNumber = "9999 8888 7777 6666",
                CardHolderName = "Jane Smith",
                ExpiryDate = "01/26",
                Cvv = "123"
            }
        };

        // In-memory store for addresses (for demo)
        private readonly List<Address> _addresses = new List<Address>
        {
            new Address
            {
                UserId ="wMO0z3baPdNSN7edWYKKc1M39gd3g9XE@clients",
                Street = "123 Main Street",
                City = "New York",
                State = "NY",
                PostalCode = "10001"
            },
            new Address
            {
                UserId = "test@clients",
                Street = "456 Elm Avenue",
                City = "Los Angeles",
                State = "CA",
                PostalCode = "90001"
            }
        };

        private readonly List<AppUser> _users = new List<AppUser>
        {
            new AppUser
            {
                Id = 2,
                Name = "John Doe",
                Email = "john.doe@example.com",
                UserAuthId = "auth0|677547672b430092f2ce6f83",
                PaymentCard = new PaymentCard
                {
                    CardNumber = "4111111111111111",
                    CardHolderName = "John Doe",
                    ExpiryDate = "12/24",
                    Cvv ="123"
                },
                Address = new Address
                {
                    Street = "123 Main Street",
                    City = "New York",
                    State = "NY",
                    PostalCode = "10001"
                }
            },
                new AppUser // "TestUser"
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    UserAuthId = "wMO0z3baPdNSN7edWYKKc1M39gd3g9XE@clients",
                    PhoneNumber = "123-456-7890",
                    Fund = 100.50m,
                    UpdatedAt = DateTime.UtcNow,
                    LastLogin = DateTime.UtcNow.AddDays(-1),
                    Provider = "auth0",
                    Role = "User",
                    IsEmailVerified = true,
                    PaymentCard = new PaymentCard
                    {
                        CardNumber = "4111111111111111",
                        CardHolderName = "John Doe",
                        ExpiryDate = "12/24",
                        Cvv = "123"
                    },
                    Address = new Address
                    {
                        Street = "123 Main Street",
                        City = "New York",
                        State = "NY",
                        PostalCode = "10001"
                    }
                },
            new AppUser
            {
                Id = 2,
                Name = "John Doe",
                Email = "john.doe@example.com",
                UserAuthId = "TestUser",
                PaymentCard = new PaymentCard
                {
                    CardNumber = "4111111111111111",
                    CardHolderName = "John Doe",
                    ExpiryDate = "12/24",
                    Cvv ="123"
                },
                Address = new Address
                {
                    Street = "123 Main Street",
                    City = "New York",
                    State = "NY",
                    PostalCode = "10001"
                }
            },

        };



        public AppUser AddUserAsync(AppUser user)
        {
            var doesUserExist = _users
                      .FirstOrDefault(u => u.UserAuthId == user.UserAuthId);
            if (doesUserExist != null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            _users.Add(user);

            return user;
        }


        public AppUser GetUserByAuthIdAsync(string userAuthId)
        {
            var user = _users
                      .FirstOrDefault(u => u.UserAuthId == userAuthId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return user;

        }

        public AppUser GetUserByIdAsync(int userId)
        {
            var user = _users
                      .FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            return user;
        }
        public AppUser UpdateUserAsync(AppUser userDto)
        {
            // Find the existing user in the in-memory collection by ID
            var existingUser = _users.FirstOrDefault(u => u.Id == userDto.Id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Update the user properties
            existingUser.Name = userDto.Name ?? existingUser.Name;
            existingUser.Email = userDto.Email ?? existingUser.Email;
            existingUser.PhoneNumber = userDto.PhoneNumber ?? existingUser.PhoneNumber;
            existingUser.PhotoUrl = userDto.PhotoUrl ?? existingUser.PhotoUrl;
            existingUser.Fund = userDto.Fund ?? existingUser.Fund;
            existingUser.UpdatedAt = DateTime.UtcNow; // Update the timestamp

            // Return the updated user as a DTO
            return existingUser;
        }


        /// <summary>
        /// Gets the current user’s payment card info, if any, from in-memory data.
        /// </summary>
        public PaymentCardDTO? GetPaymentCard(string userId)
        {
            // Look up by userId in the in-memory list
            var card = _paymentCards.FirstOrDefault(pc => pc.UserId == userId);

            if (card == null) throw new KeyNotFoundException("User not found.");

            // Return a copy, or the same object if you prefer
            return new PaymentCardDTO
            {
                CardNumber = card.CardNumber,
                CardHolderName = card.CardHolderName,
                ExpiryDate = card.ExpiryDate,
                Cvv = card.Cvv
            };
        }

        /// <summary>
        /// Saves or updates the current user’s payment card in memory.
        /// </summary>
        public PaymentCardDTO SavePaymentCard(string userId, PaymentCardDTO cardDto)
        {
            var existingCard = _paymentCards.FirstOrDefault(pc => pc.UserId == userId);
            if (existingCard == null)
            {
                // Create a new record
                _paymentCards.Add(new PaymentCard
                {
                    UserId = userId,
                    CardNumber = cardDto.CardNumber,
                    CardHolderName = cardDto.CardHolderName,
                    ExpiryDate = cardDto.ExpiryDate,
                    Cvv = cardDto.Cvv
                });
            }
            else
            {
                // Update existing
                existingCard.CardNumber = cardDto.CardNumber;
                existingCard.CardHolderName = cardDto.CardHolderName;
                existingCard.ExpiryDate = cardDto.ExpiryDate;
                existingCard.Cvv = cardDto.Cvv;
            }
            return cardDto;
        }

        /// <summary>
        /// Gets the current user’s address, if any, from in-memory data.
        /// </summary>
        public AddressDTO? GetAddress(string userId)
        {
            // Look up by userId in the in-memory list
            var address = _addresses.FirstOrDefault(a => a.UserId == userId);
            if (address == null) throw new KeyNotFoundException("User not found.");

            // Return a copy, or the same object if you prefer
            return new AddressDTO
            {
                Street = address.Street,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode
            };
        }

        /// <summary>
        /// Saves or updates the current user’s address in memory.
        /// </summary>
        public AddressDTO SaveAddress(string userId, AddressDTO addressDto)
        {
            var existingAddress = _addresses.FirstOrDefault(a => a.UserId == userId);
            if (existingAddress == null)
            {
                // Create new
                _addresses.Add(new Address
                {
                    UserId = userId,
                    Street = addressDto.Street,
                    City = addressDto.City,
                    State = addressDto.State,
                    PostalCode = addressDto.PostalCode
                });
            }
            else
            {
                // Update existing
                existingAddress.Street = addressDto.Street;
                existingAddress.City = addressDto.City;
                existingAddress.State = addressDto.State;
                existingAddress.PostalCode = addressDto.PostalCode;
            }
            return addressDto;
        }

        public bool AnonymiseCustomerDataAsync(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            user.Name = "Anonymous";
            user.Email = "anonymous@example.com";
            user.PhoneNumber = null;
            user.UserAuthId = null;
            user.Fund = 0;
            user.UpdatedAt = DateTime.UtcNow;
            user.IsEmailVerified = false;
            user.PaymentCard = null;
            user.Address = null;

            return true;
        }

        public List<AppUser> GetAllUsers()
        {
            var users = _users.ToList();
            return users;
        }
    }
}
