using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GestioneOrdiniRistorante.Models.Models.Request;

namespace GestioneOrdiniRistorante.Application.Validator
{
    public class VisaulizzaOrdiniReqValidator : AbstractValidator<VisualizzaOrdiniReq>
    {
        public VisaulizzaOrdiniReqValidator()
        {
            RuleFor(e => e.GiornoInizio)
                .NotEmpty().WithMessage("Start Date must be Specified");

            RuleFor(e => e.GiornoFine)
                .NotEmpty().WithMessage("Not a Date Found")
                .GreaterThan(elem => elem.GiornoInizio).WithMessage("Start Date could not" +
                                                                 " be forward then End Date");

        }
    }
}
