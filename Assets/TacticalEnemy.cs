using UnityEngine;

public class TacticalEnemy : MonoBehaviour
{
    public Animator anim;
    public float fireRate = 0.8f;
    private float nextFireTime;

    [Header("Ayarlar")]
    public LayerMask obstacleLayer; // Duvarlarýn olduðu Layer'ý seç (Örn: Default veya Obstacle)
    public float rotationSpeed = 20f; // Dönüþ hýzý (Hýzlandýrdýk)

    private float dynamicCloseDistance; // Otomatik hesaplanacak mesafe
    private bool isPlayerInZone = false;
    private Transform playerTransform;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Trigger'dan bu mesafeyi otomatik alacaðýz
    public void InitializeDistance(float sizeX, float sizeZ, Vector3 triggerPos)
    {
        // Kutunun merkezinden köþesine olan en uzak mesafeyi baz alalým
        float halfSize = Mathf.Max(sizeX, sizeZ) / 2f;
        float distToTrigger = Vector3.Distance(transform.position, triggerPos);
        dynamicCloseDistance = distToTrigger + halfSize;
    }

    public void SetPlayerInZone(bool isIn)
    {
        isPlayerInZone = isIn;
    }

    void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Eðer bir trigger atanmýþsa baþlangýçta mesafeyi bir kez hesapla
        // (Veya TacticalTrigger scriptinden InitializeDistance'ý çaðýrabilirsin)
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // --- 1. ADIM: ANIMASYON KONTROLÜ (Görüþe bakmaksýzýn) ---
        // Trigger içindeyse veya çok yakýndaysa "Ateþ Etme" moduna gir (Siperden çýkýþ animasyonu için)
        bool isInRange = isPlayerInZone || distance < dynamicCloseDistance;
        anim.SetBool("isShooting", isInRange);

        if (!isInRange)
        {
            ResetPosition();
            anim.SetBool("isNear", false);
            return;
        }

        // --- 2. ADIM: GÖRÜÞ HATTI KONTROLÜ (Raycast) ---
        bool hasLineOfSight = false;
        Vector3 eyePos = transform.position + Vector3.up * 1.5f;
        Vector3 dirToPlayer = (playerTransform.position + Vector3.up * 1f) - eyePos;

        if (Physics.Raycast(eyePos, dirToPlayer, out RaycastHit hit, dynamicCloseDistance + 5f))
        {
            if (hit.transform.CompareTag("Player"))
                hasLineOfSight = true;
        }

        // --- 3. ADIM: DÖNME VE HASAR MANTIÐI ---
        if (distance < dynamicCloseDistance && hasLineOfSight)
        {
            // Sadece seni görüyorsa sana doðru döner
            anim.SetBool("isNear", true);

            Vector3 lookDir = (playerTransform.position - transform.position).normalized;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            // Seni görmüyorsa veya uzaktaysa orijinal siper açýsýna sadýk kal
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * 5f);
            anim.SetBool("isNear", false);
        }

        // Hasar verme: Sadece animasyon oynuyorsa VE seni gerçekten görüyorsa
        if (isInRange && hasLineOfSight && Time.time >= nextFireTime)
        {
            ApplyDamage();
            nextFireTime = Time.time + fireRate;
        }
    }


    void ResetPosition()
    {
        transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * 5f);
        transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * 5f);
    }

    void ApplyDamage()
    {
        PlayerInventory.Instance.DecreaseHealth(Random.Range(3, 8));
    }
    // Animasyon bittiðinde veya belirli bir karesinde otomatik çaðrýlýr
    public void ShowPasswordDigit()
    {
        // Þifre gösterme mantýðýný buraya yazabilirsin
        // Eðer þu an bir sistemin yoksa sadece hata vermesin diye boþ býrakalým:
        Debug.Log("Tactical Enemy: Þifre rakamý tetiklendi!");

        /* Örnek (Eðer eski sistemindeki gibi bir kodun varsa):
        PasswordManager.Instance.ShowNextDigit(); 
        */
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, dynamicCloseDistance);
    }
}