using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServicesManager
{
    private static ServicesManager _instance;
    public static ServicesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ServicesManager();
            }

            return _instance;
        }
    }
    
    //Set logic services
    public MissionsManager MissionsManager { get; private set; }
    public LevelDataCollection LevelDataCollection { get; private set; }
    
    //AutoSet of Mono services
    protected UIStackController _UIStackController;

    public UIStackController UIStackController
    {
        get
        {
            if (_UIStackController == null)
            {
                _UIStackController = GameObject.FindObjectOfType<UIStackController>();
            }

            return _UIStackController;
        }
    }

    protected ServicesManager()
    {
        MissionsManager = new MissionsManager();
        LevelDataCollection = new LevelDataCollection();
    }

    

}