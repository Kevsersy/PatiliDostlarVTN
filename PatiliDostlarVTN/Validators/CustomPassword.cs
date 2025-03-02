using Microsoft.AspNetCore.Identity;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Validators;

public class CustomPassword : IPasswordValidator<AppUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
    {
        var errors = new List<IdentityError>();

        if (user.UserName == password)
        {
            errors.Add(new() { Code = "PasswordContainsUsername", Description = "Password can not contains user name." });


        }
        if (errors.Any())
        {
            return Task.FromResult(IdentityResult.Failed(errors.ToArray()));


        }
        return Task.FromResult(IdentityResult.Success);
    }

}

