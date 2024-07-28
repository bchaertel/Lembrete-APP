Lembrete App
Este é um aplicativo de lembretes construído usando HTML, CSS e JavaScript, com um backend em ASP.NET Core e MongoDB. O aplicativo permite que os usuários adicionem, visualizem, agrupem por data e excluam lembretes.

Estrutura do Projeto
A estrutura do projeto está organizada da seguinte forma:

Controllers: Contém os controladores responsáveis por gerenciar as requisições HTTP.
Interfaces: Define as interfaces para os serviços utilizados.
Models: Contém as classes de modelo que representam os dados da aplicação.
Services: Implementa a lógica de negócios e interação com o banco de dados.
wwwroot: Contém os arquivos estáticos, como HTML, CSS e JavaScript.
LembreteApp.Tests: Contém os testes de unidade para o backend.
Instalação
Pré-requisitos
Node.js
npm
.NET Core SDK
MongoDB
Configuração do Backend
Clone o repositório e navegue até o diretório do projeto:

sh
Copiar código
git clone <URL_DO_REPOSITORIO>
cd ProjetoMongoLocal
Configure a string de conexão para o MongoDB em appsettings.json:

json
Copiar código
{
"ConnectionStrings": {
"MongoDb": "mongodb://localhost:27017/LembreteDb"
}
}
Restaure as dependências do .NET:

sh
Copiar código
dotnet restore
Execute a aplicação:

sh
Copiar código
dotnet run
Configuração do Frontend
Navegue até o diretório do projeto:

sh
Copiar código
cd ProjetoMongoLocal
Instale as dependências do npm:

sh
Copiar código
npm install
Execute os testes para verificar se está tudo funcionando corretamente:

sh
Copiar código
npm test
Uso
Abra o arquivo index.html no navegador para visualizar a interface do usuário.
Utilize o formulário para adicionar lembretes, que serão agrupados por data.
Clique no "x" ao lado de um lembrete para deletá-lo.
Rotas REST
GET /api/lembretes
Recupera a lista de todos os lembretes.

Resposta de Sucesso: 200 OK
json
Copiar código
[
{
"id": "string",
"nome": "string",
"data": "date"
}
]
GET /api/lembretes/{id}
Recupera um lembrete específico pelo ID.

Parâmetros:
id (string): O ID do lembrete a ser recuperado.
Resposta de Sucesso: 200 OK
json
Copiar código
{
"id": "string",
"nome": "string",
"data": "date"
}
POST /api/lembretes
Cria um novo lembrete.

Corpo da Requisição:
json
Copiar código
{
"nome": "string",
"data": "date"
}
Resposta de Sucesso: 201 Created
json
Copiar código
{
"id": "string",
"nome": "string",
"data": "date"
}
PUT /api/lembretes/{id}
Atualiza um lembrete existente pelo ID.

Parâmetros:
id (string): O ID do lembrete a ser atualizado.
Corpo da Requisição:
json
Copiar código
{
"nome": "string",
"data": "date"
}
Resposta de Sucesso: 204 No Content
DELETE /api/lembretes/{id}
Exclui um lembrete pelo ID.

Parâmetros:
id (string): O ID do lembrete a ser excluído.
Resposta de Sucesso: 204 No Content
Funcionamento do Projeto
Backend
O backend é desenvolvido em ASP.NET Core e interage com um banco de dados MongoDB. Ele fornece uma API RESTful para gerenciar lembretes, permitindo operações de criação, leitura, atualização e exclusão (CRUD).

Controllers: Gerenciam as requisições HTTP e chamam os serviços apropriados para manipular os dados.
Services: Contêm a lógica de negócios e interagem com o MongoDB para executar operações de CRUD.
Models: Representam os dados no sistema, como a classe Lembrete.
Frontend
O frontend é construído usando HTML, CSS e JavaScript, fornecendo uma interface de usuário simples para gerenciar lembretes.

index.html: Contém a estrutura básica do aplicativo e referencia os arquivos CSS e JavaScript.
app.js: Implementa a lógica do frontend, incluindo a manipulação do formulário, exibição de lembretes e interação com a API backend.
styles.css: Define o estilo do aplicativo, incluindo o layout e aparência dos componentes.
Testes
O projeto inclui testes de unidade para validar a funcionalidade tanto do frontend quanto do backend.

Frontend: Utiliza Jest e @testing-library para garantir que os componentes de interface funcionem corretamente.
Backend: Inclui testes para verificar a lógica de negócios e as operações CRUD através dos controladores e serviços.
