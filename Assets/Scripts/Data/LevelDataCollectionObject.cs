﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataCollection", menuName = "Config/LevelDataCollection", order = 1)]

public class LevelDataCollectionObject : ScriptableObject
{
	public List<LevelData> LevelDataList;
}

public class LevelDataCollection
{
	private LevelDataCollectionObject _collectionObject;
	public List<LevelData> LevelDataList;
	
	public LevelDataCollection()
	{
		_collectionObject = Resources.Load("Config/LevelDataCollection") as LevelDataCollectionObject;
		LevelDataList = new List<LevelData>(_collectionObject.LevelDataList);
		LevelDataList.Sort(SortByOrder);
	}

	static int SortByOrder(LevelData a, LevelData b)
	{
		return (a.Order <= b.Order) ? -1 : 1;
	}
}