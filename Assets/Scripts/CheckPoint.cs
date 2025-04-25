using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] LevelController levelController;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            levelController.SetCheckpoint(gameObject.transform.position);
        }
    }
}
