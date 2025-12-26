using Business.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.PName)
                .NotEmpty().WithMessage("Ürün adı boş olamaz")
                .MinimumLength(2);

            RuleFor(x => x.PPrice)
                .GreaterThan(0);

            RuleFor(x => x.PStock)
                .GreaterThanOrEqualTo(0);

        }
    }
}

