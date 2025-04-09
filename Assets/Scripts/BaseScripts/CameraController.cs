using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines.Interpolators;

public class CameraController : MonoBehaviour
{
    public enum CameraMode {
        Free,
        Grabbing
    }
    private CameraMode cameraMode = CameraMode.Free;
    PlayerController playerRef;


    public float distance = 10f;
    public float sideDistance = 2f;
    private float _sideDistance;
    private float grabDistance;
    private float normalDistance;
    public LayerMask layer;


    private float yaw = 0f;
    private float pitch = 0f;
    

    public bool transFree = false;
    public bool transGrab = false;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (!playerRef) print("Palyer Not Set");

        grabDistance = sideDistance;
        normalDistance = 0;
        _sideDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transFree) {
            MoveCameraPostion(normalDistance);
            if (_sideDistance == normalDistance) transFree = false;
        }
        else if (transGrab) {
            MoveCameraPostion(grabDistance);
            if (_sideDistance == grabDistance) transGrab = false;
        }

        RotateCamera();        
        if (cameraMode == CameraMode.Grabbing) Debug.DrawLine(transform.position, transform.position + transform.forward * 100, Color.red, 0.1f);
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
        
        // Calcula el offset de la camara teniendo en cuenta las colisiones
        Vector3 offset = OffsetCalculation(finalRotation);

        transform.position = playerRef.transform.position + offset;

    }

    Vector3 OffsetCalculation(Quaternion finalRotation) 
    {
        Vector3 offset = finalRotation * new Vector3(_sideDistance, 0, -distance);

        RaycastHit hit;
        bool hited = Physics.Linecast(playerRef.transform.position, playerRef.transform.position + offset, out hit, layer);

        if (hited) {
            float newDistance = Vector3.Distance(playerRef.transform.position, hit.point);
            offset = finalRotation * new Vector3(_sideDistance,0,-newDistance);
        }

        return offset;
    }


    void MoveCameraPostion(float end) {
        _sideDistance = Mathf.Lerp(_sideDistance,end,Time.deltaTime);
    }

    public void ChangeCamera(CameraMode newMode) {
        if (newMode == CameraMode.Free) {
            transFree = true;
            transGrab = false;
        }
        else if (newMode == CameraMode.Grabbing) 
        {
            transGrab = true;
            transFree = false;
        }

        cameraMode = newMode;
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
