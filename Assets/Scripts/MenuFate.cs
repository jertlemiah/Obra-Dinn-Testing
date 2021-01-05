using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuFate : MonoBehaviour
{
    public GameObject UI_FateReasonPopup,
        UI_CrewMemberPopup,
        portraitObj,
        btn_Name, btn_FateReason, btn_Attacker;
    public TMP_Text name_TMPtext, fateReason_TMPtext, attacker_TMPtext;
    public CrewMember currentCrewMemberPage;
    public bool disabled = true;

    private void Start()
    {
        PopulatePage();
    }

    private void OnEnable()
    {
        PopulatePage();
    }

    public void PopulatePage()
    {
        if (currentCrewMemberPage.currentCrewMember.crewName == "Unknown")
        {
            name_TMPtext.text = "This unknown soul";
        }
        else if(currentCrewMemberPage.currentCrewMember.isGeneric)
        {
            name_TMPtext.text = "This " + currentCrewMemberPage.currentCrewMember.crewName.ToLower();
        }
        else
        {
            name_TMPtext.text = currentCrewMemberPage.currentCrewMember.crewName;
        }

        fateReason_TMPtext.text = currentCrewMemberPage.UpdateFateSentence();
        if (currentCrewMemberPage.hasAttacker == true)
        {
            btn_Attacker.SetActive(true);
            if (currentCrewMemberPage.currentAttacker.crewName == "Unknown")
            {
                attacker_TMPtext.text = "by an unknown attacker.";
            }
            else if (currentCrewMemberPage.currentAttacker.isGeneric)
            {
                attacker_TMPtext.text = "by an unknown " + currentCrewMemberPage.currentAttacker.quality.surrole.ToLower() + ".";
            }
            else
            {
                attacker_TMPtext.text = "by " + currentCrewMemberPage.currentAttacker.crewName + ".";
            }
            
        }
        else
        {
            btn_Attacker.SetActive(false);
        }

        portraitObj.GetComponent<Image>().sprite = currentCrewMemberPage.portrait;
    }

    public void SelectNewFate(FateReason fateReason)
    {
        currentCrewMemberPage.currentReason = fateReason;
        currentCrewMemberPage.UpdateFateSentence();
        ToggleFateReasonPopup();
    }

    public void SelectNewCurrentCrewMember(CrewMember newCrewMember)
    {
        currentCrewMemberPage.currentCrewMember = newCrewMember;
        currentCrewMemberPage.UpdateFateSentence();
        ToggleFateCrewPopup();
    }

    public void SelectNewAttacker(CrewMember newAttacker)
    {
        currentCrewMemberPage.currentAttacker = newAttacker;
        currentCrewMemberPage.UpdateFateSentence();
        ToggleFateCrewPopup();
    }

    public void UpdatedSelectedFate(CrewMember newFateDetails)
    {
        currentCrewMemberPage.currentName = newFateDetails.currentName;
    }

    public void ToggleFateReasonPopup()
    {
        if (UI_FateReasonPopup.activeSelf == true) {
            UI_FateReasonPopup.SetActive(false);
            PopulatePage();
        }
        else {
            UI_FateReasonPopup.SetActive(true);
        }
    }

    public void ToggleFateCrewPopup()
    {
        ToggleFateCrewPopup(false);
    }

    public void ToggleFateCrewPopup(bool openAttackerWindow)
    {
        if (UI_CrewMemberPopup.activeSelf == true)
        {
            UI_CrewMemberPopup.SetActive(false);

            PopulatePage();
        }
        else
        {
            UI_CrewMemberPopup.SetActive(true);
            UI_CrewMemberPopup.GetComponent<MenuFateCrew>().isAttackerWindow = openAttackerWindow;
            UI_CrewMemberPopup.GetComponent<MenuFateCrew>().ChangeToPageNumber(1);
        }
    }

    public void EnableFateMenuButtons()
    {
        btn_Name.GetComponent<Button>().enabled = true;
        btn_Name.GetComponent<FateReasonButton>().disableVisuals = false;

        btn_FateReason.GetComponent<Button>().enabled = true;
        btn_FateReason.GetComponent<FateReasonButton>().disableVisuals = false;

        if (btn_Attacker.activeInHierarchy)
        {
            btn_Attacker.GetComponent<Button>().enabled = true;
            btn_Attacker.GetComponent<FateReasonButton>().disableVisuals = false;
        }
            
        disabled = false;
    }

    public void DisableFateMenuButtons()
    {
        btn_Name.GetComponent<Button>().enabled = false;
        btn_Name.GetComponent<FateReasonButton>().disableVisuals = true;

        btn_FateReason.GetComponent<Button>().enabled = false;
        btn_FateReason.GetComponent<FateReasonButton>().disableVisuals = true;

        if (btn_Attacker.activeInHierarchy)
        {
            btn_Attacker.GetComponent<Button>().enabled = false;
            btn_Attacker.GetComponent<FateReasonButton>().disableVisuals = true;
        }
            
        disabled = true;
    }

    public void ToggleFateMenuButtons()
    {
        if (disabled)
            EnableFateMenuButtons();
        else
            DisableFateMenuButtons();
    }
   

    public void OpenFatePopup(CrewMember fatePage)
    {
        currentCrewMemberPage = fatePage;
        UI_FateReasonPopup.SetActive(true);

        name_TMPtext.text = currentCrewMemberPage.currentName;
        fateReason_TMPtext.text = currentCrewMemberPage.currentReason.rawSentence;
        attacker_TMPtext.text = currentCrewMemberPage.currentAttackerName;
    }

    //public void OpenFatePopup(FatePage fatePage)
    //{
    //    currentFate = fatePage;
    //    UI_FateReasonPopup.SetActive(true);

    //    name_TMPtext.text = currentFate.currentName;
    //    fateReason_TMPtext.text = currentFate.currentReason;
    //    attacker_TMPtext.text = currentFate.currentAttacker;
    //}
}


