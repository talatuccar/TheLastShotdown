using UnityEngine;
using UnityEngine.UI;

public class ScreenScroller : MonoBehaviour
{
    public RawImage screenImage;
    public float scrollSpeed = 0.5f;

    void Update()
    {
        // Görselin UV koordinatlarýný Y ekseninde kaydýrýyoruz
        Rect currentUV = screenImage.uvRect;
        currentUV.y -= scrollSpeed * Time.deltaTime; // Aþaðý doðru kaymasý için eksi

        screenImage.uvRect = currentUV;
    }
}