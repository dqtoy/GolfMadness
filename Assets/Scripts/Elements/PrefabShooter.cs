using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PrefabShooter : MonoBehaviour
{

	[SerializeField] private Transform _direction;
	[SerializeField] private float _delay;
	[SerializeField] private float _timeBetween;
	[SerializeField] private float _power;
	[SerializeField] private float _randomAddedDirectionRange;
	
	[SerializeField] private GameObject _launchPrefab;


	private bool notStarted = true;
	private float _currentTime;
	// Use this for initialization

	// Update is called once per frame
	void Update ()
	{
		_currentTime += Time.deltaTime;

		if (notStarted && _currentTime >= _delay)
		{
			notStarted = false;
			SpawnAndLaunch();
			_currentTime = 0;
		}

		if (!notStarted && _currentTime > _timeBetween)
		{
			SpawnAndLaunch();
			_currentTime = 0;
		}
	}

	void SpawnAndLaunch()
	{

		var gObj = Instantiate(_launchPrefab) as GameObject;

		gObj.transform.position = transform.position;
		var rigidbody = gObj.gameObject.GetComponent<Rigidbody>();

		if (rigidbody != null)
		{
			rigidbody.velocity = Vector3.zero;

			var newDirection = _direction.forward;
			newDirection.x += Random.Range(-_randomAddedDirectionRange, _randomAddedDirectionRange);
			newDirection.y += Random.Range(-_randomAddedDirectionRange, _randomAddedDirectionRange);
			newDirection.z += Random.Range(-_randomAddedDirectionRange, _randomAddedDirectionRange);
			
			rigidbody.AddForce(newDirection * _power, ForceMode.Impulse);
		}
	}
}
