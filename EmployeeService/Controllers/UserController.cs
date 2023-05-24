using EmployeeService.Models;
using EmployeeService.Models.Dto;
using EmployeeService.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        protected APIResponse response;

        public UserController(IUserService userService)
        {
            this.userService = userService;
            response = new();

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var loginRes = await userService.Login(login);
            if (loginRes == null) 
            { 
                response.StatusCode  = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(response);
            }
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            response.Result = loginRes;
            return Ok(response);
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            var newUser = await userService.CreateUser(user);

            bool ifUserNameIsUnique = userService.IsUniqueUser(user.Username);
            if (!ifUserNameIsUnique)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add("username is already taken");
                return BadRequest(response);
            }
            bool ifEmailIsUnique = userService.IsUniqueEmail(user.Email);
            if(!ifEmailIsUnique) 
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add("Email already taken");
                return BadRequest(response);
            }
            bool ifPhoneUnique = userService.IsUniquePhone(user.PhoneNumber);
            if (!ifPhoneUnique)
            {
                response.StatusCode =HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages.Add("Phone number is alreadt taken");
            }
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response);

        }

    }
}
