using System.Threading;
using Unity.VisualScripting;
using UnityEngine;


// Esta clase es hija de BaseObject, con lo cual se puede agarrar y lanzar.
// Esta clase se usa para los explosivos y aunque tiene un tiempo de explosion,
// EXPLOTAN AL CONTACTO CON OBJETOS
public class Explosive : BaseObject
{

    public float timeTillExplosion = 3f;
    private float timer = 0;

    public float explosionRadius = 5f;

    private bool on = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        timer = timeTillExplosion;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (on) timer -= Time.deltaTime;
        if (on && timer <= 0) Explode();
    }

    public override void Release()
    {
        base.Release();
        on = true;
    }

    private void Explode() {
        // Animation
        // Sound

        // Damage
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            HitInterface hitInterface = collider.gameObject.GetComponent<HitInterface>();
            if (hitInterface != null) {
                hitInterface.Hit(gameObject);
            }
        }

        // Destroy
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        string layer = LayerMask.LayerToName(collision.gameObject.layer);
        if (on && layer != "Player") {
            Explode();
        }
    }

}
