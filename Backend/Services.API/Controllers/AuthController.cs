using App.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Services.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthContext _context;
        private readonly IConfiguration _config;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthController(AuthContext context,
                              IConfiguration configuration,
                              SignInManager<AppUser> signInManager,
                              UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _config = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterViewModel model)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                #region Validate
                if (!ModelState.IsValid)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Bilgileriniz eksik, bazı alanlar gönderilmemiş. Lütfen tüm alanları doldurunuz.";

                    return BadRequest(responseViewModel);
                }

                AppUser existsUser = await _userManager.FindByNameAsync(model.Email);

                if (existsUser != null)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Kullanıcı zaten var.";

                    return BadRequest(responseViewModel);
                }
                #endregion

                //Kullanıcı bilgileri set edilir.
                AppUser user = new AppUser();

                user.FullName = model.FullName;
                user.Email = model.Email.Trim();
                user.UserName = model.Email.Trim();
                

                //Kullanıcı oluşturulur.
                IdentityResult result = await _userManager.CreateAsync(user, model.Password.Trim());

                //Kullanıcı oluşturuldu ise
                if (result.Succeeded)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(_config["Roles:User"]);

                    if (!roleExists)
                    {
                        IdentityRole role = new IdentityRole(_config["Roles:User"]);
                        role.NormalizedName = _config["Roles:User"];

                        _roleManager.CreateAsync(role).Wait();
                    }

                    //Kullanıcıya ilgili rol ataması yapılır.
                    _userManager.AddToRoleAsync(user, _config["Roles:User"]).Wait();

                    responseViewModel.IsSuccess = true;
                    responseViewModel.Message = "Kullanıcı başarılı şekilde oluşturuldu.";
                }
                else
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = string.Format("Kullanıcı oluşturulurken bir hata oluştu: {0}", result.Errors.FirstOrDefault().Description);
                }

                return Ok(responseViewModel);
            }
            catch (Exception ex)
            {
                responseViewModel.IsSuccess = false;
                responseViewModel.Message = ex.Message;

                return BadRequest(responseViewModel);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<ActionResult> CreateAdmin([FromBody] RegisterViewModel model)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                #region Validate
                if (!ModelState.IsValid)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Bilgileriniz eksik, bazı alanlar gönderilmemiş. Lütfen tüm alanları doldurunuz.";

                    return BadRequest(responseViewModel);
                }

                AppUser existsUser = await _userManager.FindByNameAsync(model.Email);

                if (existsUser != null)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Kullanıcı zaten var.";

                    return BadRequest(responseViewModel);
                }
                #endregion

                //Kullanıcı bilgileri set edilir.
                AppUser user = new AppUser();

                user.FullName = model.FullName;
                user.Email = model.Email.Trim();
                user.UserName = model.Email.Trim();

                //Kullanıcı oluşturulur.
                IdentityResult result = await _userManager.CreateAsync(user, model.Password.Trim());

                //Kullanıcı oluşturuldu ise
                if (result.Succeeded)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(_config["Roles:Admin"]);

                    if (!roleExists)
                    {
                        IdentityRole role = new IdentityRole(_config["Roles:Admin"]);
                        role.NormalizedName = _config["Roles:Admin"];

                        _roleManager.CreateAsync(role).Wait();
                    }

                    //Kullanıcıya ilgili rol ataması yapılır.
                    _userManager.AddToRoleAsync(user, _config["Roles:Admin"]).Wait();

                    responseViewModel.IsSuccess = true;
                    responseViewModel.Message = "Kullanıcı başarılı şekilde oluşturuldu.";
                }
                else
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = string.Format("Kullanıcı oluşturulurken bir hata oluştu: {0}", result.Errors.FirstOrDefault().Description);
                }

                return Ok(responseViewModel);
            }
            catch (Exception ex)
            {
                responseViewModel.IsSuccess = false;
                responseViewModel.Message = ex.Message;

                return BadRequest(responseViewModel);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel model)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                #region Validate

                if (ModelState.IsValid == false)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Bilgileriniz eksik, bazı alanlar gönderilmemiş. Lütfen tüm alanları doldurunuz.";
                    return BadRequest(responseViewModel);
                }

                //Kulllanıcı bulunur.
                AppUser user = await _userManager.FindByNameAsync(model.Email);

                //Kullanıcı var ise;
                if (user == null)
                {
                    return Unauthorized();
                }

                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user,
                                                                                                                   model.Password,
                                                                                                                   false,
                                                                                                                   false);
                //Kullanıcı adı ve şifre kontrolü
                if (signInResult.Succeeded == false)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Kullanıcı adı veya şifre hatalı.";

                    return Unauthorized(responseViewModel);
                }

                #endregion

                AppUser AppUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                var roleId = _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId).FirstOrDefault();
                var AppRole = _context.Roles.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefault();

                AccessTokenGenerator accessTokenGenerator = new AccessTokenGenerator(_context, _config, AppUser, AppRole);
                AppUserTokens userTokens = accessTokenGenerator.GetToken();

                responseViewModel.IsSuccess = true;
                responseViewModel.Message = "Kullanıcı giriş yaptı.";
                responseViewModel.TokenInfo = new TokenInfo
                {
                    Token = userTokens.Value,
                    ExpireDate = userTokens.ExpireDate,
                    Role = userTokens.Role
                    
                };

                return Ok(responseViewModel);
            }
            catch (Exception ex)
            {
                responseViewModel.IsSuccess = false;
                responseViewModel.Message = ex.Message;

                return BadRequest(responseViewModel);
            }
        }


        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public async Task<ActionResult> Logout([FromBody] LoginViewModel User)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Email);

            if (user == null)
            {
                return BadRequest();
            }

            var loggedUser = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            if (loggedUser == User.Email)
            {
                AppUser AppUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                var roleId = _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId).FirstOrDefault();
                var AppRole = _context.Roles.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefault();

                AccessTokenGenerator accessTokenGenerator = new AccessTokenGenerator(_context, _config, AppUser, AppRole);
                await accessTokenGenerator.DeleteToken();
                await _signInManager.SignOutAsync();
                var lu = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                Console.WriteLine(lu);

                return Ok();
            }

            return BadRequest();
        }
    }
}
