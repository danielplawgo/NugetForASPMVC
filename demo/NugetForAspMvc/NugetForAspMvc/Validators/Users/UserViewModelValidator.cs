using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using NugetForAspMvc.Models;
using NugetForAspMvc.ViewModels.Users;

namespace NugetForAspMvc.Validators.Users
{

    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator(DataContext db)
        {
            RuleFor(u => u.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Email jest wymagany")
                .EmailAddress()
                .WithMessage("Błędny format adresu")
                .Must(e => db.Users.Any(u => u.Email == e) == false)
                .WithMessage("Email musi być unikalny");

            RuleFor(u => u.Nip)
                .Must(n => n != null && n.Length == 10)
                .WithMessage("Błędny format NIPu")
                .When(u => u.CreateInvoice);
        }
    }
}