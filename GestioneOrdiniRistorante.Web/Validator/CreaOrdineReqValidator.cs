using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GestioneOrdiniRistorante.Models.Models.Request;
using GestioneOrdiniRistorante.Models.Models.Responses;

namespace GestioneOrdiniRistorante.Application.Validator
{
    public class CreaOrdineReqValidator : AbstractValidator<CreaOrdineReq>
    {
        public CreaOrdineReqValidator()
        {
            RuleFor(e => e.Indirizzo_Di_Consegna)
                .NotEmpty().WithMessage("Delivery Address id Required");

            RuleFor(e => e.Contenuto)
                .NotEmpty().WithMessage("The order must contain items.");

            RuleForEach(e => e.Contenuto)
                .Must(num => num >= 1 && num <= 12)
                .WithMessage("All items in the order must have values between 1 and 12.");
        }
    }
}
