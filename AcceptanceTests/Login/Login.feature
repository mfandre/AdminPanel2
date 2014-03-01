Feature: Login
	Testa o login de um usuario no sistema

Scenario: Login de um cliente com sucesso
	Given Que eu estou na tela de login do sistema
	And Eu preenchi o formulario com os seguintes dados:
		| Field		| Value		|
		| Login		| 1			|
		| Pass		| 1			|
	When Eu apertar o botão de logar
	Then Será apresentada uma mensagem de sucesso

Scenario: Login de um cliente com erro
	Given Que eu estou na tela de login do sistema
	And Eu preenchi o formulario com os seguintes dados incorretos:
		| Field		| Value		|
		| Login		| 99		|
		| Pass		| 99		|
	When Eu apertar o botão de logar
	Then Será apresentada uma mensagem de erro dizendo que o login ou senha estão errados