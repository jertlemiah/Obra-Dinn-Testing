using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class MenuFateCrew : MonoBehaviour
{
    public GameObject btn_pageRight, btn_pageLeft, 
        btnGrp_Crew;

    public TMP_Text tmpText_PageNum;

    MenuManager menuManager;
    private MenuFate menuFate;

    public List<GameObject> btnObjList_Crew = new List<GameObject>();

    public List<CrewMember> crewMemberList = new List<CrewMember>();

    public int currentPage = 1;
    [SerializeField]
    public int pageCount = 4;

    // Start is called before the first frame update
    void Start()
    {
        currentPage = 1;
        menuManager = MenuManager.Instance;
        menuFate = menuManager.menuFate;

        GetButtonObjects(btnGrp_Crew, btnObjList_Crew);
        LoadCrewMemberList();
        
        ChangePage(0);
    }



    private void OnEnable()
    {
        // turn off buttons of previous page
        //MenuManager.Instance.menuFate.DisableFateMenuButtons();
    }

    private void OnDisable()
    {
        // turn on buttons of previous page
        //menuManager.menuFate.EnableFateMenuButtons();
    }

    public void PopulateButtons(List<GameObject> btnObjList, List<CrewMember> sourceList)
    {
        int i = 0;
        foreach (GameObject buttonObj in btnObjList)
        {
            ButtonControllerCrew buttonScript = buttonObj.GetComponent<ButtonControllerCrew>();

            // If there is no further information to populate list with, turn off button
            if ((i + (currentPage - 1) * btnObjList.Count) >= sourceList.Count)
            {
                buttonScript.SetButtonInactive();
                //buttonScript.enabled = false;             
            }
            // Else populate button with info
            {
                Debug.Log("Current Page" + currentPage);
                Debug.Log("About to try to access element: " + (i + (currentPage - 1) * btnObjList.Count));
                buttonScript.ChangeCrew(sourceList[i + (currentPage - 1) * btnObjList.Count]);
            }
            i++;
        }
    }

    public void PopulateButton(int crewNum, string crewName, string quality, string crewOrigin)
    {
        //GameObject button = btnObjList_Crew[0];
        ButtonControllerCrew btn = btnObjList_Crew[0].GetComponent<ButtonControllerCrew>();
        btn.UpdateText(crewNum.ToString(), crewName, quality, crewOrigin);

    }

    public void ChangePage(int pagesTurned)
    {
        //Debug.Log("btnObjList_Reasons.Count: " + btnObjList_Reasons.Count);
        //Debug.Log("fateReasonsList.Count: " + fateReasonsList.Count);
        pageCount = (int)Mathf.Ceil(crewMemberList.Count / btnObjList_Crew.Count);
        Debug.Log("ChangePage called, on page number: " + currentPage +
            " turning " + pagesTurned + " pages");
        //Debug.Log(pageCount);
        currentPage += pagesTurned;

        // Get the new page number, clamped from 1 to max page count
        currentPage = Mathf.Clamp(currentPage, 1, pageCount);

        // Populate the buttons with info
        PopulateButtons(btnObjList_Crew, crewMemberList);

        // If first page, turn off left turn page button
        if (currentPage == 1)
        {
            btn_pageLeft.SetActive(false);
        }
        else
        {
            btn_pageLeft.SetActive(true);
        }
        // If last page, turn off right turn off button
        if (currentPage == pageCount)
        {
            btn_pageRight.SetActive(false);
        }
        else
        {
            btn_pageRight.SetActive(true);
        }
        //Update the page number display
        //lbl_PageNumber.GetComponent<TMP_Text>().text = currentPage + " / " + pageCount;
        tmpText_PageNum.text = currentPage + " / " + pageCount;
    }

    //public void PopulateButton2(int crewNum, string crewName, string quality, string crewOrigin)
    //{
    //    string buttonText;
    //    buttonText = 

    //    ButtonControllerCrew btn = btnObjList_Crew[0].GetComponent<ButtonControllerCrew>();
    //    btn.UpdateText(buttonText);
    //}

    private void GetButtonObjects(GameObject btnGrpObj, List<GameObject> btnObjList)
    {
        if (btnGrpObj == null)
        {
            Debug.Log("ButtonGroup GameObject is null, cannot get button GameObjects");
            return;
        }

        int childCount = 0;
        childCount = btnGrpObj.transform.childCount;
        //Debug.Log("Child count of the ButtonGroup: " + childCount);
        foreach (Transform childTransform in btnGrpObj.transform)
        {
            btnObjList.Add(childTransform.gameObject);
        }
    }

    private void LoadCrewMemberList()
    {
        crewMemberList.Clear();
        crewMemberList = Resources.LoadAll("", typeof(CrewMember)).Cast< CrewMember>().ToList();
        //Debug.Log("Loaded " + fateReasons.Count + " fateReason scriptable objects from the folder: " + fatesDestinationFolder);
    }
}
