using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using DAL_LocalDb;
using Products.Add;

namespace Products.Validators
{
    public class ProductValidator:AbstractValidator<ProductAddViewModel>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Required")
                .NotEmpty().WithMessage("Required")
                .MaximumLength(40).WithMessage("Too long");
            RuleFor(p => p.Quantity)
                .MaximumLength(20).WithMessage("Too long");
            RuleFor(p => p.UnitPrice);
                
        }
    }
}
