using UnityEngine;
using System.Collections;

public class PooledEffect : MonoBehaviour
{
    [SerializeField] private string poolTag = "Blood";
    [SerializeField] private float lifetime = 3f;

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(ReturnRoutine());
    }

    private IEnumerator ReturnRoutine()
    {
        yield return new WaitForSeconds(lifetime);
        EffectPooler.Instance.ReturnToPool(poolTag, gameObject);
    }
}