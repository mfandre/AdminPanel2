Feature: EditarCliente
	Testa a edição de um Cliente

Scenario: Edição de Cliente
	Given Que eu estou no popup de edição de Cliente
	And Eu preenchi o formulário com os dados:
		| Field		| Value		|
		| Name		| SpecFlow	|
		| Localidade| SpecFlow	|
	When Eu clicar em Salvar
	Then Será apresentada uma mensagem de sucesso e o Cliente estará atualizado no banco

