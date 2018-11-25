using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalGiverByCollision : ElementalDebug 
{
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _fxPrefab;

	protected override void CollisionableCollisionEnter(Collision other)
	{
		var absorber = other.gameObject.GetComponent<ElementalAbsorber>();
		if (absorber != null)
		{
			absorber.CurrentElement = CurrentElement;
		}

        AnimationAndFx();
		base.CollisionableCollisionEnter(other);
	}

	protected override void CollisionableTriggerEnter(Collider other)
	{
		var absorber = other.gameObject.GetComponent<ElementalAbsorber>();
		if (absorber != null)
		{
			absorber.CurrentElement = CurrentElement;
		}

        AnimationAndFx();
		base.CollisionableTriggerEnter(other);
	}

    void AnimationAndFx()
    {
	    SoundManager.Instance.PlayPaint();
        if(_animator != null)
        {
            _animator.SetTrigger("Fx");
        }

        if(_fxPrefab != null)
        {
            Instantiate(_fxPrefab, transform.position, transform.rotation);
        }
    }
}
