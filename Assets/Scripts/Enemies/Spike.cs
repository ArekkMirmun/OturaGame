using UnityEngine;
using Health;

public class Spike : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(10);
            Destroy(gameObject);
        }
        Destroy(gameObject,6f);
       
    }
}
