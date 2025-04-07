using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Health
{
    /// <summary>
    /// Class for managing the player health.
    /// Implements <see cref="IDamageable"/>
    /// </summary>
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField]private float maxHealth = 100f;
        private float currentHealth;
    
        public float pasiveHealAmount = 1f;
        public float tickSeconds = 5f;
        private float lastTick;
        public Slider healthSlider;
    
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
            currentHealth = maxHealth;
            //currentHealth = maxHealth/2;
            OnChangePlayerHealth?.Invoke(currentHealth);
            healthSlider.value = currentHealth;
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
                healthSlider.value = currentHealth;
                Die(); 
            }
        }
    
        public void Die()
        {
           //Load menu scene
           SceneManager.LoadScene("Menu");
        }

        /// <summary>
        /// Heal a amount and make it's not more than maxHealth.
        /// Invoke event <see cref="OnChangePlayerHealth"/>
        /// </summary>
        /// <param name="amount">Amount of heal</param>
        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
            Debug.Log("Vida: "+currentHealth);
            OnChangePlayerHealth?.Invoke(currentHealth);
        }

        /// <summary>
        /// Increase MaxHealth in a amount.
        /// Doesn't let in go less than 1.
        /// Invoke event <see cref="OnChangePlayerMaxHealth"/>
        /// </summary>
        /// <param name="amount"></param>
        public void AddMaxHealth(float amount)
        {
            maxHealth += amount;
            maxHealth = maxHealth > 1? maxHealth : 1;
            OnChangePlayerMaxHealth?.Invoke(maxHealth);
        }
    
        void Update()
        {
            if (Time.time - lastTick > tickSeconds)
            {
                lastTick = Time.time;
                Heal(pasiveHealAmount);
            }
        }
    }
}