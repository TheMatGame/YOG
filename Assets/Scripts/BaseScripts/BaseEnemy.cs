using System;
using UnityEngine;

public class BaseEnemy : GravityController, HitInterface
{
    public int lives = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HitInterface.Hit(GameObject actor)
    {
        Hit(actor);
    }

    virtual protected void Hit(GameObject actor) 
    {
        --lives;
        BounceOff(actor);
        IsAlive();
    }

    void BounceOff(GameObject actor) {
        PlayerController playerController = actor.GetComponent<PlayerController>();
        if (playerController) {
            transform.forward = playerController.GetPlayerForward();
        }
    }

    protected bool IsAlive() {
        return lives > 0;
    }

    protected void TakeDamage() {
        lives = Math.Max(0, --lives);
    }

}
