## Estrutura do Projeto

### Program
* **Ponto de entrada da aplicação:**
    * Configura os serviços, como o contexto do Entity Framework.
    * Constrói o aplicativo web.

### Models
* **UserResponse.cs:** Representa a resposta da API para a entidade "usuário".
* **UserRequest.cs:** Representa a requisição da API para criar ou atualizar um usuário.
* **User.cs:** Representa a entidade "usuário" no domínio da aplicação.

### Data
* **EntityFrameworkContext.cs:** Define o contexto do Entity Framework para a aplicação e estabelece a conexão com o banco de dados.

### Controller
* **UserController.cs:** Define as rotas da API para gerenciar usuários e utiliza os métodos `HttpGet`, `HttpPost`, `HttpPut` e `HttpDelete` para criar endpoints HTTP.

### Criando o Contexto e Modelos com Database First

Para gerar o DbContext e os modelos a partir de um banco de dados existente usando Database First, utilize o comando `dotnet ef dbcontext scaffold`:

1. **Executar o comando Scaffold:**
    ```bash
    dotnet ef dbcontext scaffold "YOUR_CONCTRION_STRING" Npgsql.EntityFrameworkCore.PostgreSQL -o Models
    ```

    Este comando cria as classes de modelo e o contexto do banco de dados com base no esquema existente do banco de dados.

### Criando Migrations e Aplicando Atualizações

Para criar uma nova migração e aplicar atualizações no banco de dados:

1. **Criar uma nova migração:**
    ```bash
    dotnet ef migrations add NomeDaMigracao
    ```

2. **Aplicar as migrações ao banco de dados:**
    ```bash
    dotnet ef database update
    ```

Esses comandos devem ser executados no diretório onde o projeto .NET está localizado.

### Controller

#### UserController.cs

O `UserController` é responsável por gerenciar as operações relacionadas aos usuários. Ele define as rotas da API e manipula as requisições HTTP para as operações CRUD.

* **Métodos:**
    * **GetAll()**
        * **Descrição:** Obtém todos os usuários do banco de dados e os retorna como uma lista de `UserResponse`.
        * **Tipo de Requisição:** `GET`
        * **Exemplo:**
            ```csharp
            [HttpGet]
            public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll()
            {
                var users = await _context.Users.ToListAsync();
                var userResponses = users.Select(u => new UserResponse(u.Id, u.Name, u.Email)).ToList();
                return Ok(userResponses);
            }
            ```

    * **GetById(Guid id)**
        * **Descrição:** Obtém um usuário específico pelo ID e o retorna como um `UserResponse`.
        * **Tipo de Requisição:** `GET`
        * **Exemplo:**
            ```csharp
            [HttpGet("{id}")]
            public async Task<ActionResult<UserResponse>> GetById(Guid id)
            {
                var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
                if (user == null)
                    return NotFound("User not found");

                return Ok(new UserResponse(user.Id, user.Name, user.Email));
            }
            ```

    * **Save(UserRequest request)**
        * **Descrição:** Cria um novo usuário com base na requisição e salva no banco de dados.
        * **Tipo de Requisição:** `POST`
        * **Exemplo:**
            ```csharp
            [HttpPost]
            public async Task<ActionResult<UserResponse>> Save(UserRequest request)
            {
                if (await _context.Users.AnyAsync(x => x.Email == request.Email))
                    return Conflict("User already exists");

                var newUser = new User(request.Name, request.Email, request.Password);
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                return Ok(new UserResponse(newUser.Id, newUser.Name, newUser.Email));
            }
            ```

    * **Update(Guid id, UserRequest request)**
        * **Descrição:** Atualiza as informações de um usuário existente com base no ID e na requisição.
        * **Tipo de Requisição:** `PUT`
        * **Exemplo:**
            ```csharp
            [HttpPut("{id}")]
            public async Task<ActionResult<UserResponse>> Update(Guid id, UserRequest request)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    return NotFound("User not found");

                user.Name = request.Name;
                user.ChangeEmail(request.Email);
                user.ChangePassword(request.Password);

                await _context.SaveChangesAsync();

                return Ok(new UserResponse(user.Id, user.Name, user.Email));
            }
            ```

    * **Delete(Guid id)**
        * **Descrição:** Exclui um usuário específico do banco de dados com base no ID.
        * **Tipo de Requisição:** `DELETE`
        * **Exemplo:**
            ```csharp
            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(Guid id)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    return NotFound("User not found");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            ```
