using System;
using System.Web.Mvc;
using System.Web.Routing;
using AdminPanel2.Controllers;
using Entity.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace AcceptanceTests.Login
{
    [Binding]
    public class LoginSteps
    {
        IE browser;

        [Given(@"Que eu estou na tela de login do sistema")]
        public void GivenQueEuEstouNaTelaDeLoginDoSistema()
        {
            browser = new IE("http://localhost:19293/");
        }
        
        [Given(@"Eu preenchi o formulario com os seguintes dados:")]
        public void GivenEuPreenchiOFormularioComOsSeguintesDados(TechTalk.SpecFlow.Table table)
        {
            browser.TextField(Find.ByName("username")).TypeText("1");
            browser.TextField(Find.ByName("password")).TypeText("1");
        }

        [Given(@"Eu preenchi o formulario com os seguintes dados incorretos:")]
        public void GivenEuPreenchiOFormularioComOsSeguintesDadosIncorretos(TechTalk.SpecFlow.Table table)
        {
            browser.TextField(Find.ByName("username")).TypeText("99");
            browser.TextField(Find.ByName("password")).TypeText("99");
        }


        [When(@"Eu apertar o botão de logar")]
        public void WhenEuApertarOBotaoDeLogar()
        {
            browser.Button(Find.ById("formCadastroUsuario_submit")).Click();
            browser.WaitForComplete(10);
        }
        
        [Then(@"Será apresentada uma mensagem de sucesso")]
        public void ThenSeraApresentadaUmaMensagemDeSucesso()
        {
            Assert.IsTrue(browser.Url == "http://localhost:19293/Home");
        }
        
        [Then(@"Será apresentada uma mensagem de erro dizendo que o login ou senha estão errados")]
        public void ThenSeraApresentadaUmaMensagemDeErroDizendoQueOLoginOuSenhaEstaoErrados()
        {
            Assert.IsTrue(browser.Url == "http://localhost:19293/");
        }
    }
}
