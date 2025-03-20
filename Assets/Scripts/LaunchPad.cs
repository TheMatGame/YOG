using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float force;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            print("Launcpading");

            PlayerController playerController = other.GetComponent<PlayerController>();

            if (!playerController) return;

            playerController.ImpulsePlayer(transform.up * force);
        }
    }
}
