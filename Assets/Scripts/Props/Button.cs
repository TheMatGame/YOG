using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, HitInterface
{
    public bool on = false;
    public bool needKey = false;
    public UnityEvent evento;

    public void Hit(GameObject actor)
    {
        if (!needKey && !on)
            evento.Invoke();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (needKey && collision.gameObject.CompareTag("Key") && !on) {
            evento.Invoke();
            // Object.Destroy(collision.gameObject);
        }

    }
}
