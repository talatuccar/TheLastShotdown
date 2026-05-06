using UnityEngine;

public class DoorController : MonoBehaviour
{

    public string correctPassword = null;
    public Animation doorUp_Anim;
    
    void Start()
    {
    
        correctPassword = "";
        for (int i = 0; i < 4; i++)
        {
            correctPassword += GameManager.Instance.passwordManager.GetPasswordPart(i).ToString();
        }
    }

    void Update()
    {

    }

    private void OpenPasswordPanel()
    {
       
        GameManager.Instance.numpadUI.OpenPanel(this);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnPasswordCorrect()
    {

        doorUp_Anim.Play();
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) OpenPasswordPanel();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GameManager.Instance.numpadUI.ClosePanel(); 
        }
    }
}