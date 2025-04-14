using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Animations;

public class BaseObject : GravityController, GrabInterface
{
    public enum ObjectState {
        Still,
        Grabbed,
        Air
    }

    public ObjectState objectState = ObjectState.Still;

    private PlayerController playerController;
    private LayerMask defaultLayer;
    private LayerMask noPlayerLayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start() 
    {
        base.Start();
        defaultLayer = LayerMask.NameToLayer("Default");
        noPlayerLayer = LayerMask.NameToLayer("NoPlayerCollision");
    }
    

    // Update is called once per frame
    protected virtual void Update()
    {
        if (objectState == ObjectState.Grabbed) {
            transform.position = playerController.holdPosition.position;
        }
        else if (objectState == ObjectState.Air) {
            if (rb.linearVelocity.magnitude <= 0) {
                gameObject.layer = defaultLayer;
                objectState = ObjectState.Still;
            }
        }
    }

    void GrabInterface.Grab(GameObject actor)
    {
        // No lo estoy usando como tal, asi que podriamos hacerlo con tag player
        playerController = actor.GetComponent<PlayerController>(); 
        if (!playerController) return;

        objectState = ObjectState.Grabbed;
        gameObject.layer = noPlayerLayer;
    }

    public virtual void Release()
    {
        objectState = ObjectState.Air;
        // Mirar si la posicion entre jugador y objeto es superior a un umbral
    }
}
