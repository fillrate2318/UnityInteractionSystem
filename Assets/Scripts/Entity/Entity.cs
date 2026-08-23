using System;
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
