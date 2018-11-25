using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpot : MonoBehaviour
{
    [SerializeField] float _activationDistance;

    Action<TeleportSpot> _spotActivatedAction;
    Transform _player;

    public void Init(Action<TeleportSpot> spotActivatedAction)
    {
        _spotActivatedAction = spotActivatedAction;
        _player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        if(distanceToPlayer <= _activationDistance)
        {
            _spotActivatedAction(this);
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _activationDistance);
    }
}
