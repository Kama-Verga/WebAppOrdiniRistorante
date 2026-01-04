using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GestioneOrdiniRistorante.Models.Models.Request;

namespace GestioneOrdiniRistorante.Application.Validator
{
    public class CreaUtenteReqValidator : AbstractValidator<CreaUtenteReq>
    {
        public CreaUtenteReqValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");

            // Password Standards
            RuleFor(e => e.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                .Must(IsStrongPassword).WithMessage("Password must contain at least one uppercase letter, one lowercase " +
                                                    "letter, one number and one special character");

            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(e => e.Cognome)
                .NotEmpty().WithMessage("Surname is required");
        }

        private static bool IsStrongPassword(string password)
        {
            return password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) &&
                   password.Any(char.IsPunctuation);
        }

    }
}
