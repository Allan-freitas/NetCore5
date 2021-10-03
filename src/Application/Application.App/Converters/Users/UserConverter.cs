using Application.App.Dtos.Users;
using Application.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.App.Converters.Users
{
    public static class UserConverter
    {
        public static UserDto UserToUserDto(User user)
        {
            if (user == null)
                throw new Exception("User cannot be empty");

            return new UserDto(user.Username, user.Email);
        }

        public static IList<UserDto> UserCollectionToUserDtoCollection(this List<User> users)
        {
            if (!users.Any())
                return new List<UserDto>();

            return users.Select(u => UserToUserDto(u)).ToList();
        }
    }
}
