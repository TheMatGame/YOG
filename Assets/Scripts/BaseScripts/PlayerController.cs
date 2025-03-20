using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : GravityController
{
    CameraController cameraRef;

    [Header("References")]
    public Transform playerObj;


    public float rotationSpeed = 7;


    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    [Header("Movement")]
    public float moveSpeed;
    private RaycastHit castHit;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode kickKey = KeyCode.E;
    public KeyCode grabKey = KeyCode.G;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    

    [Header("Fighting")]
    public float kickDistance = 1f;
    public float kickRadius = 0.5f;
    public float kickCooldown = 0.5f;
    bool readyToKick = true;
    public LayerMask kickMasK;

    [Header("Grabbing")]
    bool canGrab = true;
    bool isGrabbing = false;
    bool canThrow = false;
    public float grabDistance = 1f;
    public float grabRadius = 0.5f;
    GameObject grabedActor;
    GrabInterface grabInterface;
    public Transform holdPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        cameraRef = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        if (!cameraRef) print("Main Camera Not Set");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // DebugOrientationVectors();
        DebugPlayerVelocity();


        MyInput();
        SpeedControl();

        RaycastHit castHit;
        grounded = Physics.Raycast(transform.position, -transform.up, out castHit, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
            rb.linearDamping = groundDrag;
        else 
            rb.linearDamping = 0;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        MovePlayer();
        RotatePlayer();
    }

    void MyInput() 
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKey(kickKey) && readyToKick) {
            if (!isGrabbing) {
                HitAction();
                Invoke(nameof(ResetHitAction), kickCooldown);
            }
            else {
                ThrowAction();
            }
        }

        if (Input.GetKey(grabKey) && canGrab && !isGrabbing) {
            GrabAction();
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = cameraRef.GetCameraForwardDirection() * verticalInput 
                        + cameraRef.GetCameraRightDirection() * horizontalInput;

        // on ground
        if (grounded) {
            rb.AddForce(moveDirection * moveSpeed * 100f, ForceMode.Acceleration);
        }
        // in air
        else if (!grounded) {
            rb.AddForce(moveDirection * moveSpeed * 100f * airMultiplier, ForceMode.Acceleration);
        }
    }

    private void RotatePlayer() {

        // rotate player object
        Vector3 inputDir = cameraRef.GetCameraForwardDirection() * verticalInput 
                           + cameraRef.GetCameraRightDirection() * horizontalInput;

        if (inputDir.sqrMagnitude > 0.1f) {
            inputDir = Vector3.ProjectOnPlane(inputDir,transform.up);
            Quaternion targetRotation = Quaternion.LookRotation(inputDir.normalized, -gravityDirection);
            playerObj.rotation = Quaternion.Slerp(playerObj.rotation, targetRotation, Time.deltaTime * rotationSpeed); 
        }

    }

    private void SpeedControl() 
    {
        // Obtener velocidad del jugador en el plano
        Vector3 velocity = GetVelocityOnPlane();

        // limit velocity if needed
        if (velocity.magnitude > moveSpeed)
        {
            Vector3 limitedVel = velocity.normalized * moveSpeed;
            rb.linearVelocity = limitedVel + GetVelocityAlongGravity();
        }
    }

    protected override void UpdateGravity() 
    {
        base.UpdateGravity();
        playerObj.up = transform.up;
        cameraRef.transform.up = transform.up;
    }









    // JUMPING
    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = GetVelocityOnPlane();

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }




    // SPRINT









    // HIT
    void HitAction() {
        readyToKick = false;
        Vector3 kickDirection = transform.position + playerObj.forward * kickDistance;
        Collider[] hits = Physics.OverlapSphere(kickDirection, kickRadius, kickMasK);

        if (hits.Length > 0) {
            foreach (var hit in hits) {
                HitInterface hitInterface = hit.gameObject.GetComponent<HitInterface>();
                if (hitInterface != null) {
                    hitInterface.Hit(gameObject);
                }

                DrawWireSphere(kickDirection, kickRadius, Color.green, 1f);
            }
        }
        else DrawWireSphere(kickDirection, kickRadius, Color.red, 1f);

        rb.AddForce(kickDirection.normalized * 2, ForceMode.Impulse);
    }

    void ResetHitAction() {readyToKick = true;}








    // GRAB
    void GrabAction() {
        Vector3 grabDirection = transform.position + playerObj.forward * grabDistance;
        Collider[] hits = Physics.OverlapSphere(grabDirection, grabRadius, kickMasK);

        if (hits.Length > 0) {  // Maybe solo hacerlo con uno
            canGrab = false;

            Collider hit = hits[0];
            grabInterface = hit.gameObject.GetComponent<GrabInterface>();
            if (grabInterface != null) {

                isGrabbing = true;

                grabedActor = hit.gameObject;
                grabInterface.Grab(gameObject);

                DrawWireSphere(grabDirection, kickRadius, Color.green, 1f);
            }
            else ResetGrabAction();
        }
        else DrawWireSphere(grabDirection, kickRadius, Color.red, 1f);
    }


    void ThrowAction() {
        // Puedo sustituirlo por una llamada a la interface que me de su rb?
        GravityController gravityController = grabedActor.GetComponent<GravityController>();
        if (!gravityController) return;

        Vector3 throwDirection = (playerObj.forward + transform.up).normalized;
        Debug.DrawLine(transform.position, transform.position + throwDirection * 10, Color.red, 0.25f);

        grabInterface.Release();

        // Aplicar fuerza correctamente
        Rigidbody rb = gravityController.GetRigidbody();
        rb.linearVelocity = this.rb.linearVelocity; // Le aplicamos el momentum del player al objeto
        rb.AddForce(throwDirection * 10f, ForceMode.Impulse);

        ResetGrabAction();

    }

    void ResetGrabAction() {
        canGrab = true;
        isGrabbing = false;
    }

    ///////////////////////////////////////////////////////

    public void ImpulsePlayer(Vector3 force) {
        rb.AddForce(force, ForceMode.Impulse);
    }










    // GETTER
    public Vector3 GetVelocityOnPlane() {
        return Vector3.ProjectOnPlane(rb.linearVelocity, -gravityDirection);
    }

    public Vector3 GetVelocityAlongGravity() {
        return Vector3.Project(rb.linearVelocity, -gravityDirection);
    }

    public Vector3 GetPlayerForward() {
        return playerObj.forward;
    }

    Transform GetCamera() {
        return cameraRef.GetCameraTransform();
    }




    // DEBUG
    public static void DrawWireSphere(Vector3 center, float radius, Color color, float duration, int quality = 3)
    {
        quality = Mathf.Clamp(quality, 1, 10);

        int segments = quality << 2;
        int subdivisions = quality << 3;
        int halfSegments = segments >> 1;
        float strideAngle = 360F / subdivisions;
        float segmentStride = 180F / segments;

        Vector3 first;
        Vector3 next;
        for (int i = 0; i < segments; i++)
        {
            first = (Vector3.forward * radius);
            first = Quaternion.AngleAxis(segmentStride * (i - halfSegments), Vector3.right) * first;

            for (int j = 0; j < subdivisions; j++)
            {
                next = Quaternion.AngleAxis(strideAngle, Vector3.up) * first;
                UnityEngine.Debug.DrawLine(first + center, next + center, color, duration);
                first = next;
            }
        }

        Vector3 axis;
        for (int i = 0; i < segments; i++)
        {
            first = (Vector3.forward * radius);
            first = Quaternion.AngleAxis(segmentStride * (i - halfSegments), Vector3.up) * first;
            axis = Quaternion.AngleAxis(90F, Vector3.up) * first;

            for (int j = 0; j < subdivisions; j++)
            {
                next = Quaternion.AngleAxis(strideAngle, axis) * first;
                UnityEngine.Debug.DrawLine(first + center, next + center, color, duration);
                first = next;
            }
        }
    }

    // DEBUGING
    void DebugPlayerVectors() {
        Debug.DrawLine(transform.position, transform.position + transform.up*10, Color.green, 0.25f);
        Debug.DrawLine(transform.position, transform.position + transform.forward*10, Color.blue, 0.25f);
        Debug.DrawLine(transform.position, transform.position + transform.right*10, Color.red, 0.25f);
    }

    void DebugOrientationVectors() {
        Debug.DrawLine(transform.position, transform.position + cameraRef.GetCameraUpDirection()*10, Color.grey, 0.25f);
        Debug.DrawLine(transform.position, transform.position + cameraRef.GetCameraForwardDirection()*10, Color.cyan, 0.25f);
        Debug.DrawLine(transform.position, transform.position + cameraRef.GetCameraRightDirection()*10, Color.magenta, 0.25f);
    }

    void DebugPlayerVelocity() {
        Debug.DrawLine(transform.position, transform.position + rb.linearVelocity, Color.black, 0.1f);
    }
}
