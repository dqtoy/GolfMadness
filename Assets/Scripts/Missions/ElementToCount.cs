using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementToCount : MonoBehaviour
{
    // Use this for initialization
    private AmountElementsObjective _amountElementsObjective;
    
    void Start()
    {
        _amountElementsObjective = FindObjectOfType<AmountElementsObjective>();
    }

   
    private void OnDestroy()
    {
        if (_amountElementsObjective != null)
        {
            _amountElementsObjective.OnGetElement();
        }
    }
}