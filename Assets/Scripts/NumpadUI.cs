using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumpadUI : MonoBehaviour
{
    public TextMeshProUGUI inputField; 
    private string _currentInput = "";
    private DoorController _targetDoor;

    public void OpenPanel(DoorController door)
    {
        Time.timeScale = 0;
        //PlayerStateManager.Instance.SetControls(false); 
        _targetDoor = door;
        gameObject.SetActive(true);
        _currentInput = "";
        UpdateUI();
    }

    public void OnNumberClick(string number)
    {
        if (_currentInput.Length < 4)
        {
            _currentInput += number;
            UpdateUI();
        }

        if (_currentInput.Length == 4)
        {
            CheckPassword();
           
        }
    }

    private void CheckPassword()
    {
        
        string realPassword = "";
        for (int i = 0; i < 4; i++)
        {
            realPassword += GameManager.Instance.passwordManager.GetPasswordPart(i).ToString();
        }

        if (_currentInput == realPassword)
        {
            Debug.Log("ÞÝFRE DOÐRU!");
            _targetDoor.OnPasswordCorrect();
            ClosePanel();
        }
        else
        {
            Debug.Log("YANLIÞ ÞÝFRE!");
            _currentInput = ""; 
            UpdateUI();
        }
    }

    public void ClosePanel()
    {
        Time.timeScale = 1;
        //PlayerStateManager.Instance.SetControls(true);
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UpdateUI() => inputField.text = _currentInput;
}