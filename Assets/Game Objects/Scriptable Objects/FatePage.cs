using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FatePage : ScriptableObject
{
    public int internalID = 0;

    public bool correctFate = false,
        confirmedFate = false;

    public string currentName = "Unknown",
        correctName,
        currentReason = "Unknown",
        currentAttacker = "Unknown";

    [SerializeField]
    public AcceptableFate[] acceptableFates;

    public Sprite portrait;

    public bool ValidateFate()
    {
        if (currentName != correctName)
        {
            correctFate = false;
            return correctFate;
        }

        foreach (AcceptableFate fate in acceptableFates)
        {
            if(fate.attacker == currentAttacker && fate.reason == currentReason)
            {
                correctFate = true;
                return correctFate;
            }
        }

        // If none of the acceptableFates match, then return false
        correctFate = false;
        return false;
    }

}

[System.Serializable]
public class AcceptableFate
{
    public string reason, attacker;

    AcceptableFate() { }

    AcceptableFate(string reason, string attacker)
    {
        this.reason = reason;
        this.attacker = attacker;
    }
}
