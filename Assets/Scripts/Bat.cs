using UnityEngine;
using UnityEngine.InputSystem;

public class Bat : MonoBehaviour
{
    [SerializeField]private Camera playerCamera;

    [SerializeField]private Camera batCamera;
    
    private bool batActive = false;
    
    [SerializeField]private GameObject batObject;

    [SerializeField]private MeshRenderer[] playerRenderers;
    [SerializeField]private Player player;
    
    [SerializeField]private ParticleSystem batParticles;
    [SerializeField]private float batSpeed = 20f;

    private float baseSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseSpeed = player.speed;
        SetBat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBat(InputValue value)
    {
        batActive = !batActive;
        SetBat();
        if (batActive)
        {
            player.speed = batSpeed;
        }
        else
        {
            player.speed = baseSpeed;
        }
        ParticleSystem batObject = Instantiate(batParticles, this.batObject.transform.position, Quaternion.identity);
        Destroy(batObject, 1f);
    }

    //Set the bat and player body when transforming
    private void SetBat()
    {
        playerCamera.enabled = !batActive;
        batCamera.enabled = batActive;
        batObject.SetActive(batActive);
        batObject.SetActive(batActive);
        foreach (MeshRenderer meshRenderer in playerRenderers)
        {
            meshRenderer.enabled = !batActive;
        }
    }
}
