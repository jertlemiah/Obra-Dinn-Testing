using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private InputManager inputManager;
    public GameObject UI_FatePopup;
    public GameObject UI_FateReasonPopup;
    public GameObject UI_BookPopup;
    private bool IsBookOpen = false;
    private bool IsFateOpen = false;
    private bool IsFateReasonOpen = false;

    void Awake()
    {
        inputManager = InputManager.Instance;
        // Every time the OpenBook action is performed, run the OpenBook function here
        inputManager.GetInputActions().Player.OpenBook.performed += context => ToggleBook();
    }

    void ToggleBook()
    {
        if (IsBookOpen) CloseBook();
        else        OpenBook();
    }

    void OpenBook()
    {
        UI_BookPopup.SetActive(true);
        IsBookOpen = true;
        Cursor.visible = true;
    }

    void CloseBook()
    {
        UI_BookPopup.SetActive(false);
        IsBookOpen = false;
        Cursor.visible = false;
    }

    // Update is called once per frame
    public void ToggleFatePopup()
    {
        if (IsFateOpen)
        {
            UI_FatePopup.SetActive(false);
            IsFateOpen = false;
        }
        else
        {
            UI_FatePopup.SetActive(true);
            IsFateOpen = true;
        }
    }

    // Update is called once per frame
    public void ToggleFateReasonPopup()
    {
        if (IsFateReasonOpen)
        {
            UI_FateReasonPopup.SetActive(false);
            IsFateReasonOpen = false;
        }
        else
        {
            UI_FateReasonPopup.SetActive(true);
            IsFateReasonOpen = true;
        }
    }
}
