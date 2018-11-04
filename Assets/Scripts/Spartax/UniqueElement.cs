using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueElement : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    

}