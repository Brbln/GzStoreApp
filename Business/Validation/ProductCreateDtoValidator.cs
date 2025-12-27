using Business.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(p => p.PName)
            .NotEmpty().WithMessage("Ürün adı boş olamaz.")
            .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalı.");

            RuleFor(p => p.PPrice)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

            RuleFor(p => p.PStock)
                .GreaterThanOrEqualTo(0).WithMessage("Stok 0'dan küçük olamaz.");

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("Kategori seçilmelidir.");

        }
    }
}

