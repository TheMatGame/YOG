using UnityEngine;
using UnityEngine.Events;

public class Diana : MonoBehaviour
{
    public UnityEvent evento;

    void OnCollisionEnter(Collision collision)
    {
        GrabInterface grabbable = collision.gameObject.GetComponent<GrabInterface>();

        if (grabbable == null) return;

        evento.Invoke();
    }
}
