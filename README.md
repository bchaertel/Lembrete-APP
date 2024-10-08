# Lembrete App

- Este é um aplicativo de lembretes construído usando HTML(semântico), CSS/SCSS com Pré/Pós processadores sem bibliotecas de componentes, JavaScript, ASP.NET Core e MongoDB.
- O aplicativo permite que os usuários adicionem, visualizem, agrupem por data e excluam lembretes.

# Decisões do Projeto:

## Arquitetura do Projeto:

- A arquitetura do projeto foi organizada como MVC, para facilitar a manutenção e a escalabilidade.
- As pastas foram divididas de maneira lógica (Controllers, Models, Services, wwwroot, Tests) para separar responsabilidades e melhorar a clareza do código.

## Banco de Dados:

- Utilização do MongoDB como banco de dados devido a simplicidade da estrutura de dados utilizando "chaves-valor". Foi decidido pelo MongoDB local e não o Atlas pois seria necessário registrar o IP do usuário na lista de permissões de acesso, o que deixaria inviável sem um deploy em um site de hospedagem.

## Testes:

- Jest e @testing-library para Testes Front-end:
- Jest foi escolhido como framework de testes para o front-end devido à sua simplicidade e integração com outras bibliotecas
- @testing-library foi utilizado para testes de interface, garantindo que os componentes funcionem como esperado.

- xUnit e Moq para Testes Back-end:
- Para o back-end, xUnit foi escolhido como framework de testes devido a sua popularidade e robustez. Moq foi utilizado para facilitar o mocking de dependências durante os testes.

## Estrutura:

- Rotas RESTful:
- As rotas da API foram projetadas seguindo os princípios RESTful, facilitando a compreensão e a integração com outras aplicações e serviços.

- Uso de SCSS para Estilização:
  utilização de SCSS para a estilização do front-end, aproveitando suas funcionalidades avançadas como variáveis, aninhamento e mixins, pmelohrando a manutenção e a organização do CSS.

# Organização:

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
- .NET Core SDK SDK 8.0.303
- MongoDB
- ASP.NET Core 8.0.7
- MongoDB.Driver 2.28.0:

# Utilização:

## instalação do .NET SDK 8.0.303

- acesse o link: https://dotnet.microsoft.com/pt-br/download/dotnet/8.0
- faça o download do instalador do SDK 8.0.303 de acordo com o seu sistema operacional e especificações
- Instale.

## Instalação do MongoDB:

- Acesse o link: https://www.mongodb.com/try/download/community
- Selecione a plataform que você está utilizando.
- package: msi
- Download.
- Instale.

### Se quiser ter uma GUI para o MongoDb:

- acesse: https://www.mongodb.com/try/download/compass
- Selecione a plataforma que você está utilizando.
- pachage: exe.
- Download.
- Instale.
- Execute o MongoDBCompass
- Em New Connection preencha com: mongodb://localhost:27017.
- clique em connect.
- Pronto, você tem visualização do nosso Banco de Dados.

## Restaurando depedências, Compilando e iniciando o servidor:

### Utilize os seguintes comandos na raíz do projeto:

- dotnet restore
- npm install
- npm run build-css
- dotnet build
- dotnet run

## Visualização do aplicativo:

- Abra o arquivo index.html ou acesse a url http://localhost:5246 para visualizar a interface do usuário.

## Criar Lembrete:

- No container "Lembrete App", preencha o nome do lembrete e a data que você deseja.
- Precione o botão Criar para salvar o lembrete na "Lista de Lembretes".

## Deletar Lembrete:

- No container "Lista de Lembretes", precione o "x" para deletar o lembrete.

# Funcionamento:

## Rotas REST

### GET

- /api/lembretes
  -Recupera a lista de todos os lembretes.
  -Resposta de Sucesso: 200 OK.

- /api/lembretes/{id}
  - Recupera um lembrete específico pelo ID.
  - Parâmetros: "id"(string): O ID do lembrete a ser recuperado.
  - Resposta de Sucesso: 200 OK.

### POST

- /api/lembretes
  - Cria um novo lembrete.
  - Body da Requisição: {"nome": "string", "data": "date"}.
  - Resposta de Sucesso: 201 Created.

### DELETE

- /api/lembretes/{id} - Exclui um lembrete pelo ID.
  - Parâmetros: "id" (string): O ID do lembrete a ser excluído.
  - Resposta de Sucesso: 204 No Content.

# Back-end

## Ferramentas Utilizadas:

- ASP.NET Core 8.0.7: Utilizado para construir a API RESTful que gerencia os lembretes.
- MongoDB: Banco de dados NoSQL utilizado para armazenar os lembretes.
- MongoDB.Driver 2.28.0: Driver do MongoDB para .NET, utilizado para interagir com o banco de dados.

- O back-end é desenvolvido em ASP.NET Core e interage com um banco de dados MongoDB Local.Ele fornece uma API RESTful para gerenciar lembretes, permitindo operações de Criação, leitura, exclusão.
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

# Testes - Execução:

## Front-end:

- Estão definidos no arquivo app.test.js
- Na raiz do projeto, digite o comando: npm test
- Serão realizados os seguintes testes:
  - Valida as entradas do formulário
  - Agrupa os lembretes por data
  - Deleta um lembrete
  - Carrega lembretes ao iniciar a página
  - Exibe erro ao falhar no carregamento dos lembretes

## Back-end:

- Estão definidos no arquivo LembreteControllerTests.cs no caminho LembreteApp.Tests
- Na raiz do projeto, digite o comando dotnet test
- Serão realizados os seguintes testes:

  - GetLembretes_DeveRetornarListaDeLembretes:
    Verifica se o método Get retorna uma lista de lembretes corretamente.

  - PostLembrete_DeveRetornarBadRequestQuandoNomeNaoPreenchidoOuDataInvalida:
    Testa se o método Post retorna um BadRequest quando o nome não é preenchido ou a data é inválida.

  - PostLembrete_DeveRetornarCreatedAtRouteQuandoLembreteValido:
    Verifica se o método Post retorna CreatedAtRoute quando um lembrete válido é criado.

  - DeleteLembrete_DeveRetornarNotFoundOuNoContentQuandoLembreteNaoEncontradoOuEncontrado:
    Testa se o método Delete retorna NotFound quando o lembrete não é encontrado, ou NoContent quando o lembrete é encontrado e deletado.
