using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPlaces2020.Client.Models;
using FluentValidation;

namespace WebPlaces2020.Validator
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("This Id field is required")
                .GreaterThan(0).WithMessage("This Id field must be bigger than 0");

            RuleFor(x => x.FirstName_ACC)
                .NotNull().WithMessage("This Firstname field is required")
                .Length(1, 10).WithMessage("This Firstname field must be from 1 to 10 char length");

        }

    }
}
