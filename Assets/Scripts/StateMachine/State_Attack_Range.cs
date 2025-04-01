using UnityEditor;
using UnityEngine;

public class State_Attack_Range : State
{
    [SerializeReference] float cooldown = 1f;
    [SerializeReference] GameObject projectilePrefab;
    private float timer;
    GameObject owner;
    LayerMask whatIsPlayer;

    public State_Attack_Range(GameObject owner, LayerMask whatIsPlayer)
    {
        this.owner = owner;
        this.whatIsPlayer = whatIsPlayer;

        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
        if (projectilePrefab == null)
        {
            Debug.LogError("¡Prefab del proyectil no encontrado!");
        }
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
            Shoot();
        }
    }

    void Shoot() {
        Vector3 attackPoint = owner.transform.position + owner.transform.forward;

        GameObject projectile = GameObject.Instantiate(projectilePrefab, attackPoint, owner.transform.rotation);

        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null) {
            proj.Launch(owner.transform.forward);
        }
    }
}
