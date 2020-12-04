﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuFate : MonoBehaviour
{
    public GameObject UI_FateReasonPopup;
    public TMP_Text name_TMPtext, fateReason_TMPtext, attacker_TMPtext;
    public FatePage currentFate;

    public void ToggleFateReasonPopup()
    {
        if (UI_FateReasonPopup.activeSelf == true) {
            UI_FateReasonPopup.SetActive(false);
        }
        else {
            UI_FateReasonPopup.SetActive(true);
        }
    }

    public void OpenFatePopup(FatePage fatePage)
    {
        currentFate = fatePage;
        UI_FateReasonPopup.SetActive(true);
        name_TMPtext.text = fatePage.currentName;
    }
}


