using UnityEngine;

public class SpikesCircle : SkillBase
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Activate()
    {
        Debug.Log("Spikes Circle");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            // other.GetComponent<Enemy>().GetDamage(damage);
        }
    }
}
