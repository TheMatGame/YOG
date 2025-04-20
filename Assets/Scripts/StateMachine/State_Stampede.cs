using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class State_Stampede : State
{
    GameObject owner;
    GameObject target;
    float speed;
    float ycords;

    float timer = 3f;
    bool charging = false;
    bool stuned = false;
    Quaternion lockRotation;
    LayerMask whatIsGround;

    public State_Stampede(GameObject owner, GameObject target, float speed = 1f)
    {
        this.owner = owner;
        this.target = target;
        this.speed = speed;
    }

    public override void EnterState()
    {
        ycords = owner.transform.position.y;
        lockRotation = owner.transform.rotation;
        charging = true;
        whatIsGround = LayerMask.NameToLayer("whatIsGround");
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if (charging) {
            timer -= Math.Min(timer,Time.deltaTime);
            if (timer <= 0) {
                charging = false;
                timer = 3f;
            }
        }
        else if (!charging && !stuned) {
            owner.transform.rotation = lockRotation;

            RaycastHit hitInfo;
            bool hit = Physics.Linecast(owner.transform.position, owner.transform.position + owner.transform.forward*100f, out hitInfo);
            if (!hit) Debug.Log("NO HIT !!!!!!!!!!!!!!!!");

            Vector3 nextPosition = Vector3.MoveTowards(owner.transform.position, hitInfo.point, 0.05f*speed);
            bool wallHit = Physics.CheckSphere(owner.transform.position + owner.transform.forward * 5f, 1f, whatIsGround);
            
            if (wallHit) {
                stuned = true;
            }
            else {
                owner.transform.position = new Vector3(nextPosition.x, ycords, nextPosition.z);
            }
        }
        else if (stuned) {
            //Necesito spawnear cohetes o algo 
            timer -= Math.Min(timer,Time.deltaTime);
            if (timer <= 0) {
                stuned = false;
                timer = 3f;
            }
        }
    }
}
