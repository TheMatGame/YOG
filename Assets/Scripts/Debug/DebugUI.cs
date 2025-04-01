using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI velocity;
    public TextMeshProUGUI grounded;
    public TextMeshProUGUI jumpDirection;

    void Update()
    {
        if (!playerController) return;

        velocity.text = "Velocity: " + playerController.GetRigidbody().linearVelocity;
        grounded.text = "Grounded: " + playerController.grounded;
        jumpDirection.text = "JD: " + playerController.jumpDirection;
    }
}
