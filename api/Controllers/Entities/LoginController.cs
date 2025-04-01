using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.DTOs.Entities;
using api.Models.Entities;
using api.Services.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService loginService;
        private IConfiguration config;

        public LoginController(LoginService loginService, IConfiguration config)
    {
        this.loginService = loginService;
        this.config = config;
    }

    [HttpPost("authenticate", Name = "Login")]
    public async Task<IActionResult> Login(AdministratorDTO adminDTO)
    {
        var admin = await loginService.GetAdmin(adminDTO);

        if (admin is null) {
           return BadRequest(new { message = "Invalid credentials." });
        }

        string jwtToken = GenerateToken(admin);
        return Ok( new { token = jwtToken});
    }

    private string GenerateToken(Administrator admin)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, admin.Name),
                new Claim(ClaimTypes.Email, admin.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                                      claims: claims,
                                      expires: DateTime.Now.AddMinutes(60),
                                      signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
