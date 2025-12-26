using Business.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class UserUpdateDtoValidator:AbstractValidator<UserUpdateDto>
    {        public UserUpdateDtoValidator()
        {
            RuleFor(u => u.UserId)
                .GreaterThan(0).WithMessage("Geçersiz kullanıcı ID.");

            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalı.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçersiz email formatı.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı.");
        }
    }
}
