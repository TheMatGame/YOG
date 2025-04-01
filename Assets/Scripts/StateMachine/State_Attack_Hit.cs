using UnityEditor;
using UnityEngine;

public class State_Attack_Hit : State
{
    [SerializeReference] float cooldown = 1f;
    private float timer;
    GameObject owner;
    LayerMask whatIsPlayer;

    public State_Attack_Hit(GameObject owner, LayerMask whatIsPlayer)
    {
        this.owner = owner;
        this.whatIsPlayer = whatIsPlayer;
    }

    public override void EnterState()
    {
        timer = cooldown;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else {
            timer = cooldown; 
            Attack();
        }
    }

    void Attack() {
        Vector3 attackPoint = owner.transform.position + owner.transform.forward;
        Collider[] colliders = Physics.OverlapSphere(attackPoint, 1f, whatIsPlayer);
        MyFunctions.DrawWireSphere(attackPoint, 1f, Color.black, 0.5f);
        
        if (colliders.Length > 0) {
            HitInterface hitInterface = colliders[0].gameObject.GetComponent<HitInterface>();

            if (hitInterface == null) return;

            hitInterface.Hit(owner);
        }
    }
}
