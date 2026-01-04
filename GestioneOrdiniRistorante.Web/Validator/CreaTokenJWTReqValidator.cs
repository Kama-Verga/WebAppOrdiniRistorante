using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GestioneOrdiniRistorante.Models.Models.Request;

namespace GestioneOrdiniRistorante.Application.Validator
{
    public class CreaTokenJWTReqValidator : AbstractValidator<CreaTokenJWTReq>
    {
        public CreaTokenJWTReqValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
