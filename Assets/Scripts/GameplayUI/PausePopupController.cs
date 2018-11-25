using UnityEngine.SceneManagement;

public class PausePopupController : UIController
{
    public void OnAccept()
    {
        ServicesManager.Instance.GoToMainScene();
    }

    public void OnCancel()
    {
        ServicesManager.Instance.UIStackController.Pop(this);
    }
}