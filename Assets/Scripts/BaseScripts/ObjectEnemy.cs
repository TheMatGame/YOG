using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Splines;

public class ObjectEnemy : BaseEnemy
{
    public enum MovementType {
        NoMovement,
        DirectionalMovement
    }
    public enum SplineType { Linear, Bezier }

    [SerializeField] private MovementType movementType = MovementType.NoMovement;


    [ShowIf("movementType", MovementType.DirectionalMovement)]
    [SerializeField] private Vector3 direction;
    
    [ShowIf("movementType", MovementType.DirectionalMovement)]
    [SerializeField] private float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementType == MovementType.DirectionalMovement) {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
