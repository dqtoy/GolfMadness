using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Example of elemental absorber
public class ElementalDeactivater : ElementalDebug
{
    private ElementalAbsorber playerAbsorber;

    [SerializeField] private GameObject _objectsToDeactivate;

    public override void Init()
    {
        var player = FindObjectOfType<PlayerController>();
        playerAbsorber = player.gameObject.GetComponent<ElementalAbsorber>();

        playerAbsorber.OnPlayerAbsorbs += OnPlayerAbsorbed;

        OnPlayerAbsorbed(playerAbsorber.CurrentElement);
    }

    void OnPlayerAbsorbed(Element element)
    {
        Collider.isTrigger = element == CurrentElement;
        if (_objectsToDeactivate != null)
            _objectsToDeactivate.SetActive(element == CurrentElement);
    }
}