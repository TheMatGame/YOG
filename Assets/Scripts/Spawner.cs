using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    private GameObject spawnedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject == null) {
            SpawnObject();
        }
    }

    void SpawnObject() {
        spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
