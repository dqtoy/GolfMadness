using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : Collisionable
{
    public enum Element
    {
        NONE,
        FIRE,
        WATER,
        ICE,
        AIR,
        EARTH,
        ANY
    }

    [SerializeField] private Element _element;
    public Element CurrentElement
    {
        get
        {
            return _element;
        }
        set
        {
            if (_element != value)
            {
                _element = value;
                OnElementChange();
            }
            
        }
    }

    public static string GetElementLayer(Elemental elemental)
    {
        string layer = "default";

        switch (elemental.CurrentElement)
        {
            case Element.AIR:
                layer = "element_air";
                break;
            case Element.ICE:
                layer = "element_ice";
                break;
            case Element.FIRE:
                layer = "element_fire";
                break;
            case Element.NONE:
                layer = "element_none";
                break;
            case Element.WATER:
                layer = "element_water";
                break;
            case Element.EARTH:
                layer = "element_earth";
                break;
            default:
                Debug.LogError("Element not supported: " + elemental.CurrentElement.ToString());
                break;
        }

        return layer;
    }

    public virtual void Init()
    {
    }

    protected virtual void OnElementChange()
    {
    }
}