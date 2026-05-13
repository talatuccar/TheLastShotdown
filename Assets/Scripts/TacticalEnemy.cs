using UnityEngine;

public class TacticalEnemy : MonoBehaviour
{
    public Animator anim;
    public float fireRate = 0.8f;
    private float nextFireTime;

    [Header("Ayarlar")]
    public LayerMask obstacleLayer;
    public float rotationSpeed = 20f;

    private float dynamicCloseDistance;
    private bool isPlayerInZone = false;
    private Transform playerTransform;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    
    public ParticleSystem muzzleFlashParticle;
    public EnemyDataSo enemyData;
    public GameObject flashGo;
    public Transform muzzlePoint;
    public void InitializeDistance(float sizeX, float sizeZ, Vector3 triggerPos)
    {

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

        flashGo = Instantiate(enemyData.muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation, muzzlePoint);


        muzzleFlashParticle = flashGo.GetComponent<ParticleSystem>();



    }


    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);


        bool isInRange = isPlayerInZone || distance < dynamicCloseDistance;
        anim.SetBool("isShooting", isInRange);

        if (!isInRange)
        {
            ResetPosition();
            anim.SetBool("isNear", false);
            return;
        }


        bool hasLineOfSight = false;

        Vector3 eyePos = transform.position + Vector3.up * 1.5f;
        Vector3 dirToPlayer = (playerTransform.position + Vector3.up * 1f) - eyePos;

      
        float rayLength = 50f; 

        if (Physics.Raycast(eyePos, dirToPlayer, out RaycastHit hit, rayLength, ~0, QueryTriggerInteraction.Ignore))
        {
           

            if (hit.transform.CompareTag("Player"))
            {
                hasLineOfSight = true;
                
            }
           
        }
       
        if (isInRange && hasLineOfSight)
        {

            anim.SetBool("isNear", distance < dynamicCloseDistance);


            Vector3 lookDir = (playerTransform.position - transform.position).normalized;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * rotationSpeed);
            }


            if (Time.time >= nextFireTime)
            {
                ApplyDamage();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * 5f);
            anim.SetBool("isNear", false);
        }
    }



    void ResetPosition()
    {
        transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * 5f);
        transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * 5f);
    }

    public void ApplyDamage() // tacticalShooting animation event
    {     
            muzzleFlashParticle.Play();
            SoundManager.Instance.PlayAudioClip(enemyData.enemyfireSound);
            int randomHealthDecrease = Random.Range(enemyData.minDamageAmount,enemyData.maxDamageAmount);
            PlayerInventory.Instance.DecreaseHealth(randomHealthDecrease);
    }
}