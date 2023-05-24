using AutoMapper;
using EmployeeService.Data;
using EmployeeService.Models;
using EmployeeService.Models.Dto;
using EmployeeService.Repository.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repository
{
    public class UserService : IUserService
    {
        private readonly EmployeeContext context;
        private string secretkey;
        private readonly IMapper mapper;
        public UserService(EmployeeContext context,IConfiguration configuration,IMapper mapper)
        {
            this.context = context;
            secretkey = configuration.GetValue<string>("Jwt:Key");
            this.mapper = mapper;


        }

        public async Task<User> CreateUser(User user)
        {
            if(user == null)
            {
                return null;
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 10);
            context.Users.Add(user);
            await context.SaveChangesAsync();
            user.Password = "";
            return user;

        }

        public bool IsUniqueEmail(string email)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if(user == null)
            {
                return false;
            }
            return true;
        }

        public bool IsUniquePhone(string phone)
        {
            var user = context.Users.FirstOrDefault(u => u.PhoneNumber == phone);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public bool IsUniqueUser(string username)
        {
            var user = context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<LoginResponseDto> Login(Login login)
        {
            var user = await context.Users.FirstOrDefaultAsync(u=>u.Username.ToLower()==login.Username.ToLower() );
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password,user.Password)) 
            {
                return null;
            }
            var userDto = mapper.Map<UserDto>(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Username)
                    //new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = userDto
            };
            return loginResponseDto;
            

        }
    }
}
