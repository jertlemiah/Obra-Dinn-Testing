using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuFate : MonoBehaviour
{
    public GameObject UI_FateReasonPopup,
        portraitObj,
        btn_Name, btn_FateReason, btn_Attacker;
    public TMP_Text name_TMPtext, fateReason_TMPtext, attacker_TMPtext;
    public CrewMember currentFatePage;
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
        if (currentFatePage.currentName == "Unknown")
        {
            name_TMPtext.text = "This unknown soul";
        }
        else
        {
            name_TMPtext.text = currentFatePage.currentName;
        }

        fateReason_TMPtext.text = currentFatePage.UpdateFateSentence();
        if (currentFatePage.hasAttacker == true)
        {
            btn_Attacker.SetActive(true);
            attacker_TMPtext.text = "by " + currentFatePage.currentAttacker;
        }
        else
        {
            btn_Attacker.SetActive(false);
        }

        portraitObj.GetComponent<Image>().sprite = currentFatePage.portrait;
    }

    public void SelectNewFate(FateReason fateReason)
    {
        currentFatePage.currentReason = fateReason;
        currentFatePage.UpdateFateSentence();
        ToggleFateReasonPopup();
    }

    public void UpdatedSelectedFate(CrewMember newFateDetails)
    {
        currentFatePage.currentName = newFateDetails.currentName;
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
        currentFatePage = fatePage;
        UI_FateReasonPopup.SetActive(true);

        name_TMPtext.text = currentFatePage.currentName;
        fateReason_TMPtext.text = currentFatePage.currentReason.rawSentence;
        attacker_TMPtext.text = currentFatePage.currentAttacker;
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


