using UnityEditor;
using UnityEngine;

public class State_Attack_Explosion : State
{
    float countDown = 3f;
    GameObject owner;
    LayerMask whatIsPlayer;

    public State_Attack_Explosion(GameObject owner, LayerMask whatIsPlayer)
    {
        this.owner = owner;
        this.whatIsPlayer = whatIsPlayer;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if (countDown > 0) countDown -= Time.deltaTime;
        else {
            Explosion();
        }
    }

    void Explosion() {
        Collider[] colliders = Physics.OverlapSphere(owner.transform.position, 10f, whatIsPlayer);
        MyFunctions.DrawWireSphere(owner.transform.position, 10f, Color.black, 5f);
        
        if (colliders.Length > 0) {

        }

        Object.Destroy(owner);
    }
}
