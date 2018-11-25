using BlastyEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMagnet : MonoBehaviour
{
    [SerializeField] float _maxPowerDistance = 1f;
    [SerializeField] float _minPowerDistance = 3f;
    [SerializeField] float _minSpeedInfluence = 1f;
    [SerializeField] float _maxSpeedInfluence = 5f;

    [SerializeField] bool _fakeLerpWhenReachingMaxSpeedZone;

    Rigidbody _playerRigidbody;
    bool _playerCaught;
    bool _magnetActive = true;

	void Start()
    {
        _playerRigidbody = FindObjectOfType<PlayerController>().GetComponent<Rigidbody>();
        EventManager.Instance.StartListening(TouchEvent.EventName, OnTouch);
    }
	
	void Update()
    {
        if(!_magnetActive)
        {
            return;
        }

        var playerToMagnetDirection = transform.position - _playerRigidbody.transform.position;
        var distanceToPlayer = playerToMagnetDirection.magnitude;
        _playerCaught = distanceToPlayer <= _minPowerDistance;
        if(distanceToPlayer > 1f)
        {
            playerToMagnetDirection.Normalize();
        }

        if(_fakeLerpWhenReachingMaxSpeedZone && distanceToPlayer <= _maxPowerDistance)
        {
            if(_playerRigidbody.angularVelocity.magnitude < 0.2f) // Release the ball when throwing
            {
                _playerRigidbody.transform.position = Vector3.Lerp(_playerRigidbody.transform.position, transform.position, 0.9f);
            }
        }
        else if (distanceToPlayer <= _minPowerDistance)
        {
            var speedInfluence = Mathf.Lerp(_minSpeedInfluence, _maxSpeedInfluence, distanceToPlayer / (_minPowerDistance - _maxPowerDistance));
            if(_fakeLerpWhenReachingMaxSpeedZone) _playerRigidbody.angularVelocity = Vector3.zero;
            _playerRigidbody.velocity += playerToMagnetDirection * speedInfluence;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _minPowerDistance);
        Gizmos.DrawWireSphere(transform.position, _maxPowerDistance);
    }

    private void OnTouch(BlastyEventData ev)
    {
        var touchEventData = (TouchEventData)ev;

        if (touchEventData.TouchState == TouchManager.TouchState.FinishPan)
        {
            var ray = Camera.main.ScreenPointToRay(touchEventData.CurPosition);
            var touchedElements = Physics.RaycastAll(ray);
            foreach(var touchedElement in touchedElements)
            {
                if(touchedElement.collider.gameObject == gameObject)
                {
                    OnMagnetPressed();
                    return;
                }
            }
        }
    }

    void OnMagnetPressed()
    {
        if(_playerCaught)
        {
            StartCoroutine(DeactivateMagnet(3f));
            // Add some extra speed to the ball?
        }
    }

    IEnumerator DeactivateMagnet(float deactivationTime)
    {
        _magnetActive = false;
        yield return new WaitForSeconds(deactivationTime);
        _magnetActive = true;
    }
}
