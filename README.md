# Unity Interaction System

A data-driven interaction system implemented in Unity 6 using 
ScriptableObject definitions, faction-based availability rules, priorities, 
timed interactions, interruption, and reusable effects.

## Engine version
Unity 6.5 (6000.5.9f1)

## Interaction Selection

Every enabled Entity is registered in EntityRegistry.

At its configured update interval, an entity:

1. Evaluates its interaction definitions against all other registered entities.
2. Rejects candidates whose initiator-target faction pair is not allowed.
3. Sorts valid candidates by descending priority.
4. Resolves equal priorities by interaction identifier and target display name.
5. Starts the selected interaction.
6. Interrupts the current interaction only when the new candidate has a strictly higher priority.

Rejected faction pairs are logged once per interaction-target combination to avoid Console spam.

## Effects

Effects are reusable ScriptableObject assets implementing lifecycle callbacks:

- EmitLogEffect - emits a configurable message using {initiator} and
    {target} placeholders.
- ChangeColorEffect - changes the target's visual color.
- RestoreHealthOverTimeEffect - gradually restores target health.
- ApplyDamageEffect - immediately reduces target health.

Runtime state is stored on scene components such as EntityStats, not inside effect ScriptableObjects.

## Adding a New Interaction Effect

To add a new effect:

1. Create a class derived from Effect.
2. Override only the lifecycle methods required by the behavior:
    - OnStart for immediate application or initialization;
    - OnTick for behavior applied over time;
    - OnComplete for normal completion;
    - OnCancel for interruption cleanup.
3. Add CreateAssetMenu to the class.
4. Create and configure an effect asset.
5. Create an InteractionDefinition asset.
6. Assign the effect, interaction kind, duration, priority, and allowed faction pairs.
7. Add the interaction definition to an Entity in the Inspector.

Selection, faction filtering, priority handling, timing, completion, and interruption 
are handled by the existing interaction pipeline.

A new effect may require a new target component when it introduces new mutable 
state. For example, a mana effect would require an EntityMana component, but
it would still not require changes to the interaction system itself.

## Project Structure

Assets/
|-- Definitions/
|   |-- Effect/
|   |-- Factions/
|   |-- Interactions/
|-- Scenes/
|   |-- DemoScene.unity
|-- Scripts/
    |-- Definitions/
    |-- Effects/
    |-- Entity/
    |-- Interactions/

Key classes:
- Entity: owns a faction and available interaction definitions.
- EntityRegistry: tracks enabled entities.
- InteractionController: evaluates candidates and manages the current interaction.
- InteractionSelector: performs deterministic priority-based selection.
- InteractionInstance: owns runtime lifecycle state.
- InteractionAvailability: validates faction pairs.
- Effect: base class for reusable interaction effects.

## Design Notes

Interaction definitions and effects are separated from runtime state:

- ScriptableObjects describe reusable configuration and behavior.
- InteractionInstance stores per-execution state such as elapsed time.
- Scene components store mutable entity state such as health and visual color.

This allows new factions, interactions, and effects to be added without
modifying the interaction controller.

## Scope and Further Work

The implementation focuses on the core interaction pipeline and an automated
scene that demonstrates faction filtering, deterministic selection, immediate
and timed execution, effect lifecycle, and priority-based interruption.

The following items were cut for time:

- reusable interaction conditions beyond faction-pair checks;
- distance and spatial availability rules;
- cooldowns and one-shot interactions;
- automated Edit Mode and Play Mode tests;
- runtime UI for health, active interactions, and priorities;
- support for combining multiple effects in one interaction;
- production-level diagnostics and configurable logging;

With three additional days, I would prioritize:

1. **Availability conditions**
    Introduce reusable condition objects such as health thresholds, distance,
    cooldown, required components, and runtime flags. This would prevent
    interactions from becoming candidates when their effects cannot be applied.

2. **Multiple effects per interaction**
    Replace the single effect reference with an ordered collection so one
    interaction could, for example, apply damage, change color, and emit a log
    without requiring a combined effect class.

3. **Improved runtime feedback**
    Add health bars, active-interaction labels, priority display, and visible
    cancellation feedback so the demo can be understood without relying on the
    Console.

4. **Editor validation**
    Add clearer validation errors for incompatible configurations, missing
    components, invalid durations, empty identifiers, and duplicated faction
    pairs.

5. **Lifecycle robustness**
    Clamp the final timed tick to the remaining duration, cancel interactions
    when an Entity is disabled, and add explicit cleanup for destroyed targets.

