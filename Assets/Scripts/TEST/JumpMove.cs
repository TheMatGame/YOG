using UnityEngine;

public class JumpMove : MonoBehaviour
{
    Vector3 initialPosition;
    bool moveObject = false;
    Rigidbody rb;
    public float distance = 20f;  // Altura deseada
    public float travelTime = 2f; // Tiempo solo de subida
    public float elapsedTime = 0;
    public float gravity = -9.81f; // Gravedad personalizada

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Usamos nuestra propia gravedad

        initialPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ResetObject();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            StartMovement();
        }
    }

    void FixedUpdate()
    {
        // Aplicamos la gravedad manualmente
        rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

        if (!moveObject) return;

        elapsedTime += Time.fixedDeltaTime;


        // Si el objeto empieza a bajar después de alcanzar el punto más alto, lo detenemos
        
        if (rb.linearVelocity.y <= 0 || (transform.position - initialPosition).magnitude >= distance)
        {
            moveObject = false;
            rb.linearVelocity = Vector3.zero;
            Debug.Log("Tiempo real: " + elapsedTime + " segundos");
        }
    }

    void StartMovement()
    {
        moveObject = true;
        elapsedTime = 0f;
        transform.position = initialPosition;

        // **Calculamos la velocidad inicial en Y para alcanzar exactamente `distance` en `travelTime`**
        float initiallinearVelocityY = (distance + (0.5f * Mathf.Abs(gravity) * travelTime * travelTime)) / travelTime;

        rb.linearVelocity = new Vector3(0, initiallinearVelocityY, 0);
    }

    void ResetObject()
    {
        moveObject = false;
        rb.linearVelocity = Vector3.zero;
        transform.position = initialPosition;
        elapsedTime = 0f;
    }
}
