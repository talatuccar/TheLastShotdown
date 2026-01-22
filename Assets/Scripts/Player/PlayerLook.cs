using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float sensitivity = 2f;
    public Transform cameraTransform;

    private FPSInput input;
    private float xRotation = 0f;

    void Awake()
    {
        input = GetComponent<FPSInput>();
    }
    void Start()
    {
        // Fare imlecini oyunun ortasýna kilitler ve gizler bu daha sonra normal c harp clasýna taþýnabilir
        // mesela oyun ayarlarý diye genel bir sýnýfa
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
