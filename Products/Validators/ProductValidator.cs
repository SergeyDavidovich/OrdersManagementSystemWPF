using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using DAL_LocalDb;
using Products.Add;
using System.Globalization;

namespace Products.Validators
{
    public class ProductValidator : AbstractValidator<ProductAddViewModel>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Required")
                .NotEmpty().WithMessage("Required")
                .MaximumLength(40).WithMessage("Too long");
            RuleFor(p => p.Quantity)
                .MaximumLength(20).WithMessage("Too long");
            RuleFor(p => p.SelectedCategory)
                .NotNull().WithMessage("Select category");
            RuleFor(p => p.SelectedSupplier)
                .NotNull().WithMessage("Select supplier");
            RuleFor(p => p.UnitPrice).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Required")
                .NotEmpty().WithMessage("Required")
                .Must(s => s.ToCharArray().Where(ch => ch.ToString() == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator).Count() <= 1)
                                .WithMessage("Must have one digitai sign only")
                .Must(HasDigitsOnly).WithMessage("Must have figures or digital separator only")
                ;
        }

        private bool HasDigitsOnly(string arg)
        {
            bool x = decimal.TryParse(arg, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out decimal number);
            return x ? true : false;
        }
    }

}

