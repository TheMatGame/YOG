using UnityEngine;

public class Door : MonoBehaviour
{
    public bool vertical = false;
    public Transform rightDoor;
    public Transform leftDoor;
    bool rotate = false;
    Vector3 endPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate) {
            if (vertical) {
                rightDoor.position = Vector3.Lerp(rightDoor.position, endPosition, Time.deltaTime);
                if (rightDoor.position == endPosition) rotate = false;

            }
            else {
                rightDoor.rotation = Quaternion.Lerp(rightDoor.rotation, Quaternion.Euler(0,90,0), Time.deltaTime);
                leftDoor.rotation = Quaternion.Lerp(rightDoor.rotation, Quaternion.Euler(0,-90,0), Time.deltaTime);
                if (rightDoor.rotation == Quaternion.Euler(0,90,0)) rotate = false;
            }
        }
    }

    public void OpenDoor() {
        rotate = true;    
        endPosition = rightDoor.position + new Vector3(0,-20,0);
    }
}
