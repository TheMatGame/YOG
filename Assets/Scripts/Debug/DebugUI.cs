using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI velocity;
    public TextMeshProUGUI grounded;
    public TextMeshProUGUI jumpDirection;
    public TextMeshProUGUI isJumping;
    public TextMeshProUGUI isFalling;

    void Update()
    {
        if (!playerController) return;

        velocity.text = "Velocity: " + playerController.GetRigidbody().linearVelocity;
        grounded.text = "Grounded: " + playerController.grounded;
        jumpDirection.text = "JD: " + playerController.jumpDirection;
        isJumping.text = "Jumping: " + playerController.isJumping;
        isFalling.text = "Falling: " + playerController.isFalling;
    }
}
