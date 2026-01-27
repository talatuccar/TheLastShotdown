using UnityEngine;
using TMPro; // TextMeshPro kullanýyorsan bunu ekle, düz Text ise UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [Header("Raycast Ayarlarý")]
    public float interactDistance = 5f;
    public LayerMask interactLayer; // Inspector'dan "Interactable" katmanýný seçmeyi unutma!

    [Header("UI Ayarlarý")]
    public GameObject interactTextObj; // Ekranda çýkan o yazý objesi

    void Update()
    {
        CheckInteraction();
    }

    void CheckInteraction()
    {
        // Kameranýn merkezinden (crosshair) ileriye bir ýþýn tanýmlýyoruz
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Sadece 'interactLayer' katmanýndaki objeleri kontrol et
        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            // Eðer baktýðýmýz objede BreakableBox scripti varsa
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
            // Menzilde hiçbir þey yoksa yazýyý kapat
            interactTextObj.SetActive(false);
        }
    }
}