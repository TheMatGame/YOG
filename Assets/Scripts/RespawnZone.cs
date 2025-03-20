using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    public Transform respawnPoint;
    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position = respawnPoint.position;
    }
}
