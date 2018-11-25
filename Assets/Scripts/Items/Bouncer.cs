using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Elemental
{

    [SerializeField] float Power;
    [SerializeField] float CollisionSize;
    [SerializeField] Transform DirectionTransform;

    [SerializeField] float WaitBetweenJumps;
    [SerializeField] float JumpAnimationSpeed;

    [SerializeField] bool _overrideForce = true;
    
    Animation _animation;

    float _curWaitingTime = 0f;

    private void Awake()
    {
        _animation = gameObject.GetComponent<Animation>();
        _animation["BounceAnimation"].speed = JumpAnimationSpeed;
    }

    private void Update()
    {
        _curWaitingTime += Time.deltaTime;

        if(_curWaitingTime > WaitBetweenJumps)
        {
            _curWaitingTime = 0f;
            Bounce();
        }
    }

    public void Bounce()
    {
        _animation.Play();

        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(CollisionSize, CollisionSize, CollisionSize));
        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.name.ToLower().Contains("player"))
            {
                var rigidbody = hitCollider.gameObject.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    if (_overrideForce)
                    {
                        rigidbody.velocity = Vector3.zero;
                    }
                    rigidbody.AddForce(DirectionTransform.up * Power, ForceMode.Impulse);
                }
            }
        }

    }
}
