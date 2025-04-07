using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Update()
    {
        if(Camera.main)
        {
            // Rotate the UI to face the camera
            transform.LookAt(Camera.main.transform);
            // Optionally, adjust rotation so it isn’t mirrored:
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
}
