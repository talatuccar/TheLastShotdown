using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSo", menuName = "Scriptable Objects/EnemyDataSo")]
public class EnemyDataSo : ScriptableObject
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float detectionRadius = 10f;
    public float attackRange = 5f;

    public int minDamageAmount;
    public int maxDamageAmount;

    public GameObject muzzleFlashPrefab;
    public AudioClip enemyfireSound;

}
