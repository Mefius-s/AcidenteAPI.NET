# AcidenteAPI.NET

Este é um projeto em .NET que implementa uma API para gerenciar informações sobre acidentes. Ele utiliza boas práticas de desenvolvimento como **injeção de dependência**, **mapeamento de objetos** com AutoMapper, e **testes unitários** com Xunit e Moq.

## Funcionalidades

- **Cadastro de Acidentes**: Permite o registro de novos acidentes, incluindo informações como data, hora, gravidade e localização.
- **Atualização e Exclusão**: Suporte para atualização e remoção de registros de acidentes.
- **Recuperação de Acidentes**: Consultas para buscar acidentes por diferentes critérios.
- **Tratamento de Erros**: Implementação de exceções personalizadas como `NotFoundException` para lidar com recursos não encontrados.
- **Testes Unitários**: Testes automatizados usando Xunit e Moq para garantir a qualidade do código.

## Tecnologias Utilizadas

- **.NET 6.0**: Framework de desenvolvimento.
- **Entity Framework Core**: Para o acesso ao banco de dados.
- **AutoMapper**: Mapeamento de objetos entre DTOs e entidades de domínio.
- **Xunit**: Para a escrita de testes unitários.
- **Moq**: Para mockar dependências nos testes unitários.
- **GitHub Actions**: Utilizado para integração contínua (CI), compilação e execução de testes.

## Como Rodar o Projeto

### Pré-requisitos

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Banco de dados configurado (Exemplo: Oracle SQL Developer).
- Ferramenta de linha de comando Git.

### Passos

1. Clone o repositório:
    ```bash
    git clone https://github.com/SEU_USUARIO/AcidenteAPI.NET.git
    cd AcidenteAPI.NET
    ```

2. Restaure as dependências:
    ```bash
    dotnet restore
    ```

3. Compile o projeto:
    ```bash
    dotnet build --configuration Release
    ```

4. Execute a aplicação:
    ```bash
    dotnet run
    ```

5. Acesse a API:
    A API estará rodando em `http://localhost:5000` (ou `https://localhost:5001` se estiver usando HTTPS).

### Testes Unitários

Para rodar os testes unitários, utilize o comando:

```bash
dotnet test
