using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    [SerializeField] private Button btn;
    void Awake()
    {
        btn.onClick.AddListener(OnClicked);
    }

    public abstract void OnClicked();
    
}