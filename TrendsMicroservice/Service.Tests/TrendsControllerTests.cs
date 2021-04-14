using BusinessLogic.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Controllers;

namespace Service.Tests
{
    [TestClass]
    public class TrendsControllerTests
    {
        private readonly Mock<ITrendBusinessLogic> trendBusinessLogicMock;

        private readonly TrendController systemUnderTest;

        public TrendsControllerTests()
        {
            trendBusinessLogicMock = new Mock<ITrendBusinessLogic>();
            systemUnderTest = new TrendController(trendBusinessLogicMock.Object);
        }
    }
}
