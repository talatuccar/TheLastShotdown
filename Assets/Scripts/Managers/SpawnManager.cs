using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform npcParent;
    public void SpawnEnemies(int spawnEnemyCount)
    {
        if (npcParent == null) return;

        int totalChildren = npcParent.transform.childCount;

        for (int i = 0; i < totalChildren; i++)
        {

            npcParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < spawnEnemyCount; i++)
        {           
            npcParent.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
