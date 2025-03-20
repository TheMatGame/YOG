using UnityEngine;

public class SphereGravityModifier : GravityModifier
{
    bool active = false;
    void Update()
    {
        if (active) CalculateGravityDirectionSphere();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        playerController = other.gameObject.GetComponent<PlayerController>();
        if (!playerController) return;

        // Save initial gravity parameters
        initialGravityDirection = playerController.gravityDirection;
        initialGravityForce = playerController.gravityForce;
        active = true;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        active = false;
    }

    void CalculateGravityDirectionSphere() {
        Vector3 dir = transform.position - playerController.transform.position;
        Debug.DrawLine(transform.position, playerController.transform.position - dir.normalized * 100, Color.black, 0.1f);
        gravityDirection = dir.normalized;
        // playerController.ChangeGravity(gravityDirection,gravityForce);
    }
}
