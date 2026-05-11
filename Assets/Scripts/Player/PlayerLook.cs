using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sensitivity;
    public Transform cameraTransform;

    private FPSInput input;
    private float xRotation = 0f;

    void Awake()
    {
        input = GetComponent<FPSInput>();
        sensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 5f);
    }
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Vector2 look = input.LookInput * sensitivity * Time.deltaTime;

        xRotation -= look.y;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * look.x);

    }
}
