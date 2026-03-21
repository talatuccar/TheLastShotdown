using UnityEngine;
using System.Collections;

public class Sniper : WeaponBase
{
    [SerializeField] private GameObject scopeUI;
    [SerializeField] private GameObject croosHair;
    private bool isAiming = false;
    private float defaultFOV;
    private Camera mainCam;
    private FPSInput input;
    private int weaponLayer;
   
    private void Awake()
    {
        mainCam = Camera.main;
        defaultFOV = mainCam.fieldOfView;
        input = GetComponentInParent<FPSInput>();
        weaponLayer = LayerMask.NameToLayer("Player"); 
    }

    private void OnEnable()
    {
       
        input.OnAimStarted += ToggleAim;
    }

    private void OnDisable()
    {
        input.OnAimStarted -= ToggleAim;
        ResetScope();
    }

    private void ToggleAim()
    {
        if (!weaponData.isSniper) return; 

        isAiming = !isAiming;

        
        if (!isAiming) ResetScope();
    }

    private void ResetScope()
    {
        isAiming = false;
        mainCam.fieldOfView = defaultFOV;
        if (scopeUI != null) scopeUI.SetActive(false);
        ShowWeaponModel(true);
    }

    void Update()
    {
        float targetFOV = isAiming ? weaponData.zoomFOV : defaultFOV;
        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFOV, Time.deltaTime * weaponData.zoomSpeed);

       
        if (isAiming && mainCam.fieldOfView <= weaponData.zoomFOV + 1f)
        {
            if (scopeUI != null) scopeUI.SetActive(true);
            croosHair.SetActive(false);
            ShowWeaponModel(false); 
        }
        else
        {
            if (scopeUI != null) scopeUI.SetActive(false);
            croosHair.SetActive(true);
            ShowWeaponModel(true); 
        }
    }

    private void ShowWeaponModel(bool show)
    {
        if (show)
            mainCam.cullingMask |= (1 << weaponLayer); 
        else
            mainCam.cullingMask &= ~(1 << weaponLayer); 
    }

    protected override void ExecuteShoot()
    {
      
        base.ExecuteShoot();

        
    }


}