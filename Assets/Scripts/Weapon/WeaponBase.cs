using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponDataSo weaponData;
    protected float nextFireTime;

    public Transform muzzlePoint;

    public static event Action OnShooted;

    private ParticleSystem muzzleFlashParticle;


    private void Start()
    {
       
        if (weaponData.muzzleFlashPrefab != null)
        {
           
            GameObject flashGo = Instantiate(weaponData.muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation, muzzlePoint);

           
            muzzleFlashParticle = flashGo.GetComponent<ParticleSystem>();

           
        }
    }
    public void Fire()
    {
        if (Time.timeScale == 0) return;
        //if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
        if (Time.time >= nextFireTime)
        {
            ExecuteShoot();
            nextFireTime = Time.time + (1f / weaponData.fireRate);
        }
    }

    protected virtual void ExecuteShoot()
    {
        PlayerStats.totalShootedBullet++;
        if (PlayerInventory.Instance.CurrentAmmo() <= 0)
        {
            Debug.Log("Mermi yok!");
            SoundManager.Instance.PlayAudioClip(weaponData.emptyGunSound);
            return;
        }

        OnShooted?.Invoke();

        if (muzzleFlashParticle != null)
        {
            muzzleFlashParticle.Play();
        }

        if (weaponData.fireSound != null)
            SoundManager.Instance.PlayAudioClip(weaponData.fireSound);

        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        
        int playerLayer = LayerMask.NameToLayer("Player");

        // Player hariç her þeyi kapsayan bir maske Bitwise NOT operatörü ~ 
        int layerMask = ~(1 << playerLayer);
        if (Physics.Raycast(ray, out RaycastHit hit, weaponData.range, layerMask))
        {
            //Debug.Log("Vurulan: " + hit.transform.name);
            PlayerStats.totalHitDistance += hit.distance;

            Debug.Log("mesafe: " + hit.distance);
           
            IDamageable hitTarget = hit.transform.GetComponent<IDamageable>();

            if (hitTarget != null)
            {
                hitTarget.TakeDamage(weaponData.damage,hit.point);
            }

            
            HandleHitVisuals(hit);
        }

        GetComponentInParent<WeaponManager>().ApplyRecoil();

    }

    
    private void HandleHitVisuals(RaycastHit hit)
    {
        

        string poolTag = "";
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            PlayerStats.totalSuccessfulHits++;
            Debug.Log("NPC Vuruldu! Kan çýkýyor");
            
        }

        if (hit.transform.CompareTag("Metal"))
        {
           
            poolTag = "MetalHit"; 
        }
        else if (hit.transform.CompareTag("Stone"))
        {
            poolTag = "StoneHit"; 
        }

       
        if (!string.IsNullOrEmpty(poolTag))
        {
            // Normal vektörü kullanarak mermi izinin yüzeye doðru bakmasýný saðla
            Quaternion rotation = Quaternion.LookRotation(hit.normal);

           
            EffectPooler.Instance.SpawnFromPool(poolTag, hit.point + (hit.normal * 0.01f), rotation);
        }
    }


}