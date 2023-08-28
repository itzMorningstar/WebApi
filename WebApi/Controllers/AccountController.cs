using AutoMapper;
using Entities.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServicesLibrary.AccountServices;
using ServicesLibrary.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models.Accounts;
using WebApi.Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IGeneralService generalService;
        private readonly IConfiguration configuration;

        public AccountController(IAccountService accountService,IGeneralService generalService, IConfiguration configuration)
        {
            this.accountService = accountService;
            this.generalService = generalService;
            this.configuration = configuration;
        }
        // GET: api/<AccountController>
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
           
            
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username is required.");
            }
            var acc = accountService.GetAccountByUsername(username);
            if (acc == null)
            {
                return NotFound("No account was found with this username.");                
            }
            var response = new ResponseModel
            {
                ResponseStatus = Enums.ResponseStatus.Success,
                Data = acc
            };
            return Ok(response);
        }

        // GET api/<AccountController>/5
        [HttpGet("{username} {password}")]
        public IActionResult Get(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username is required.");
            }
            var acc = accountService.GetAccountByUsernameAndPassword(username,password);
            if (acc == null)
            {
                return NotFound("No account was found with this username.");
            }
            var response = new ResponseModel
            {
                ResponseStatus = Enums.ResponseStatus.Success,
                Data = acc
            };
            return Ok(response);
        }

        // POST api/<AccountController>
        [HttpPost]
        public IActionResult Post([FromBody] AccountCreateModel accountModel)
        {
            if (!ModelState.IsValid) return BadRequest("The model is not valid");

            var account = new Account
            {
                Username = accountModel.Username,
                Password = accountModel.Password,
                Email = accountModel.Email,
                Phone = accountModel.Phone,
                CreateOn = DateTime.Now,
                ApiKey = Guid.NewGuid(),
                LastLoginIP = HttpContext.Connection.RemoteIpAddress.ToString(),
                LastLoginUserAgent = HttpContext.Request.Headers["User-Agent"].ToString().Substring(99),
                Salt = "none"
            };
            accountService.AddAccount(account);
            var response = new ResponseModel
            {
                ResponseStatus = Enums.ResponseStatus.Success,
                Data = account
            };
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = accountService.GetAccountByUsername(model.Username);

            if (user == null || ! accountService.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized();
            }


            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            //var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256); 
            //var tokenOptions = new JwtSecurityToken (
            //    issuer: "https://localhost:7148", 
            //    audience: "https://localhost:7148", 
            //    claims: new List<Claim>(),
            //    expires: DateTime.Now.AddMinutes(5), 
            //    signingCredentials: signingCredentials);
            //var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            //return Ok(new { Token = tokenString });


            var token = generalService.GenerateJwtToken(user,model.RememberMe);
            return Ok(new { Token = token });

        }

    }
}
