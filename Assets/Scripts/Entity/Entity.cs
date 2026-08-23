using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private string displayName;
    [SerializeField] private FactionDefinition faction;
    [SerializeField] private InteractionDefinition[] interactions;
    [SerializeField] private float updateInterval = 1f;

    public string DisplayName => displayName;
    public FactionDefinition Faction => faction;
    public IReadOnlyList<InteractionDefinition> Interactions => interactions;

    private InteractionController controller;
    private float elapsedTime;

    private void OnValidate()
    {
        if (string.IsNullOrWhiteSpace(displayName))
        {
            Debug.LogWarning("Entity has no display name.", this);
        }

        if (faction == null)
        {
            Debug.LogWarning("Entity has no faction.", this);
        }

        if (interactions == null)
        {
            Debug.LogWarning("Entity interactions are not initialized.", this);
        }
        
        if (updateInterval <= 0)
        {
            Debug.LogWarning("Entity update interval must be greater than 0.", this);
        }
    }

    private void Awake()
    {
        controller = new InteractionController(this);
    }

    private void OnEnable()
    {
        EntityRegistry.RegisterEntity(this);
    }

    private void Update()
    {
        ControllerUpdate(Time.deltaTime);
    }

    private void OnDisable()
    {
        EntityRegistry.UnregisterEntity(this);
    }

    private void ControllerUpdate(float deltaTime)
    {
        controller.Tick(deltaTime);
        elapsedTime += deltaTime;
        if (elapsedTime >= updateInterval)
        {
            elapsedTime = 0;
            controller.EvaluateInteractions();
        }
    }
}
