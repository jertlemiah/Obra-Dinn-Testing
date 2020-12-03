using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class FateReasonButton : MonoBehaviour, 
    ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    GameObject SelectorArrows, SelectedOutline, PressedBackground;
    [SerializeField]
    TMP_Text text;
    [SerializeField]
    Color darkColor, lightColor;

    public void OnSelect(BaseEventData eventData)
    {
        SelectedOutline.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        SelectedOutline.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SelectorArrows.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SelectorArrows.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PressedBackground.SetActive(true);
        text.color = darkColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PressedBackground.SetActive(false);
        text.color = lightColor;
    }
}
