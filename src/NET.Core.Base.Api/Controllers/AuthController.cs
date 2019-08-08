using System;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NET.Core.Base.Api.Extensions;
using NET.Core.Base.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using NET.Core.Base.Business.Interfaces;

namespace NET.Core.Base.Api.Controllers
{
    [Route("api")]
    //[DisableCors]
    public class AuthController : MainController
    {
        public AuthController(
            INotificador notificador,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IOptions<AppSettings> appSettings,
            IUser user) 
            : base(user, notificador)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private readonly AppSettings _appSettings;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            IdentityUser user = new IdentityUser
            {
                UserName = registerUserViewModel.Email,
                Email = registerUserViewModel.Email,
                TwoFactorEnabled = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerUserViewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(await GerarJwt(user.Email));
            }
            foreach (IdentityError error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(registerUserViewModel);
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUserViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUserViewModel.Email, loginUserViewModel.Password, false, true);

            if (result.Succeeded) return CustomResponse(await GerarJwt(loginUserViewModel.Email));
            if (result.IsLockedOut)
            {
                NotificarErro("usuário temporariamente bloqueado por tentativas inválidas.");
                return CustomResponse(loginUserViewModel);
            }

            NotificarErro("Usuário ou senha incorretos.");
            return CustomResponse(loginUserViewModel);
        }

        private async Task<LoginResponseViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
