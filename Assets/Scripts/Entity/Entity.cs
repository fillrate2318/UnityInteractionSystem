using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private string displayName;
    [SerializeField] private FactionDefinition faction;
    [SerializeField] private InteractionDefinition[] interactions;
    
    [SerializeField] private float updateInterval = 1;

    private InteractionController controller;
    private float delta;
    
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
        delta += Time.deltaTime;
        if (delta >= updateInterval)
        {
            delta = 0;
            controller.EvaluateInteractions();
        }
    }

    private void OnDisable()
    {
        EntityRegistry.UnregisterEntity(this);
    }
}
