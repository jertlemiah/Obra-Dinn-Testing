using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu]
public class CrewMember : ScriptableObject
{
    public int internalID = 0;

    public bool correctFate = false,
        confirmedFate = false,
        hasAttacker = false,
        isGeneric = false,
        usesMasculine = true;

    public string currentName = "Unknown",
        crewName,
        //currentReason = "Unknown",
        //currentDetails = "Unknown",
        crewOrigin = "Unknown",
        currentAttacker = "Unknown";

    [SerializeField]
    public Quality quality;

    [SerializeField]
    public FateReason currentReason;

    [SerializeField]
    public AcceptableFate[] acceptableFates = new AcceptableFate[1];

    [SerializeField]
    public string fateSentence;

    public Sprite portrait;

    public string UpdateFateSentence()
    {
        Debug.Log("Updating the fateSentence of fatePage ID " + internalID);
        fateSentence = currentReason.rawSentence;
        //Debug.Log("Raw: " + fateSentence);

        fateSentence = fateSentence.Replace("[details]", currentReason.detail);
        //Debug.Log("After details: " + fateSentence);

        if (usesMasculine)
            fateSentence = fateSentence.Replace("[pronoun]", "his");
        else
            fateSentence = fateSentence.Replace("[pronoun]", "her");
        //Debug.Log("After pronouns: " + fateSentence);

        fateSentence = fateSentence.Replace("\\n", "\n");
        //Debug.Log("After newline: " + fateSentence);

        hasAttacker = currentReason.requiresAttacker;

        return fateSentence;
    }

    public bool ValidateFate()
    {
        if (currentName != crewName)
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
    public FateReason reason;
    public string attacker;

    public AcceptableFate() { }

    public AcceptableFate(FateReason reason, string attacker)
    {
        this.reason = reason;
        this.attacker = attacker;
    }
}
