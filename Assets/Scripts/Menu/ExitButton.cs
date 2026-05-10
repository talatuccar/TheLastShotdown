using UnityEngine;

public class ExitButton : ButtonBase
{
    public override void OnClicked()
    {
        Application.Quit();
    }
}
