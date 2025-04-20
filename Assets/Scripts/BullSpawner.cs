using UnityEngine;

public class BullSpawner : MonoBehaviour
{

    [SerializeField] GameObject bull;
    [SerializeField] int num;
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (bull == null) return;

        for (int i = 0; i < num; i++) {
            Instantiate(bull, transform.position, transform.rotation);
        }

    }
}
