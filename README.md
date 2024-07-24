# Documentação Completa do Projeto

1. Descrição do Projeto

O projeto consiste em duas partes principais:

    * API de Fornecedores: Uma API ASP.NET Core que gerencia fornecedores e produtos.
    * Aplicação Windows Forms: Uma aplicação Windows Forms para gerenciar fornecedores e produtos utilizando a API.

2. Estrutura do Projeto

    * FornecedoresApi: Projeto ASP.NET Core para a API.
    * FornecedoresApp: Projeto Windows Forms para a interface gráfica.

3. API de Fornecedores
3.1 Estrutura do Projeto

    - Controllers
        * FornecedoresController: Controlador para gerenciar fornecedores.
        * ProdutosController: Controlador para gerenciar produtos.
        * ProdutoFornecedoresController: Controlador para gerenciar a relação entre produtos e fornecedores.

    - Models
        * Fornecedor: Modelo de fornecedor contendo campos como Id, Nome, Cnpj, Endereco, Telefone.
        * Produto: Modelo de produto contendo campos como Id, Descricao, UnidadeDeMedida.
        * ProdutoFornecedor: Modelo de relacionamento entre produtos e fornecedores contendo campos como ProdutoFornecedorId, ProdutoId, FornecedorId, ValorCompra.
        * ViaCepResposta: Modelo para resposta da API ViaCEP, contendo informações de endereço.

    - Data
        * AppDbContext: Contexto do banco de dados que gerencia a conexão e manipulação das tabelas Fornecedores, Produtos, ProdutoFornecedores.

    - Program
        * Configuração da aplicação, incluindo a configuração do banco de dados e registro dos serviços.

3.2 Endpoints da API

- Fornecedores

    GET /api/Fornecedores: Retorna todos os fornecedores.
    GET /api/Fornecedores/{id}: Retorna um fornecedor por ID.
    POST /api/Fornecedores: Adiciona um novo fornecedor.
    PUT /api/Fornecedores/{id}: Atualiza um fornecedor existente.
    DELETE /api/Fornecedores/{id}: Remove um fornecedor por ID.

- Produtos

    GET /api/Produtos: Retorna todos os produtos.
    GET /api/Produtos/{id}: Retorna um produto por ID.
    POST /api/Produtos: Adiciona um novo produto.
    PUT /api/Produtos/{id}: Atualiza um produto existente.
    DELETE /api/Produtos/{id}: Remove um produto por ID.

- ProdutoFornecedores

    GET /api/ProdutoFornecedores: Retorna todas as relações entre produtos e fornecedores.
    GET /api/ProdutoFornecedores/{id}: Retorna uma relação por ID.
    POST /api/ProdutoFornecedores: Adiciona uma nova relação.
    PUT /api/ProdutoFornecedores/{id}: Atualiza uma relação existente.
    DELETE /api/ProdutoFornecedores/{id}: Remove uma relação por ID.

3.3 Configuração do Banco de Dados

O arquivo appsettings.json contém a configuração da string de conexão do banco de dados.

4. Aplicação Windows Forms
4.1 Estrutura do Projeto

    - Forms
        * MainForm: Formulário principal que permite a navegação entre os formulários de gerenciamento.
        * FornecedorForm: Formulário para gerenciar fornecedores, com funcionalidades para adicionar, atualizar e remover fornecedores.
        * ProdutoForm: Formulário para gerenciar produtos, com funcionalidades para adicionar, atualizar e remover produtos.
        * ProdutoFornecedoresForm: Formulário para gerenciar a relação entre produtos e fornecedores, com funcionalidades para adicionar, atualizar e remover relações.

4.2 Funcionalidades dos Formulários

    - MainForm
        * Botões para acessar os formulários de fornecedores, produtos e relacionamento entre produtos e fornecedores.

    - FornecedorForm
        * Campos para inserir Nome, Cnpj, Endereco e Telefone do fornecedor.
        * Botões para salvar, atualizar e remover fornecedores.

    - ProdutoForm
        * Campos para inserir Descricao e UnidadeDeMedida do produto.
        * Botões para salvar, atualizar e remover produtos.

    - ProdutoFornecedoresForm
        * Campos para inserir ProdutoId, FornecedorId e ValorCompra para criar uma relação.
        * Botões para salvar, atualizar e remover relações.

4.3 Integração com a API

    * Cada formulário utiliza o HttpClient para realizar chamadas HTTP para a API, enviando e recebendo dados no formato JSON.
    * Métodos assíncronos são usados para garantir que a interface do usuário permaneça responsiva durante as operações de rede.


# Comandos para Criação do projeto
- dotnet new webapi -n FornecedoresApi --framework net6.0
- dotnet new winforms -n FornecedoresApp

# Pacotes Instalados na WEB API
- dotnet add package FluentValidation.AspNetCore --version 6.0.424
- dotnet add package System.Net.Http.Json --version 6.0.424
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.424
- dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.424
- dotnet add package DotNetEnv

# Pacotes Instalados no Windows Forms
- dotnet add package System.Net.Http
