using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public UIStackController.Type Type;

    public virtual void OnAppeared()
    {
    }

    public virtual void OnDisappeared()
    {
    }
}