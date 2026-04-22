using UnityEngine;

[CreateAssetMenu(fileName = "BreakableDataSo", menuName = "Scriptable Objects/BreakableDataSo")]
public class BreakableDataSo : ScriptableObject
{
    public GameObject boxPrefab;
    public GameObject brokenBoxPrefab;
    public float maxHealth = 30f;
    public string breakableBoxPoolTag;
    public GameObject[] lootPrefabs;   
}
