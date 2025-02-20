using System;
using UnityEngine;


/// <summary>
/// Class for managing the player health
/// Implements <see cref="IDamageable"/>
/// </summary>
public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField]private float maxHealth = 100f;
    private float currentHealth;
    
    public float pasiveHealAmount = 1f;
    public float tickSeconds = 5f;
    private float lastTick;
    
    /// <summary>
    /// Event that is triggered whenever the player's health changes.
    /// This event should be invoked whenever there is a change in the player's current health,
    /// allowing other systems (e.g., UI or gameplay effects) to react to health updates.
    /// </summary>
    /// <remarks>
    /// The event passes the new health value as a float when it is invoked.
    /// </remarks>
    public static event Action<float> OnChangePlayerHealth;

    /// <summary>
    /// Event that is triggered whenever the player's maximum health changes.
    /// This event should be invoked when the player's maximum health value is updated.
    /// </summary>
    /// <remarks>
    /// The event passes the new maximum health value as a float when it is invoked.
    /// </remarks>
    public static event Action<float> OnChangePlayerMaxHealth;
    

    void Start()
    {
        OnChangePlayerMaxHealth?.Invoke(maxHealth);
        currentHealth = maxHealth/2;
        OnChangePlayerHealth?.Invoke(currentHealth);
        lastTick = Time.time;
    }

    /// <summary>
    /// Take damage, make it so it's not less than 0.
    /// Invoke an event <see cref="OnChangePlayerHealth"/>
    /// If currentHealth is 0 call Die <see cref="Die"/>
    /// </summary>
    /// <param name="amount">amount of damage taken</param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = currentHealth > 0 ? currentHealth : 0;
        OnChangePlayerHealth?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log(name + " vida: " + currentHealth);
            Die(); 
        }
    }

    /// <summary>
    /// Is called when you make it die
    /// </summary>
    public void Die()
    {
        Debug.Log("El jugador ha muerto.");
        // TODO Death logic and/or another event to delegate the efects of the death.
    }

    /// <summary>
    /// Heal a amount and make it's not more than maxHealth.
    /// Send an event <see cref="OnChangePlayerHealth"/>
    /// </summary>
    /// <param name="amount">Amount of heal</param>
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
        Debug.Log("Vida: "+currentHealth);
        OnChangePlayerHealth?.Invoke(currentHealth);
    }

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        maxHealth = maxHealth > 1? maxHealth : 1;
        OnChangePlayerMaxHealth?.Invoke(maxHealth);
    }

    
    /// <summary>
    /// This method is called every frame to update the state of the player.
    /// It checks if enough time has passed since the last heal tick, 
    /// and if so, it applies passive healing.
    /// </summary>
    void Update()
    {
        // Check if enough time has passed since the last healing tick
        if (Time.time - lastTick > tickSeconds)
        {
            // Update the last tick time
            lastTick = Time.time;

            // Apply passive healing to the player
            Heal(pasiveHealAmount);
        }
    }
}