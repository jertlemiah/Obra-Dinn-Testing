using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu]
public class FateReasonCompressed : ScriptableObject
{
    public string fateName = "garbage",
        rawSentence = "garbage";
    public bool hasDetails = false,
        requiresAttacker = false;
    public FateReason[] fateDetails;
}
