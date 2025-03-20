using UnityEngine;

public class BaseEnemy : GravityController, HitInterface
{
    private enum EnemyState {
        Moving,
        Grabbed,
        Air,
        Stuned
    }

    EnemyState enemyState = EnemyState.Moving;
    public int lives = 1;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();   // PUEDE QUE HAYA UN PROBLEMA AL LLAMAR A LOS HIJOS, ESTE NO DEBERIA HACER NADA O EL HIJO NO DEBERIA HACER NADA
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HitInterface.Hit(GameObject actor)
    {
        --lives;
        BounceOff(actor);
        CheckIfAlive();
    }

    void BounceOff(GameObject actor) {
        PlayerController playerController = actor.GetComponent<PlayerController>();
        if (playerController) {
            transform.forward = playerController.GetPlayerForward();
        }
    }

    void CheckIfAlive() {
        if (lives <= 0) Destroy(gameObject);
    }

}
