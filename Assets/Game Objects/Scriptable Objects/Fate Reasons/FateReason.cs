using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu]
public class FateReason : ScriptableObject
{
    public string name,
        detail,
        sentence;
    public bool hasDetails = false, 
        requiresAttacker = false;
}
