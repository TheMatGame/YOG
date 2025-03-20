using UnityEngine;

public class EffectZone : MonoBehaviour
{
    protected PlayerController playerController;

    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        playerController = other.gameObject.GetComponent<PlayerController>();

        if (!playerController) return;

        // Set State

    }

    virtual protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        if (playerController) {
            // Take state
            playerController = null;
        }
    }
}
