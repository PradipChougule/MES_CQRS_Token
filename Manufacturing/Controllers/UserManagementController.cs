namespace Manufacturing.Controllers
{
    using Manufacturing.Core.Features;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly IMediator mediator;
        IConfiguration _configuration;

        public UserManagementController(IMediator mediator, IConfiguration configuration)
        {
            this.mediator = mediator;
            _configuration = configuration;
        }

        [Authorize(Roles = "SuperAdmin, User")]
        //[Authorize(Roles = "Supervisor")]
        [HttpGet]
        [Route("Users")]
        public async Task<List<GetUserDetailsResponse>> GetUserList()
        {
            var productDetails = await mediator.Send(new GetUserDetailsQuery());
            return productDetails;
        }

        [HttpPost]
        [Route("PostLoginDetails")]
        public async Task<IActionResult> GenerateToken(GenerateTokenQuery message)
        {
            var user = await mediator.Send(message);

            //if (user == null)
            //    return (0, "Invalid username");
            //if (!await userManager.CheckPasswordAsync(user, model.Password))
            //    return (0, "Invalid password");

            //var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserPin),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in user.Roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GeToken(authClaims);
            return Ok(new { token = token });
        }

        private string GeToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //public string GeToken(GetUserDetailsResponse _userData)
        //{
        //    var claims = new[] {
        //                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //                new Claim("UserPin", _userData.UserPin),
        //                new Claim("FirstName", _userData.FirstName),
        //                new Claim("Email", _userData.Email)
        //            };


        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        //    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var token = new JwtSecurityToken(
        //        _configuration["Jwt:Issuer"],
        //        _configuration["Jwt:Audience"],
        //        claims,
        //        expires: DateTime.UtcNow.AddMinutes(10),
        //        signingCredentials: signIn);


        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}


    }
}
