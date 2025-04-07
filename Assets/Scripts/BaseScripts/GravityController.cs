using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float gravityForce = 9.81f;
    private float gravityMultiplier = 1f;
    public Vector3 gravityDirection = new Vector3(0,-1,0);
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
        rb.AddForce(gravityDirection * gravityForce * gravityMultiplier, ForceMode.Acceleration);
    }
    

    public Rigidbody GetRigidbody() {
        return rb;
    }

    protected void SetGravityMultiplier(float multiplier) 
    {
        gravityMultiplier = multiplier;
    }
    
    protected void SetGravityMultiplierDefault() 
    {
        gravityMultiplier = 1f;
    }

    protected void SetGravity(float gravity) 
    {
        gravityForce = gravity;
    }
    
    protected void SetGravityDefault() 
    {
        gravityForce = 9.81f;
    }

    virtual public void ChangeGravity(Vector3 direction, float force = 9.81f) {
        gravityDirection = direction;
        gravityForce = force;
        transform.up = -gravityDirection;
    }
}
