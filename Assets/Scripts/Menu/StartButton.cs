
using UnityEngine;
using DG.Tweening;

public class StartButton : ButtonBase
{
    public RectTransform difficultyPanel;
    public float duration = 0.5f;
    public float targetYPosition = 400f; 

    public override void OnClicked()
    {
        
        difficultyPanel.gameObject.SetActive(true);

        // Baþlangýçta butonun olduðu yerde, boyutu sýfýr (görünmez) olsun
        difficultyPanel.localScale = Vector3.zero;
        difficultyPanel.anchoredPosition = new Vector2(0, 0); 

        // Animasyon - Boyutu orijinal haline (1,1,1) getir
        difficultyPanel.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
      
        difficultyPanel.DOAnchorPosY(targetYPosition, duration).SetEase(Ease.OutCubic);
    }
}