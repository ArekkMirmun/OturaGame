public abstract class EnemyState
{
    protected EnemyController enemy; // Referencia al enemigo

    public EnemyState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public abstract void Enter();  // Se ejecuta al entrar en el estado
    public abstract void Update(); // Se ejecuta cada frame en el estado
    public abstract void Exit();   // Se ejecuta al salir del estado
}


public class PatrolState : EnemyState
{
    public PatrolState(EnemyController enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.SetRandomDestination();
    }

    public override void Update()
    {
        if (enemy.CanSeePlayer()) 
        {
            enemy.ChangeState(new ChaseState(enemy));
            return;
        }

        if (!enemy.Agent.pathPending && enemy.Agent.remainingDistance < 0.5f)
            enemy.SetRandomDestination();
    }

    public override void Exit() { }
}

public class ChaseState : EnemyState
{
    public ChaseState(EnemyController enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.Agent.speed = enemy.ChaseSpeed;
    }

    public override void Update()
    {
        enemy.Agent.SetDestination(enemy.Player.position);

        if (!enemy.CanSeePlayer())
        {
            enemy.ChangeState(new PatrolState(enemy));
            return;
        }

        if (enemy.CanAttackPlayer())
        {
            enemy.ChangeState(new AttackState(enemy));
        }
    }

    public override void Exit() { }
}

public class AttackState : EnemyState
{
    public AttackState(EnemyController enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.Agent.isStopped = true;
        enemy.StartCoroutine(enemy.Attack(enemy));
    }

    public override void Update()
    {
        if (!enemy.CanAttackPlayer())
        {
            enemy.Agent.isStopped = false;
            enemy.ChangeState(new ChaseState(enemy));
        }
    }

    public override void Exit()
    {
        enemy.Agent.isStopped = false;
    }
}


public class DyingState : EnemyState
{
    public DyingState(EnemyController enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.Agent.isStopped = true;
        enemy.Die();
    }

        public override void Update()
    {

    }

    public override void Exit()
    {

    }
}
