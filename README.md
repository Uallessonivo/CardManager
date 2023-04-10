# Card Manager

Card Manager é uma aplicação para gerenciar cartões.

# Tecnologias

* .NET 7
* PostgreSQL
* Entity Framework Core
* FluentValidation
* AutoMapper


# Arquitetura

A aplicação segue a arquitetura limpa (Clean Architecture), onde temos as seguintes camadas:

* Presentation
* Application
* Domain
* Infrastructure

# Como executar a aplicação

* Clone o repositório: git clone https://github.com/seu-usuario/card-manager.git
* Execute o comando docker-compose up para subir o banco de dados PostgreSQL.
* Com o banco de dados rodando, execute a aplicação com o comando dotnet 
```
run --project src/Presentation/CardManager.Api/CardManager.Api.csproj.
```
* Acesse a url https://localhost:5001/swagger para visualizar a documentação da API.


# Autor

Card Manager foi desenvolvido por Uallesson Nunes.