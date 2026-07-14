using System;

namespace API.Entities;

public class AppUser
{
    public required string Id {get; set;} = Guid.NewGuid().ToString();

    public string DisplayName {get; set;}

    public string Email {get; set;}
}