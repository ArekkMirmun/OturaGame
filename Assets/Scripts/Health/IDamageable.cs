/// <summary>
/// Interface to damage entities. It can be implemented for damage the player, enemies, objects etc.
/// </summary>

public interface IDamageable
{
    /// <summary>
    /// To recieve damage
    /// </summary>
    /// <param name="amount"> amount of damage taken</param>
    void TakeDamage(float amount);  
    
    /// <summary>
    /// For dying.
    /// </summary>
    void Die();  
}
