using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bat : MonoBehaviour
{
    [SerializeField]private Player player;
    [Header("Player")]
    [SerializeField]private Camera playerCamera;
    [SerializeField]private GameObject playerObject;
    
    [Header("Bat")]
    [SerializeField]private Camera batCamera;
    
    private bool batActive = false;
    
    [SerializeField]private GameObject batObject;

    
    
    
    [SerializeField]private ParticleSystem batParticles;
    [SerializeField]private float batSpeed = 20f;
    [SerializeField]private AudioSource soundEffect;

    private float baseSpeed;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseSpeed = player.speed;
        rb = player.GetComponent<Rigidbody>();
        SetBat();
    }

    // Update is called once per frame
    void Update()
    {
        if (batActive && player.CheckForGround())
        {
            batActive = false;
            SetBat();
        }
    }

    private void OnBat(InputValue value)
    {
        if (!batActive && !player.CheckForGround() || batActive)
        {
            batActive = !batActive;
            SetBat();
        }

    }

    //Set the bat and player body when transforming
    private void SetBat()
    {
        soundEffect.Play();
        playerCamera.enabled = !batActive;
        batCamera.enabled = batActive;
        batObject.SetActive(batActive);
        batObject.SetActive(batActive);
        playerObject.SetActive(!batActive);
        
        if (batActive)
        {
            player.speed = batSpeed;
            rb.useGravity = false;
            
            //stop vertical movement
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        }
        else
        {
            player.speed = baseSpeed;
            rb.useGravity = true;
        }
        ParticleSystem batParticle = Instantiate(batParticles, this.batObject.transform.position, Quaternion.identity);
        StartCoroutine(StopParticleEffect(batParticle,0.5f));
    }
    IEnumerator StopParticleEffect(ParticleSystem p, float delay)
    {
        yield return new WaitForSeconds(delay);
        p.Stop();
    }
}
