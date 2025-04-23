using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    private GameObject spawnedObject;
    bool locked = false;

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

        if (spawnedObject.transform.parent == gameObject.transform && locked) {
            spawnedObject.transform.position = gameObject.transform.position;
        }
        else if (spawnedObject.transform.parent != gameObject.transform) locked = false;
    }

    void SpawnObject() {
        spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
        spawnedObject.transform.SetParent(gameObject.transform);
        locked = true;
    }
}
