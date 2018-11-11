﻿using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Config/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string LevelId;
    public int Order;
    [HideInInspector] public int Index;
    public int AmountOfTries;
    public string Title;
    public string MainMissionDescription;
    public string SecondaryMissionDescription;
    public string ThirdMissionDescription;
    public string Scene;
}

