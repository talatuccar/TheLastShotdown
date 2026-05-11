using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelDataSo", menuName = "Scriptable Objects/LevelDataSo")]
public class LevelDataSo : ScriptableObject
{ 
    public List<GameObject> roadPoints;
    public GameObject npc_parent;

}

