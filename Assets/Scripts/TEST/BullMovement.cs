using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class BullMovement : MonoBehaviour
{
    public bool bigBull = false;

    private bool moving = false;
    private bool fight = false;
    public Vector3 movementDirection = new Vector3(1,0,0);
    public float movementSpeed = 1;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) transform.position += movementDirection * movementSpeed * Time.deltaTime;
    }


    void OnCollisionEnter(Collision collision)
    {
        print("Collision enter");
        HitInterface hitInterface = collision.gameObject.GetComponent<HitInterface>();
        if (hitInterface == null) return;

        hitInterface.Hit(gameObject);
    }

    public void StartMoving() {moving = true;}
    public void StopMoving() {moving = false;}
}
