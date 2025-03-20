using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController playerRef;

    [Header("References")]
    public float distance = 10f;
    // public Transform mainCamera;
    // public Transform playerObj;
    // public Transform orientation;
    // public Material overlappingMaterial;

    // public float rotationSpeed = 7;


    // float horizontalInput;
    // float verticalInput;

    // Vector3 moveDirection;

    //CAMERA
    private float yaw = 0f;
    private float pitch = 0f;
    //////////
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (!playerRef) print("Palyer Not Set");
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }
    private void RotateCamera()
    {
        yaw += Input.GetAxis("Mouse X") * 5f;
        pitch -= Input.GetAxis("Mouse Y") * 5f;

        pitch = Math.Clamp(pitch, -80f, 80f);

        transform.position = playerRef.transform.position;
        transform.rotation = playerRef.transform.rotation;

        // Construir la rotación Yaw (horizontal) alrededor del "up" del jugador
        Quaternion yawRotation = Quaternion.AngleAxis(yaw, playerRef.transform.up);

        // Construir la rotación Pitch (vertical) alrededor del "right" del jugador
        Quaternion pitchRotation = Quaternion.AngleAxis(pitch, playerRef.transform.right);

        // Combinar las rotaciones correctamente
        Quaternion finalRotation = yawRotation * pitchRotation * playerRef.transform.rotation;
        transform.rotation = finalRotation;
        
        Vector3 offset = finalRotation * new Vector3(0, 0, -distance);
        transform.position = playerRef.transform.position + offset;

        // Refleja el forward de la camara en el plano
        // Vector3 forward = Vector3.ProjectOnPlane(mainCamera.forward, transform.up).normalized;
        // orientation.LookAt(orientation.position + forward, transform.up);
    }


    public Vector3 GetCameraForwardDirection()
    {
        return Vector3.ProjectOnPlane(transform.forward, -playerRef.gravityDirection).normalized; 

    }
    
    public Vector3 GetCameraRightDirection()
    {
        return Vector3.ProjectOnPlane(transform.right, -playerRef.gravityDirection).normalized; 

    }
    
    public Vector3 GetCameraUpDirection()
    {
        return Vector3.Project(transform.up, -playerRef.gravityDirection).normalized; 

    }

    public Transform GetCameraTransform() 
    {
        return transform;
    }
}
