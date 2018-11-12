using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
	public int LoadedLevelIndex;

	private Dictionary<int, List<bool>> _completedStars;

	public PlayerModel(int amountLevels)
	{
		_completedStars = new Dictionary<int, List<bool>>();
		for (int i = 0; i < amountLevels; i++)
		{
			var list = new List<bool> {false, false, false};
			_completedStars.Add(i,list);
		}
	}

	public void SetLevelStars(List<bool> completed)
	{
		if (_completedStars[LoadedLevelIndex] == null)
		{
			_completedStars[LoadedLevelIndex] = new List<bool>(completed);
		}
		else
		{
			_completedStars[LoadedLevelIndex][0] |= completed[0];
			_completedStars[LoadedLevelIndex][1] |= completed[1];
			_completedStars[LoadedLevelIndex][2] |= completed[2];
		}
	}

	public List<bool> GetCurrentCompletedStarsFromLevel(int index)
	{
		return _completedStars[index];
	}
}
