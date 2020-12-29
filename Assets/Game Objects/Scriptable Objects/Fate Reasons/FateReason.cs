﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu]
public class FateReason : ScriptableObject
{
    public string fateName = "garbage",
        detail = "garbage",
        rawSentence = "garbage";
    public bool hasDetails = false, 
        requiresAttacker = false;
}
