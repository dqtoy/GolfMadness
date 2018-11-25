using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabLoaderComponent : LogicComponent
{
    [SerializeField] private string _path;
    [SerializeField] private Transform _parent;

    public override void Initialize()
    {
        var obj = Resources.Load(_path) as GameObject;
        var unique = obj.GetComponent<UniqueElement>();
        if (unique != null)
        {
            var objects = FindObjectsOfType<UniqueElement>();
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].name.Contains(obj.name))
                {
                    return;
                }
            }
        }
        
        Instantiate(obj, _parent);
    }
}