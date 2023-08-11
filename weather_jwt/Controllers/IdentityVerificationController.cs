using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using weather_jwt.Models;

namespace weather_jwt.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityVerificationController : ControllerBase
    {
        private readonly JwtSettings _settings;
        public IdentityVerificationController(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }
        [HttpPost("Access")]
        public IActionResult Access([FromBody] ApiUser user)
        {
            var apiUser = MakeVerification(user);
            if (apiUser==null)
            {
                return NotFound("User not found");
            }

            var token = GenerateToken(apiUser);
            return Ok(token);
        }

        private ApiUser MakeVerification(ApiUser user)
        {
            return ApiUsers.users.FirstOrDefault
            (x => x.Name?.ToLower() == user.Name 
                  && x.Code == user.Code);
        }

        private string GenerateToken(ApiUser apiUser)
        {
            if (_settings.Key==null)
            {
                throw new Exception("Jwt key can not be null");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, apiUser.Name),
                new Claim(ClaimTypes.Role, apiUser.Role)
            };
            var token = new JwtSecurityToken(_settings.Issuer, _settings.Audience, claims,
                expires:DateTime.Now.AddHours(1),signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
