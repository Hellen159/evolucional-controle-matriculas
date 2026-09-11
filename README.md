# Evolucional Controle de Matrículas

API REST para controle de matrículas escolares, desenvolvida como teste prático para a posição de Desenvolvedor(a) .NET Pleno Back-End.

A aplicação permite o gerenciamento de alunos, consulta de turmas, realização de matrículas e geração de relatório de alunos por turma.

## Tecnologias

- .NET Framework 4.8
- ASP.NET Web API
- C#
- SQL Server LocalDB
- Dapper
- SQL escrito manualmente
- Swagger / OpenAPI
- Unity para injeção de dependência

## Arquitetura

O projeto foi organizado em camadas, separando responsabilidades entre API, regras de aplicação, domínio e acesso a dados.

```text
EvolucionalControleMatriculas/
├── src/
│   ├── EvolucionalControleMatriculas.Api/
│   ├── EvolucionalControleMatriculas.Application/
│   ├── EvolucionalControleMatriculas.Domain/
│   └── EvolucionalControleMatriculas.Infrastructure/
├── tests/
│   └── EvolucionalControleMatriculas.Tests/
├── sql/
│   └── script-banco.sql
└── README.md
```

### Responsabilidade das camadas

**Api**
- Controllers
- Configuração do Web API
- Swagger
- Configuração da injeção de dependência

**Application**
- Casos de uso da aplicação
- Orquestração das regras
- DTOs
- Interfaces dos repositories e services

**Domain**
- Entidades
- Regras de negócio próprias do domínio
- Exceções de domínio

**Infrastructure**
- Acesso ao banco de dados
- Dapper
- SQL
- Implementação dos repositories
- Controle de transações

## Pré-requisitos

- Visual Studio 2022
- .NET Framework 4.8
- SQL Server LocalDB

O projeto utiliza a instância:

```text
(localdb)\MSSQLLocalDB
```

## Banco de dados

O script de criação do banco está localizado em:

```text
sql/script-banco.sql
```

O script cria o banco `TesteEscola`, as tabelas `Aluno`, `Turma` e `Matricula`, e insere dados iniciais para testes.

### Estrutura das tabelas

**Aluno:** dados dos alunos e campo `Ativo`, utilizado para exclusão lógica.

**Turma:** dados da turma e controle de `VagasTotal` e `VagasDisponiveis`.

**Matricula:** relacionamento entre aluno e turma, incluindo a data da matrícula.

## Connection String

No `Web.config`:

```xml
<connectionStrings>
  <add name="TesteEscola"
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TesteEscola;Integrated Security=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

No projeto de testes, a mesma configuração é utilizada no `App.config`.

## Executando o projeto

1. Abra `EvolucionalControleMatriculas.sln`.
2. Certifique-se de que o SQL Server LocalDB está disponível.
3. Execute `sql/script-banco.sql`.
4. Confirme que o banco `TesteEscola` foi criado.
5. Compile a solução.
6. Execute `EvolucionalControleMatriculas.Api` utilizando o IIS Express.

A documentação Swagger estará disponível em:

```text
https://localhost:{porta}/swagger
```

A rota raiz da aplicação redireciona para o Swagger.

# Endpoints

## Alunos

### Listar alunos

```http
GET /api/alunos
```

Permite paginação e filtro opcional por nome.

Parâmetros:

```text
nome
pagina
tamanhoPagina
```

Exemplo:

```http
GET /api/alunos?nome=Ana&pagina=1&tamanhoPagina=10
```

A resposta informa os itens retornados e o total de registros.

### Obter aluno

```http
GET /api/alunos/{id}
```

Retorna `200 OK` quando encontrado e `404 Not Found` quando não encontrado.

### Criar aluno

```http
POST /api/alunos
```

Exemplo:

```json
{
  "nome": "João Silva",
  "email": "joao@email.com",
  "dataNascimento": "2005-08-20"
}
```

Retorna `201 Created`.

### Atualizar aluno

```http
PUT /api/alunos/{id}
```

Exemplo:

```json
{
  "nome": "João Silva Atualizado",
  "email": "joao.atualizado@email.com",
  "dataNascimento": "2005-08-20"
}
```

Retorna `204 No Content` quando atualizado e `404 Not Found` quando o aluno não existe.

### Desativar aluno

```http
DELETE /api/alunos/{id}
```

A exclusão é lógica. O registro não é removido do banco; o campo `Ativo` é alterado para `0`.

Retorna `204 No Content` quando desativado e `404 Not Found` quando o aluno não existe.

## Turmas

### Listar turmas

```http
GET /api/turmas
```

Retorna as turmas e a quantidade de vagas disponíveis.

Exemplo:

```json
[
  {
    "id": 1,
    "nome": "3A - Ensino Medio",
    "vagasDisponiveis": 28
  },
  {
    "id": 2,
    "nome": "3B - Ensino Medio",
    "vagasDisponiveis": 30
  }
]
```

## Matrículas

### Realizar matrícula

```http
POST /api/matriculas
```

Exemplo:

```json
{
  "alunoId": 1,
  "turmaId": 2
}
```

Antes da matrícula são verificadas as seguintes regras:

- O aluno precisa existir.
- A turma precisa existir.
- O aluno precisa estar ativo.
- O aluno não pode estar matriculado anteriormente na mesma turma.
- A turma precisa possuir vaga disponível.

Quando a matrícula é realizada:

1. O registro é inserido na tabela `Matricula`.
2. `VagasDisponiveis` da turma é decrementado.

As duas operações são realizadas dentro de uma única transação de banco de dados. Caso uma das operações falhe, a transação é revertida.

Possíveis respostas:

```text
201 Created
404 Not Found
409 Conflict
```

## Relatório

### Alunos por turma

```http
GET /api/relatorios/alunos-por-turma
```

Retorna:

- Nome da turma
- Quantidade de alunos matriculados
- Vagas restantes

A consulta é executada diretamente no banco utilizando SQL com `JOIN`, `GROUP BY` e `COUNT`. O agrupamento não é realizado em memória no C#.

Exemplo:

```json
[
  {
    "nomeTurma": "3A - Ensino Medio",
    "quantidadeAlunos": 2,
    "vagasRestantes": 28
  },
  {
    "nomeTurma": "3B - Ensino Medio",
    "quantidadeAlunos": 0,
    "vagasRestantes": 30
  }
]
```

# Status HTTP

| Status | Utilização |
|---|---|
| `200 OK` | Consulta realizada com sucesso |
| `201 Created` | Recurso criado com sucesso |
| `204 No Content` | Operação realizada sem conteúdo para retorno |
| `400 Bad Request` | Requisição inválida |
| `404 Not Found` | Registro não encontrado |
| `409 Conflict` | Regra de negócio impede a operação |
| `500 Internal Server Error` | Erro inesperado da aplicação |

# Dapper e acesso a dados

O acesso ao banco é realizado utilizando Dapper.

Não foi utilizado Entity Framework ou outro ORM.

As consultas SQL são escritas manualmente nos repositories.

A paginação e o agrupamento dos relatórios são realizados diretamente no SQL Server.

# Injeção de dependência

A aplicação utiliza Unity para realizar a injeção de dependência.

As implementações são registradas na camada API, que funciona como composition root da aplicação.

Exemplo:

```text
IAlunoApplication
        ↓
AlunoApplication

IAlunoRepository
        ↓
AlunoRepository

IMatriculaRepository
        ↓
MatriculaRepository
```

Dessa forma, os controllers dependem de abstrações e não das implementações concretas.

# Decisões importantes

## Exclusão lógica

O endpoint `DELETE /api/alunos/{id}` não remove fisicamente o registro.

O aluno é desativado através do campo:

```text
Ativo = 0
```

Isso preserva o histórico dos dados relacionados ao aluno.

## Transação da matrícula

A inclusão da matrícula e a redução das vagas precisam ser atômicas.

Por isso, as duas operações são executadas dentro da mesma transação.

Além disso, a atualização da turma verifica no próprio SQL se ainda existe uma vaga disponível:

```sql
UPDATE Turma
SET VagasDisponiveis = VagasDisponiveis - 1
WHERE Id = @TurmaId
  AND VagasDisponiveis > 0
```

Essa condição evita que o número de vagas fique negativo em situações de concorrência.

## Validação de matrícula duplicada

Antes de criar uma nova matrícula, a camada de aplicação consulta o repository para verificar se o aluno já está matriculado na turma.

Caso exista uma matrícula, a operação é interrompida e retorna `409 Conflict`.

# Swagger

A API possui documentação Swagger para facilitar a visualização e execução dos endpoints.

Após iniciar a aplicação:

```text
https://localhost:{porta}/swagger
```

A interface permite visualizar os endpoints e executar as requisições diretamente pelo navegador.

# Testes

Os testes automatizados estão localizados em:

```text
tests/EvolucionalControleMatriculas.Tests
```

A estrutura permite testar principalmente as regras de negócio e a camada de aplicação de forma isolada.

# Possíveis melhorias

Caso o projeto fosse evoluído além do escopo deste teste, algumas melhorias poderiam ser adicionadas:

- Cache das turmas utilizando Redis.
- Testes de integração para repositories.
- Proteção adicional contra concorrência nas matrículas, utilizando restrições e/ou atualizações condicionais no SQL.
- Interface web para consulta dos alunos.
- Melhorias na validação dos DTOs.
- Observabilidade e logging estruturado.

Os demais itens não fazem parte da implementação obrigatória do desafio.

# Como testar rapidamente

Com a API em execução, a forma mais simples de testar todos os endpoints é acessar:

```text
/swagger
```

A partir da interface Swagger é possível executar:

```text
GET    /api/alunos
GET    /api/alunos/{id}
POST   /api/alunos
PUT    /api/alunos/{id}
DELETE /api/alunos/{id}

GET    /api/turmas

POST   /api/matriculas

GET    /api/relatorios/alunos-por-turma
```

## Histórico de desenvolvimento

O projeto foi desenvolvido em etapas, com commits realizados ao longo da implementação para manter o histórico das decisões e evolução da aplicação.
