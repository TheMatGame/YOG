using UnityEngine;

public class PlatformMomentum : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform);
    }

    void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
}
