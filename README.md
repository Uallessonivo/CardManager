# Card Manager

Card Manager � uma aplica��o para gerenciar cart�es.

# Tecnologias

* .NET 7
* PostgreSQL
* Entity Framework Core
* FluentValidation
* AutoMapper


# Arquitetura

A aplica��o segue a arquitetura limpa (Clean Architecture), onde temos as seguintes camadas:

* Presentation
* Application
* Domain
* Infrastructure

# Como executar a aplica��o

* Clone o reposit�rio: git clone https://github.com/seu-usuario/card-manager.git
* Execute o comando docker-compose up para subir o banco de dados PostgreSQL.
* Com o banco de dados rodando, execute a aplica��o com o comando dotnet 
```
run --project src/Presentation/CardManager.Api/CardManager.Api.csproj.
```
* Acesse a url https://localhost:5001/swagger para visualizar a documenta��o da API.


# Autor

Card Manager foi desenvolvido por Uallesson Nunes.