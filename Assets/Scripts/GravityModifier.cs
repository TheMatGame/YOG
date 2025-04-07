using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GravityModifier : MonoBehaviour
{
    [Header("Gravity")]
    public Vector3 gravityDirection = new Vector3(0,1,0);
    public float gravityForce = 9.81f;
    public int priority = 0;

    protected PlayerController playerController;
    protected Vector3 initialGravityDirection;
    protected float initialGravityForce;

    private void OnValidate()
    {
        // Calcular la nueva dirección de la gravedad basada en la rotación del objeto
        gravityDirection = -transform.up;
    }


    virtual protected void OnTriggerEnter(Collider other)
    {   
        GravityController gravityController = other.GetComponent<GravityController>();
        if (!gravityController) return;

        gravityController.ChangeGravity(gravityDirection, gravityForce);
    }

    virtual protected void OnTriggerExit(Collider other)
    {
        GravityController gravityController = other.GetComponent<GravityController>();
        if (!gravityController) return;

    }
}
