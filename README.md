# 👥 Cadastro de Pessoas - Avaliação Técnica

Este repositório contém a solução para o desafio técnico de desenvolvimento de uma aplicação CRUD de Cadastro de Pessoas, utilizando a stack solicitada. O foco principal deste projeto é demonstrar organização de código, clareza na definição de responsabilidades e aplicação de regras de negócio.

## 🛠️ Tecnologias Utilizadas

* **Back-end:** C# com ASP.NET MVC 5 (.NET Framework)
* **Acesso a Dados:** Entity Framework 6 (Code-First)
* **Banco de Dados:** Microsoft SQL Server
* **Front-end:** HTML, JavaScript, Knockout.js e Tailwind CSS (via CDN)
* **Integrações:** API ViaCEP (para preenchimento automático de endereço)

## 🏗️ Arquitetura e Decisões Técnicas

Para atender aos requisitos do teste priorizando boas práticas e separação de responsabilidades, as seguintes decisões foram tomadas:

* **Comunicação Assíncrona:** O `PessoaController` foi desenhado para atuar de forma semelhante a uma API, respondendo às requisições do Knockout.js exclusivamente via JSON (AJAX). Isso evita o recarregamento completo da página (Postback) e melhora a experiência do usuário.
* **Entity Framework Code-First:** A modelagem do banco de dados foi feita através de classes de domínio (`Pessoa` e `Endereco`). As regras de negócio (e-mail único, campos obrigatórios, tamanho de strings) foram aplicadas via *Data Annotations* e validadas tanto no banco quanto no Controller.
* **Regras de Negócio:**
    * A validação de "maior de 18 anos" foi encapsulada em um método próprio dentro da entidade `Pessoa`.
    * A garantia de e-mail único é validada no back-end antes da persistência, e o banco possui um `[Index(IsUnique = true)]` para garantir integridade.
* **Integração ViaCEP:** O consumo da API do ViaCEP foi implementado inteiramente no front-end para reduzir o acoplamento do back-end com serviços externos, garantindo que o formulário seja preenchido instantaneamente assim que o CEP é digitado.

### 🤖 Nota de Transparência
Toda a arquitetura, regras de negócio, modelagem de dados, acesso a banco (Entity Framework) e a lógica do Controller no **Back-end foram desenvolvidas por mim**. 
Para otimizar o tempo e entregar uma interface mais polida e responsiva, utilizei o auxílio de Inteligência Artificial para gerar o layout em **Tailwind CSS** e estruturar as amarrações visuais do **Knockout.js** no Front-end.

---

## 🚀 Como executar o projeto localmente

Siga os passos abaixo para rodar a aplicação na sua máquina.

### 1. Pré-requisitos
* Visual Studio 2022 (com a carga de trabalho "Desenvolvimento para a Web e ASP.NET" e suporte ao .NET Framework legados instalados).
* Microsoft SQL Server (2022 ou Express).

### 2. Configurando o Banco de Dados
Antes de rodar a aplicação, é necessário apontar a string de conexão para o seu servidor SQL Server local.

1. Abra o arquivo `Web.config` localizado na raiz do projeto.
2. Localize a tag `<connectionStrings>`.
3. Altere o parâmetro `Data Source` para o nome da sua instância local do SQL Server (ex: `localhost`, `.\SQLEXPRESS`, ou o nome da sua máquina). O nome do banco configurado por padrão é `CadastroPessoas`.

```xml
<connectionStrings>
  <add name="ConexaoCadastro" 
       connectionString="Data Source=SEU_SERVIDOR_AQUI;Initial Catalog=CadastroPessoas;Integrated Security=True;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
