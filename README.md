# Unity Interaction System

A data-driven interaction system implemented in Unity 6 using ScriptableObject
definitions, faction-based availability, priorities, timed execution,
interruption, and reusable effects.

**Unity version:** `6000.5.9f1`

## Running the Demo

Open `Assets/Scenes/DemoScene.unity`, open the Console and enter Play Mode.
The demo runs automatically.

Player starts a timed healing interaction with Ally. Enemy becomes available
after a delay, allowing the higher-priority `PaintEnemy` interaction to cancel
the active healing. Enemy is marked red and applies damage. The Console also
shows rejected faction pairs and log-based interactions.

## Interaction Pipeline

At its configured interval, each registered Entity evaluates its definitions
against other entities. Candidates with disallowed directed faction pairs are
rejected. Valid candidates are ordered by priority, interaction identifier and
target name.

A candidate interrupts the active interaction only when its priority is
strictly higher. Rejected faction pairs are logged once per
interaction-target combination.

Immediate interactions execute `OnStart` and `OnComplete`. Timed interactions
also execute `OnTick` and finish through either `OnComplete` or `OnCancel`.

## Architecture

- `Entity` is a `MonoBehaviour` configured in the Inspector. It registers
    itself in `EntityRegistry` and creates one `InteractionController` in
    `Awake`.

- `EntityRegistry` is a static runtime registry of currently enabled entities.
    Entities register through `OnEnable` and unregister through `OnDisable`.

- `InteractionController` is a plain C# object owned by one `Entity`. It
    discovers candidates and owns that entity's current `InteractionInstance`.

- `InteractionDefinition` and `FactionDefinition` are ScriptableObjects created
    as project assets. They contain reusable interaction and faction
    configuration.

- `InteractionInstance` is a plain runtime object created by
    `InteractionController` whenever an interaction starts. It stores elapsed
    time and drives the effect lifecycle.

- `InteractionAvailability`, `InteractionSelector`, and
    `InteractionPriorityPolicy` are stateless static services responsible for
    faction filtering, deterministic candidate selection, and interruption
    rules.

- `Effect` is an abstract ScriptableObject. Concrete effect assets implement
    lifecycle callbacks and are referenced by `InteractionDefinition`.

- `InteractionContext` is an immutable value passed to effects. It contains the
    initiator, target, and selected interaction definition.

- `EntityStats` and `EntityVisual` are MonoBehaviour components that store
    mutable per-entity state. Shared ScriptableObject assets do not store runtime
    interaction state.


## Effects

Effects are reusable ScriptableObject assets:

- `EmitLogEffect` emits a configurable message.
- `ChangeColorEffect` changes the target's visual color.
- `RestoreHealthOverTimeEffect` gradually restores health.
- `ApplyDamageEffect` immediately reduces health.

Mutable runtime state is stored on scene components such as `EntityStats`, not
inside shared effect assets.

## Adding a New Effect

Add the effect class under `Assets/Scripts/Effects`, deriving it from `Effect`.
Override only the required lifecycle callbacks: `OnStart`, `OnTick`,
`OnComplete`, or `OnCancel`.

Create its asset under `Assets/Definitions/Effect`, then create an
`InteractionDefinition` under `Assets/Definitions/Interactions`. Configure its
kind, duration, priority, allowed faction pairs and effect reference, then add
the interaction to an Entity through the Inspector.

A new effect does not require changes to:

- `InteractionController`;
- `InteractionSelector`;
- `InteractionInstance`;
- `InteractionAvailability`;
- `InteractionPriorityPolicy`;
- `EntityRegistry`.

An effect may require a new scene component when it introduces new mutable
state, but the interaction pipeline remains unchanged.

## Scope and Further Work

Cut for time:

- conditions beyond faction-pair checks;
- distance, cooldown and one-shot rules;
- multiple effects per interaction;
- runtime UI and configurable diagnostics;
- automated Edit Mode and Play Mode tests.

With three additional days, I would prioritize:

1. Add tests for faction filtering, deterministic selection, completion and
    interruption.
2. Add reusable availability conditions for distance, health, cooldowns,
    required components and runtime flags.
3. Support ordered effect collections per interaction.
4. Add health bars, active-interaction labels and visible cancellation feedback.
5. Improve lifecycle robustness by clamping the final tick and cancelling
    interactions when entities or targets are disabled.