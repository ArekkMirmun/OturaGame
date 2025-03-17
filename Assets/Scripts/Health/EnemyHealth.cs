using UnityEngine;
namespace Health
{

public class EnemyHealth : MonoBehaviour, IDamageable
{        
    public float currentHealth;
    public bool dead = false;

    public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            currentHealth = currentHealth > 0 ? currentHealth : 0;
            if (currentHealth <= 0)
            {
                Debug.Log(name + " vidaENEMIGO: " + currentHealth);
                Die(); 
            }
        }
    
        public void Die()
        {
            dead = true;
            Debug.Log("El enemigo ha muerto.");
            gameObject.GetComponent<EnemyController>().GetAnimator().SetTrigger("die");
            gameObject.GetComponent<EnemyController>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject,2.5f);
            // TODO Death logic and/or another event to delegate the efects of the death.
        }
}
}