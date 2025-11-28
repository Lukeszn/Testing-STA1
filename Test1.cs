using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecordApp.ViewModels;
using RecordApp.Services;

namespace RecordApp.Tests
{

    [TestClass]
    public class LoginViewModelTests
    {
        [TestMethod]
        public void LoginShouldAuthenticate_WhenadminCredsAreValid()
        {
            //Arange 
            var mockLoginService = new Mock<ILoginService>();
            var mockSessionService = new Mock<ISessionService>();
            String expectedRole = "All_Privilages";
            mockLoginService
                .Setup(s => s.ValidateCredentials("admin", "admin", out expectedRole)).Returns(true);

            var vm = new LoginViewModel(mockLoginService.Object, mockSessionService.Object);
            {
                vm.Username = "admin";
                vm.Password = "admin";

            }

            //Act
            vm.LoginCommand.Execute(null);

            Assert.IsTrue(vm.IsLoginSuccessful, "Expected login to succeded");
            Assert.AreEqual(expectedRole, vm.UserRole, "Expected Role to match");
            mockSessionService.Verify(s => s.StartSession("admin", expectedRole), Times.Once);


        }
        [TestMethod]
        public void LoginShouldNotAuthenticate_WhenAdminUserNameInvalid()
        {
            //Arange 
            var mockLoginService = new Mock<ILoginService>();
            var mockSessionService = new Mock<ISessionService>();
            String expectedRole = "All_Privilages";
            mockLoginService
                .Setup(s => s.ValidateCredentials("admin", "admin", out expectedRole)).Returns(true);

            var vm = new LoginViewModel(mockLoginService.Object, mockSessionService.Object);
            {
                vm.Username = "123";
                vm.Password = "admin";

            }

            //Act
            vm.LoginCommand.Execute(null);

            Assert.IsFalse(vm.IsLoginSuccessful, "Expected login to Fail");
            Assert.AreNotEqual(expectedRole, vm.UserRole, "Expected Role to match");
            mockSessionService.Verify(s => s.StartSession("admin", expectedRole), Times.Never);
        }

        [TestMethod]
        public void LoginShouldNotAuthenticate_WhenAdminPasswordInvalid()
        {
            //Arange 
            var mockLoginService = new Mock<ILoginService>();
            var mockSessionService = new Mock<ISessionService>();
            String expectedRole = "All_Privilages";
            mockLoginService
                .Setup(s => s.ValidateCredentials("admin", "admin", out expectedRole)).Returns(true);

            var vm = new LoginViewModel(mockLoginService.Object, mockSessionService.Object);
            {
                vm.Username = "admin";
                vm.Password = "123";

            }

            //Act
            vm.LoginCommand.Execute(null);

            Assert.IsFalse(vm.IsLoginSuccessful, "Expected login to Fail");
            Assert.AreNotEqual(expectedRole, vm.UserRole, "Expected Role to match");
            mockSessionService.Verify(s => s.StartSession("admin", expectedRole), Times.Never);
        }
        [TestMethod]
        public void LoginShouldNotAuthenticate_WhenAdminBothInvalid()
        {
            //Arange 
            var mockLoginService = new Mock<ILoginService>();
            var mockSessionService = new Mock<ISessionService>();
            String expectedRole = "All_Privilages";
            mockLoginService
                .Setup(s => s.ValidateCredentials("admin", "admin", out expectedRole)).Returns(true);

            var vm = new LoginViewModel(mockLoginService.Object, mockSessionService.Object);
            {
                vm.Username = "123";
                vm.Password = "123";

            }

            //Act
            vm.LoginCommand.Execute(null);

            Assert.IsFalse(vm.IsLoginSuccessful, "Expected login to Fail");
            Assert.AreNotEqual(expectedRole, vm.UserRole, "Expected Role to match");
            mockSessionService.Verify(s => s.StartSession("admin", expectedRole), Times.Never);
        }
        [TestMethod]
        public void LoginShouldAuthenticate_WhenUserCredsValid()
        {
            //Arange 
            var mockLoginService = new Mock<ILoginService>();
            var mockSessionService = new Mock<ISessionService>();
            String expectedRole = "RESTRICTED_PRIVILEDGES";
            mockLoginService
                .Setup(s => s.ValidateCredentials("user", "user", out expectedRole)).Returns(true);

            var vm = new LoginViewModel(mockLoginService.Object, mockSessionService.Object);
            {
                vm.Username = "user";
                vm.Password = "user";

            }

            //Act
            vm.LoginCommand.Execute(null);

            Assert.IsTrue(vm.IsLoginSuccessful, "Expected login to be successful");
            Assert.AreEqual(expectedRole, vm.UserRole, "Expected Role to match");
            mockSessionService.Verify(s => s.StartSession("user", expectedRole), Times.Once);
        }
        [TestMethod]
        public void LoginShouldNotAuthenticate_WhenUserUserNameInValid()
        {
            //Arange 
            var mockLoginService = new Mock<ILoginService>();
            var mockSessionService = new Mock<ISessionService>();
            String expectedRole = "RESTRICTED_PRIVILEDGES";
            mockLoginService
                .Setup(s => s.ValidateCredentials("user", "user", out expectedRole)).Returns(true);

            var vm = new LoginViewModel(mockLoginService.Object, mockSessionService.Object);
            {
                vm.Username = "123";
                vm.Password = "user";

            }

            //Act
            vm.LoginCommand.Execute(null);

            Assert.IsFalse(vm.IsLoginSuccessful, "Expected login to Fail");
            Assert.AreNotEqual(expectedRole, vm.UserRole, "Expected Role to match");
            mockSessionService.Verify(s => s.StartSession("user", expectedRole), Times.Never);
        }
        [TestMethod]
        public void LoginShouldNotAuthenticate_WhenUserPasswordInValid()
        {
            //Arange 
            var mockLoginService = new Mock<ILoginService>();
            var mockSessionService = new Mock<ISessionService>();
            String expectedRole = "RESTRICTED_PRIVILEDGES";
            mockLoginService
                .Setup(s => s.ValidateCredentials("user", "user", out expectedRole)).Returns(true);

            var vm = new LoginViewModel(mockLoginService.Object, mockSessionService.Object);
            {
                vm.Username = "user";
                vm.Password = "admin";

            }

            //Act
            vm.LoginCommand.Execute(null);

            Assert.IsFalse(vm.IsLoginSuccessful, "Expected login to Fail");
            Assert.AreNotEqual(expectedRole, vm.UserRole, "Expected Role to match");
            mockSessionService.Verify(s => s.StartSession("user", expectedRole), Times.Never);
        }
        [TestMethod]
        
        public void LoginShouldNotAuthenticate_WhenUserBothInValid()
        {
            //Arange 
            var mockLoginService = new Mock<ILoginService>();
            var mockSessionService = new Mock<ISessionService>();
            String expectedRole = "RESTRICTED_PRIVILEDGES";
            mockLoginService
                .Setup(s => s.ValidateCredentials("user", "user", out expectedRole)).Returns(true);

            var vm = new LoginViewModel(mockLoginService.Object, mockSessionService.Object);
            {
                vm.Username = "123";
                vm.Password = "123";

            }

            //Act
            vm.LoginCommand.Execute(null);

            Assert.IsFalse(vm.IsLoginSuccessful, "Expected login to Fail");
            Assert.AreNotEqual(expectedRole, vm.UserRole, "Expected Role to match");
            mockSessionService.Verify(s => s.StartSession("user", expectedRole), Times.Never);
        }
    }
}
