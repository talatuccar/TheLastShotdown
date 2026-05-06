using UnityEngine;
public class PasswordManager : MonoBehaviour
{
    private int[] _fullPassword = new int[4];

    public void Initialize() 
    {
        for (int i = 0; i < 4; i++) _fullPassword[i] = Random.Range(1, 10);
        Debug.Log("Þifre Oluþturuldu!");
    }

    public int GetPasswordPart(int index) => _fullPassword[index];
}