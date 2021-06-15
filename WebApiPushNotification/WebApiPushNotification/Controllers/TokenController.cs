using WebApiPushNotification.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApiPushNotification.Controllers
{
    ///<Summary>
    /// Controlador para el manejo de Token
    ///</Summary>
    ///

    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Token/[controller]")]
    public class TokenController : ControllerBase
    {
        ///<Summary>
        /// Parametros de configuracion
        ///</Summary>
        ///
        public IConfiguration _configuration;

        ///<Summary>
        /// Creacion de Instancia con parametos de configuracion
        ///</Summary>
        ///
        public TokenController(IConfiguration config)
        {
            _configuration = config;
        }

        /// <summary>  
        /// Envio de Notificaciones JWT Token Authentication  
        /// </summary>  
        [HttpPost]
        [MapToApiVersion("1.0")]
#pragma warning disable 1998
        public async Task<IActionResult> Token(UserInfo _userData)
        {

            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {


                if (_userData != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Password", _userData.Password),
                    new Claim("Email", _userData.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
