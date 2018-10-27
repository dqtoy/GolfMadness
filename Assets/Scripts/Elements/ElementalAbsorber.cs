using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Example of elemental absorber
public class ElementalAbsorber : ElementalDebug
{
    protected override void OnElementChange()
    {
        Debug.Log("ABSORBING ELEMENT: " + CurrentElement);
        base.OnElementChange();
    }
}
