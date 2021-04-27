using Common.Constraints;
using FluentValidation;
using Models.Trends;

namespace BusinessLogic.Validators
{
    public class TrendCreateDtoValidator : AbstractValidator<TrendCreateDto>
    {
        public TrendCreateDtoValidator()
        {
            RuleFor(trendDto => trendDto.Name)
                .NotEmpty()
                .NotNull()
                .Length(TrendConstraints.NameMinLength, TrendConstraints.NameMaxLength);
        }
    }
}
