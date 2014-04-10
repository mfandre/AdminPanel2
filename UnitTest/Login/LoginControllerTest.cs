using System;
using Entity.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTest.Utils.Test;

namespace UnitTest.Login
{
    [TestClass]
    public class LoginControllerTest
    {
        private string EncodeSHA1(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }

        [TestMethod]
        public void TextValidLoginDoLogin()
        {
            User validUser = new User() { Username = "fandre", Password = EncodeSHA1("1") };

            //validando model
            ModelStateTestController controller = new ModelStateTestController();
            var result = controller.TestTryValidateModel(validUser);
            Assert.IsTrue(result, "model invalido:" + result);

            //mock do CheckLogin
            var mock = new Mock<DataAccess.Usuario.LoginDAO>();
            mock.Setup(m => m.CheckLogin(validUser))
                .Returns(new User() { Username = "fandre", Password = EncodeSHA1("1") });

            DataAccess.Usuario.LoginDAO loginDAO = mock.Object;
            Assert.IsNotNull(loginDAO.CheckLogin(validUser), "Login inválido");
        }

        [TestMethod]
        public void TextInvalidLoginDoLogin()
        {
            User invalidUser = new User() { Username = "fandre", Password = EncodeSHA1("2") };

            //validando model
            ModelStateTestController controller = new ModelStateTestController();
            var result = controller.TestTryValidateModel(invalidUser);
            Assert.IsTrue(result, "model invalido:" + result);

            //mock do CheckLogin
            var mock = new Mock<DataAccess.Usuario.LoginDAO>();
            mock.Setup(m => m.CheckLogin(invalidUser))
                .Returns((User)null);

            DataAccess.Usuario.LoginDAO loginDAO = mock.Object;
            Assert.IsNull(loginDAO.CheckLogin(invalidUser), "Login inválido");
        }
    }
}
