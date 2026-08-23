using System.Collections.Generic;
using UnityEngine;

public static class EntityRegistry
{
    public static IReadOnlyList<Entity> Entities => entities;
    
    private static List<Entity> entities = new List<Entity>();

    public static void RegisterEntity(Entity entity)
    {
        entities.Add(entity);
    }

    public static void UnregisterEntity(Entity entity)
    {
        entities.Remove(entity);
    }
}
