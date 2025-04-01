using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        HitInterface hitInterface = other.gameObject.GetComponent<HitInterface>();
        if (hitInterface == null) return;

        hitInterface.Hit(gameObject);
        Destroy(gameObject);
    }
}
