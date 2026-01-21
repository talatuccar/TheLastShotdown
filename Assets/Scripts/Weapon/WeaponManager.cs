using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private FPSInput input;
    public WeaponBase currentWeapon;
    private bool isFiring = false;

    void Awake() => input = GetComponentInParent<FPSInput>();

    void OnEnable()
    {
        input.OnAttackStarted += StartFiring;
        input.OnAttackCanceled += StopFiring;
    }

    void OnDisable()
    {
        input.OnAttackStarted -= StartFiring;
        input.OnAttackCanceled -= StopFiring;
    }

    void StartFiring()
    {
        isFiring = true;
        if (currentWeapon != null) currentWeapon.Fire(); // Ýlk týk anýnda mermi at (Hybrid sistem)
    }

    void StopFiring() => isFiring = false;

    void Update()
    {
        if (isFiring && currentWeapon != null)
        {
            currentWeapon.Fire(); // Basýlý tutulduðu sürece otomatik ateþ
        }
    }
}