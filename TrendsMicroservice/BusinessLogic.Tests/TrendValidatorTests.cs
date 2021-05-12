using BusinessLogic.Validators;
using Common.Constraints;
using FluentValidation.TestHelper;
using Xunit;

namespace BusinessLogic.Tests
{
    public class TrendValidatorTests
    {
        private readonly TrendCreateDtoValidator _validator = new TrendCreateDtoValidator();

        [Fact]
        public void GivenATrendName_ShouldHaveNonEmptyValidation()
        {
            _validator.ShouldHaveValidationErrorFor(trendDto => trendDto.Name, string.Empty);
        }

        [Fact]
        public void GivenATrendName_Validate_ShouldHaveLengthValidation()
        {
            _validator.ShouldHaveValidationErrorFor(trendDto => trendDto.Name, new string('A', TrendConstraints.NameMinLength - 1));
            _validator.ShouldHaveValidationErrorFor(trendDto => trendDto.Name, new string('A', TrendConstraints.NameMaxLength + 1));
            _validator.ShouldNotHaveValidationErrorFor(trendDto => trendDto.Name, new string('A', TrendConstraints.NameMinLength));
            _validator.ShouldNotHaveValidationErrorFor(trendDto => trendDto.Name, new string('A', TrendConstraints.NameMaxLength));
        }
    }
}
