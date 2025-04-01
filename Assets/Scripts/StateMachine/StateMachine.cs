using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    // public enum States {
    //     Idle,
    //     Moving,
    //     Following,
    //     Attacking
    // }

    // public States currentStateName = States.Idle;
    // public State currentState = new State_Idle();

    // Animator animator;
    
    // public float sightRange, attackRange;  
    // bool isInSight, isInAttackRange;
    // public LayerMask whatIsPlayer;

    // GameObject target;

    // void Start() 
    // {
    //     animator = GetComponent<Animator>();
    //     currentState.EnterState();
    // }

    // void Update() 
    // {
    // }

    // void FixedUpdate()
    // {
    //     Collider[] hitColliders = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
    //     Collider[] hitColliders2 = Physics.OverlapSphere(transform.position, attackRange, whatIsPlayer);
    //     MyFunctions.DrawWireSphere(transform.position, sightRange, Color.gray, 0.1f);
    //     MyFunctions.DrawWireSphere(transform.position, attackRange, Color.red, 0.1f);

    //     if (hitColliders.Length > 0) {
    //         isInSight = true;
    //         target = hitColliders[0].gameObject;    // ESTO LO PODEMOS HACER PORQUE SOLO PUEDE HABER UN PLAYER
    //     }
    //     else isInSight = false;

    //     if (hitColliders2.Length > 0) {
    //         isInAttackRange = true;
    //         target = hitColliders2[0].gameObject;    // ESTO LO PODEMOS HACER PORQUE SOLO PUEDE HABER UN PLAYER
    //     }
    //     else isInAttackRange = false;

    //     animator.SetBool("isInSight", isInSight);
    //     animator.SetBool("isInAttackRange", isInAttackRange);


    //     Conditions();

    //     currentState.UpdateState();
    // }

    // void Conditions() {
    //     if (currentStateName == States.Idle) {
    //         if (isInSight) ChangeStates(States.Following);
    //     }
    //     else if (currentStateName == States.Moving) {
    //         if (isInSight) ChangeStates(States.Following);
    //     }
    //     else if (currentStateName == States.Following) {
    //         if (!isInSight) ChangeStates(States.Idle);
    //         else if (isInAttackRange) ChangeStates(States.Attacking);
    //     }
    //     else if (currentStateName == States.Attacking) {
    //         // if (!isInAttackRange) ChangeStates(States.Following);
    //     }
    // }

    // void ChangeStates(States state) {
    //     currentState.ExitState();

    //     if (state == States.Idle) {
    //         currentStateName = States.Idle;
    //         currentState = new State_Idle();
    //     }
    //     else if (state == States.Moving) {
    //         currentStateName = States.Moving;
    //         currentState = new State_Idle();
    //     }
    //     else if (state == States.Following) {
    //         currentStateName = States.Following;
    //         currentState = new State_Following(gameObject, target);
    //     }
    //     else if (state == States.Attacking) {
    //         currentStateName = States.Attacking;
    //         currentState = new State_Attack_Explosion(gameObject, whatIsPlayer);
    //     }

    //     currentState.EnterState();
    // }

}