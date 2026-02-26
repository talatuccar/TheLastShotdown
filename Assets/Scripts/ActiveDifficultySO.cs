using UnityEngine;

[CreateAssetMenu(fileName = "ActiveDifficulty", menuName = "Scriptable Objects/ActiveDifficulty")]
public class ActiveDifficultySO : ScriptableObject
{
    public DifficultyData currentDifficulty;
}