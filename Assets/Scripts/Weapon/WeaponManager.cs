using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private FPSInput input;

    [Header("Weapon Settings")]
    public WeaponBase currentWeapon; // O an elimizde olan silah
    public WeaponBase[] allWeapons; // 0: AK47, 1: Sniper (Inspector'dan sürükle)
    private int selectedWeaponIndex = 0;
    private bool isFiring = false;

    [Header("Recoil Settings")]
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    void Awake()
    {
        input = GetComponentInParent<FPSInput>();
    }

    void Start()
    {
        // Oyun baþladýðýnda ilk silahý (genelde AK47) seçelim
        if (allWeapons.Length > 0)
        {
            SelectWeapon(0);
        }
    }

    void OnEnable()
    {
        // Ateþ etme eventleri
        input.OnAttackStarted += StartFiring;
        input.OnAttackCanceled += StopFiring;

        // Silah deðiþtirme eventleri (1 ve 2 tuþlarý)
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

    void StartFiring()
    {
        isFiring = true;
        if (currentWeapon != null) currentWeapon.Fire();
    }

    void StopFiring() => isFiring = false;

    void Update()
    {
        // 1. Seri Ateþ Kontrolü
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

            // Sadece bu objenin (CurrentWeapons hiyerarþisi) rotasyonunu deðiþtir
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }

    public void SelectWeapon(int index)
    {
        // Geçersiz index kontrolü
        if (index < 0 || index >= allWeapons.Length) return;

        // Silah deðiþtirirken ateþ etmeyi durdur
        StopFiring();

        for (int i = 0; i < allWeapons.Length; i++)
        {
            bool shouldBeActive = (i == index);

            // KRÝTÝK: Sniper'dan baþka silaha geçiyorsak zoom'u kapatmalýyýz
            if (!shouldBeActive && allWeapons[i] is Sniper sniper)
            {
                // Sniper scriptini devre dýþý býrakmak zoom'u ve UI'ý resetleyecektir
                // (Eðer Sniper içindeki OnDisable metodu ResetScope çaðýrýyorsa)
                allWeapons[i].enabled = false;
            }

            // Objeyi aç veya kapat
            allWeapons[i].gameObject.SetActive(shouldBeActive);

            if (shouldBeActive)
            {
                allWeapons[i].enabled = true; // Silahý tekrar aktif et
                currentWeapon = allWeapons[i];
                selectedWeaponIndex = i;

                // Yeni silaha geçtiðimizde eski geri tepme kalýntýlarýný temizleyelim
                targetRotation = Vector3.zero;
                currentRotation = Vector3.zero;
            }
        }
    }

    public void ApplyRecoil()
    {
        if (currentWeapon == null) return;

        // WeaponDataSo üzerindeki deðerlere göre tepme uygula
        targetRotation += new Vector3(-currentWeapon.weaponData.recoilX,
            Random.Range(-currentWeapon.weaponData.recoilY, currentWeapon.weaponData.recoilY),
            Random.Range(-currentWeapon.weaponData.recoilY, currentWeapon.weaponData.recoilY));
    }













    //private FPSInput input;
    //public WeaponBase currentWeapon;
    //private bool isFiring = false;
    //[Header("Recoil Settings")]
    //private Vector3 currentRotation;
    //private Vector3 targetRotation;
    //void Awake() => input = GetComponentInParent<FPSInput>();

    //void OnEnable()
    //{
    //    input.OnAttackStarted += StartFiring;
    //    input.OnAttackCanceled += StopFiring;

    //}

    //void OnDisable()
    //{
    //    input.OnAttackStarted -= StartFiring;
    //    input.OnAttackCanceled -= StopFiring;

    //}

    //void StartFiring()
    //{
    //    isFiring = true;
    //    if (currentWeapon != null) currentWeapon.Fire(); 
    //}

    //void StopFiring() => isFiring = false;

    //void Update()
    //{
    //    if (isFiring && currentWeapon != null)
    //    {
    //        currentWeapon.Fire(); 
    //    }


    //    targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, currentWeapon.weaponData.returnSpeed * Time.deltaTime);

    //    currentRotation = Vector3.Slerp(currentRotation, targetRotation, currentWeapon.weaponData.snappiness * Time.deltaTime);


    //    transform.localRotation = Quaternion.Euler(currentRotation);
    //}



    //public void ApplyRecoil()
    //{
    //    targetRotation += new Vector3(-currentWeapon.weaponData.recoilX,
    //        Random.Range(-currentWeapon.weaponData.recoilY, currentWeapon.weaponData.recoilY),
    //        Random.Range(-currentWeapon.weaponData.recoilY, currentWeapon.weaponData.recoilY));
    //}
}