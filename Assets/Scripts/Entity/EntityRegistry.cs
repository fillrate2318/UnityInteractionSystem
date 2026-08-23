using System.Collections.Generic;

public static class EntityRegistry
{
    public static IReadOnlyList<Entity> Entities => entities;
    
    private static readonly List<Entity> entities = new List<Entity>();

    public static void RegisterEntity(Entity entity)
    {
        if (!entities.Contains(entity))
        {
            entities.Add(entity);
        }
    }

    public static void UnregisterEntity(Entity entity)
    {
        entities.Remove(entity);
    }
}
