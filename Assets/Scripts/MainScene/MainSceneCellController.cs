using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneCellController : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _title;
	[SerializeField] private List<GameObject> _stars;

	private LevelData _levelData;
	private Action<LevelData> _onPressOK;
	public void Init(LevelData data, List<bool> alreadyCompleted, Action<LevelData> onPressOK)
	{
		_levelData = data;
		_onPressOK = onPressOK;
		_title.text = _levelData.Title;
		for (int i = 0; i < _stars.Count; i++)
		{
			if (alreadyCompleted.Count > i)
			{
				_stars[i].SetActive(alreadyCompleted[i]);
			}
		}
	}

	public void OnPressOK()
	{
		if (_onPressOK != null)
		{
			_onPressOK(_levelData);
		}
	}
}
