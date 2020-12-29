using System.Collections;
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
    public bool hasDetails = false,
        showDetails = false;
    [SerializeField]
    public List<FateReason> detailsList = new List<FateReason>();

    private MenuFateReason menuFateReason;
    private MenuManager menuManager;
    private MenuFate menuFate;

    private void Start()
    {
        menuManager = MenuManager.Instance;
        menuFateReason = menuManager.menuFateReason;
        menuFate = menuManager.menuFate;
    }

    public void FateButtonPressed()
    {
        Debug.Log("FateButton has been pressed");
        if(hasDetails==false || 
            (hasDetails == false && menuFateReason.isDetailsOpen == true))
        {
            //Debug.Log("if 1");
            // Set reason for this page
            menuFate.SelectNewFate(this.fateReason);
            // if details is open switch back to just reason page
            menuFateReason.SwitchFatePopup(false);
            // close fate reason menu
            menuFate.UI_FateReasonPopup.SetActive(false); ;
        }
        else if (hasDetails == true && menuFateReason.isDetailsOpen == false)
        {
            //Debug.Log("else if 2");
            // Switch to details menu for the specified button
            menuFateReason.LoadFateDetailPopup(this);
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
        if(showDetails == true)
        {
            this.tmpText.text = fateReason.detail;
            detailsArrow.SetActive(false);
        }
        else
        {
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
