using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "FPS/GunData")]
public class WeaponDataSo : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public int maxAmmo;
    public float range;
    //public GameObject hitEffect; // Kan efekti
    public AudioClip fireSound;  // Ateþ sesi
}