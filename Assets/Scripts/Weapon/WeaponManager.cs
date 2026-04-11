using UnityEngine;
using System.Collections;
public class WeaponManager : MonoBehaviour
{

    private FPSInput input;

    [Header("Weapon Settings")]
    public WeaponBase currentWeapon; 
    public WeaponBase[] allWeapons; 
    private int selectedWeaponIndex = 0;
    private bool isFiring = false;

    [Header("Recoil Settings")]
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    public Animator weaponHolder;
    private bool isSwitching = false;
    private int pendingWeaponIndex; 
    void Awake()
    {
        input = GetComponentInParent<FPSInput>();
    }

    void Start()
    {
       
        if (allWeapons.Length > 0)
        {
            SelectWeapon(0);
        }
    }

    void OnEnable()
    {
      
        input.OnAttackStarted += StartFiring;
        input.OnAttackCanceled += StopFiring;

     
        input.OnAlpha1Pressed += () => SelectWeapon(0);
        input.OnAlpha2Pressed += () => SelectWeapon(1);
    }

    void OnDisable()
    {
        input.OnAttackStarted -= StartFiring;
        input.OnAttackCanceled -= StopFiring;

        input.OnAlpha1Pressed -= () => SelectWeapon(0);
        input.OnAlpha2Pressed -= () => SelectWeapon(1);
    }


    void StopFiring() => isFiring = false;

    void Update()
    {
        
        if (isFiring && currentWeapon != null)
        {
            currentWeapon.Fire();
        }

        // 2. Recoil (Geri Tepme) Hesaplamalarý
        if (currentWeapon != null)
        {
            // Geri tepmeyi sýfýra doðru yumuþat (Return Speed)
            targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, currentWeapon.weaponData.returnSpeed * Time.deltaTime);
            // Mevcut rotasyonu hedefe sarsýntýlý bir þekilde ulaþtýr (Snappiness)
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, currentWeapon.weaponData.snappiness * Time.deltaTime);

            
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }

    public void SelectWeapon(int index)
    {
        if (index < 0 || index >= allWeapons.Length || isSwitching) return;
        if (currentWeapon == allWeapons[index] && allWeapons[index].gameObject.activeSelf) return;

        pendingWeaponIndex = index; 
        isSwitching = true;
        StopFiring();

        weaponHolder.SetTrigger("ChangeWeapon"); 
    }

    // animasyonun ortasýnda eklenen event methodu
    public void ExecuteWeaponSwitch()
    {
        
        for (int i = 0; i < allWeapons.Length; i++)
        {
            bool shouldBeActive = (i == pendingWeaponIndex);

            if (!shouldBeActive && allWeapons[i] is Sniper sniper)
                allWeapons[i].enabled = false;

            allWeapons[i].gameObject.SetActive(shouldBeActive);

            if (shouldBeActive)
            {
                allWeapons[i].enabled = true;
                currentWeapon = allWeapons[i];
                selectedWeaponIndex = i;
                targetRotation = Vector3.zero;
                currentRotation = Vector3.zero;
            }
        }
    }

    //  animasyonun bittiði event 
    public void FinishWeaponSwitch()
    {
        isSwitching = false;
    }


    void StartFiring()
    {
        if (isSwitching) return; // Silah deðiþirken ateþ etme
        isFiring = true;
        if (currentWeapon != null) currentWeapon.Fire();
    }

    public void ApplyRecoil()
    {
        if (currentWeapon == null) return;

        // WeaponDataSo üzerindeki deðerlere göre tepme uygula
        targetRotation += new Vector3(-currentWeapon.weaponData.recoilX,
            Random.Range(-currentWeapon.weaponData.recoilY, currentWeapon.weaponData.recoilY),
            Random.Range(-currentWeapon.weaponData.recoilY, currentWeapon.weaponData.recoilY));
    }

}