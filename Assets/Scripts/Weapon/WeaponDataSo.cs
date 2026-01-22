using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "FPS/GunData")]
public class WeaponDataSo : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public int maxAmmo;
    public float range;
    
    public GameObject muzzleFlashPrefab;
    public GameObject hitEffectPrefab;

    [Header("Weapon_Shoot_Animation_Settings")]
    public float recoilX = 2f; 
    public float recoilY = 0.5f; 
    public float snappiness = 10f; 
    public float returnSpeed = 5f; 
    public AudioClip fireSound;  
}