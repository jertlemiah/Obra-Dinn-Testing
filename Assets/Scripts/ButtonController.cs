using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonController : MonoBehaviour,
    /*ISelectHandler, IDeselectHandler,*/ IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    GameObject UiSelector, UiSelected, UiPressed;
    [SerializeField]
    TMP_Text tmpText;
    [SerializeField]
    Color colorDarkText, colorLightText;
    [SerializeField]
    bool hasText = false;

    private void Start()
    {
        if(hasText == true)
        {
            if (tmpText == null)
            {
                Debug.LogError("Button '" + this.name + "': TMP obj ref not given in editor");
            }
        }

    }

    //public void OnSelect(BaseEventData eventData)
    //{
    //    UiSelected.SetActive(true);
    //}

    //public void OnDeselect(BaseEventData eventData)
    //{
    //    UiSelected.SetActive(false);
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        UiSelector.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UiSelector.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UiPressed.SetActive(true);
        if(hasText == true)
        {
            tmpText.color = colorDarkText;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UiPressed.SetActive(false);
        if (hasText == true)
        {
            tmpText.color = colorLightText;
        }
    }
}
