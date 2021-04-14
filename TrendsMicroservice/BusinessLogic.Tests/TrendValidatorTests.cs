using BusinessLogic.Validators;
using Common.Constraints;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.Tests
{
    [TestClass]
    public class TrendValidatorTests
    {
        private readonly TrendCreateDtoValidator _validator = new TrendCreateDtoValidator();

        [TestMethod]
        public void GivenATrendName_ShouldHaveNonEmptyValidation()
        {
            _validator.ShouldHaveValidationErrorFor(trendDto => trendDto.Name, string.Empty);
        }

        [TestMethod]
        public void GivenATrendName_Validate_ShouldHaveLengthValidation()
        {
            _validator.ShouldHaveValidationErrorFor(trendDto => trendDto.Name, new string('A', TrendConstraints.NameMinLength - 1));
            _validator.ShouldHaveValidationErrorFor(trendDto => trendDto.Name, new string('A', TrendConstraints.NameMaxLength + 1));
            _validator.ShouldNotHaveValidationErrorFor(trendDto => trendDto.Name, new string('A', TrendConstraints.NameMinLength));
            _validator.ShouldNotHaveValidationErrorFor(trendDto => trendDto.Name, new string('A', TrendConstraints.NameMaxLength));
        }
    }
}
