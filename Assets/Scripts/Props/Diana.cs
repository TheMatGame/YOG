using UnityEngine;
using UnityEngine.Events;

public class Diana : MonoBehaviour, HitInterface
{
    public UnityEvent evento;

    public void Hit(GameObject actor)
    {
        evento.Invoke();
    }
}
