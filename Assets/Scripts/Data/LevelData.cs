using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Config/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int Order;
    public string Title;
    public string MainMissionDescription;
    public string SecondaryMissionDescription;
    public string ThirdMissionDescription;
    public string Scene;
}

