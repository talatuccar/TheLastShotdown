using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private FPSInput input;
    private CharacterController controller;
   

    [Header("Speed_Settings")]
    public float walkSpeed = 5f;
    private float sprintSpeed;
    private float crouchSpeed;
    private float currentSpeed;
    public float acceleration = 10f;
    private Vector3 currentVelocity;
    private Vector3 velocity;


    public float jumpForce = 2f;
    private bool isGrounded;

   
    void Awake()
    {
        input = GetComponent<FPSInput>();
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        crouchSpeed = walkSpeed * 0.5f;
        sprintSpeed = walkSpeed * 2;
    }

    private void Start()
    {
        input.SprintStarted += () => currentSpeed = sprintSpeed;
        input.SprintCanceled += () => currentSpeed = walkSpeed;

        input.CrouchStarted += StartCrouch;
        input.CrouchCanceled += StopCrouch;
    }
    void StartCrouch()
    {
        controller.height = 1f; 
        currentSpeed = crouchSpeed;      
    }

    void StopCrouch()
    {
        controller.height = 2f; 
        currentSpeed = walkSpeed;
    }
    void OnEnable()
    {
        input.JumpEvent += Jump;
    }

    void OnDisable()
    {
        input.JumpEvent -= Jump;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        Vector2 move = input.MoveInput;
        Vector3 targetDir = transform.right * move.x + transform.forward * move.y;

        currentVelocity = Vector3.Lerp(currentVelocity, targetDir * currentSpeed, acceleration * Time.deltaTime);

        controller.Move(currentVelocity * Time.deltaTime);

       
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        
    }

    void Jump()
    {
        if (isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
    }

   
}
