using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Portal.Application;
using Portal.Application.Models;
using Portal.Application.RepositoriesInterfaces;
using Portal.Controllers;
using Portal.Models;
using System;

namespace Tests
{
    public class ClientControllerTests
    {
        private Mock<IClientQueryRepository> _mockClient;
        private Mock<IClientService> _mockClientS;
        private Mock<ICarnetService> _mockCarnetS;
        private Mock<IUserContext> _mockUserID;

        private ClientController GetMocks()
        {
            _mockClient = new Mock<IClientQueryRepository>();
            _mockClientS = new Mock<IClientService>();
            _mockCarnetS = new Mock<ICarnetService>();
            _mockUserID = new Mock<IUserContext>();
            var controller = new ClientController(_mockClient.Object, _mockUserID.Object, _mockClientS.Object, _mockCarnetS.Object);
            return controller;
        }

        [Test]
        public void GetDetailsValid()
        {
            //Arrange
            Guid fakeId = new Guid("046857a4-2f02-4876-a4ac-1dbe043868b7");
            ClientController controller = GetMocks();
            _mockClientS.Setup(x => x.GetDetails(fakeId)).Returns(() => GetMyDetails());
            _mockUserID.Setup(x => x.CurrentUserId).Returns(fakeId);

            //Act
            var result = controller.GetDetails();

            //Assert
            _mockClientS.Verify(p => p.GetDetails(fakeId), Times.Once);
            Assert.IsInstanceOf<ActionResult<Client>>(result);
            Assert.IsTrue(result != null);
        }

        private Client GetMyDetails()
        {
            Client output = new Client
            {
                PersonID = new Guid("046857a4-2f02-4876-a4ac-1dbe043868b7"),
                Name = "test",
                Surname = "test",
                Address = "test",
                ContactData = "test",
                Email = "test@fg.dfg",
                Login = "test",
                Password = "test",
                Role = "Client",
                ProfileComplete = true,
                Sex = Portal.Application.Helpers.Sex.Female
            };
            return output;
        }

        [Test]
        public void AddCarnetValid()
        {
            //Arrange
            ClientDto c = new ClientDto
            {
                Name = "test",
                Surname = "test",
                Address = "test",
                Sex = Portal.Application.Helpers.Sex.Female,
                ContactData = "test",
                Email = "test@fg.dfg"
            };
            Guid fakeId = new Guid("046857a4-2f02-4876-a4ac-1dbe043868b7");

            ClientController controller = GetMocks();
            _mockClientS.Setup(x => x.UpdateProfile(fakeId, c));
            _mockUserID.Setup(x => x.CurrentUserId).Returns(fakeId);

            //Act
            var result = controller.AddCarnet(c);

            //Assert
            _mockClientS.Verify(p => p.UpdateProfile(fakeId, c), Times.Once);
            Assert.IsTrue(result != null);
        }
    }
}