# Unity Interaction System

Unity 6 (`6000.5.9f1`) implementation of the data-driven entity interaction assessment.
Open `Assets/Scenes/DemoScene.unity`, open the Console, and enter Play Mode; the demo runs automatically.

## Architecture

- `Entity` is an Inspector-authored scene component that owns one plain-C# `InteractionController` and registers with `EntityRegistry`.
- `EntityRegistry` exposes the enabled entities considered during each configurable evaluation interval.
- `InteractionDefinition` and `FactionDefinition` are shared ScriptableObject assets containing designer-authored rules and metadata.
- `InteractionAvailability`, `InteractionSelector`, and `InteractionPriorityPolicy` implement filtering, deterministic selection, and interruption rules outside MonoBehaviour lifecycles.
- `InteractionController` builds candidates from its owner's offered definitions and owns at most one active `InteractionInstance`.
- `InteractionInstance` contains per-occurrence context and elapsed time and invokes the effect's start, tick, completion, or cancellation callbacks.
- `Effect` is an abstract ScriptableObject implemented by additive effect types; mutable target state remains on components such as `EntityStats` and `EntityVisual`.

## Shared Data and Runtime State

Entities reference project-level faction and interaction assets, so every entity offering the same interaction uses the same definition object; definitions are neither copied nor cloned at runtime. Shared definitions contain identifiers, descriptions, kind, duration, priority, allowed directed faction pairs, and an effect reference. Each execution creates an `InteractionInstance` containing only its initiator, target, definition reference, and elapsed time. Mutable health and visual state belong to scene entities, not shared assets.

## Adding an Effect

Add a class under `Assets/Scripts/Effects` that derives from `Effect` and overrides the required lifecycle callbacks. Create its asset under `Assets/Definitions/Effect`, then assign it to an interaction asset under `Assets/Definitions/Interactions`. No changes are required in `InteractionController`, `InteractionInstance`, `InteractionAvailability`, `InteractionSelector`, `InteractionPriorityPolicy`, or `EntityRegistry`.

## Scope and Further Work

Cut for time: automated Edit Mode/Play Mode regression tests, stronger authoring validation for duplicate identifiers and invalid references, and runtime handling for entities disabled during an interaction.

With three more days, I would first add tests for availability, deterministic tie-breaking, equal-priority rejection, interruption order, completion, and cancellation. I would then add project-wide definition validation and cancel active interactions safely when either participant is disabled.
