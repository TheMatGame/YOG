using UnityEngine;

public class AgresiveEnemy : BaseEnemy
{
    public StateMachine stateMachine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Hit(GameObject actor) 
    {
        TakeDamage();
        
        if (!IsAlive()) Destroy(gameObject); 
    }

}
