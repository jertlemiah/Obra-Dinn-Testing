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

    private MenuFate menuFate;
    private MenuManager menuManager;
    private MenuFateCrew menuFateCrew;

    private void Start()
    {
        menuManager = MenuManager.Instance;
        menuFate = menuManager.menuFate;
        menuFateCrew = menuManager.menuFateCrew;
    }

    public void CrewButtonPressed()
    {
        Debug.Log("CrewButton " + crewMember.crewName + " has been pressed");

        // If this is not the attacker, set the crew name
        if (!menuFateCrew.isAttackerWindow) {
            menuFate.SelectNewCurrentCrewMember(crewMember);     
        }
        //else set the attacker name
        else {
            menuFate.SelectNewAttacker(crewMember);
        }
        // close fate crew menu
        //menuFate.UI_CrewMemberPopup.SetActive(false); ;
    }

    public void ChangeCrew(CrewMember newCrewMember)
    {
        crewMember = newCrewMember;

        //if(!newCrewMember.quality)
        //{
        //    newCrewMember.quality = fin
        //}

        if (newCrewMember.quality.surrole == "Unknown")
        {
            UpdateText("Unknown");
        }
        else if (newCrewMember.quality.surrole == "Beast")
        {
            UpdateText("Terrible Beast");
        }
        else if (crewMember.isGeneric == true)
        {
            UpdateText("", "Unknown", crewMember.quality.surrole, "");
        }
        else
        {
            //string firstName, lastName = "";
            //string[] names = crewMember.crewName.Split();
            //firstName = names[0];
            //if (names.Length >= 2)
            //    lastName = names[names.Length - 1];

            //UpdateText(crewMember.internalID.ToString(), firstName + " " + lastName, crewMember.quality.role, crewMember.crewOrigin);
            UpdateText(crewMember.internalID.ToString(), crewMember.crewName, crewMember.quality.role, crewMember.crewOrigin);

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
