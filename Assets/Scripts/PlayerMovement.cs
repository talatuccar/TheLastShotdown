using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private FPSInput input;
    private CharacterController controller;

    public float speed = 5f;
    public float jumpForce = 2f;
    private Vector3 velocity;
    private bool isGrounded;

    public float acceleration = 10f; // Hýzlanma katsayýsý
    private Vector3 currentVelocity; // Mevcut hýz vektörü
    void Awake()
    {
        input = GetComponent<FPSInput>();
        controller = GetComponent<CharacterController>();
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

        // Lerp kullanarak hýzý yumuþatýyoruz, bu CS'deki o akýþ hissini verir
        currentVelocity = Vector3.Lerp(currentVelocity, targetDir * speed, acceleration * Time.deltaTime);

        controller.Move(currentVelocity * Time.deltaTime);

        // Yerçekimi
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
