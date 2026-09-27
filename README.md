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

## ❓ Pergunta 1

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

## ❓ Pergunta 2

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

## ❓ Pergunta 3

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

## 📌 Descrição

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

## ⚙️ Complexidade
A abordagem com HashSet apresenta complexidade média de:
```text
O(n)

##  🔎 Considerações

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
## 🛠️ Tecnologias utilizadas

```text
- C#
- .NET
- Collections
- List<int>
- HashSet<int>
```

### Questão 3 : 🧠 Análise de Caracteres em C#

## 📌 Descrição do Projeto

Este projeto desenvolvida desenvolvida em **C# / .NET** para realizar a análise de caracteres de uma frase informada pelo usuário.

A aplicação recebe uma `string` como entrada, realiza a higienização e normalização do texto e, em seguida, executa análises de frequência dos caracteres.

O processamento contempla a remoção de espaços, pontuações, caracteres especiais e acentuações, além da conversão de todas as letras para minúsculas.

Após a normalização, o sistema identifica:

- O primeiro caractere que aparece apenas uma vez no texto;
- Os três caracteres com maior número de ocorrências;
- A quantidade exata de vezes que cada um desses caracteres aparece.

A solução foi estruturada separando a lógica de negócio da execução principal da aplicação, facilitando a leitura, manutenção e evolução do código.

## Funcionalidades

A aplicação realiza:

- Higienização do texto;
- Remoção de espaços, pontuações e caracteres especiais;
- Conversão para letras minúsculas;
- Remoção de acentuação;
- Identificação do primeiro caractere não repetido;
- Identificação dos 3 caracteres mais frequentes.

## Exemplo

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

> **Observação:** o exemplo disponibilizado no enunciado apresenta divergências na contagem de alguns caracteres.  
> A implementação segue as regras descritas e realiza a contagem diretamente sobre o texto higienizado.

## Estrutura do Projeto

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

## Estruturas Utilizadas

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

## ⚙️ Complexidade

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

## 🛠️ Tecnologias Utilizadas
```text
- C#
- .NET
- Dictionary
- List
- StringBuilder
- System.Globalization
- Unicode Normalization
```