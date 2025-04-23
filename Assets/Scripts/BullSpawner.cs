using UnityEngine;

public class BullSpawner : MonoBehaviour
{

    [SerializeField] GameObject bull;
    [SerializeField] int num;
    [SerializeField] Transform spawnPoint;
    [SerializeField] AudioSource music;

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (bull == null) return;
        
        int x = 0;
        int z = 0;

        for (int i = 0; i < num; i++) {
            z += 2;
            if (z >= 10) {
                z = 0;
                x += 2;
            }
            Vector3 pos = new Vector3(spawnPoint.position.x + x, spawnPoint.position.y, spawnPoint.position.z + z);
            Instantiate(bull, pos, spawnPoint.rotation);
        }

        music.Play();

    }
}
