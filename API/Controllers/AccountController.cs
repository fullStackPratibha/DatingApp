using API.Data;
using API.Entities;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using API.Extensions;

namespace API.Controllers
{
    public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")] // api/account/register
        public async Task<ActionResult<UserDto>> Register([FromBody]RegisterDto registerDto)
        {
            if (await EmailExists(registerDto.Email)) return BadRequest("Email taken");

            Console.WriteLine("testing");
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                passwordSalt = hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Ok(user.ToDto(tokenService));

        }

        [HttpPost("login")] // api/account/login
        public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto loginDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == loginDto.Email.ToLower());
            if(user == null) return Unauthorized("Invalid email");

            using var hmac = new HMACSHA512(user.passwordSalt);

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(var i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.passwordHash[i]) return Unauthorized("Invalid Password");
            }

            return Ok(user.ToDto(tokenService));
        }

        private async Task<bool> EmailExists(string email)
        {
            return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
