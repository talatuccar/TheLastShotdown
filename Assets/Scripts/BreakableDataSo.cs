using UnityEngine;

[CreateAssetMenu(fileName = "BreakableDataSo", menuName = "Scriptable Objects/BreakableDataSo")]
public class BreakableDataSo : ScriptableObject
{
    public float maxHealth = 30f;
    public GameObject brokenPrefab; 
    public GameObject[] lootPrefabs;   
}
