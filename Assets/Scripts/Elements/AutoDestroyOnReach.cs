using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyOnReach : MonoBehaviour
{
	[SerializeField] private float height = -6;
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= height)
		{
			Destroy(gameObject);
		}
	}
}
