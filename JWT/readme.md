# JWT Authentication Project

This project demonstrates a simple implementation of JWT (JSON Web Token) authentication in a .NET application.

## Project Structure

The project consists of the following main components:

1. `Configuration.cs`: Contains the private key for JWT signing.
2. `Program.cs`: The main entry point of the application.
3. `TokenService.cs`: Handles token generation.
4. `User.cs`: Defines the User model.

## Configuration

The JWT private key is stored in the `Configuration` class:

```csharp
namespace JWT
{
    public class Configuration
    {
        public static string PrivateKey { get; set; } = "93587149-6e87-45f7-b1bd-6faae9a0e24c";
    }
}
```

## Setup

The `Program.cs` file sets up the application and defines a single endpoint:

```csharp
using JWT.Models;
using JWT.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

var app = builder.Build();

app.MapGet("/", (TokenService service) => service.GenerateToken(user: new User(
    Guid.NewGuid(),
    "ERICKKRAMER@HOTMAIL.COM",
    "ERICKKRAMER123@",
    ["student", "premium"]
)));

app.Run();
```

## Token Service

The `TokenService` class is responsible for generating JWTs:

```csharp
public class TokenService
{
    public string GenerateToken(User user)
    {
        // Token generation logic
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        // Claims generation logic
    }
}
```

## User Model

The `User` model is defined as a record:

```csharp
namespace JWT.Models
{
    public record User(
        Guid Id,
        string Email,
        string Password,
        string[] Roles
    );
}
```

## Usage

To generate a token, make a GET request to the root endpoint ("/"). The application will create a new user and return a JWT.

## Security Note

Ensure that you replace the hardcoded private key in the `Configuration` class with a secure, environment-specific key in a production environment.

## Dependencies

This project uses the following NuGet packages:

- Microsoft.AspNetCore.Authentication.JwtBearer

Make sure to install these packages before running the application.

## Running the Application

1. Clone the repository
2. Navigate to the project directory
3. Run `dotnet restore` to install dependencies
4. Run `dotnet run` to start the application

#

# Projeto de Autenticação JWT

Este projeto demonstra uma implementação simples de autenticação JWT (JSON Web Token) em uma aplicação .NET.

## Estrutura do Projeto

O projeto consiste nos seguintes componentes principais:

1. `Configuration.cs`: Contém a chave privada para assinatura JWT.
2. `Program.cs`: O ponto de entrada principal da aplicação.
3. `TokenService.cs`: Lida com a geração de tokens.
4. `User.cs`: Define o modelo de Usuário.

## Configuração

A chave privada JWT é armazenada na classe `Configuration`:

```csharp
namespace JWT
{
    public class Configuration
    {
        public static string PrivateKey { get; set; } = "93587149-6e87-45f7-b1bd-6faae9a0e24c";
    }
}
```

## Configuração Inicial

O arquivo `Program.cs` configura a aplicação e define um único endpoint:

```csharp
using JWT.Models;
using JWT.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

var app = builder.Build();

app.MapGet("/", (TokenService service) => service.GenerateToken(user: new User(
    Guid.NewGuid(),
    "ERICKKRAMER@HOTMAIL.COM",
    "ERICKKRAMER123@",
    ["student", "premium"]
)));

app.Run();
```

## Serviço de Token

A classe `TokenService` é responsável por gerar JWTs:

```csharp
public class TokenService
{
    public string GenerateToken(User user)
    {
        // Lógica de geração de token
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        // Lógica de geração de claims
    }
}
```

## Modelo de Usuário

O modelo `User` é definido como um record:

```csharp
namespace JWT.Models
{
    public record User(
        Guid Id,
        string Email,
        string Password,
        string[] Roles
    );
}
```

## Uso

Para gerar um token, faça uma requisição GET para o endpoint raiz ("/"). A aplicação criará um novo usuário e retornará um JWT.

## Nota de Segurança

Certifique-se de substituir a chave privada codificada na classe `Configuration` por uma chave segura e específica do ambiente em um ambiente de produção.

## Dependências

Este projeto utiliza os seguintes pacotes NuGet:

- Microsoft.AspNetCore.Authentication.JwtBearer

Certifique-se de instalar esses pacotes antes de executar a aplicação.

## Executando a Aplicação

1. Clone o repositório
2. Navegue até o diretório do projeto
3. Execute `dotnet restore` para instalar as dependências
4. Execute `dotnet run` para iniciar a aplicação

A aplicação será iniciada e você poderá acessar o endpoint de geração de token em `http://localhost:5000/` (ou na porta especificada pelo seu ambiente).