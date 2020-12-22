﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuFate : MonoBehaviour
{
    public GameObject UI_FateReasonPopup;
    public TMP_Text name_TMPtext, fateReason_TMPtext, attacker_TMPtext;
    public FatePage currentFate;

    private void Start()
    {
        PopulatePage();
    }

    public void SelectNewFate(FateReason fateReason)
    {
        currentFate.currentReason = fateReason;
        ToggleFateReasonPopup();
    }

    public void ToggleFateReasonPopup()
    {
        if (UI_FateReasonPopup.activeSelf == true) {
            UI_FateReasonPopup.SetActive(false);
        }
        else {
            UI_FateReasonPopup.SetActive(true);
        }
    }

    public void PopulatePage()
    {
        if(currentFate.currentName == "Unknown")
        {
            name_TMPtext.text = "This unknown soul";
        }
        else
        {
            name_TMPtext.text = currentFate.currentName;
        }
        
        fateReason_TMPtext.text = currentFate.currentReason.sentence;
        if(currentFate.hasAttacker == true)
        {
            attacker_TMPtext.text = currentFate.currentAttacker;
        }
        else
        {
            attacker_TMPtext.text = "";
        }

    }

    public void OpenFatePopup(FatePage fatePage)
    {
        currentFate = fatePage;
        UI_FateReasonPopup.SetActive(true);

        name_TMPtext.text = currentFate.currentName;
        fateReason_TMPtext.text = currentFate.currentReason.sentence;
        attacker_TMPtext.text = currentFate.currentAttacker;
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


