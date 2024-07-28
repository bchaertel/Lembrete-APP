# Lembrete App

Este é um aplicativo de lembretes construído usando HTML, CSS e JavaScript, ASP.NET Core e MongoDB.
O aplicativo permite que os usuários adicionem, visualizem, agrupem por data e excluam lembretes.

## Estrutura do Projeto

A estrutura do projeto está organizada da seguinte forma:

- **Controllers**: Contém os controladores responsáveis por gerenciar as requisições HTTP.
- **Interfaces**: Define as interfaces para os serviços utilizados.
- **Models**: Contém as classes de modelo que representam os dados da aplicação.
- **Services**: Implementa a lógica de negócios e interação com o banco de dados.
- **wwwroot**: Contém os arquivos estáticos, como HTML, CSS e JavaScript.
- **LembreteApp.Tests**: Contém os testes unitários automatizados do back-end.

# Instalação

## Pré-requisitos:

- Node.js
- npm
- .NET Core SDK
- MongoDB

## Instale as dependências do npm:

utilize este comando na raiz do projeto: npm install

# Utilização:

## Restaurando depedências, Compilando e iniciando o servidor:

- Utilize os seguintes comandos na raíz do projeto:

* dotnet restore
* npm run build-css
* dotnet build
* dotnet run

## Visualização do aplicativo:

- Abra o arquivo index.html ou acesse a url http://localhost:5246 para visualizar a interface do usuário.

## Criar Lembrete:

- No container "Lembrete App", preencha o nome do lembrete e a data que você deseja.
- Precione o botão Criar para salvar o lembrete na "Lista de Lembretes".

## Deletar Lembrete:

- No container "Lista de Lembretes", precione o "x" para deletar o lembrete.

# Funcionamento:

## Rotas REST

- GET

  - /api/lembretes
    -Recupera a lista de todos os lembretes.
    -Resposta de Sucesso: 200 OK.

  - /api/lembretes/{id}
    - Recupera um lembrete específico pelo ID.
    - Parâmetros: "id"(string): O ID do lembrete a ser recuperado.
    - Resposta de Sucesso: 200 OK.

- POST

  - /api/lembretes
    - Cria um novo lembrete.
    - Body da Requisição: {"nome": "string", "data": "date"}.
    - Resposta de Sucesso: 201 Created.

- DELETE

  - /api/lembretes/{id} - Exclui um lembrete pelo ID.
    - Parâmetros: "id" (string): O ID do lembrete a ser excluído.
    - Resposta de Sucesso: 204 No Content.

# Back-end

## Ferramentas Utilizadas:

- ASP.NET Core: Utilizado para construir a API RESTful que gerencia os lembretes.
- MongoDB: Banco de dados NoSQL utilizado para armazenar os lembretes.
- MongoDB.Driver: Driver do MongoDB para .NET, utilizado para interagir com o banco de dados.

- O backend é desenvolvido em ASP.NET Core e interage com um banco de dados MongoDB Local.Ele fornece uma API RESTful para gerenciar lembretes, permitindo operações de Criação, leitura, exclusão.
  - Controllers: Gerenciam as requisições HTTP e chamam os serviços apropriados para manipular os dados.
  - Services: Contêm a lógica de negócios e interagem com o MongoDB para executar operações de Criação, Leitura e Exclusão.
  - Models: Representam os dados no sistema, como a classe Lembrete.

# Front-end

## Ferramentas Utilizadas:

- HTML: Utilizado para estruturar a página web.
- CSS: Utilizado para estilizar a página web.
- JavaScript: Utilizado para adicionar interatividade à página web.

- O front-end é construído utilizando HTML, CSS e JavaScript, fornecendo uma interface de usuário simples para a criação e gerenciamentos dos lembretes.
  - wwwroot/index.html: Contém a estrutura básica do aplicativo e referencia os arquivos CSS e JavaScript.
  - wwwroot/js/app.js: Implementa a lógica do Front-end, incluindo a manipulação do formulário, exibição de lembretes e interação com a API do back-end.
  - wwwroot/css/styles.css: Define o estilo do aplicativo, incluindo o layout e aparência dos componentes.

# Testes

- O projeto inclui testes de unidade para validar as funcionalidades tanto do front-end, quanto do back-end.

## Front-end:

- Utiliza Jest e @testing-library para garantir que os componentes de interface funcionem corretamente.

## Back-end:

- Utiliza xUnit e Moq para garantir que os controladores e serviços do backend funcionem corretamente.
- Os Arquivos dos testes estão na pasta "LembreteApp.Tests".
