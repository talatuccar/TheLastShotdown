//using UnityEngine;
//using DG.Tweening;

//public class StartButton : ButtonBase
//{
//    public RectTransform difficultyPanel;
//    public float duration = 0.5f;

//    public override void OnClicked()
//    {
//        // 1. Paneli aktif et
//        difficultyPanel.gameObject.SetActive(true);

//        // 2. Baþlangýç durumu: Butonun içinde, küçücük ve þeffaf (isteðe baðlý)
//        difficultyPanel.localScale = Vector3.zero;
//        // Eðer butonun tam üstünden çýksýn istiyorsan pozisyonu buraya eþitleyebilirsin
//        // difficultyPanel.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

//        // 3. Animasyon: Yukarý doðru büyüme ve açýlma
//        // Önce boyutunu (scale) 1'e getir
//        difficultyPanel.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);

//        // Eðer yukarý doðru biraz da kaysýn istiyorsan (Y ekseninde yükselme)
//        // Mevcut konumundan 300 birim yukarý hedefleyelim:
//        difficultyPanel.DOAnchorPosY(400f, duration).SetEase(Ease.OutCubic);
//    }
//}

using UnityEngine;
using DG.Tweening;

public class StartButton : ButtonBase
{
    public RectTransform difficultyPanel;
    public float duration = 0.5f;
    public float targetYPosition = 400f; // Panelin duracaðý yükseklik

    public override void OnClicked()
    {
        // 1. Paneli aktif et ve baþlangýç deðerlerini sýfýrla
        difficultyPanel.gameObject.SetActive(true);

        // Baþlangýçta butonun olduðu yerde, boyutu sýfýr (görünmez) olsun
        difficultyPanel.localScale = Vector3.zero;
        difficultyPanel.anchoredPosition = new Vector2(0, 0); // Butonun merkezinden baþlar

        // 2. Animasyon - Boyutu orijinal haline (1,1,1) getir
        difficultyPanel.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);

        // 3. Animasyon - Aþaðýdan yukarýya doðru kaydýr
        // (Butonun olduðu 0 noktasýndan targetYPosition noktasýna çýkar)
        difficultyPanel.DOAnchorPosY(targetYPosition, duration).SetEase(Ease.OutCubic);
    }
}