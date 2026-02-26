using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyData", menuName = "Scriptable Objects/DifficultyData")]
public class DifficultyData : ScriptableObject
{
    public DifficultyType DifficultyType;
    public int spawnedEnemyCount;
}
