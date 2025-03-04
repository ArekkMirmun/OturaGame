using UnityEngine;

public class BloodSpike : SkillBase
{
    [SerializeField] private GameObject[] spikes;
    [SerializeField] private float speed = 5;
    [SerializeField] private float damage = 5;

    public GameObject player;


    void Start()
    {
        
    }

    void Update()
    {
        // if (player.isAiming)
        if (true)
        {
        }
    }

    public override void Activate()
    {
        Debug.Log("Blood Spike");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            // other.GetComponent<Enemy>().GetDamage(damage);
        }
    }
}
