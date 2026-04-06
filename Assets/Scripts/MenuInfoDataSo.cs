using UnityEngine;

[CreateAssetMenu(fileName = "NewMenuInfo", menuName = "ScriptableObjects/MenuInfoData")]
public class MenuInfoDataSo : ScriptableObject
{
    [Header("Görsel Bilgiler")]
    public Sprite menuIcon;          
    

    [Header("Detaylý Bilgiler")]
    [TextArea(3, 10)]                
    public string menuDescription;   

    
}