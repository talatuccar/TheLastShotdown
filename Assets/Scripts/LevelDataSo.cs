using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelDataSo", menuName = "Scriptable Objects/LevelDataSo")]
public class LevelDataSo : ScriptableObject
{
    //public LevelData[] levelData;
    public List<GameObject> roadPoints;
    //public EnemyController[] enemies;
    public GameObject npc_parent; // control and delete this


}


//[System.Serializable]
//public class LevelData
//{
    
//    public Transform spawnPoint;
//}
