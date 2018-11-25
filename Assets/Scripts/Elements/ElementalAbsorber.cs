using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Example of elemental absorber
public class ElementalAbsorber : ElementalDebug
{
    public Action<Element> OnPlayerAbsorbs;
    
    protected override void OnElementChange()
    {
        Debug.Log("ABSORBING ELEMENT: " + CurrentElement);
        base.OnElementChange();

        if (OnPlayerAbsorbs != null)
        {
            OnPlayerAbsorbs(CurrentElement);
        }
    }
}
