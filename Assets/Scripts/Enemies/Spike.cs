using UnityEngine;
using Health;

public class Spike : MonoBehaviour
{
    public bool pinchoSuelo = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(10);
            if(!pinchoSuelo)
            Destroy(gameObject);
        }else if(other.CompareTag("NoEnemy")){Destroy(gameObject);}
        Destroy(gameObject,6f);
       
    }
}
