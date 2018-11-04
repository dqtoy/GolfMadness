using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager
{
    private static ServiceManager _instance;
    public static ServiceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ServiceManager();
            }

            return _instance;
        }
    }
    
    //Set logic services
    public MissionsManager MissionsManager { get; private set; }
    
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

    protected ServiceManager()
    {
        MissionsManager = new MissionsManager();
    }

    

}