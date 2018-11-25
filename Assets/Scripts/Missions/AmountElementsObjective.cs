using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmountElementsObjective : Objective
{
	[SerializeField] private int _amountToGet;
	private int _currentAmount;

	public void OnGetElement()
	{
		_currentAmount++;
		if (_currentAmount >= _amountToGet)
		{
			OnObjectiveUpdated(true, this);
		}
	}
}
