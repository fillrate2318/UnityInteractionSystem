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

Every enabled `Entity` is registered in `EntityRegistry`. At its configured
update interval, an entity:

1. Evaluates its definitions against other registered entities.
2. Rejects candidates whose directed faction pair is not allowed.
3. Selects the candidate with the highest priority.
4. Resolves ties by interaction identifier and target display name.
5. Starts the selected interaction.
6. Interrupts the current interaction only if the new priority is strictly higher.

Rejected faction pairs are logged once per interaction-target combination.

Immediate interactions execute `OnStart` and `OnComplete` in the same
evaluation. Timed interactions execute `OnStart`, `OnTick`, and either
`OnComplete` or `OnCancel`.

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