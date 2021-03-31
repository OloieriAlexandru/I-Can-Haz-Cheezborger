using Common.Constraints;
using FluentValidation;
using Models;

namespace BusinessLogic.Validators
{
    public class TrendDtoValidator : AbstractValidator<TrendDto>
    {
        public TrendDtoValidator()
        {
            RuleFor(trendDto => trendDto.Name)
                .NotEmpty()
                .NotNull()
                .Length(TrendConstraints.NameMinLength, TrendConstraints.NameMaxLength);
        }
    }
}
