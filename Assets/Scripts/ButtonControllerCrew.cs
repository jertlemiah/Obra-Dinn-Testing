using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonControllerCrew : ButtonController
{
    [SerializeField]
    public TMP_Text crewNumber, crewName, crewQuality, crewOrigin, centerText;

    [SerializeField]
    public CrewMember crewMember;

    public void ChangeCrew(CrewMember newCrewMember)
    {
        crewMember = newCrewMember;

        if(newCrewMember.correctName == "Unknown")
        {
            UpdateText("Unknown");
        }
        else
        {
            UpdateText(crewMember.internalID.ToString(), crewMember.correctName, crewMember.quality.role, crewMember.crewOrigin);
        }

    }

    public void SetButtonInactive()
    {
        UpdateText("");
        this.GetComponent<Button>().enabled = false;
        
    }

    public void UpdateText(string crewNumber, string crewName, string crewQuality, string crewOrigin)
    {
        this.GetComponent<Button>().enabled = true;
        this.centerText.text = "";
        this.crewNumber.text = crewNumber;
        this.crewName.text = crewName;
        this.crewQuality.text = crewQuality;
        this.crewOrigin.text = crewOrigin;
    }

    public void UpdateText(string centerText)
    {
        this.GetComponent<Button>().enabled = true;
        this.crewNumber.text = "";
        this.crewName.text = "";
        this.crewQuality.text = "";
        this.crewOrigin.text = "";
        this.centerText.text = centerText;
    }
}
