using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Api.Controllers;
using TaxCalculator.Entities;
using TaxCalculator.Pl.Repositories;

namespace TaxCalc.Test.Controller
{
    class TaxCalcControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockRepo;
        private readonly TaxCalculatorController _controller;
        public TaxCalcControllerTests()
        {
            _mockRepo = new Mock<IUnitOfWork>();
            _controller = new TaxCalculatorController(_mockRepo.Object);
        }

        [Test]
        public async Task Check_Result_For_Validate_Method()
        {
            var municipalityName = "vilnius";
            _mockRepo.Setup(_ => _.CheckIfExists(It.IsAny<string>())).ReturnsAsync(true);
            var result = await _controller.Validate(municipalityName);
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task Check_Result_For_GetTax_Method()
        {
            Municipalities input = new Municipalities
            {
                MunicipalityName = "vilnius",
                Date = DateTime.Parse("2020.01.01"),
                Tax = 0.1,
                TaxRule = 2
            };
            _mockRepo.Setup(_ => _.CalculateTax(input)).ReturnsAsync(input);

            var result = await _controller.GetTax(input);
            Assert.AreEqual(input.Tax, result.Tax);
        }
    }
}
