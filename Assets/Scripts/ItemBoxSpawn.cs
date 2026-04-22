using System.Collections.Generic;
using UnityEngine;

public class ItemBoxSpawn : MonoBehaviour
{
    
    public BreakableDataSo breakableDataSo;
  
    public int spawnCount = 5;   // kaç tane kutu oluþturulacak

    private List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
       
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }

        SpawnBoxes();
    }

    void SpawnBoxes()
    {
        if (spawnPoints.Count < spawnCount)
        {
            
            spawnCount = spawnPoints.Count;
        }

        // Listeyi karýþtýr (Fisher-Yates Shuffle)
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Transform temp = spawnPoints[i];
            int randomIndex = Random.Range(i, spawnPoints.Count);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }

       
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(breakableDataSo.boxPrefab, spawnPoints[i].position, spawnPoints[i].rotation, spawnPoints[i]);
        }
    }


}
