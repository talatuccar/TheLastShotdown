using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponDataSo weaponData;
    protected float nextFireTime;

    public Transform muzzlePoint; // Müfettiþten (Inspector) atayacaðýmýz namlu ucu

    private ParticleSystem muzzleFlashParticle;


    private void Start()
    {
        // Silah ilk oluþtuðunda efekti namlunun ucuna takalým
        if (weaponData.muzzleFlashPrefab != null)
        {
            // Efekti yarat ve bu silahýn (transform) çocuðu yap
            GameObject flashGo = Instantiate(weaponData.muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation, muzzlePoint);

            // Üzerindeki ParticleSystem bileþenini al
            muzzleFlashParticle = flashGo.GetComponent<ParticleSystem>();

            // Eðer efektin içinde alt objelerde de particle varsa hepsini kapsar
        }
    }
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

        // --- Efekti Oynat ---
        if (muzzleFlashParticle != null)
        {
            muzzleFlashParticle.Play(); // Zaten tek seferlikse sadece Play() yeterli
        }
        // Ses Çalma
        if (weaponData.fireSound != null)
            //AudioSource.PlayClipAtPoint(weaponData.fireSound, transform.position);
             SoundManager.Instance.PlayAudioClip(weaponData.fireSound);

        // Ateþ Etme (Ekranýn tam ortasýna Raycast)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, weaponData.range))
        {
            // SÝHÝRLÝ SATIR: 
            // Quaternion.LookRotation(hit.normal) mermi efektini çarptýðý yüzeye dik çevirir.
            // hit.point + (hit.normal * 0.01f) efekti duvara gömülmesin diye çok az dýþarý kaydýrýr.

            Quaternion rotation = Quaternion.LookRotation(hit.normal);
            GameObject effect = Instantiate(weaponData.hitEffectPrefab, hit.point + (hit.normal * 0.01f), rotation);

            // WebGL performansý için efekti 1.5 saniye sonra yok et
            Destroy(effect, 1.5f);
            Debug.Log("Vurulan: " + hit.transform.name);
        }
    }
}