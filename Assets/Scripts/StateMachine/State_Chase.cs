using UnityEngine;

public class State_Chase : State
{
    GameObject owner;
    GameObject target;
    float speed;
    float ycords;

    public State_Chase(GameObject owner, GameObject target, float speed = 1f)
    {
        this.owner = owner;
        this.target = target;
        this.speed = speed;
    }

    public override void EnterState()
    {
        ycords = owner.transform.position.y;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        Vector3 nextPosition = Vector3.MoveTowards(owner.transform.position, target.transform.position, 0.05f*speed);
        owner.transform.position = new Vector3(nextPosition.x, ycords, nextPosition.z);
    }
}
