# Test Task – In-Game Shop with Strict Separation of Concerns

This project implements a modular in-game shop system in Unity where **each gameplay domain is fully isolated** in terms of:
- Data
- Business logic
- View + View control
- Resource link dependencies
- Interfaces for cross-domain interaction

The goal: strong **separation of concerns** and **extensibility** for future gameplay features.

---

## Structure

### Domains

| Domain | Responsibility | Resource Type |
|--------|----------------|---------------|
| **Core** | App bootstrap, PlayerData management | — |
| **Shop** | Purchase processing, validation rules, UI | — |
| **Gold** | Earnable/spendable currency | `int` |
| **Health** | Health as integer + percentage operations | `int + %` |
| **Location** | Player’s current world region | `string` |
| **VIP** | Time-based VIP status | `TimeSpan` |

Each domain follows the same architectural pattern:
- Independent data representation
- Clear business logic service
- Minimal but useful abstraction surfaces
- Example UI and logic for purchasing behavior

---

## Player Resources

All player resource types implement:

- **`IPlayerResource`**  
  Represents a player-owned resource that can be gained or spent.

- **`IResourceAmountData`**  
  Defines the *value* or *status* for the resource (e.g., amount, time, string).

- **`IResourceAmountProvider`**  
  A unified abstraction for **any format** capable of providing a resource amount:
  - ScriptableObject configs
  - JSON data
  - Runtime-generated data

A base ScriptableObject class:

- **`ResourceAmountSOBase`**  
  Abstract implementation of `IResourceAmountProvider`.  
  This is just **one** example of how resource amounts can be defined — not the only option.

> 💡 **Design Note**  
> Certain pieces of code *look* similar across domains, but aggressively extracting shared behavior would create hidden coupling on many shared classes — which goes against the point of strict separation.  
>  
> The **single** allowed general helper is:  
> **`ResourceRuntimeData<T>`** – convenience wrapper for storing `T` with update events for UI subscription.

---

## Shop Setup

The shop is designed to allow **any** product composition:
- A product may contain **multiple** price blocks and reward blocks
- A domain can act as a price *or* a reward source  
  Example:  
  - **Gold → VIP time**
  - **Health → Gold** (half of health points)
  - **Gold → Location unlock**

> ⚠️ Even if a domain conceptually doesn’t fit the “price” model (e.g., location is not a collectible list), the architecture still **allows** it for testing and creative edge-cases.

## Cool Stuff to Note

✔ Strict domain separation  
✔ Easy to plug new shop processors  
✔ Config via JSON/SO without code changes  
✔ Unified interface pattern for any resource type  
✔ Runtime merge of product configuration  
✔ Multiple representations of health (int + %)  
✔ Allows experimentation with resource relationships  
✔ Minimal coupling preserved deliberately
