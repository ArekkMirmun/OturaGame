using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform Player;
    public NavMeshAgent Agent;
    public float ChaseSpeed = 4f;
    public float PatrolSpeed = 2f;
    public float AttackRange = 1.5f;
    public float VisionRange = 10f;
    
    private EnemyState currentState;
    [SerializeField] private Animator anim;
    bool detected = false;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        ChangeState(new PatrolState(this));
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        if (currentState is PatrolState)
        {
            detected = false;
            anim.SetBool("detected", detected);
        }else if(currentState is ChaseState)
        {
            anim.SetBool("attack",false);
             detected = true;
            anim.SetBool("detected", detected);
        }
        
    }

    public bool CanSeePlayer()
    {
        return Vector3.Distance(transform.position, Player.position) < VisionRange;
    }

    public bool CanAttackPlayer()
    {
        return Vector3.Distance(transform.position, Player.position) < AttackRange;
    }

    public void Attack()
    {
        anim.SetBool("attack",true);
        Debug.Log("Atacando al jugador...");
        
        // Aquí agregas animaciones y lógica de ataque.
    }

    public void SetRandomDestination()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * 10f;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
        {
            Agent.SetDestination(hit.position);
        }
    }

    public void Die(){
        anim.SetBool("die", true);
        Destroy(gameObject, 2f);
    }
}
