using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    private InputManager inputManager;
    public GameObject obj_Book,
        obj_FatePageMenu,
        obj_FateReasonMenu,
        obj_CrewMenu;
    public MenuFateReason menuFateReason;
    public MenuFate menuFate;
    public MenuBook menuBook;
    public MenuFateCrew menuFateCrew;
    private bool IsBookOpen = false;
    private bool IsFateOpen = false;
    private bool IsFateReasonOpen = false;

    void Awake()
    {
        inputManager = InputManager.Instance;

        menuFateReason = obj_FateReasonMenu.GetComponent<MenuFateReason>();
        menuFate = obj_FatePageMenu.GetComponent<MenuFate>();
        menuBook = obj_Book.GetComponent<MenuBook>();
        menuFateCrew = obj_CrewMenu.GetComponent<MenuFateCrew>();

        // Every time the OpenBook action is performed, run the OpenBook function here
        inputManager.GetInputActions().Player.ToggleBook.performed += context => ToggleBook();
        inputManager.GetInputActions().Player.Back.performed += context => NavBackOnce();
    }

    void ToggleBook()
    {
        if (IsBookOpen) CloseBook();
        else        OpenBook();
    }

    void NavBackOnce()
    {
        /*if FateReason Menu is active && details are open
         *      nav back to fate reason*/
        if (obj_FateReasonMenu.activeInHierarchy && menuFateReason.isDetailsOpen == true)
        {
            menuFateReason.SwitchFatePopup(false);
        }
        /* else if FateReason Menu is active &&  fate reason is open
         *      nav back to fate page */
        else if (obj_FateReasonMenu.activeInHierarchy && menuFateReason.isDetailsOpen == false)
        {
            menuFate.ToggleFateReasonPopup();
        }
        /* else if fate page is open
         *      nav back to book */
        else if (obj_FatePageMenu.activeInHierarchy)
        {
            ToggleFatePopup();
        }
        else
        {
            Debug.Log("Nav back one page?");
        }

        /*if details are open
         *      nav back to fate reason
         * else if fate reason is open
         *      nav back to fate page
         * else if fate page is open
         *      nav back to book
         * else if book is open
         *      nav back to previous page (?)
         */

    }

    void OpenBook()
    {
        obj_Book.SetActive(true);
        IsBookOpen = true;
        Cursor.visible = true;
    }

    void CloseBook()
    {
        obj_Book.SetActive(false);
        IsBookOpen = false;
        Cursor.visible = false;
    }

    // Update is called once per frame
    public void ToggleFatePopup()
    {
        if (IsFateOpen)
        {
            obj_FatePageMenu.SetActive(false);
            IsFateOpen = false;
        }
        else
        {
            obj_FatePageMenu.SetActive(true);
            IsFateOpen = true;
        }
    }

    // Update is called once per frame
    public void ToggleFateReasonPopup()
    {
        if (IsFateReasonOpen)
        {
            obj_FateReasonMenu.SetActive(false);
            IsFateReasonOpen = false;
        }
        else
        {
            obj_FateReasonMenu.SetActive(true);
            IsFateReasonOpen = true;
        }
    }
}
