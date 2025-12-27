using Business.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class ProductUpdateDtoValidator:AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(p => p.ProductId)
                .GreaterThan(0).WithMessage("Geçerli bir ürün Id girilmelidir.");

            RuleFor(p => p.PName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(p => p.PPrice)
                .GreaterThan(0);

            RuleFor(p => p.PStock)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.CategoryId)
                .GreaterThan(0);
        }
    }
}
