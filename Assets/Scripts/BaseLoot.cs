using UnityEngine;
using DG.Tweening;

public abstract class BaseLoot : MonoBehaviour
{
    protected virtual void Start()
    {
        // Tüm lootlar ortaklaþa döner ve süzülür
        transform.DORotate(new Vector3(0, 360, 0), 3f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

        transform.DOMoveY(transform.position.y + 0.2f, 1f)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    protected virtual void OnDestroy()
    {
        // Bu transform üzerindeki tüm DOTween iþlemlerini anýnda öldür.
        transform.DOKill();
    }
}