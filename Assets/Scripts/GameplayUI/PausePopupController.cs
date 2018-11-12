using UnityEngine.SceneManagement;

public class PausePopupController : UIController
{
    public void OnAccept()
    {
        ServicesManager.Instance.MissionsManager.MissionsActive = false;
        ServicesManager.Instance.UIStackController.PopAll();
        SceneManager.LoadScene(ServicesManager.MainScene);
    }

    public void OnCancel()
    {
        ServicesManager.Instance.UIStackController.Pop(this);
    }
}