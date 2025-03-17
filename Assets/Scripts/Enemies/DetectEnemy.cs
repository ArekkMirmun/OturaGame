using Health;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    private Player p;

    void Start()
    {
        p = GameObject.Find("----- PLAYER -----/Player").GetComponent<Player>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(p.isAttacking && other.CompareTag("Enemy")){
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(10);
        }else{return;}
    }
}
