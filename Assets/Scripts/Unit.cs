using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Unit : MonoBehaviour
{
    public string unitName;

    public Unit (string unitName)
    {
        this.unitName = unitName;
    }
    
}
