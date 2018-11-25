using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Example of elemental receiver
public class ElementalReceiveDestroy : ElementalDebug
{
    [SerializeField] private GameObject _particleWhenLeaving;

    [SerializeField] private bool _destroyOnHitAnything;
    [SerializeField] private bool _destroyByBombs;
    
    protected override void CollisionableCollisionEnter(Collision other)
    {
        var otherElemental = other.gameObject.GetComponent<Elemental>();
        bool isPlayer = other.gameObject.GetComponent<PlayerController>() != null;

        if (!isPlayer && _destroyByBombs)
        {
            isPlayer = other.gameObject.name.Contains("Bomb");
        }

        if (_destroyOnHitAnything || isPlayer && (CurrentElement == Element.ANY || otherElemental.CurrentElement == CurrentElement))
        {
            DestroyProcess();
        }
    }
    
    protected override void CollisionableTriggerEnter(Collider other)
    {
        var otherElemental = other.gameObject.GetComponent<Elemental>();
        bool isPlayer = other.gameObject.GetComponent<PlayerController>() != null;
        if (_destroyByBombs)
        {
            isPlayer = other.gameObject.name.Contains("Bomb");
        }
        if (_destroyOnHitAnything || isPlayer && (CurrentElement == Element.ANY || otherElemental.CurrentElement == CurrentElement))
        {
            DestroyProcess();
        }
    }

    void DestroyProcess()
    {
        if (_particleWhenLeaving != null)
        {
            _particleWhenLeaving.transform.parent = null;
            _particleWhenLeaving.SetActive(true);
        }

        Destroy(gameObject);
    }
}