using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    [SerializeField] private Button btn;
    void Awake()
    {
        btn.onClick.AddListener(OnClicked);
    }

    // Update is called once per frame
    public abstract void OnClicked();
    
}