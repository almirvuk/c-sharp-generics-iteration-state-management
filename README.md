<div align="center">

# C# 14 Generics, Iteration, and State Management

**Companion source code for the Pluralsight course [C# 14 Generics, Iteration, and State Management](https://app.pluralsight.com/ilx/video-courses/c-sharp-14-generics-iteration-state-management)**

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![C# 14](https://img.shields.io/badge/C%23-14-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/whats-new/csharp-14)
[![Pluralsight](https://img.shields.io/badge/Pluralsight-Course-F15B2A?logo=pluralsight&logoColor=white)](https://app.pluralsight.com/ilx/video-courses/c-sharp-14-generics-iteration-state-management)

*Write more reusable, expressive, and null-safe backend code with C# 14's advanced language features.*

**[▶️ Watch the course on Pluralsight](https://app.pluralsight.com/ilx/video-courses/c-sharp-14-generics-iteration-state-management)**

</div>

---

## 📖 About the course

Real-world backend code is full of repeated patterns: helper methods that differ only by type, nested loops with awkward break-out logic, tangled state-checking `switch` statements, and defensive `null` checks scattered through every layer.

This course shows how to use C#'s advanced language features to remove that boilerplate at the algorithm and class level:

- 🔁 **Iteration and generic algorithms.** Replace duplicated code and clumsy nested loops with reusable, type-safe building blocks.
- 🧩 **State and control flow.** Model them concisely with pattern matching, switch expressions, and property and positional patterns.
- ⚡ **Object optimization.** Use `Lazy<T>`, `ValueTuple`, and clean object-to-object mapping with **C# 14 extension members**.
- 🛡️ **Null safety.** Remove null-reference risks with the **Null Object pattern** and a custom **`Option<T>`** type.

The code uses a small e-commerce domain throughout (customers, orders, line items, registrations) so you can focus on the techniques rather than learning a new domain each time.

---

## 🗂️ Repository structure

Each course module is its own console project in a single solution. Every project contains **two demos**, and you pick one with a command-line argument.

```text
c-sharp-generics-iteration-state-management/
├── c-sharp-generics-iteration-state-management.slnx
├── Module1/   → Advanced iteration            (nested | pagination)
├── Module2/   → Generic algorithms            (retry  | builder)
├── Module3/   → Pattern matching & state      (actions | machine)
├── Module4/   → Lazy<T>, ValueTuple, field    (lazy   | stats)
├── Module5/   → Extension members & mapping   (mapping | registry)
└── Module6/   → Null Object & Option<T>       (nullobject | option)
```

---

## 🚀 Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (C# 14 is the default language version)
- Any editor: Visual Studio 2026, JetBrains Rider, or VS Code with the C# Dev Kit

### Clone and build

```bash
git clone https://github.com/almirvuk/c-sharp-generics-iteration-state-management.git
cd c-sharp-generics-iteration-state-management
dotnet build
```

### Run a demo

```bash
dotnet run --project Module1 -- pagination
```

Run a project without an argument to print its usage:

```text
$ dotnet run --project Module3
Usage: dotnet run -- [actions|machine]
```

---

## 📚 Modules

### Module 1: Advanced Iteration Techniques

> Break out of nested loops cleanly and stream paged data lazily with `yield return`.

| Demo | Command | What it shows |
|------|---------|---------------|
| Nested loop search | `dotnet run --project Module1 -- nested` | The same search written three ways: flag-and-`break`, early `return` from an extracted method, and LINQ `SelectMany` |
| Lazy pagination | `dotnet run --project Module1 -- pagination` | An iterator that fetches API pages only when the caller needs them |

**Key files:** [`FlaggedItemSearch.cs`](Module1/NestedLoops/FlaggedItemSearch.cs) · [`RegistrationsApi.cs`](Module1/Pagination/RegistrationsApi.cs)

<details>
<summary>💡 Highlight: cursor-based paging with <code>yield return</code></summary>

```csharp
public IEnumerable<Registration> GetAllForEvent(Guid eventId)
{
    string? cursor = null;
    do
    {
        var page = fetchPage(eventId, cursor);
        foreach (var registration in page.Items)
            yield return registration;
        cursor = page.NextCursor;
    }
    while (cursor is not null);
}
```

```text
ToList()            -> 3 page(s), 150 items
Take(40).ToList()   -> 1 page(s), 40 items
FirstOrDefault(VIP) -> 1 page(s), found 'Attendee 5'
```

The caller decides how much data to pull, and pages it never asks for are never fetched.
</details>

---

### Module 2: Generic Algorithms with Constraints

> Collapse type-specific helpers into one reusable, type-safe generic algorithm.

| Demo | Command | What it shows |
|------|---------|---------------|
| Generic retry | `dotnet run --project Module2 -- retry` | Three copy-pasted retry helpers (`RetryHelper`) replaced by one `Retry.Run<T>` that uses exception filters |
| Generic builder | `dotnet run --project Module2 -- builder` | A fluent `Builder<T>` for test data that relies on the `where T : class, new()` constraint |

**Key files:** [`RetryHelper.cs`](Module2/Resilience/RetryHelper.cs) (before) · [`Retry.cs`](Module2/Resilience/Retry.cs) · [`RetryOptions.cs`](Module2/Resilience/RetryOptions.cs) · [`Builder.cs`](Module2/Builders/Builder.cs)

<details>
<summary>💡 Highlight: one retry algorithm for every operation</summary>

```csharp
public static RetryOptions For<TException>(int maxAttempts = 3)
    where TException : Exception =>
    new(maxAttempts, TimeSpan.FromMilliseconds(200), ex => ex is TException);

var order     = Retry.Run(() => LoadOrder(id),      RetryOptions.For<SqlException>());
var response  = Retry.Run(() => CallApi(url),       RetryOptions.For<HttpRequestException>());
var published = Retry.Run(() => Publish(message),   RetryOptions.For<BrokerException>(maxAttempts: 5));
```
</details>

---

### Module 3: Pattern Matching and State Modeling

> Replace tangled `if`/`else` chains with switch expressions, property patterns, and tuple patterns.

| Demo | Command | What it shows |
|------|---------|---------------|
| Next action | `dotnet run --project Module3 -- actions` | An `if` chain rewritten as a switch expression with **property patterns**, with a check that both versions agree |
| State machine | `dotnet run --project Module3 -- machine` | An order lifecycle modeled with **tuple (positional) patterns** and a `when` guard; illegal transitions are rejected |

**Key files:** [`OrderActions.cs`](Module3/Actions/OrderActions.cs) · [`OrderMachine.cs`](Module3/Machine/OrderMachine.cs)

<details>
<summary>💡 Highlight: a state machine in one expression</summary>

```csharp
public static OrderState Transition(OrderState state, OrderEvent orderEvent, int daysSincePaid) =>
    (state, orderEvent) switch
    {
        (Pending, PaymentReceived)                 => Paid,
        (Pending, CancelRequested)                 => Cancelled,
        (Paid, OrderEvent.Shipped)                 => OrderState.Shipped,
        (Paid, CancelRequested)                    => Cancelled,
        (OrderState.Shipped, OrderEvent.Delivered) => OrderState.Delivered,

        (OrderState.Delivered, RefundRequested) when daysSincePaid <= 30 => Refunded,

        _ => throw new InvalidOperationException(
                 $"Cannot apply {orderEvent} to an order in {state}.")
    };
```
</details>

---

### Module 4: Optimizing Objects with `Lazy<T>` and `ValueTuple`

> Defer expensive work until it is needed, and return several values without writing a throwaway class.

| Demo | Command | What it shows |
|------|---------|---------------|
| Lazy config | `dotnet run --project Module4 -- lazy` | `Lazy<T>` loads configuration on first access and caches it; also covers the **C# 14 `field` keyword** in a property setter |
| Compute stats | `dotnet run --project Module4 -- stats` | A method returning a named `ValueTuple`, read by name or deconstructed with discards |

**Key files:** [`ReportService.cs`](Module4/Lazily/ReportService.cs) · [`ComputeStatsDemo.cs`](Module4/Stats/ComputeStatsDemo.cs)

<details>
<summary>💡 Highlight: <code>Lazy&lt;T&gt;</code> and the C# 14 <code>field</code> keyword</summary>

```csharp
public sealed class ReportService
{
    private readonly Lazy<Config> _config = new(() => Config.LoadFromDisk());

    public Config Config => _config.Value;

    public string Region
    {
        get;
        set => field = value ?? throw new ArgumentNullException(nameof(value));
    } = string.Empty;
}
```
</details>

---

### Module 5: Object Mapping with C# 14 Extension Members

> Write clean object-to-object mapping with the new `extension` blocks, including extension **properties** and **static** members.

| Demo | Command | What it shows |
|------|---------|---------------|
| Extension mapping | `dotnet run --project Module5 -- mapping` | `ToDto()` / `ToDomain()` extension methods, an `IsRefundable` extension property, and a static `OrderDto.Empty` |
| Mapping registry | `dotnet run --project Module5 -- registry` | A generic `Mapper` keyed by `(Type Source, Type Target)` tuples, with clear errors for unregistered mappings |

**Key files:** [`OrderMapping.cs`](Module5/Mapping/OrderMapping.cs) · [`Mapper.cs`](Module5/Registry/Mapper.cs)

<details>
<summary>💡 Highlight: C# 14 <code>extension</code> blocks</summary>

```csharp
public static class OrderMapping
{
    extension(Order order)
    {
        public OrderDto ToDto() =>
            new(order.Id, order.CustomerEmail, order.Total, order.Status.ToString());

        public bool IsRefundable => order.Status == OrderStatus.Delivered;
    }

    extension(OrderDto)
    {
        public static OrderDto Empty =>
            new(Guid.Empty, "", 0m, nameof(OrderStatus.Pending));
    }
}
```
</details>

---

### Module 6: Eliminating Null-reference Risks

> Replace defensive null checks with types that make "nothing" explicit.

| Demo | Command | What it shows |
|------|---------|---------------|
| Null Object | `dotnet run --project Module6 -- nullobject` | A `NoDiscount` policy stands in for "no discount", so `Checkout` never has to check for `null` |
| Option&lt;T&gt; | `dotnet run --project Module6 -- option` | A custom `Option<T>` (`Some<T>` / `None<T>`) built from records and consumed with pattern matching |

**Key files:** [`DiscountPolicies.cs`](Module6/NullObject/DiscountPolicies.cs) · [`Checkout.cs`](Module6/NullObject/Checkout.cs) · [`Option.cs`](Module6/Options/Option.cs) · [`OptionDemo.cs`](Module6/Options/OptionDemo.cs)

<details>
<summary>💡 Highlight: <code>Option&lt;T&gt;</code> in three lines</summary>

```csharp
public abstract record Option<T>;
public sealed record Some<T>(T Value) : Option<T>;
public sealed record None<T> : Option<T>;

private static string DescribeManager(Customer customer) =>
    Directory.FindManager(customer) switch
    {
        Some<Customer>(var manager) => $"Notify {manager.Name}",
        None<Customer>              => "No manager — escalate instead",
        _ => throw new InvalidOperationException("Unreachable.")
    };
```
</details>

---

## 🧭 Quick reference

| Module | Topic | Demos |
|:------:|-------|-------|
| 1 | Advanced iteration | `nested`, `pagination` |
| 2 | Generic algorithms & constraints | `retry`, `builder` |
| 3 | Pattern matching & state machines | `actions`, `machine` |
| 4 | `Lazy<T>`, `ValueTuple`, `field` keyword | `lazy`, `stats` |
| 5 | C# 14 extension members & mapping | `mapping`, `registry` |
| 6 | Null Object pattern & `Option<T>` | `nullobject`, `option` |

---

## 💬 Feedback

Found an issue or have a question about the code? [Open an issue](https://github.com/almirvuk/c-sharp-generics-iteration-state-management/issues) in this repository, or use the discussion section on the [course page](https://app.pluralsight.com/ilx/video-courses/c-sharp-14-generics-iteration-state-management) on Pluralsight.

<div align="center">

Made with ☕ and C# by **Almir Vuk**

</div>
