## Estrutura do Projeto

### EntityFrameworkCore.Data
* **AppDbContext.cs:**
    * Define o contexto do Entity Framework para a aplicação.
    * Estabelece a conexão com o banco de dados.
    * Define os DbSet para as entidades que serão mapeadas para as tabelas do banco de dados.
    * **Exemplo:**
        ```csharp
        public DbSet<User> Users { get; set; }
        ```

### EntityFrameworkCore.Mapping
* **UserMapping.cs:**
    * Mapeia a classe `User` para a tabela "user" no banco de dados.
    * Define as propriedades da tabela, seus tipos e relacionamentos.
    * **Exemplo:**
        ```csharp
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        ```

### EntityFrameworkCore.Models
* **User.cs:**
    * Representa a entidade "usuário" no domínio da aplicação.
    * Define as propriedades que correspondem aos campos da tabela "user".
    * **Exemplo:**
        ```csharp
        public class User
        {
            public Guid Id { get; init; }
            public string Name { get; set; }
            // ... outras propriedades
        }
        ```

### EntityFrameworkCore.Routes
* **UserRoutes.cs:**
    * Define as rotas da API para gerenciar usuários.
    * Utiliza o middleware `MapGet`, `MapPost` e `MapDelete` para criar endpoints HTTP.
    * **Exemplo:**
        ```csharp
        app.MapGet("/users", async (AppDbContext context) =>
        {
            return await context.Users.ToListAsync();
        });
        ```

### Program.cs
* **Ponto de entrada da aplicação:**
    * Configura os serviços, como o contexto do Entity Framework.
    * Constrói o aplicativo web.
    * **Exemplo:**
        ```csharp
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        ```

### Criando Migrations e Aplicando Atualizações

Para criar uma nova migração e aplicar atualizações no banco de dados, siga os passos abaixo:

1. **Criar uma nova migração:**
    * Utilize o comando abaixo para gerar uma nova migração com base nas alterações feitas nos modelos:
    ```bash
    dotnet ef migrations add NomeDaMigracao
    ```

2. **Aplicar as migrações ao banco de dados:**
    * Após criar a migração, utilize o comando a seguir para aplicar as alterações ao banco de dados:
    ```bash
    dotnet ef database update
    ```

Esses comandos devem ser executados no diretório onde o projeto .NET está localizado, geralmente na raiz do projeto.

***

### EntityFrameworkCore.Migrations

* **Herda de `Migration`:** Indica que esta classe representa uma migração do banco de dados.
* **Método `Up`:**
  * **Cria a tabela "user":** Utiliza o `migrationBuilder.CreateTable` para criar uma nova tabela com as seguintes colunas:
    * `Id`: Chave primária do tipo GUID.
    * `name`: Nome do usuário (varchar(50), obrigatório).
    * `email`: Email do usuário (varchar(100), obrigatório).
    * `password`: Senha do usuário (varchar(50), obrigatório). **Importante:** É altamente recomendável criptografar as senhas antes de armazená-las no banco de dados.
  * **Define a chave primária:** Especifica a coluna `Id` como a chave primária da tabela.
* **Método `Down`:**
  * **Remove a tabela "user":** Caso seja necessário reverter a migração, este método remove a tabela criada no método `Up`.

**Exemplo de código:**

```csharp
public partial class init : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "user",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: false),
                // ... outras colunas
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_user", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "user");
    }
}

```

