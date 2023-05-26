using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CurrencyAccountValidator : AbstractValidator<CurrencyAccount>
    {
        public CurrencyAccountValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Firma Adı Boş Olamaz");
            RuleFor(p => p.Name).MinimumLength(4).WithMessage("Firma adı en az 4 karakter uzunşuğunda olmalı");

            RuleFor(p => p.Address).NotEmpty().WithMessage("Firma adressi Boş Olamaz");
            RuleFor(p => p.Address).MinimumLength(4).WithMessage("Firma adresi en az 4 karakter uzunşuğunda olmalı");
        }
    }
}
