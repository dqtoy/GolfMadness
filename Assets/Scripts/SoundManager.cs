using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager _instance;

	[SerializeField] private AudioSource _shootClip;
	[SerializeField] private AudioSource _reboundClip;
	[SerializeField] private AudioSource _victoryClip;
	[SerializeField] private AudioSource _defeatClip;
	[SerializeField] private AudioSource _paintClip;
	
	public static SoundManager Instance
	{
		get
		{
			return _instance;
		}
		
	}

	void Start ()
	{
		_instance = this;
	}
	
	public void PlayShoot()
	{
		_shootClip.Play();
	}

	public void PlayRebound()
	{
		_reboundClip.Play();
	}

	public void PlayVictory()
	{
		_victoryClip.Play();
	}

	public void PlayDefat()
	{
		_defeatClip.Play();
	}
	
	public void PlayPaint()
	{
		_paintClip.Play();
	}
}
