using UnityEngine;

public class DestroyBulls : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SM_Bull bull = other.gameObject.GetComponent<SM_Bull>();
        if (bull == null) return;

        Destroy(other.gameObject);
    }
}
