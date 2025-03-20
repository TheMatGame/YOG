using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float gravityForce = 9.81f;
    public Vector3 gravityDirection = new Vector3(0,-1,0);
    protected List<GravityModifier> gravityModifiers = new List<GravityModifier>();
    protected Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }



    protected virtual void FixedUpdate()
    {
        ApplyGravity();
    }

    void ApplyGravity() 
    {
        rb.AddForce(gravityDirection * gravityForce, ForceMode.Acceleration);
    }
    
    public void ChangeGravity(GravityModifier grav) {
        if (gravityModifiers.Count == 0) {
            gravityModifiers.Add(grav);
        }
        else 
        {
            bool found = false;
            int index = 0;

            while (index < gravityModifiers.Count && !found) {
                if (gravityModifiers[index].priority < grav.priority) found = true;
                else index++;
            }

            gravityModifiers.Insert(index, grav);
        }

        UpdateGravity();
    }

    public void RemoveGravity(GravityModifier grav) {
        gravityModifiers.Remove(grav);

        UpdateGravity();
    }
    
    virtual protected void UpdateGravity() {      
        if (gravityModifiers.Count > 0) {
            gravityForce = gravityModifiers[0].gravityForce;
            gravityDirection = gravityModifiers[0].gravityDirection;
        }
        else {
            gravityForce = 9.81f;
            gravityDirection = new Vector3(0,-1,0);
        }
        transform.up = -gravityDirection;
    }

    public Rigidbody GetRigidbody() {
        return rb;
    }
}
