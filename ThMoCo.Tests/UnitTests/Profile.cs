using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThMoCo.Api.Controllers;
using ThMoCo.Api.DTO;
using ThMoCo.Api.IServices;

namespace ThMoCo.Tests.UnitTests;

public class Profile
{
    private readonly Mock<IProfileService> _mockProfileService;
    private readonly ProfileController _controller;


    public Profile()
    {
        _mockProfileService = new Mock<IProfileService>();
        _controller = new ProfileController(_mockProfileService.Object);
    }


    [Fact]
    public async Task RegisterUser_ReturnsConflict_WhenUserAlreadyExists()
    {
        // Arrange
        var userDto = new RegisterUserDTO { user_id = "existingUser" };
        _mockProfileService.Setup(s => s.GetUserByAuthIdAsync(userDto.user_id))
                           .Returns(new AppUser()); // Simulate user exists

        // Act
        var result = await _controller.RegisterUser(userDto);

        // Assert
        Assert.IsType<ConflictObjectResult>(result.Result);
    }

    [Fact]
    public async Task RegisterUser_ReturnsBadRequest_WhenUserDtoIsNull()
    {
        // Act
        var result = await _controller.RegisterUser(null);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task RegisterUser_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var userDto = new RegisterUserDTO { user_id = "newUser" };
        _mockProfileService.Setup(s => s.GetUserByAuthIdAsync(userDto.user_id))
                           .Returns((AppUser)null);
        _mockProfileService.Setup(s => s.AddUserAsync(It.IsAny<AppUser>()))
                           .Returns(new AppUser());

        // Act
        var result = await _controller.RegisterUser(userDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result); // Expect OkObjectResult
        Assert.NotNull(okResult.Value); // Ensure it contains an object
    }




    [Fact]
    public void GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _mockProfileService.Setup(s => s.GetUserByAuthIdAsync(It.IsAny<string>()))
                           .Throws(new KeyNotFoundException());

        // Act
        var result = _controller.GetUser();

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void GetUser_ReturnsOk_WhenUserExists()
    {
        // Arrange
        var user = new AppUser { Name = "John Doe" };
        _mockProfileService.Setup(s => s.GetUserByAuthIdAsync(It.IsAny<string>()))
                           .Returns(user);

        // Act
        var result = _controller.GetUser();

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }




    [Fact]
    public void UpdateUserInfo_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _mockProfileService.Setup(s => s.GetUserByAuthIdAsync(It.IsAny<string>()))
                           .Throws(new KeyNotFoundException());

        // Act
        var result = _controller.UpdateUserInfo(new AppUserDTO());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void UpdateUserInfo_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var user = new AppUser { Id = 1, Name = "John Doe" };
        _mockProfileService.Setup(s => s.GetUserByAuthIdAsync(It.IsAny<string>()))
                           .Returns(user);
        _mockProfileService.Setup(s => s.UpdateUserAsync(It.IsAny<AppUser>()))
                           .Returns(user);

        // Act
        var result = _controller.UpdateUserInfo(new AppUserDTO { Name = "Updated Name" });

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }




    [Fact]
    public async Task AddFunds_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _mockProfileService.Setup(s => s.GetUserByAuthIdAsync(It.IsAny<string>()))
                           .Throws(new KeyNotFoundException());

        // Act
        var result = await _controller.AddFunds(new AddFundsDTO { Amount = 100 });

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task AddFunds_ReturnsBadRequest_WhenAmountIsInvalid()
    {
        // Act
        var result = await _controller.AddFunds(new AddFundsDTO { Amount = -50 });

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    // todo: fix this test.

    //[Fact]
    //public async Task AddFunds_ReturnsOk_WhenSuccessful()
    //{
    //    // Arrange
    //    var user = new AppUser
    //    {
    //        Id = 2,
    //        Name = "John Doe",
    //        Email = "john.doe@example.com",
    //        UserAuthId = "TestUser",
    //        PaymentCard = new PaymentCard
    //        {
    //            CardNumber = "4111111111111111",
    //            CardHolderName = "John Doe",
    //            ExpiryDate = "12/24",
    //            Cvv = "123"
    //        },
    //        Address = new Address
    //        {
    //            Street = "123 Main Street",
    //            City = "New York",
    //            State = "NY",
    //            PostalCode = "10001"
    //        }
    //    };

    //    _mockProfileService.Setup(s => s.GetUserByAuthIdAsync(It.IsAny<string>()))
    //                       .Returns(user); // Ensure this is awaited correctly

    //    _mockProfileService.Setup(s => s.UpdateUserAsync(It.IsAny<AppUser>()))
    //                       .Returns(user); // Ensure this is awaited correctly

    //    // Act
    //    var result = await _controller.AddFunds(new AddFundsDTO { Amount = 50, Cvv = "123"});

    //    // Assert
    //    Assert.IsType<OkObjectResult>(result);
    //}



}