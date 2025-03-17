using UnityEngine;
using UnityEngine.AI;
using Health;

public class StatueController : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public float speed = 3.5f; // Velocidad del enemigo
    private NavMeshAgent agent;
    private Camera mainCamera;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        mainCamera = Camera.main;
        
    }

    void Update()
    {
        if (IsVisible())
        {
            agent.ResetPath(); // Se detiene si no lo ve
            
        }
        else
        {
            agent.SetDestination(player.position); // Persigue al jugador solo si lo ve
        }

        
    }

    void FixedUpdate(){
        
        if(Vector3.Distance(transform.position, player.position) < 2.5f){       
             PlayerHealth player = global::Player.Instance.GetComponent<PlayerHealth>();
        player.TakeDamage(5);}
    }

    bool IsVisible()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);
        bool isInView = viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0;

        return isInView; // Devuelve true si está en la pantalla
    }

    
}

