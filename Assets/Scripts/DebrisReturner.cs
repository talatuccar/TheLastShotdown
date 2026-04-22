using UnityEngine;

public class DebrisReturner : MonoBehaviour
{
    public string poolTag = "BreakableBox";
    public float lifetime = 5f;

    private void OnEnable()
    {
        
        Invoke(nameof(Return), lifetime);
    }

    void Return()
    {
        EffectPooler.Instance.ReturnToPool(poolTag, gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke(); 
    }
}