using DG.Tweening;
using System;
using UnityEngine;

public abstract class BaseLoot : MonoBehaviour
{

    
    protected virtual void Start()
    {

        transform.DORotate(new Vector3(0, 360, 0), 3f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

        transform.DOMoveY(transform.position.y + 0.2f, 1f)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    protected virtual void OnDestroy()
    {
        transform.DOKill();
    }


    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlayAudioClip(SoundManager.Instance.lootTakenSfx);
            Collect();
        }
    }
    protected virtual void Collect()
    {
        Destroy(gameObject);
    }
}