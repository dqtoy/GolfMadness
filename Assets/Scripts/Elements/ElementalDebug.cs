using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ElementVisualController))]
public class ElementalDebug : Elemental
{
    private ElementVisualController _elementVisualController;

    private ElementVisualController ElementVisualController
    {
        get
        {
            if (_elementVisualController == null)
            {
                _elementVisualController = gameObject.GetComponent<ElementVisualController>();
            }

            return _elementVisualController;
        }
    }

    void Awake()
    {
        ElementVisualController.OnElementChange(CurrentElement);
    }

    protected override void OnElementChange()
    {
        ElementVisualController.OnElementChange(CurrentElement);
        base.OnElementChange();
    }
}