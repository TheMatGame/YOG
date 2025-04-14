using UnityEngine;


// Esta clase otorga la propiedad de destruirse a cualquier objeto,
// puede ser tanto por cualquier cosa, patadas, enemigos, etc.
// o unicamente por objetos, como podrian ser explosivos.
public class Breakable : MonoBehaviour, HitInterface
{
    [SerializeField] bool onlyObjectsCanBreak = true;

    public void Hit(GameObject actor)
    {
        if (onlyObjectsCanBreak) {
            
            BaseObject baseObject = actor.gameObject.GetComponent<BaseObject>();
            if (baseObject == null) return;

            DestroyBox();
        }
        else DestroyBox();
    }

    void DestroyBox() {
        // Play breaking animation 
        // Play breaking sound
        Destroy(gameObject);
    }

}
