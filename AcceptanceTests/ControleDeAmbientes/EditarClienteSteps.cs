using System;
using AdminPanel2.Controllers.ControleDeAmbientes;
using TechTalk.SpecFlow;
using System.Web.Mvc;
using Entity.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;

namespace AcceptanceTests.ControleDeAmbientes
{
    [Binding]
    public class EditarClienteSteps
    {
        ClienteController controller;
        ActionResult result;
        Cliente model;

        [Given(@"Que eu estou no popup de edição de Cliente")]
        public void GivenQueEuEstouNoPopupDeEdicaoDeCliente()
        {
            controller = new ClienteController();
        }

        [Given(@"Eu preenchi o formulário com os dados:")]
        public void GivenEuPreenchiOFormularioComOsDados(Table table)
        {
            model = new Cliente();
            model.ClienteId = 3;
            model.DataReg = DateTime.Now;
            model.EstReg = "A";
            model.Localidade = "SpecFlow";
            model.Name = "SpecFlow";
        }

        [When(@"Eu clicar em Salvar")]
        public void WhenEuClicarEmSalvar()
        {
            result = controller.Edit(model);
        }
        
        [Then(@"Será apresentada uma mensagem de sucesso e o Cliente estará atualizado no banco")]
        public void ThenSeraApresentadaUmaMensagemDeSucessoEOClienteEstaraAtualizadoNoBanco()
        {
            var data = new RouteValueDictionary((result as JsonResult).Data);

            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(true,data["success"]);
        }
    }
}
