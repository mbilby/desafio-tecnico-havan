## Descrição do Desafio Técnico Havan

Este repositório contém a resolução do **Teste Técnico para Desenvolvedor(a) C# (.NET) Júnior da Havan**, desenvolvido com o objetivo de demonstrar conhecimentos em **C#, .NET, Programação Orientada a Objetos, lógica de programação, regras de negócio e organização de software**.

O desafio é dividido em etapas que abordam desde conceitos fundamentais da plataforma .NET até a implementação prática de uma aplicação para gerenciamento de tarefas.

Entre os principais tópicos trabalhados estão:

* Conceitos de **Value Types e Reference Types**, uso de memória **Stack e Heap** e funcionamento do **Garbage Collector**;
* Diferenças entre **Interfaces e Classes Abstratas**;
* Funcionamento de operações assíncronas utilizando **async/await**;
* Resolução de problemas envolvendo **sequências numéricas**, **análise e frequência de caracteres** e **regras de cálculo financeiro**;
* Desenvolvimento de uma aplicação de **Gerenciamento de Tarefas (To-Do List)** utilizando C# e .NET;
* Aplicação de boas práticas de **Orientação a Objetos**, separação de responsabilidades, validações e tratamento de exceções;
* Utilização de persistência **In-Memory** para armazenamento dos dados;
* Documentação das decisões técnicas e defesa da solução implementada.

O projeto também busca demonstrar não apenas o funcionamento do código, mas o raciocínio utilizado durante seu desenvolvimento, incluindo decisões de arquitetura, desafios encontrados, soluções adotadas e possíveis melhorias futuras.

Como parte da proposta do desafio, o histórico de desenvolvimento é mantido por meio de **commits frequentes e descritivos**, permitindo acompanhar a evolução da solução ao longo da implementação.

# 📘 Parte 1: Questão Teórica

## 🧠 Questão 1: Conceitos de Orientação a Objetos e Runtime do .NET

---

### ❓ Pergunta 1

**Qual é a diferença fundamental entre Classes (Value Types vs Reference Types) em C# e como isso afeta o uso de memória (Stack vs Heap)?**

### ✅ Resposta

> **Value Types** armazenam seus dados diretamente e, quando atribuídos a outra variável, o valor é copiado.
>
> **Reference Types** armazenam uma referência para um objeto e, quando atribuídos, a referência é copiada, fazendo com que duas variáveis possam apontar para o mesmo objeto.
>
> Objetos de **Reference Types** normalmente são alocados no **Managed Heap** e gerenciados pelo **Garbage Collector**.
>
> Porém, é uma simplificação incorreta dizer que todo **Value Type** fica no **Stack**, pois um Value Type pode, por exemplo, estar armazenado dentro de um objeto no **Heap**.

---

### Value Types

São tipos como int, double, bool, char, decimal, struct e enum.

Quando você atribui um Value Type a outra variável, normalmente ocorre uma cópia do valor:

```csharp
int a = 10;
int b = a;

b = 20;

Console.WriteLine(a); // 10
Console.WriteLine(b); // 20

```
Imaginamos:

>Stack
>
>a → 10
>
>b → 20

a e b são independentes.

### Reference Types

São tipos como class, string, arrays, delegates e interfaces.

Com uma classe:

```csharp
class Pessoa
{
    public string Nome { get; set; }
}
```
Se fizermos:

```csharp
Pessoa p1 = new Pessoa();
p1.Nome = "Marcelo";

Pessoa p2 = p1;

p2.Nome = "João";

Console.WriteLine(p1.Nome); // João
```
Variáveis/referências             Heap
```text
p1 ────────────────┐
                   ├──────► Pessoa
p2 ────────────────┘         Nome = "João"
```
Quando fazemos:

```csharp
Pessoa p2 = p1;
```
não estamos copiando o objeto **Pessoa**. Estamos copiando a **referência**.

### **E Stack vs Heap?**

A explicação didática tradicional diz:

```text
Value Type      → Stack
Reference Type  → Heap
```
**não é tecnicamente correto em todos os casos.**

Por exemplo:

```csharp
class Pessoa
{
    public int Idade { get; set; }
}
```
Aqui Idade é um Value Type (int), mas faz parte de um objeto **Pessoa** que está no heap.

Conceitualmente:
```text
Stack / frame                  Managed Heap

pessoa ─────────────────────► Pessoa
                              ├── Idade = 45
                              └── Nome ─────────► "Marcelo"
```
Então não podemos simplesmente afirmar:
```text
int → Stack
```
O int pode estar **dentro de um objeto no Heap.**

De maneira simplificada, uma variável local como idade pode fazer parte da estrutura de execução do método, enquanto o objeto criado com:
```csharp
new Pessoa()
```
fica normalmente no Managed Heap.

### ❓ Pergunta 2

**Explique a diferença entre usar `Interface` e `Classe Abstrata`. Dê um exemplo prático de quando escolheria uma em detrimento da outra.**

### ✅ Resposta

>
>Em C#, tanto **interfaces** quanto **classes abstratas** ajudam a definir contratos e comportamentos comuns, mas são usadas em situações diferentes.
>
>Uma **interface** define principalmente **o que uma classe deve fazer**. Ela estabelece um contrato que pode ser implementado por classes completamente diferentes entre si.
>
---
```csharp
public interface IPagamento
{
    void Processar(decimal valor);
}
```

Diferentes classes podem implementar a interface:

```csharp
public class PagamentoPix : IPagamento
{
    public void Processar(decimal valor)
    {
        Console.WriteLine($"Pagamento PIX de {valor}");
    }
}

public class PagamentoCartao : IPagamento
{
    public void Processar(decimal valor)
    {
        Console.WriteLine($"Pagamento com cartão de {valor}");
    }
}
```
Aqui, `PagamentoPix` e `PagamentoCartao` podem ter implementações totalmente diferentes, mas ambos garantem que possuem o método `Processar`.

Já uma **classe abstrata** é mais indicada quando as classes possuem uma **base comum**, com estado e comportamento compartilhados.

```csharp
public abstract class Funcionario
{
    public string Nome { get; set; }

    public void RegistrarEntrada()
    {
        Console.WriteLine($"{Nome} registrou entrada.");
    }

    public abstract decimal CalcularSalario();
}
```
As classes filhas reutilizam parte da implementação:

```csharp
public class FuncionarioCLT : Funcionario
{
    public decimal SalarioMensal { get; set; }

    public override decimal CalcularSalario()
    {
        return SalarioMensal;
    }
}
```

### Pontos importantes

| Interface                                     | Classe Abstrata                               |
| --------------------------------------------- | --------------------------------------------- |
| Define principalmente um contrato             | Define uma estrutura/base comum               |
| Uma classe pode implementar várias interfaces | Uma classe só pode herdar de uma classe       |
| Ideal para comportamentos independentes       | Ideal para classes fortemente relacionadas    |
| Não é usada para manter estado de instância   | Pode possuir atributos, propriedades e estado |
| Favorece baixo acoplamento                    | Favorece reutilização de implementação        |

### Quando escolher cada uma

Eu escolheria uma **interface** quando diferentes classes precisam oferecer a mesma capacidade, mas não necessariamente pertencem à mesma hierarquia.

Exemplo:

```csharp
public interface IImprimivel
{
    void Imprimir();
}
```

Classes como `Relatorio`, `NotaFiscal`, `Contrato` e `Comprovante` poderiam implementar essa interface sem precisar ter um ancestral comum.

Já escolheria uma **classe abstrata** quando existe uma relação clara de **“é um”** e existe código que deve ser compartilhado.

Por exemplo, `FuncionarioCLT` e `FuncionarioPJ` são tipos de `Funcionario` e podem compartilhar propriedades como `Nome` e `Documento`, além de comportamentos comuns, enquanto cada um implementa sua própria regra de cálculo de salário.

> **Obs. Interface define um contrato de comportamento e é ideal para baixo acoplamento e múltiplas implementações. Classe abstrata representa uma base comum entre classes relacionadas e permite compartilhar estado e implementação.**

### ❓ Pergunta 3

**O que é e para que serve o operador ‘async/await’? O que acontece na prática quando uma thread do .NET executa uma operação assíncrona?.**

### ✅ Resposta

```text
async e await são recursos do C# usados para trabalhar com operações assíncronas, ou seja, operações que podem levar algum tempo para terminar sem precisar bloquear a thread enquanto aguardam.
```
### O que é async?

```text
A palavra-chave async indica que um método pode executar operações assíncronas e normalmente retornar um Task ou Task<T>.
```
```csharp
public async Task<string> BuscarDadosAsync()
{
    return "Dados carregados";
}
```
### O que é await?
```textx
O await é usado para aguardar a conclusão de uma operação assíncrona.são recursos do C# utilizados para implementar operações assíncronas sem bloquear desnecessariamente a thread em execução. O async indica que um método pode possuir operações assíncronas e normalmente retornar um Task ou Task<T>, enquanto o await aguarda a conclusão de uma dessas operações.
```
Exemplo:

```csharp
public async Task<string> BuscarDadosAsync()
{
    string resultado = await httpClient.GetStringAsync("https://exemplo.com");

    return resultado;
}
```
O ponto importante é que **await** não significa simplesmente **"parar tudo e esperar"**.
Quando a execução chega aqui:
```csharp
await httpClient.GetStringAsync(...);
```
e a operação ainda não terminou, o método é temporariamente suspenso.
De forma simplificada:

```text
Thread executando método
        │
        ▼
Inicia requisição HTTP
        │
        ▼
      await
        │
        ├── Operação ainda não terminou
        │
        └── Thread é liberada para executar outro trabalho
```
### Vamos de prática

```csharp
public async Task ProcessarAsync()
{
    Console.WriteLine("Início");

    await Task.Delay(3000);

    Console.WriteLine("Fim");
}
```
Ao chegar em:
```csharp
await Task.Delay(3000);
```
a thread não precisa ficar ocupada durante os três segundos.
Ela pode ser utilizada por outras tarefas.

Depois que o **Task.Delay** é concluído, a execução continua em:
```csharp
Console.WriteLine("Fim");
```
### O que acontece na prática no .NET?

Quando um método é marcado como async, o compilador transforma esse método internamente em uma estrutura semelhante a uma máquina de estados (state machine). quando a execução encontra um await cuja operação ainda não foi concluída, o método salva seu estado e pode liberar a thread para executar outros trabalhos. Quando a operação termina, a continuação do método é agendada e a execução prossegue a partir do ponto após o await. O compilador implementa esse comportamento utilizando uma máquina de estados.

Considere:
```csharp
public async Task ExecutarAsync()
{
    Console.WriteLine("A");

    await BuscarDadosAsync();

    Console.WriteLine("B");
}
```
```text
Estado 0
Executar Console.WriteLine("A")

        ↓

Iniciar BuscarDadosAsync()

        ↓

Operação terminou?
      /        \
    SIM        NÃO
     │          │
     │          └── salva o estado atual
     │              e libera a thread
     │
     ▼
Continuar execução

        ↓

Console.WriteLine("B")
```
Quando BuscarDadosAsync() termina, o runtime pode continuar o método a partir daquele ponto.

### Isso cria uma nova thread?

Não necessariamente.
Esse é um ponto importante.

```csharp
await httpClient.GetAsync(...)
```
não significa:
```text
Criar uma nova thread
```
Operações de I/O, como:
- acesso HTTP;
- acesso a banco de dados;
- leitura de arquivos;
- chamadas de rede;
podem ser executadas de forma assíncrona sem manter uma thread bloqueada esperando pelo resultado.
Por isso async/await é especialmente útil em aplicações Web.

Imagine uma API:

```csharp
[HttpGet]
public async Task<IActionResult> BuscarCliente()
{
    var cliente = await repository.BuscarClienteAsync();

    return Ok(cliente);
}
```
Enquanto o banco está processando a consulta, a thread que estava atendendo aquela requisição pode ser liberada para atender outra requisição.
Isso ajuda a aplicação a ter melhor **escalabilidade**.

### async/await não significa paralelismo
É importante separar os conceitos:
```text
Assíncrono ≠ Paralelo
```
Assíncrono significa principalmente:
```text
não bloquear a execução enquanto uma operação está sendo aguardada.
```
Paralelismo significa executar trabalhos simultaneamente, normalmente utilizando múltiplas threads ou núcleos do processador.

# Parte 2: Questão Prática

## 🧠 Questão 2: Validador de Sequências e Agrupamento

### 📌 Descrição

Esta aplicação foi desenvolvida em **C#/.NET** para resolver o seguinte problema:

> Dada uma lista de números inteiros desordenados, encontrar e retornar a maior sequência de números inteiros consecutivos presentes na lista.

### Exemplo

Entrada:

```text
[100, 4, 200, 1, 3, 2]
```
Saída esperada:
```text
[1, 2, 3, 4]
```
Tamanho da sequência:
```text
4
```
### 🧠 Estratégia utilizada
A solução utiliza a estrutura:
```text
HashSet<int>
```
O HashSet permite verificar rapidamente se determinado número existe na coleção.
A ideia principal é identificar apenas os números que podem representar o início de uma sequência.
Para cada número n, verificamos se existe:
```text
n - 1
```
Se n - 1 não existir, significa que n pode ser o início de uma sequência.
A partir dele, buscamos:
```text
n + 1
n + 2
n + 3
...
```
até que o próximo número não seja encontrado.

### ⚙️ Complexidade
A abordagem com HashSet apresenta complexidade média de:
```text
O(n)
```

###  🔎 Considerações

A utilização de HashSet<int> também evita problemas com números duplicados.

Por exemplo:
```text
[1, 2, 2, 3, 4]
```
é tratado internamente como:
```text
[1, 2, 3, 4]
```
e o resultado permanece:
```text
[1, 2, 3, 4]
```
### 🛠️ Tecnologias utilizadas

```text
- C#
- .NET
- Collections
- List<int>
- HashSet<int>
```

## Questão 3 : 🧠 Análise de String e Frequência

### 📌 Descrição do Projeto

Este projeto desenvolvida desenvolvida em **C# / .NET** para realizar a análise de caracteres de uma frase informada pelo usuário.

A aplicação recebe uma `string` como entrada, realiza a higienização e normalização do texto e, em seguida, executa análises de frequência dos caracteres.

O processamento contempla a remoção de espaços, pontuações, caracteres especiais e acentuações, além da conversão de todas as letras para minúsculas.

Após a normalização, o sistema identifica:

- O primeiro caractere que aparece apenas uma vez no texto;
- Os três caracteres com maior número de ocorrências;
- A quantidade exata de vezes que cada um desses caracteres aparece.

A solução foi estruturada separando a lógica de negócio da execução principal da aplicação, facilitando a leitura, manutenção e evolução do código.

### Funcionalidades

A aplicação realiza:

- Higienização do texto;
- Remoção de espaços, pontuações e caracteres especiais;
- Conversão para letras minúsculas;
- Remoção de acentuação;
- Identificação do primeiro caractere não repetido;
- Identificação dos 3 caracteres mais frequentes.

### Exemplo

### Entrada

```text
A Bateria do computador está Fraca!
```

### Texto higienizado

```text
abateriadocomputadorestafraca
```

### Saída

```text
Primeiro caractere não repetido: 'b'

Top 3 caracteres mais frequentes:
Letra 'a': 7 vezes
Letra 't': 3 vezes
Letra 'r': 3 vezes
```

> **Observação:** o exemplo disponibilizado no enunciado apresenta divergências na contagem de alguns caracteres.  
> A implementação segue as regras descritas e realiza a contagem diretamente sobre o texto higienizado.

### Estrutura do Projeto

```text
questao_3/
│
├── Program.cs
├── AnaliseTextoService.cs
├── questao_3.csproj
└── README.md
```

### `Program.cs`

Responsável por:

- Receber a frase informada pelo usuário;
- Chamar os métodos de análise;
- Exibir os resultados no console.

### `AnaliseTextoService.cs`

Responsável pelas regras de negócio da aplicação.

Principais métodos:

- `HigienizarTexto`
- `ContarCaracteres`
- `EncontrarPrimeiroNaoRepetido`
- `ObterTop3Caracteres`
- `RemoverAcentos`

### Estruturas Utilizadas

Para armazenar a frequência dos caracteres foi utilizado:

```csharp
Dictionary<char, int>
```

Exemplo:

```text
a -> 7
b -> 1
t -> 3
r -> 3
```

### ⚙️ Complexidade

A higienização e a contagem dos caracteres percorrem o texto de forma linear.

Considerando `n` como a quantidade de caracteres da frase:

```text
O(n)
```

A busca pelo primeiro caractere não repetido também possui complexidade:

```text
O(n)
```

A ordenação utilizada para encontrar os caracteres mais frequentes depende da quantidade `k` de caracteres distintos:

```text
O(k log k)
```

### 🛠️ Tecnologias Utilizadas
```text
- C#
- .NET
- Dictionary
- List
- StringBuilder
- System.Globalization
- Unicode Normalization
```
## Questão 4 : 🧠 Processamento Financeiro e Regra de Negócio

### 📌 Descrição do Projeto

Este projeto consiste em calcular o valor final de uma fatura de acordo com a data em que o pagamento foi realizado.

A aplicação considera uma fatura com valor base de **R$ 1.000,00** e vencimento em **10/10/2026**, aplicando regras específicas de desconto, multa e juros conforme a data de pagamento.

O objetivo da solução é demonstrar a implementação de regras de negócio, manipulação de datas, cálculos financeiros e organização do código em responsabilidades separadas.

### Regras de Negócio

Quando o pagamento é realizado antes da data de vencimento:

- É aplicado desconto de **1% por dia de antecipação**;
- O desconto máximo permitido é de **10%**.

Exemplo:

```text
Pagamento: 05/10/2026
Vencimento: 10/10/2026

Dias de antecipação: 5
Desconto: 5%
Valor do desconto: R$ 50,00
Valor final: R$ 950,00
```
### Pagamento no vencimento

Quando o pagamento é realizado exatamente na data de vencimento:

- Não existe desconto;
- Não existe multa;
- Não existem juros.

```text
Pagamento: 10/10/2026
Valor final: R$ 1.000,00
```

### Pagamento em atraso

Quando o pagamento é realizado após a data de vencimento:

- É aplicada uma multa fixa de **2%**;
- São aplicados juros simples de **0,5% por dia de atraso**.

Exemplo:

```text
Pagamento: 13/10/2026
Vencimento: 10/10/2026

Dias de atraso: 3
Multa: R$ 20,00
Juros: R$ 15,00
Valor final: R$ 1.035,00
```
## Tecnologias e Recursos Utilizados
```text
- C#
- .NET
- `DateTime`
- `decimal`
- `DateTime.TryParseExact`
- Classes estáticas
- Namespaces
- Separação de responsabilidades
```
## Questão 5 : 🧠 Mini-API / Console App de Gerenciamento de Tarefas (To-Do List)

### 📌 Descrição do Projeto

Esta aplicação foi desenvolvida com objetivo de realizar o gerenciamento de tarefas pessoais.

A API permite cadastrar tarefas, consultar tarefas, atualizar o status, listar tarefas ativas e filtrar tarefas concluídas por período.

A solução foi organizada em camadas para separar responsabilidades entre domínio, regras de negócio, persistência em memória e exposição dos endpoints HTTP.

## Funcionalidades

A aplicação permite:

- Cadastrar uma nova tarefa;
- Consultar uma tarefa por `Id`;
- Listar todas as tarefas;
- Listar apenas tarefas ativas;
- Atualizar o status de uma tarefa;
- Concluir uma tarefa com preenchimento automático da data de conclusão;
- Filtrar tarefas concluídas por intervalo de datas;
- Validar regras de negócio;
- Retornar mensagens adequadas em casos de erro.

### Cadastro de Tarefa

Uma tarefa possui:

- `Id`;
- `Titulo`;
- `Descricao`;
- `DataDeCriacao`;
- `DataDeConclusao`;
- `Status`.

O `Id` é gerado automaticamente utilizando:

```csharp
Guid.NewGuid()
```

A data de criação também é preenchida automaticamente:

```csharp
DateTime.Now
```

Toda nova tarefa é criada inicialmente com status:

```text
Pendente
```

A `DataDeConclusao` permanece nula enquanto a tarefa não for concluída.

### Validação do Título

O título:

- Não pode ser vazio;
- Não pode conter apenas espaços;
- Deve possuir no mínimo 5 caracteres.

Exemplos:

```text
"ABC"       -> inválido
"    "      -> inválido
"Teste"     -> válido
"Estudar C#" -> válido
```

### Status da Tarefa

Os status disponíveis são:

```text
Pendente
EmAndamento
Concluida
```

Internamente, são representados por um `enum`:

```csharp
public enum StatusTarefa
{
    Pendente = 1,
    EmAndamento = 2,
    Concluida = 3
}
```

A API está configurada para representar o status no JSON através do nome:

```json
{
  "status": "EmAndamento"
}
```

### Conclusão da Tarefa

Quando o status for alterado para:

```text
Concluida
```

a aplicação preenche automaticamente:

```csharp
DataDeConclusao = DateTime.Now;
```

Uma tarefa já concluída não pode ter seu status alterado novamente.

### Tarefas Ativas

São consideradas tarefas ativas:

```text
Pendente
EmAndamento
```

Tarefas com status `Concluida` não são retornadas na consulta de tarefas ativas.

### Filtro de Tarefas Concluídas

É possível consultar tarefas concluídas dentro de um intervalo de datas.

A aplicação valida que:

```text
dataInicio <= dataFim
```

Caso contrário, retorna uma mensagem de erro.

## Arquitetura

A aplicação segue uma separação de responsabilidades:

```text
HTTP Request
     ↓
Controller
     ↓
Service
     ↓
Repository
     ↓
Persistência In-Memory
```
## Tecnologias Utilizadas
```text
- C#
- .NET 10
- ASP.NET Core Web API
- Swagger / OpenAPI
- Dependency Injection
- Repository Pattern
- Service Layer
- DTOs
- Enums
- Persistence In-Memory
- REST
```
## Observações

A aplicação utiliza persistência em memória, conforme permitido pelo requisito do desafio.

Por esse motivo, os dados não são armazenados permanentemente. Ao encerrar ou reiniciar a aplicação, as tarefas cadastradas são removidas.

A estrutura foi desenvolvida priorizando:

- Separação de responsabilidades;
- Organização em camadas;
- Legibilidade;
- Manutenção;
- Validação de regras de negócio;
- Clareza dos retornos da API.

## Questão Bonus : 🧠 Interface Web em React (Frontend Integration)

## Descrição do Projeto

Este projeto corresponde ao frontend da aplicação de gerenciamento de tarefas desenvolvida como questão bônus do desafio técnico.

A aplicação foi construída utilizando **React com TypeScript** e consome a Web API desenvolvida em **C# / ASP.NET Core**.

O objetivo principal é permitir a criação, visualização e atualização do status das tarefas através de uma interface web simples, responsiva e integrada ao backend.

## Funcionalidades

A aplicação permite:

- Cadastrar novas tarefas;
- Informar título e descrição;
- Exibir mensagens de validação retornadas pelo backend;
- Listar as tarefas cadastradas;
- Exibir visualmente o status atual da tarefa;
- Avançar o status de uma tarefa;
- Atualizar automaticamente a listagem após cadastro ou alteração;
- Exibir data de criação;
- Exibir data de conclusão quando disponível.

## Status das Tarefas

Os status disponíveis são:

```text
Pendente
EmAndamento
Concluida
```
O fluxo de atualização utilizado na interface é:

```text
Pendente
   ↓
EmAndamento
   ↓
Concluida
```
Quando uma tarefa está concluída, não é exibida uma nova ação para alteração de status.

## Tecnologias Utilizadas

- React
- TypeScript
- Vite
- Fetch API
- HTML
- CSS
- ESLint

## Observações

O foco desta implementação é demonstrar:

- Integração entre React e ASP.NET Core;
- Consumo de API REST;
- Uso de TypeScript;
- Gerenciamento de estado;
- Uso de `useState`;
- Uso de `useEffect`;
- Tratamento de erros;
- Componentização;
- Atualização dinâmica da interface.

Não foi utilizado um framework visual complexo, pois o foco principal do bônus é a integração frontend/backend e o funcionamento das regras da aplicação.

# Parte 4: Defesa da Solução

## 1. Decisão de Arquitetura

### Como a Questão 5 foi organizada

Na Questão 5, optei por desenvolver uma **ASP.NET Core Web API** com separação de responsabilidades em camadas.

A estrutura principal foi organizada em:

```text
Questao5/
│
├── Constants/
├── Controllers/
├── Domain/
│   ├── Entities/
│   └── Enums/
├── DTOs/
├── Repositories/
├── Services/
└── Program.cs
```

Cada camada possui uma responsabilidade específica.

### Domain

A camada `Domain` contém os elementos centrais da aplicação.

A entidade `Tarefa` representa uma tarefa do sistema e possui propriedades como:

```text
Id
Titulo
Descricao
DataDeCriacao
DataDeConclusao
Status
```

O status da tarefa foi representado utilizando um `enum`:

```csharp
public enum StatusTarefa
{
    Pendente = 1,
    EmAndamento = 2,
    Concluida = 3
}
```

Essa abordagem evita trabalhar com valores de status livres em formato de `string` e reduz a possibilidade de estados inválidos.

### DTOs

Os DTOs foram utilizados para representar os dados recebidos pela API.

Por exemplo, no cadastro de uma tarefa, o cliente informa apenas:

```text
Titulo
Descricao
```

Dados como:

```text
Id
DataDeCriacao
DataDeConclusao
Status
```

são controlados pela própria aplicação.

Essa decisão evita que o consumidor da API informe valores que devem ser definidos pela regra de negócio.

### Controllers

A camada de `Controllers` é responsável pela comunicação HTTP.

Sua função é:

```text
Receber a requisição
        ↓
Encaminhar para o Service
        ↓
Receber o resultado
        ↓
Retornar a resposta HTTP
```

Procurei evitar colocar regras de negócio diretamente no Controller.

### Services

A camada `Services` concentra as regras de negócio da aplicação.

Entre elas:

- validação do título;
- criação da tarefa;
- alteração de status;
- preenchimento automático da data de conclusão;
- impedimento de alteração de uma tarefa já concluída;
- validação do intervalo de datas.

Dessa forma, as regras ficam centralizadas e podem ser reutilizadas independentemente da forma como a aplicação é consumida.

### Repositories

A camada `Repositories` ficou responsável pelo armazenamento e recuperação das tarefas.

Como o requisito permitia persistência In-Memory, foi utilizada uma:

```csharp
List<Tarefa>
```

para armazenar os dados durante a execução da aplicação.

Também foi criada uma interface:

```csharp
ITarefaRepository
```

para reduzir o acoplamento entre o `Service` e a implementação concreta do repositório.

### Constants

Foi criada uma camada separada para constantes utilizadas pelas regras da aplicação.

Exemplos:

```text
Tamanho mínimo do título
Mensagens de erro
Mensagens de validação
```

Essa abordagem evita valores e mensagens espalhados pelo código.

### Por que escolhi essa estrutura?

Escolhi essa estrutura porque ela permite uma melhor **separação de responsabilidades** e reduz o acoplamento entre as partes da aplicação.

O fluxo principal ficou:

```text
HTTP Request
     ↓
Controller
     ↓
Service
     ↓
Repository
     ↓
Persistência In-Memory
```

Com essa separação, cada camada possui uma responsabilidade clara.

Por exemplo, caso a persistência em memória fosse substituída futuramente por um banco de dados, a principal alteração aconteceria na camada de repositório, preservando grande parte das regras existentes no `Service`.

Essa organização também facilita:

- manutenção;
- leitura do código;
- testes;
- evolução da solução;
- substituição de implementações.

---

## 2. Desafios e Soluções

### Parte mais complexa

Entre as Questões 2, 3 e 4, considero que a parte mais interessante em relação à lógica foi a **Questão 2 — identificação da maior sequência de números consecutivos em um array desordenado**.

O desafio era encontrar a maior sequência sem depender simplesmente da ordenação completa do array.

Exemplo:

```text
Entrada:

[100, 4, 200, 1, 3, 2]

Resultado:

[1, 2, 3, 4]
```

A abordagem utilizada foi baseada em:

```csharp
HashSet<int>
```

O `HashSet` foi escolhido porque permite verificar rapidamente se determinado número está presente na coleção.

### Estratégia utilizada

Primeiramente, os números do array são inseridos em um `HashSet`.

Em seguida, para cada número `n`, verifico se o número anterior existe:

```text
n - 1
```

Se o número anterior existir, significa que `n` não representa o início de uma nova sequência.

Por exemplo:

```text
2
```

não deve iniciar uma sequência se:

```text
1
```

já estiver presente.

Por outro lado, se `n - 1` não existir, aquele número pode representar o início de uma sequência.

A partir dele, o algoritmo procura:

```text
n + 1
n + 2
n + 3
...
```

até encontrar um número que não esteja presente no conjunto.

### Por que utilizei HashSet?

A principal vantagem é a busca eficiente por existência de elementos.

Em vez de percorrer todo o array repetidamente procurando cada número, o `HashSet` permite fazer essas verificações com custo médio constante.

Dessa forma, a solução possui complexidade média próxima de:

```text
O(n)
```

Outro benefício é que valores duplicados são naturalmente eliminados pelo `HashSet`.

Por exemplo:

```text
[1, 2, 2, 3, 4]
```

passa a ser tratado como:

```text
[1, 2, 3, 4]
```

sem afetar a identificação da sequência.

### Como resolvi e testei

Para validar a solução, utilizei diferentes cenários.

#### Sequência no meio de valores desordenados

```text
Entrada:

[100, 4, 200, 1, 3, 2]

Resultado esperado:

[1, 2, 3, 4]
```

#### Valores duplicados

```text
Entrada:

[1, 2, 2, 3, 4]

Resultado esperado:

[1, 2, 3, 4]
```

#### Sequência pequena

```text
Entrada:

[10, 5, 6, 7, 20]

Resultado esperado:

[5, 6, 7]
```

#### Elementos sem sequência relevante

```text
Entrada:

[10, 30, 50]

Resultado:

Uma sequência com apenas um elemento
```

Também acompanhei manualmente o comportamento do algoritmo para verificar se apenas os possíveis inícios de sequência realizavam a busca pelos próximos valores.

Isso ajudou a evitar processamento desnecessário.

---

## 3. Autoavaliação e Trade-offs

A implementação atual atende aos requisitos propostos pelo desafio, porém existem melhorias que eu aplicaria caso houvesse mais tempo disponível.

### Testes Unitários

Uma das primeiras melhorias seria adicionar projetos de testes utilizando, por exemplo:

```text
xUnit
```

Os testes poderiam validar principalmente:

- criação de tarefas;
- título vazio;
- título com menos de 5 caracteres;
- atualização de status;
- conclusão de tarefa;
- bloqueio de alteração de tarefa concluída;
- filtro por período;
- algoritmos das Questões 2, 3 e 4.

Isso aumentaria a segurança durante futuras alterações.

### Testes de Integração

Também adicionaria testes de integração para validar os endpoints da Web API.

Exemplos:

```text
POST /api/Tarefa
GET /api/Tarefa
PATCH /api/Tarefa/{id}/status
GET /api/Tarefa/concluidas
```

Esses testes permitiriam validar não apenas as regras internas, mas também os códigos HTTP e contratos da API.

### Banco de Dados Real

A implementação atual utiliza persistência In-Memory, conforme permitido pelo desafio.

O trade-off dessa abordagem é que os dados são perdidos sempre que a aplicação é encerrada.

Em uma aplicação real, substituiria essa implementação por um banco de dados utilizando:

```text
Entity Framework Core
```

com um banco como:

```text
SQL Server
PostgreSQL
```

Como a aplicação possui uma camada de Repository separada, essa evolução poderia ser feita sem alterar significativamente as demais camadas.

### Tratamento Global de Exceções

Atualmente parte das exceções é tratada diretamente nos Controllers.

Uma melhoria seria utilizar um mecanismo global de tratamento de exceções, centralizando respostas como:

```text
400 Bad Request
404 Not Found
409 Conflict
500 Internal Server Error
```

Isso reduziria duplicação de código nos Controllers.

### Logs

Também adicionaria logs estruturados para registrar:

- criação de tarefas;
- alteração de status;
- erros inesperados;
- falhas de validação;
- informações importantes de execução.

Em uma aplicação maior, ferramentas como Serilog poderiam ser consideradas.

### Data e Hora

A implementação utiliza:

```csharp
DateTime.Now
```

Em uma aplicação distribuída ou executada em diferentes regiões, seria interessante trabalhar com:

```csharp
DateTime.UtcNow
```

e converter a data apenas na camada de apresentação.

Outra possibilidade seria abstrair a obtenção da data e hora através de um serviço, facilitando testes automatizados.

### Paginação e Filtros

Caso a quantidade de tarefas crescesse, a listagem completa deixaria de ser eficiente.

Uma melhoria futura seria adicionar:

- paginação;
- ordenação;
- filtros por status;
- busca por título.

### Configuração do Frontend

Na implementação bônus, a URL da API poderia ser movida para uma variável de ambiente.

Em vez de manter:

```typescript
const API_URL = "http://localhost:5000/api/tarefas";
```

poderia ser utilizada uma configuração como:

```text
VITE_API_URL
```

Isso facilitaria a execução em diferentes ambientes.

### Docker

Outra possível evolução seria criar arquivos Docker para frontend e backend, permitindo executar toda a solução de forma padronizada.

---

## 4. Pergunta de Checagem Técnica — Questão 2

### O que acontece desde a entrada do array até a resposta final?

Considere a entrada:

```text
[100, 4, 200, 1, 3, 2]
```

O processamento ocorre da seguinte forma.

### Passo 1 — O array entra na função

O método recebe a coleção desordenada:

```text
[100, 4, 200, 1, 3, 2]
```

Nesse momento ainda não sabemos qual é a maior sequência consecutiva.

### Passo 2 — Criação do HashSet

Os elementos são inseridos em um:

```csharp
HashSet<int>
```

Conceitualmente:

```text
{100, 4, 200, 1, 3, 2}
```

O objetivo é permitir verificações rápidas como:

```text
Existe o número 2?
Existe o número 3?
Existe o número 101?
```

### Passo 3 — Percorrer os números

O algoritmo percorre os números do conjunto.

Para cada número `n`, verifica:

```text
n - 1
```

A pergunta é:

```text
Existe um número anterior a ele?
```

Se existir, significa que aquele número pertence a uma sequência que começou antes e, portanto, não precisamos começar uma nova busca a partir dele.

### Passo 4 — Identificar o início de uma sequência

Considere:

```text
100
```

Verificamos:

```text
99 existe?
```

Não.

Então:

```text
100
```

pode ser o início de uma sequência.

Procuramos:

```text
101
```

Como não existe, a sequência termina:

```text
[100]
```

### Passo 5 — Avaliar o número 4

Para:

```text
4
```

verificamos:

```text
3 existe?
```

Sim.

Portanto, `4` não inicia uma nova sequência.

Nenhuma busca adicional é necessária a partir dele.

### Passo 6 — Avaliar o número 200

Verificamos:

```text
199 existe?
```

Não.

Então `200` inicia uma possível sequência.

Procuramos:

```text
201
```

Não existe.

Resultado:

```text
[200]
```

### Passo 7 — Avaliar o número 1

Agora chegamos ao:

```text
1
```

Verificamos:

```text
0 existe?
```

Não.

Portanto:

```text
1
```

é o início de uma sequência.

Começamos então a procurar os valores seguintes:

```text
2 → existe
3 → existe
4 → existe
5 → não existe
```

A sequência encontrada é:

```text
[1, 2, 3, 4]
```

Seu tamanho é:

```text
4
```

Como ela é maior do que as sequências encontradas anteriormente, passa a ser armazenada como a maior sequência.

### Passo 8 — Avaliar 3 e 2

Quando o algoritmo chega ao:

```text
3
```

verifica:

```text
2 existe?
```

Sim.

Então `3` não inicia uma sequência.

O mesmo ocorre com:

```text
2
```

pois:

```text
1 existe
```

Portanto, eles não precisam iniciar uma nova busca.

### Passo 9 — Finalização

Depois de percorrer todos os números, a maior sequência armazenada é:

```text
[1, 2, 3, 4]
```

Esse valor é retornado pelo método.

O fluxo completo pode ser resumido como:

```text
Array desordenado
        ↓
Criar HashSet
        ↓
Percorrer os números
        ↓
Verificar se n - 1 existe
        ↓
     Existe?
     /     \
   Sim     Não
    |       |
Ignora    Início de sequência
            ↓
       Buscar n + 1
            ↓
       Buscar n + 2
            ↓
          ...
            ↓
Comparar tamanho com a maior sequência atual
            ↓
Atualizar maior sequência quando necessário
            ↓
Retornar maior sequência
```

Para o exemplo utilizado:

```text
Entrada:

[100, 4, 200, 1, 3, 2]

Saída:

[1, 2, 3, 4]
```

A principal ideia da solução é evitar iniciar buscas a partir de números que já pertencem ao meio de uma sequência.

Isso permite manter a solução eficiente e com complexidade média próxima de:

```text
O(n)
```