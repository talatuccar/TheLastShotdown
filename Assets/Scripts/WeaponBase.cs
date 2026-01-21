using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponDataSo weaponData;
    protected float nextFireTime;

    public void Fire()
    {
        // Silahýn ateþ etme hýzý (fireRate) saniyede kaç mermi atacaðýný belirler
        // Örneðin fireRate 10 ise, 1/10 = 0.1 saniye aralýkla ateþ eder.
        if (Time.time >= nextFireTime)
        {
            ExecuteShoot();
            nextFireTime = Time.time + (1f / weaponData.fireRate);
        }
    }
    protected virtual void ExecuteShoot()
    {
        // Ses Çalma
        if (weaponData.fireSound != null)
            AudioSource.PlayClipAtPoint(weaponData.fireSound, transform.position);

        // Ateþ Etme (Ekranýn tam ortasýna Raycast)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, weaponData.range))
        {
            //if (weaponData.hitEffect != null)
            //{
            //    GameObject effect = Instantiate(weaponData.hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //    Destroy(effect, 1.5f); // Bellek temizliði
            //}
            Debug.Log("Vurulan: " + hit.transform.name);
        }
    }
}