using UnityEngine;


// Esta clase otorga la propiedad de destruirse a cualquier objeto,
// puede ser tanto por cualquier cosa, patadas, enemigos, etc.
// o unicamente por objetos, como podrian ser explosivos.
public class Breakable : MonoBehaviour, HitInterface
{
    [SerializeField] bool playerCanBreak = true;
    [SerializeField] GameObject particle;

    public void Hit(GameObject actor)
    {
        if (!playerCanBreak && actor.gameObject.CompareTag("Player")) return;
        
        DestroyBox();
    }

    void DestroyBox() {
        // Play breaking animation 
        Instantiate(particle, transform.position, transform.rotation);
        // Play breaking sound
        Destroy(gameObject);
    }

}
