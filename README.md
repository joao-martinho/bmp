# BMP: Aplicação ASP.NET Core 6.0

Este projeto é uma aplicação desenvolvida em ASP.NET Core 6.0, com testes unitários usando xUnit e Moq.

Este documento explica, passo a passo, como executar a aplicação e rodar os testes.

---

## Requisitos 

- .NET SDK 6.0

## Restaurando as dependências

Antes de executar o projeto, é necessário baixar todas as dependências.

Na pasta raiz do projeto (`bmp`), execute

````bash
dotnet restore
````

## Executando a aplicação

Acesse o diretório bmp/src e execute

````bash
dotnet run
````

## Executando os testes

Acesse o diretório src/tests e execute

````bash
dotnet test
````
