using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventorySo", menuName = "Scriptable Objects/PlayerInventorySo")]
public class PlayerInventorySo : ScriptableObject
{
    public int HealtAmount = 100;
    public int AmmoAmount = 30;
    public AudioClip PlayerDeadAudioClip;  
}
