using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestAPI.Controllers;
using TestDAL.Core;

namespace TestAPITests
{
    [TestClass]
    public class ClassesControllerTests
    {
        [TestMethod]
        public void IndexTest()
        {
            // Arrange
            var mockUOW = new Mock<IUnitOfWork>();
            var mockMap = new Mock<IMapper>();
            var controller = new ClassesController(mockUOW.Object, mockMap.Object);
            
            // Act
            var result = (StatusCodeResult)controller.Index();
            

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        }
    }
}
