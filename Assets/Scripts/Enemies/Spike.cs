using UnityEngine;
using Health;

public class Spike : MonoBehaviour
{
    public bool pinchoSuelo = false;
    void OnTriggerStay(Collider other)
    {
        Debug.Log("SE HA CHOCADO CON:"+other.name);
        if (other.CompareTag("Player")) return;

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(10);
            if(!pinchoSuelo)
            Destroy(gameObject);
        }else if(other.CompareTag("NoEnemy") && !pinchoSuelo){Destroy(gameObject);}
        Destroy(gameObject,6f);
       
    }
}
