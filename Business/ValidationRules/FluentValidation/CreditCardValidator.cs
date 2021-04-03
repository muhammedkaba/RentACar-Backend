using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    class CreditCardValidator : AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(c => c.CardNo).NotEmpty();
            RuleFor(c => c.CustomerId).NotEmpty();
            RuleFor(c => c.CVV).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.ExpiringDate).NotEmpty();
        }
    }
}
