using Microsoft.AspNetCore.Identity;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Validators;

public class CustomUserValidator : IUserValidator<AppUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
    {
        var errors = new List<IdentityError>();

        // Kullanıcı adı, email'den türetilmiş mi kontrolü
        if (user.Email.Contains("@"))
        {
            var emailPrefix = user.Email.Substring(0, user.Email.IndexOf("@"));

            if (emailPrefix.Equals(user.UserName, StringComparison.OrdinalIgnoreCase))
            {
                errors.Add(new IdentityError
                {
                    Code = "UserNameContainsEmailName",
                    Description = "Kullanıcı adı, email adresinden türetilmemelidir."
                });
            }
        }

        return errors.Any()
            ? Task.FromResult(IdentityResult.Failed(errors.ToArray()))
            : Task.FromResult(IdentityResult.Success);
    }
}
