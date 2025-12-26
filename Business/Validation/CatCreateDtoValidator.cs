using Business.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class CatCreateDtoValidator : AbstractValidator<CatCreateDto>
    {
        public CatCreateDtoValidator()
        {
            RuleFor(x => x.CName)
             .NotEmpty()
             .MinimumLength(2);
        }
    }
}
