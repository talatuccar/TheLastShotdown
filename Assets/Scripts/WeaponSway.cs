using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway (Bakýþ Etkisi)")]
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.05f;
    public float smoothAmount = 6f;

    [Header("Bobbing (Yürüyüþ Etkisi)")]
    public float walkBobSpeed = 12f;
    public float walkBobAmount = 0.03f;

    private Vector3 initialPosition;
    private float timer = 0;
    private FPSInput input;

    void Start()
    {
        initialPosition = transform.localPosition;
        input = GetComponentInParent<FPSInput>();
    }

    void Update()
    {
        // 1. SWAY HESAPLAMA (Fare hareketi)
        float moveX = -input.LookInput.x * swayAmount;
        float moveY = -input.LookInput.y * swayAmount;
        moveX = Mathf.Clamp(moveX, -maxSwayAmount, maxSwayAmount);
        moveY = Mathf.Clamp(moveY, -maxSwayAmount, maxSwayAmount);

        Vector3 targetSway = new Vector3(moveX, moveY, 0);

        // 2. BOBBING HESAPLAMA (Yürüme hareketi)
        Vector3 targetBob = Vector3.zero;
        if (input.MoveInput.magnitude > 0.1f)
        {
            timer += Time.deltaTime * walkBobSpeed;
            targetBob.x = Mathf.Cos(timer) * walkBobAmount;
            targetBob.y = Mathf.Sin(timer * 2) * walkBobAmount;
        }
        else
        {
            timer = 0; // Durunca sýfýrla
        }

        // 3. TÜM HAREKETLERÝ BÝRLEÞTÝR VE YUMUÞAT (Titremeyi engelleyen kýsým)
        // Lerp kullanarak silahý hedef pozisyona akýcý bir þekilde götürüyoruz
        Vector3 targetPosition = initialPosition + targetSway + targetBob;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothAmount);
    }
}