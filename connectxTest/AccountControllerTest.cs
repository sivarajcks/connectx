using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectxTest
{
    [TestFixture]
    public class AccountControllerTest
    {
        private AccountController _controller;
        private Mock<IWebHostEnvironment> _mockHostingEnvironment;

        [SetUp]
        public void Setup()
        {
            _mockHostingEnvironment = new Mock<IWebHostEnvironment>();
            _controller = new AccountController(_mockHostingEnvironment.Object);
        }

        [Test]
        public void Index_ReturnsViewResult()
        {
            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Register_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Register_Post_ValidModel_RedirectsToLogin()
        {
            // Arrange
            var model = new User
            {
                Email = "test@example.com",
                Password = "password"
            };

            // Act
            var result = _controller.Register(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ActionName);
            Assert.AreEqual("Account", result.ControllerName);
        }

        [Test]
        public void Register_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Email is required");
            var model = new User();

            // Act
            var result = _controller.Register(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(model, result.Model);
        }

        [Test]
        public void Login_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Login_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "password"
            };

            // Act
            var result = _controller.Login(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }

        [Test]
        public void Login_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Email is required");
            var model = new LoginViewModel();

            // Act
            var result = _controller.Login(model) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewName);
            Assert.AreEqual(model, result.Model);
        }

        [Test]
        public void Profile_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.Profile() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Profile_Post_ValidModelWithProfilePicture_RedirectsToProfile()
        {
            // Arrange
            var model = new UserProfileViewModel
            {
                Name = "John Doe",
                Email = "john@example.com",
                DOB = DateTime.Parse("1990-01-01"),
                Location = "New York"
            };

            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.FileName).Returns("test.jpg");
            formFileMock.Setup(f => f.CopyTo(It.IsAny<Stream>())).Verifiable();

            _mockHostingEnvironment.Setup(env => env.WebRootPath).Returns("wwwroot");

            // Act
            var result = _controller.Profile(model, formFileMock.Object) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Profile", result.ActionName);
            Assert.AreEqual(null, result.ControllerName);

            formFileMock.Verify(f => f.CopyTo(It.IsAny<Stream>()), Times.Once);
        }

        [Test]
        public void Profile_Post_ValidModelWithoutProfilePicture_RedirectsToProfile()
        {
            // Arrange
            var model = new UserProfileViewModel
            {
                Name = "John Doe",
                Email = "john@example.com",
                DOB = DateTime.Parse("1990-01-01"),
                Location = "New York"
            };

            // Act
            var result = _controller.Profile(model, null) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Profile", result.ActionName);
            Assert.AreEqual(null, result.ControllerName);
        }

        [Test]
        public void Profile_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Email is required");
            var model = new UserProfileViewModel();

            // Act
            var result = _controller.Profile(model, null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Profile", result.ViewName);
            Assert.AreEqual(model, result.Model);
        }
    }
}
