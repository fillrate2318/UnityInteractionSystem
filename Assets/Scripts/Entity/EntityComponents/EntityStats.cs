using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField] private float health = 20f;
    [SerializeField] private float maxHealth = 100f;

    public float Health => health;
    public float MaxHealth => maxHealth;

    public void RestoreHealth(float delta)
    {
        if (delta <= 0f)
        {
            return;
        }
        health = Mathf.Min(health + delta, maxHealth);
    }

    public void ApplyDamage(float delta)
    {
        if (delta <= 0f)
        {
            return;
        }
        health = Mathf.Max(health - delta, 0);
    }
}
