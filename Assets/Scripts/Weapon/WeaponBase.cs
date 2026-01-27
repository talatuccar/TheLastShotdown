using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponDataSo weaponData;
    protected float nextFireTime;

    public Transform muzzlePoint; 

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

        
        if (muzzleFlashParticle != null)
        {
            muzzleFlashParticle.Play(); 
        }
        if (weaponData.fireSound != null)
            SoundManager.Instance.PlayAudioClip(weaponData.fireSound);

     
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, weaponData.range))
        {
            
            Quaternion rotation = Quaternion.LookRotation(hit.normal);
            GameObject effect = Instantiate(weaponData.hitEffectPrefab, hit.point + (hit.normal * 0.01f), rotation);

    
            Destroy(effect, 1.5f);
            Debug.Log("Vurulan: " + hit.transform.name);
            BreakableBox box = hit.transform.GetComponent<BreakableBox>();
            if (box != null)
            {
                box.TakeDamage(10); // Hasar veriyoruz
            }
        }

        GetComponentInParent<WeaponManager>().ApplyRecoil();


    }


}