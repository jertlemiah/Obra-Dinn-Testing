﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class FateReasonButton : ButtonController
{
    [SerializeField]
    public FateReason fateReason;
    [SerializeField]
    public GameObject detailsArrow;
    [SerializeField]
    public bool hasDetails = false;
    [SerializeField]
    public List<FateReason> detailsList = new List<FateReason>();

    private MenuFateReason menuFateReason;

    private void Start()
    {
        menuFateReason = MenuFateReason.Instance;
    }

    public void FateButtonPressed()
    {
        Debug.Log("FateButton has been pressed");
        if(hasDetails==false || 
            (hasDetails == false && menuFateReason.isDetailsOpen == true))
        {
            // Set reason for this page
            // if details is open switch back to just reason page
            // close fate reason menu
        }
        else if (hasDetails == true && menuFateReason.isDetailsOpen == false)
        {
            // Switch to details menu for the specified button
            menuFateReason.
        }

    }

    public void UpdateFateReason(FateReason fr)
    {
        fateReason = fr;
        detailsList.Clear();
        UpdateButtonDisplay();
    }

    public void AddNewDetail(FateReason fr)
    {
        detailsList.Add(fr);
    }

    public void SetButtonInactive()
    {
        this.GetComponent<Button>().enabled = false;
        this.tmpText.text = "";
    }

    public void UpdateButtonDisplay()
    {
        this.GetComponent<Button>().enabled = true;
        this.tmpText.text = fateReason.fateName;
        if (fateReason.hasDetails)
        {
            detailsArrow.SetActive(true);
        }
        else
        {
            detailsArrow.SetActive(false);
        }

    }
    //[SerializeField]
    //GameObject SelectorArrows, SelectedOutline, PressedBackground;
    //[SerializeField]
    //TMP_Text text;
    //[SerializeField]
    //Color darkColor, lightColor;

    //public void OnSelect(BaseEventData eventData)
    //{
    //    SelectedOutline.SetActive(true);
    //}

    //public void OnDeselect(BaseEventData eventData)
    //{
    //    SelectedOutline.SetActive(false);
    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    SelectorArrows.SetActive(true);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    SelectorArrows.SetActive(false);
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    PressedBackground.SetActive(true);
    //    text.color = darkColor;
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    PressedBackground.SetActive(false);
    //    text.color = lightColor;
    //}
}
