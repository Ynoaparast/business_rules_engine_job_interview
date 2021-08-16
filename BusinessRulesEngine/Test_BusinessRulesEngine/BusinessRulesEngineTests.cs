using System.Linq;
using System.Net.Http.Headers;
using BusinessRulesEngine;
using BusinessRulesEngine.BusinessRules;
using BusinessRulesEngine.Models;
using NUnit.Framework;
using FluentAssertions;

namespace Test_BusinessRulesEngine
{
    public class BusinessRulesEngineTests
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
            var product = new Product() {IsPhysical = true};
            var order = new Order();
            order.Products.Add(product);
            var payment = new Payment()
            {
                Order = order
            };

            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);

            //Assert 
            processedPayment.BusinessRules.Should().ContainSingle(rule => rule.GetType() == typeof(GeneratePackagingSlipBusinessRule));
        }
     
        [Test]
        public void Should_ApplyPackagingSlipRuleTwice_ForBook()
        {
            //Arrange
            var paymentHandler = new PaymentHandler();
            var product = new Book() {IsPhysical = true};
            var order = new Order();
            order.Products.Add(product);

            var payment = new Payment()
            {
                Order = order
            };

            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);
            
            //Assert
            processedPayment.BusinessRules.Count(rule => rule.GetType() == typeof(GeneratePackagingSlipBusinessRule)).Should().Be(2, "because two packaging slips are to be created");
        }
      
        [Test]
        public void Should_AddressPackagingSlipsToCustomerAndRoyaltyDepartment_ForBook()
        {
            //Arrange
            var paymentHandler = new PaymentHandler();
            var product = new Book() { IsPhysical = true };
            var order = new Order();
            order.Products.Add(product);

            var payment = new Payment()
            {
                Order = order
            };
            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);
            processedPayment.ExecuteBusinessRules();
            var returnedRules = processedPayment.BusinessRules.FindAll(rule => rule.GetType() == typeof(GeneratePackagingSlipBusinessRule)).Cast<GeneratePackagingSlipBusinessRule>().ToList();

            //Assert

            payment.Order.PackagingSlips.Should().Contain(slip => slip.SlipDestination == "Customer").And
                .Contain(slip => slip.SlipDestination == "Royalty");
        }

        
        [Test]
        public void Should_ApplyAgentCommissionRule_ForPhysicalProductOrBook()
        {
            //Arrange
            var paymentHandler = new PaymentHandler();
            var product = new Book() { IsPhysical = true };
            var order = new Order();
            order.Products.Add(product);

            var payment = new Payment()
            {
                Order = order
            };

            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);
            //Assert
            processedPayment.BusinessRules.Should().ContainSingle(rule => rule.GetType() == typeof(GenerateAgentCommissionBusinessRule), "because a physical product or book should generate a commission to an agent");
        }
      
        [Test]
        public void Should_ApplyMembershipActivationRule_ForMembershipProduct()
        {
            //Arrange
            var paymentHandler = new PaymentHandler();
            var product = new Membership() {IsPhysical = false, IsActive = false};
            var order = new Order();
            order.Products.Add(product);

            var payment = new Payment()
            {
                Order = order
            };
           
            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);

            //Assert
            processedPayment.BusinessRules.Should()
                .ContainSingle(rule => rule.GetType() == typeof(ActivateMembershipBusinessRule), "because a payment for a membership should apply the membership activation rule");
        }

       
        [Test]
        public void Should_ActivateMembership_ForMembershipProduct()
        {
            //Arrange
            var paymentHandler = new PaymentHandler();
            var product = new Membership() { IsPhysical = false, IsActive = false };
            var customer = new Customer
            {
                Email = "email@mailer.com",
                Firstname = "Foo",
                Lastname = "Bar"
            };

            var order = new Order() {Customer = customer};
            order.Products.Add(product);

            var payment = new Payment()
            {
                Order = order
            };
            
            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);
            processedPayment.ExecuteBusinessRules();
            var processedProduct = (Membership) processedPayment.Order.Products.Find(prod => prod == product);

            //Assert
            processedProduct.IsActive.Should().Be(true, "because a payment for a membership should activate the membership");
        }
       
        [Test]
        public void Should_UpgradeMembership_ForMembershipUpgradeProduct()
        {

           //Arrange
            var paymentHandler = new PaymentHandler();
            var membership = new Membership();
            var payment = new Payment()
            {
                Product = new MembershipUpgrade() { Membership = membership},
                Customer = new Customer
                {
                    Email = "email@mailer.com",
                    Firstname = "Foo",
                    Lastname = "Bar"
                }
            };

            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);
            processedPayment.ExecuteBusinessRules();
            var processedProduct = (MembershipUpgrade) processedPayment.Product;

            //Assert
            processedProduct.Membership.IsUpgraded.Should().Be(true, "because a payment for a membership upgrade should upgrade the membership");

        }

        [Test]
        public void Should_ApplySendEmailOnActivationOrUpgrade_ForMembershipActions()
        {
            //Arrange
            var paymentHandler = new PaymentHandler();
            var payment = new Payment()
            {
                Product = new Membership()
            };

            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);
            
            //Assert
            processedPayment.BusinessRules.Should().ContainSingle(rule => rule.GetType() == typeof(SendEmailForMembershipBusinessRule),
                "because creating or upgrading a membership should send the owner an email");
        }


        [Test]
        public void Should_SetCustomerDetailsForSendingEmail_ForMembershipActions()
        {
            //Arrange
            var paymentHandler = new PaymentHandler();
            var payment = new Payment()
            {
                Product = new Membership(),
                Customer = new Customer()
                {
                    Email = "email@mailer.com",
                    Firstname = "Foo",
                    Lastname = "Bar"
                }
            };

            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);
            processedPayment.ExecuteBusinessRules();
            var businessRule = (SendEmailForMembershipBusinessRule) processedPayment.BusinessRules.Find(rule => rule.GetType() == typeof(SendEmailForMembershipBusinessRule));
            
            //Assert
            businessRule.Email.Should().BeEquivalentTo(new Email()
                {To = payment.Customer.Email, Content = $"Dear {payment.Customer.Firstname} {payment.Customer.Lastname}, your membership has been activated"});

        }

        [Test]
        public void Should_ApplyFreeVideoRule_ForLearningToSkiVideo()
        {
            //Arrange
            var paymentHandler = new PaymentHandler();
            var payment = new Payment()
            {
                Product = new Video()
                {
                    Title = "Learning to Ski",
                    IsPhysical = true
                }
               
            };

            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);

            //Assert
            processedPayment.BusinessRules.Should().ContainSingle(rule => rule.GetType() == typeof(AddFreeFirstAidVideoBusinessRule), "because buying 'the Learning to Ski' qualifies the customer for a free 'First aid' video");

        }

        [Test]
        public void Should_AddFirstVideoToPackagingSlip_ForLearningToSkiVideo()
        {
            //Arrange
            var paymentHandler = new PaymentHandler();
            var payment = new Payment()
            {
                Product = new Video()
                {
                    Title = "Learning to Ski",
                    IsPhysical = true
                }

            };

            //Act
            var processedPayment = paymentHandler.ApplyBusinessRules(payment);
            processedPayment.ExecuteBusinessRules();
            var businessRule = (AddFreeFirstAidVideoBusinessRule) processedPayment.BusinessRules.Find(rule => rule.GetType() == typeof(AddFreeFirstAidVideoBusinessRule));
            var product = businessRule.PackagingSlip.ProductsToPack.Find(product => product.GetType() == typeof(Video));
            //Assert
            product.Should().BeEquivalentTo(new Video() {Title = "First Aid", IsPhysical = true});

        } */
    }
}