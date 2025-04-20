using UnityEngine;

public class State_Following : State
{
    GameObject owner;
    GameObject target;
    float speed;

    public State_Following(GameObject owner, GameObject target, float speed = 1f)
    {
        this.owner = owner;
        this.target = target;
        this.speed = speed;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, target.transform.position, 0.05f*speed);
    }
}
