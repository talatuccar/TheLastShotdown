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

    [Header("Recoil (Geri Tepme)")]
    public float rotationSmoothness = 10f; 
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private Vector3 initialPosition;
    private Quaternion initialRotation; 
    private float timer = 0;
    private FPSInput input;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation; 
        input = GetComponentInParent<FPSInput>();
    }

    void Update()
    {
        // 1. SWAY & BOBBING HESAPLAMA (Mevcut kodun ayný kalabilir)
        float moveX = -input.LookInput.x * swayAmount;
        float moveY = -input.LookInput.y * swayAmount;
        moveX = Mathf.Clamp(moveX, -maxSwayAmount, maxSwayAmount);
        moveY = Mathf.Clamp(moveY, -maxSwayAmount, maxSwayAmount);

        Vector3 targetSway = new Vector3(moveX, moveY, 0);

        Vector3 targetBob = Vector3.zero;
        if (input.MoveInput.magnitude > 0.1f)
        {
            timer += Time.deltaTime * walkBobSpeed;
            targetBob.x = Mathf.Cos(timer) * walkBobAmount;
            targetBob.y = Mathf.Sin(timer * 2) * walkBobAmount;
        }
        else { timer = 0; }

        // 2. RECOIL RECOVERY (Rotasyonu sürekli sýfýra çekiyoruz)
        // targetRotation ateþ edildiðinde artar, burada her karede azalýr
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * rotationSmoothness);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, Time.deltaTime * rotationSmoothness);
        transform.localRotation = initialRotation * Quaternion.Euler(currentRotation);

        // 3. POZÝSYONU UYGULA
        Vector3 targetPosition = initialPosition + targetSway + targetBob;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothAmount);
    }

    public void ApplyRecoil()
    {
        WeaponDataSo weaponData = GetComponentInChildren<WeaponBase>().weaponData;
        if (weaponData == null) return;

        
        targetRotation += new Vector3(-weaponData.recoilX,
            Random.Range(-weaponData.recoilY, weaponData.recoilY),
            Random.Range(-weaponData.recoilY, weaponData.recoilY));
    }
}