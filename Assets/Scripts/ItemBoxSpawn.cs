using UnityEngine;

public class ItemBoxSpawn : MonoBehaviour
{
    public GameObject itemBoxPrefab;

    public Transform boxSpawnPoint;
    void Start()
    {
        Instantiate(itemBoxPrefab,boxSpawnPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
