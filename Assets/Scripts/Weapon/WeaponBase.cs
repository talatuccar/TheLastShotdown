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
        
        if (Time.time >= nextFireTime)
        {
            ExecuteShoot();
            nextFireTime = Time.time + (1f / weaponData.fireRate);
        }
    }

    protected virtual void ExecuteShoot()
    {
        if (PlayerInventory.Instance.playerInventoryDataSo.AmmoAmount <= 0)
        {
            Debug.Log("Mermi yok!");
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
        if (Physics.Raycast(ray, out RaycastHit hit, weaponData.range))
        {
            Debug.Log("Vurulan: " + hit.transform.name);

            
            IDamageable hitTarget = hit.transform.GetComponent<IDamageable>();

            if (hitTarget != null)
            {
                //TDO Buradaki '10' yerine weaponData.damage (eðer SO'da varsa) kullan
                hitTarget.TakeDamage(10,hit.point);
            }

            
            HandleHitVisuals(hit);
        }

        GetComponentInParent<WeaponManager>().ApplyRecoil();
    }

    // Görsel efektleri (toz, kan, kývýlcým) yöneten metod
    private void HandleHitVisuals(RaycastHit hit)
    {
        GameObject effectToSpawn = weaponData.hitEffectPrefab;

       
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
           
            Debug.Log("<color=red>NPC Vuruldu! Kan çýkýyor...</color>");
            
        }

        if (effectToSpawn != null)
        {
            Quaternion rotation = Quaternion.LookRotation(hit.normal);
            GameObject effect = Instantiate(effectToSpawn, hit.point + (hit.normal * 0.01f), rotation);
            Destroy(effect, 1.5f);
        }
    }


}