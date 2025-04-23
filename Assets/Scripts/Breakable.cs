using System;
using UnityEngine;


// Esta clase otorga la propiedad de destruirse a cualquier objeto,
// puede ser tanto por cualquier cosa, patadas, enemigos, etc.
// o unicamente por objetos, como podrian ser explosivos.
public class Breakable : MonoBehaviour, HitInterface
{
    [SerializeField] bool playerCanBreak = true;
    [SerializeField] GameObject particle;
    [SerializeField] bool respawn = false;
    public float respawnTime = 5f;
    private float timer;
    private bool timerOn = false;

    void Start()
    {
        timer = respawnTime;
    }

    void Update()
    {
        if (timerOn) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                timerOn = false;
                timer = respawnTime;
                gameObject.SetActive(true);
            }
        }
    }

    public void Hit(GameObject actor)
    {
        if (!playerCanBreak && actor.gameObject.CompareTag("Player")) return;
        
        DestroyBox();
    }

    void DestroyBox() {
        // Play breaking animation 
        Instantiate(particle, transform.position, transform.rotation);
        // Play breaking sound

        if (respawn) gameObject.SetActive(false);
        else Destroy(gameObject);
    }

}
