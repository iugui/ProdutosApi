# ProdutosApi

Esse é um projeto para estudo sobre API utilizando controladores no ASPNET.Core.

* Framework web = ASPNET.Core
* Banco de dados = SQL
* ORM = Entity Framework Core

## Definição do projeto

| API              | Descrição                                   | Request Body | Response Body     |
| ---------------- | --------------------------------------------- | ------------ | ----------------- |
| GET /produtos    | retorna todos os produtos                     |              | lista de produtos |
| GET /produtos/id | retorna um produto específico a partir do id |              | produto           |
| POST /produtos   | Adiciona um novo produto                      | produto      | produto           |
| PUT /produtos    | Atualiza um produto existente                 | produto      | produto           |
| DELETE /produtos | Deleta um produto existente                   |      | produto           |

# Banco de dados

Descrição das tabelas do banco de dados

| Tabela | Relacionamentos | Nome do relacionamento | Descrição |
| ------ | --------------- | ---------------------- | --------- |
| Produtos |  |  | Descreve os produtos |

Descrição dos atributos das tabelas do banco de dados

| Coluna | Tipo | Comprimento | Restrição | Valor padrão | Extra | Descrição |
| ------ | ---- | ----------- | --------- | ------------ | ----- | --------- |
| Id | int |  |  |  | Primary Key  | Identificação única de cada produto |
| Nome | nvarchar(255) |  |  | NOT NULL |   | Nome de cada produto |
| Descricao | nvarchar(MAX) |  |  |  |   | Descrição de cada produto |