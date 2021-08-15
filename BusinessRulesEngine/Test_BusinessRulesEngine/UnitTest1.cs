using System.Net.Http.Headers;
using BusinessRulesEngine;
using BusinessRulesEngine.BusinessRules;
using BusinessRulesEngine.Models;
using NUnit.Framework;
using FluentAssertions;

namespace Test_BusinessRulesEngine
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Should_ApplyPackagingSlipRule_ForPhysicalProduct()
        {
            //Arrange
            var paymentHandler = new PaymentHandler();
            var payment = new Payment()
            {
                Product = new Product() {IsPhysical = true, ProductType = "CD"}
            };

            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);

            //Assert 
            processedPayment.BusinessRules.Should().ContainSingle(rule => rule.GetType() == typeof(GeneratePackagingSlipBusinessRule));
        }
    }
}