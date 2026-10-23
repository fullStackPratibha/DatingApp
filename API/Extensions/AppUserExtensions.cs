using API.DTOs;
using API.Entities;
using API.Interfaces;
using System;

namespace API.Extensions
{
    public static class AppUserExtensions
    {
        public static UserDto ToDto(this AppUser user, ITokenService token)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token.CreateToken(user),
                ImageUrl = null
            };
        }
    }
}
