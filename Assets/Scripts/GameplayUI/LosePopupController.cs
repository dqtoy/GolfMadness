using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePopupController : UIController
{

    public void OnClose()
    {
        ServicesManager.Instance.GoToMainScene();
    }

    public void OnRetry()
    {
       ServicesManager.Instance.ReloadCurrentLevel();
        
    }
}