using App.Library;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Services.API.Controllers;
using Services.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Services.Test.UnitTests.Controllers
{
    public class AuthControllerUnitTest
    {
        private readonly IFixture _fixture;
        private readonly AuthContext _context;
        private readonly Mock<IConfiguration> _config;
        private readonly Mock<SignInManager<AppUser>> _signInManager;
        private readonly Mock<UserManager<AppUser>> _userManager;
        private readonly Mock<RoleManager<IdentityRole>> _roleManager;
        private readonly AuthController _controller;
        private readonly Mock<IHttpContextAccessor> _contextAccessor;
        public AuthControllerUnitTest()
        {
            _contextAccessor = new Mock<IHttpContextAccessor>();
            DbContextOptions<AuthContext> options = new();
            _fixture = new Fixture();
            _context = new AuthContext(options, _contextAccessor.Object);
            _config = new Mock<IConfiguration>();

            //UserManager setup
            var store = new Mock<IUserStore<AppUser>>();
            var mgr = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<AppUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.UpdateAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);

            _signInManager = new Mock<SignInManager<AppUser>>();
            _roleManager = new Mock<RoleManager<IdentityRole>>();
            _controller = new AuthController(_context, _config.Object, _signInManager.Object, _userManager.Object, _roleManager.Object);

        }

        [Fact]
        public void Register_UserExist_ReturnsBadRequest()
        {
            //Arrange
            RegisterViewModel model = new RegisterViewModel() { FullName = "Furkan Kaya", Email = "example@hotmail.com", Password = "123Asd.", ConfirmPassword = "123Asd." };
            AppUser user = new AppUser() { UserName="example@hotmail.com", Email = "example@hotmail.com", FullName= "Furkan Kaya", Id=It.IsAny<string>(), PasswordHash=It.IsAny<string>()};
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            
            //Act
            var result = _controller.Register(model);

            //Assert
            //result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }
        //[Fact]
        //public void Register_UserNotExistAndCreationSucceeds_ReturnsOk()
        //{
        //    //Arrange
        //    RegisterViewModel model = new RegisterViewModel() { FullName="Furkan Kaya", Email="example@hotmail.com", Password="123Asd.", ConfirmPassword="123Asd."};
        //    AppUser nullUser = null;
        //    _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(nullUser);
        //    _userManager.Setup(x => x.CreateAsync());
        //    //Act
        //    var result = _controller.Register(model);

        //    //Assert

        //}
    }
}
