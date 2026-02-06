using UnityEngine;
using TMPro; 

public class PlayerInteract : MonoBehaviour
{
    [Header("Raycast Ayarlarý")]
    public float interactDistance = 5f;
    public LayerMask interactLayer; 

    [Header("UI Ayarlarý")]
    public GameObject interactTextObj; 

    void Update()
    {
        CheckInteraction();
    }

    void CheckInteraction()
    {
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            
            if (hit.transform.CompareTag("Breakable"))
            {
                interactTextObj.SetActive(true);
            }
            else
            {
                interactTextObj.SetActive(false);
            }
        }
        else
        {
            interactTextObj.SetActive(false);
        }
    }
}