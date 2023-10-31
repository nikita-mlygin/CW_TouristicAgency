using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Clients.Open
{
    internal class OpenViewUpdateInputDataValidator : AbstractValidator<ClientUpdateInputData>
    {
        public OpenViewUpdateInputDataValidator()
        {
            RuleFor(client => client.FirstName)
                .NotEmpty().WithMessage("Имя должно быть заполнено.")
                .MaximumLength(50).WithMessage("Имя не должно превышать 50 символов.");

            RuleFor(client => client.LastName)
                .NotEmpty().WithMessage("Фамилия должна быть заполнена.")
                .MaximumLength(50).WithMessage("Фамилия не должна превышать 50 символов.");

            RuleFor(client => client.MiddleName)
                .MaximumLength(50).WithMessage("Отчество не должно превышать 50 символов.");

            RuleFor(client => client.PhoneNumber)
                .NotEmpty().WithMessage("Номер телефона должен быть заполнен.");

            RuleFor(client => client.Address)
                .NotEmpty().WithMessage("Адрес должен быть заполнен.")
                .MaximumLength(100).WithMessage("Адрес не должен превышать 100 символов.");
        }
    }
}
