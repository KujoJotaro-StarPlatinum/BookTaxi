using BookTaxiEntyties.Entyties;
using Mapster;
using UserDto = BookTaxi.Common2.DTOs.UserDto;

namespace BookTaxi.Services.Extensions;

public static class ParseToDtoExtensions
{
    public static UserDto ParseToDto(this User user)
    {
        return user.Adapt<UserDto>();
    }

    public static List<UserDto> ParseToDtos(this List<User> users)
    {
        var dtos = new List<UserDto>();
        if (users == null || users.Count == 0)
        {
            return dtos;
        }
        dtos.AddRange(users.Select(u => u.ParseToDto()));
        return dtos;
    }
}