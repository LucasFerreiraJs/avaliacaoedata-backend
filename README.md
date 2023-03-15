<h1 style="text-align: center; font-weight: bold;">Avaliação Técnica</h1>


<h1 style="margin-bottom: 30px; margin-top: 30px; text-align: center; font-weight: bold;">Cadastro de Usuários🧑‍💼</h1>


## Sobre o Projeto
Backend para consultas e registros no banco de dados (CRUD).
Frondend disponível nesse repositório - [Frontend](https://github.com/thasuka/avaliacaoedata-frontend)

### 🛠 Tecnologias
As seguintes ferramentas foram usadas na construção do projeto:

- [Nuxt](https://nuxtjs.org/) - (vue)
- [Aspnet](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)

### 🎲 Rodando o projeto

Configure a string de conexão no arquivo appsettings.json na raiz do projeto com os valores userId e password
```
  "ConnectionStrings": {
    "WebApiDatabase": "Server=localhost,1433;Database=LucasFerreira;User ID=;Password=;Encrypt=False"
  },
```

Instale as dependências do projeto e crie a tabela através das migrations pelo console de gerenciador de pacotes

```
  dotnet build

  Add-Migration NomeDaMigration

  update-database
```
