using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewGun", menuName = "FPS/GunData")]
public class WeaponDataSo : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public float range;
    public int maxAmmo; 

    [HideInInspector]
    public int currentAmmo; 

   
    public void Initialize()
    {
        currentAmmo = maxAmmo;
    }

    public GameObject muzzleFlashPrefab;
    public Image gunSwitchedFrameUI;

  

    [Header("Weapon_Shoot_Animation_Settings")]
    public float recoilX = 2f; 
    public float recoilY = 0.5f; 
    public float snappiness = 10f; 
    public float returnSpeed = 5f; 
    public AudioClip fireSound;
    public AudioClip emptyGunSound;


    [Header("Sniper Settings")]
    public bool isSniper;
    public float zoomFOV = 20f;
    public float zoomSpeed = 12f;
    public float adsSensitivityMultiplier = 0.4f;
}