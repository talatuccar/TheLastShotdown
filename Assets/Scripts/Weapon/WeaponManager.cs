using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private FPSInput input;
    public WeaponBase currentWeapon;
    private bool isFiring = false;
    [Header("Recoil Settings")]
    private Vector3 currentRotation;
    private Vector3 targetRotation;
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
        if (currentWeapon != null) currentWeapon.Fire(); 
    }

    void StopFiring() => isFiring = false;

    void Update()
    {
        if (isFiring && currentWeapon != null)
        {
            currentWeapon.Fire(); 
        }

        
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, currentWeapon.weaponData.returnSpeed * Time.deltaTime);
    
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, currentWeapon.weaponData.snappiness * Time.deltaTime);

       
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

  
    public void ApplyRecoil()
    {
        targetRotation += new Vector3(-currentWeapon.weaponData.recoilX,
            Random.Range(-currentWeapon.weaponData.recoilY, currentWeapon.weaponData.recoilY),
            Random.Range(-currentWeapon.weaponData.recoilY, currentWeapon.weaponData.recoilY));
    }
}