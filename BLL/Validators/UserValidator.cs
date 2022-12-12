using BLL.Models;
using FluentValidation;

namespace BLL.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        readonly decimal minValue = 170.5M;
        public UserValidator()
        {

            RuleFor(x => x.Height)
                .GreaterThanOrEqualTo(minValue).WithMessage("Podany urzytkownik jest za niski");
        }
    }
}
