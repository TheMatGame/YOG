using UnityEngine;

public class SM_BigBull : MonoBehaviour
{
    public bool usingAI = false;
    [Header("Debug Variables")]
    public bool animations = false;
    public enum States {
        Idle,
        Stampede,
    }

    public States currentStateName = States.Idle;
    public State currentState = new State_Idle();

    Animator animator;
    
    public float sightRange;  
    bool isInSight, isAttacking;
    public LayerMask whatIsPlayer;

    GameObject target;

    void Start() 
    {
        animator = GetComponent<Animator>();
        currentState.EnterState();
    }

    void Update() 
    {
        if (!usingAI) return;

        if (target == null) return;

        Vector3 playerDirection = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(playerDirection, transform.up), transform.up);
    }

    void FixedUpdate()
    {
        if (!usingAI) return;
        Collider[] sightHits = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
        // MyFunctions.DrawWireSphere(transform.position, sightRange, Color.gray, 0.1f);

        if (sightHits.Length > 0) {
            isInSight = true;
            target = sightHits[0].gameObject;    // ESTO LO PODEMOS HACER PORQUE SOLO PUEDE HABER UN PLAYER
        }
        else isInSight = false;

        if (animations) {
            animator.SetBool("isInSight", isInSight);
        }

        if (!isAttacking) isAttacking = RandomAttacking();

        Conditions();

        currentState.UpdateState();
    }

    void Conditions() {
        if (currentStateName == States.Idle) {
            if (isInSight && isAttacking) ChangeStates(States.Stampede);
        }
        else if (currentStateName == States.Stampede) {
            if (!isInSight || (isInSight && !isAttacking)) ChangeStates(States.Idle);
        }
    }

    void ChangeStates(States state) {
        currentState.ExitState();

        if (state == States.Idle) {
            currentStateName = States.Idle;
            currentState = new State_Idle();
        }
        else if (state == States.Stampede) {
            currentStateName = States.Stampede;
            currentState = new State_Stampede(gameObject, target,3);
        }

        currentState.EnterState();
    }

    void OnCollisionEnter(Collision collision)
    {
        print("Collision enter");
        HitInterface hitInterface = collision.gameObject.GetComponent<HitInterface>();
        if (hitInterface == null) return;

        hitInterface.Hit(gameObject);
    }

    bool RandomAttacking() {
        return 10 < Random.Range(0,100);
    }

}