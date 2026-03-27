using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class EffectPooler : MonoBehaviour
{
    public static EffectPooler Instance { get; private set; }

    [System.Serializable]
    public class PoolItem
    {
        public string tag;
        public GameObject prefab;
        public int defaultCapacity = 10;
        public int maxSize = 50;
    }

    public List<PoolItem> poolItems;
    private Dictionary<string, ObjectPool<GameObject>> _pools = new Dictionary<string, ObjectPool<GameObject>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var item in poolItems)
        {
            var pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(item.prefab, transform),
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: false,
                defaultCapacity: item.defaultCapacity,
                maxSize: item.maxSize
            );
            _pools.Add(item.tag, pool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_pools.ContainsKey(tag)) return null;

        GameObject obj = _pools[tag].Get();
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj;
    }

    public void ReturnToPool(string tag, GameObject obj)
    {
        if (_pools.ContainsKey(tag))
            _pools[tag].Release(obj);
    }
}