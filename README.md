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
