using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class SplineMover : MonoBehaviour
{
    private enum MovementMode {
        Bounce,
        Loop
    }
    [SerializeField] MovementMode movementMode = MovementMode.Bounce;
    SplineContainer splineContainer; // Referencia a la spline
    Transform movingObject; // Objeto que se mueve
    public float speed = 1.0f; // Velocidad de movimiento

    private float t = 0f; // Parámetro de interpolación
    private int direction = 1; // Dirección (1 hacia adelante, -1 hacia atrás)

    void Awake()
    {
        splineContainer = GetComponent<SplineContainer>();
        movingObject = transform.GetChild(0);
    }

    void Start()
    {
        Transform platform = movingObject.GetChild(0);
        if (!platform) return;

        platform.AddComponent<PlatformMomentum>();
    }

    void FixedUpdate()
    {
        if (splineContainer == null || movingObject == null) return;

        // Obtener la spline (suponiendo que solo hay una)
        Spline spline = splineContainer.Spline;

        Unity.Mathematics.float3 pos = spline.EvaluatePosition(t);
        
        // Mover el objeto a lo largo de la spline
        movingObject.position = new Vector3(
            transform.position.x + pos.x,
            transform.position.y + pos.y,
            transform.position.z + pos.z
        );
        
        // Avanzar el parámetro t en función de la velocidad
        t += Time.deltaTime * speed * direction;
        
        if (movementMode == MovementMode.Bounce) {
            // Invertir dirección si llegamos al final o al inicio
            if (t >= 1.0f)
            {
                t = 1.0f;
                direction = -1;
            }
            else if (t <= 0.0f)
            {
                t = 0.0f;
                direction = 1;
            }
        } else {
            // Invertir dirección si llegamos al final o al inicio
            direction = 1;
            if (t >= 1.0f)
            {
                t = 0.0f;
            }
        }
    }
}
