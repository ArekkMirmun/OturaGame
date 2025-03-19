using Health;
using UnityEngine;
using System.Collections;

public class BloodSpike : SkillBase
{
    [SerializeField] private GameObject[] spikes;
    [SerializeField] private float speed = 30f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private AudioSource soundEffect; // Sonido de la habilidad

    public GameObject player;
    private bool canUseSkill = true;

    void Start()
    {
        activationKey = KeyCode.Mouse0;
    }

    public override void Activate()
    {
        if (!canUseSkill) return;
        if (player.GetComponent<Player>().isAiming)
        {
            int randomIndex = Random.Range(0, spikes.Length);
            GameObject selectedSpike = spikes[randomIndex];

            GameObject instantiatedSpike = Instantiate(selectedSpike, shootPoint.position, Camera.main.transform.rotation);
            instantiatedSpike.SetActive(true);
            soundEffect.Play();

            // Movimiento del pincho
            Rigidbody rb = instantiatedSpike.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.linearVelocity = instantiatedSpike.transform.forward * speed; // Corregido para usar 'velocity' correctamente
            }
            else
            {
                instantiatedSpike.transform.position += instantiatedSpike.transform.forward * speed * Time.deltaTime;
            }

            Destroy(instantiatedSpike, range);

            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        canUseSkill = false;
        yield return new WaitForSeconds(cooldown);
        canUseSkill = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(10);
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
