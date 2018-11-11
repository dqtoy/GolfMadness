using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoaderComponent : LogicComponent
{
    [SerializeField] private string _path;
    [SerializeField] private Transform _parent;

    public override void Initialize()
    {
        if (_parent == null)
        {
            _parent = transform;
        }

        var obj = Resources.Load(_path);
        Instantiate(obj, _parent);
    }
}