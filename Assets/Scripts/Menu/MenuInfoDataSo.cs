using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMenuInfo", menuName = "Scriptable Objects/MenuInfoData")]
public class MenuInfoDataSo : ScriptableObject
{
    public MenuInfoData[] menuInfoDatas;
}

[Serializable]
public class MenuInfoData
{

    [Header("Görsel Bilgiler")]
    public Sprite menuIcon;


    [Header("Detaylý Bilgiler")]
    [TextArea(3, 10)]
    public string menuDescription;

}