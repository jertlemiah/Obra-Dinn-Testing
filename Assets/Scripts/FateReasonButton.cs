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
    //public bool

    public void updateFateReason(FateReason fr)
    {
        fateReason = fr;
        updateButtonDisplay();
    }

    public void setButtonInactive()
    {
        this.GetComponent<Button>().enabled = false;
        this.tmpText.text = "";
    }

    public void updateButtonDisplay()
    {
        this.GetComponent<Button>().enabled = true;
        this.tmpText.text = fateReason.name;
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
