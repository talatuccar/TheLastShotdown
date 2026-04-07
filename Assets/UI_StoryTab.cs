
using UnityEngine;

public class UI_StoryTab : MonoBehaviour
{
    public RectTransform storyTextTab; 
    public RectTransform maskArea;      
    public float flowSpeed = 1f;
    public int textEndCloseOffset;

    private float startPosY;

    void Awake()
    {
        
        startPosY = storyTextTab.anchoredPosition.y;
    }

    void OnEnable()
    {
       
        storyTextTab.anchoredPosition = new Vector2(storyTextTab.anchoredPosition.x, startPosY);
    }

    void FixedUpdate()
    {
       
        storyTextTab.anchoredPosition += Vector2.up * flowSpeed;

       
        if (storyTextTab.anchoredPosition.y > storyTextTab.sizeDelta.y + textEndCloseOffset)
        {
            HandleStoryEnd();
        }
    }

    void HandleStoryEnd()
    {
        gameObject.SetActive(false);    
    }

}