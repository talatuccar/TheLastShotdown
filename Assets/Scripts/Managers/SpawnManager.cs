using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public LevelDataSo levelDataso;

    public Transform npcParent;
    public void SpawnEnemies(int spawnEnemyCount)
    {
        if (npcParent == null) return;

        int totalChildren = npcParent.transform.childCount;



        //npcParent.transform.GetChild(0).gameObject.SetActive(true);


        for (int i = 0; i < totalChildren; i++)
        {

            npcParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < spawnEnemyCount; i++)
        {
            //levelDataso.enemies[i].gameObject.SetActive(true);
            npcParent.transform.GetChild(i).gameObject.SetActive(true);

        }

    }
}
