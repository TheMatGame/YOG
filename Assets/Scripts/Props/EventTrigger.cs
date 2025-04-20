using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public bool shouldBePlayer = false;
    public UnityEvent triggerEvent;
    void OnTriggerEnter(Collider other)
    {
        if (shouldBePlayer && !other.gameObject.CompareTag("Player")) return;

        triggerEvent.Invoke();
    }
}
