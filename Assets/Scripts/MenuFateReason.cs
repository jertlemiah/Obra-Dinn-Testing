using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEditor;
using System.Linq;
using UnityEngine.UI;

public class MenuFateReason : Singleton<MenuFateReason>
{
    public string fateReasonsJsonFileName = "Assets/Scripts/FateReasons.txt";
    public List<FateReason> fateReasonsList = new List<FateReason>(),
        fateDetailsList = new List<FateReason>();
    //public FateReason[] fateReasons;
    public GameObject block_FateReaons, block_FateDetails,
        panel_FateReasonMenu,
        btnGrp_Reasons,
        btn_pageRight, btn_pageLeft,
        //lbl_PageNumber,
        btnGrp_Details;//, lbl_DetailsName;
    public TMP_Text tmpText_PageNum,
        tmpText_FateName;

    public bool isDetailsOpen = false;

    [SerializeField]
    public int panelDefaultHeight = 604,
        panelMinHeight = 264, 
        panelMaxHeight = 800, 
        btnIncHeight = 52; 

    public List<GameObject> btnObjList_Reasons = new List<GameObject>(),
        btnObjList_Details = new List<GameObject>();

    public int currentPage = 1;
    [SerializeField]
    public int pageCount = 4;
    public string fatesDestinationFolder = "Assets/Game Objects/Scriptable Objects/Fate Reasons/Resources";

    MenuManager menuManager;
    private MenuFate menuFate;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = MenuManager.Instance;
        menuFate = menuManager.menuFate;

        GetButtonObjects(btnGrp_Reasons, btnObjList_Reasons);
        Debug.Log("Count in buttons List " + btnObjList_Reasons.Count);
        

        GetButtonObjects(btnGrp_Details, btnObjList_Details);

        //ReadFateReasonsFile();
        LoadCreatedFateReasons();
        currentPage = 1;
        //pageCount = (int)Mathf.Ceil(fateReasonsList.Count / btnObjList_Reasons.Count);
        //SwitchFatePopup(false);
        //ChangePage(0);
        ChangePage(0);
    }

    private void OnEnable()
    {
        // turn off buttons of previous page
        MenuManager.Instance.menuFate.DisableFateMenuButtons();
    }

    private void OnDisable()
    {
        // turn on buttons of previous page
        menuManager.menuFate.EnableFateMenuButtons();
    }

    public void ChangePage(int pagesTurned)
    {
        //Debug.Log("btnObjList_Reasons.Count: " + btnObjList_Reasons.Count);
        //Debug.Log("fateReasonsList.Count: " + fateReasonsList.Count);
        pageCount = (int)Mathf.Ceil(fateReasonsList.Count / btnObjList_Reasons.Count);
        Debug.Log("ChangePage called, on page number: " + currentPage + 
            " turning " + pagesTurned + " pages");
        //Debug.Log(pageCount);
        currentPage += pagesTurned;

        // Get the new page number, clamped from 1 to max page count
        currentPage = Mathf.Clamp(currentPage, 1, pageCount);

        // Populate the buttons with info
        PopulateButtons(btnObjList_Reasons, fateReasonsList, false);

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

    private void GetButtonObjects(GameObject btnGrpObj, List<GameObject> btnObjList)
    {
        if(btnGrpObj == null)
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

    private void PopulateButtons(List<GameObject> btnObjList, List<FateReason> sourceList, bool autoSize)
    {
        //pageCount = (int)Mathf.Ceil(sourceList.Count / btnObjList.Count);
        //pageCount = (int)Mathf.Ceil(fateReasons.Length / buttons.Count);
        //FateReason previousFate = new FateReason(),
        //    currentFate = new FateReason();

        FateReason currentFate = ScriptableObject.CreateInstance<FateReason>(),
            previousFate = ScriptableObject.CreateInstance<FateReason>();


        int i = 0, numActive = 0;
        foreach (GameObject buttonObj in btnObjList)
        {
            //TMP_Text btnTMPobj = buttonObj.GetComponentInChildren<TMP_Text>();
            //btnTMPobj.text = fateReasons[i + (currentPage-1)*buttons.Count].name;

            FateReasonButton buttonScript = buttonObj.GetComponent<FateReasonButton>();

            // If there is no further information to populate list with, turn off button
            if ((i + (currentPage - 1) * btnObjList.Count) >= sourceList.Count)
            {
                if(autoSize == true)
                    buttonObj.SetActive(false);
                else
                {
                    buttonScript.SetButtonInactive();
                    buttonScript.enabled = false;
                }
                i++;
            }
            // Else, populate button with correct information
            else
            {
                buttonObj.SetActive(true);
                buttonScript.enabled = true;
                numActive++;
                bool again = false;
                
                do
                {
                    // If there are no more fates to read OR

                    if (i + (currentPage - 1) * btnObjList.Count >= sourceList.Count){
                        break;
                    }
                    currentFate = sourceList[i + (currentPage - 1) * btnObjList.Count];

                    // if this is a retry, and the previous has a different name than the current
                    // then this should be a new button, not compressed into current button
                    if (again == true && previousFate.fateName != currentFate.fateName)
                    {
                        break;
                    }

                    // If there are details, need to compress them into a single button
                    if (isDetailsOpen == false && currentFate.hasDetails == true)
                    {
                        // if first time, repopulate the main fate of the button
                        if(previousFate.fateName != currentFate.fateName)
                        {
                            buttonScript.UpdateFateReason(currentFate);
                            buttonScript.AddNewDetail(currentFate);
                            buttonScript.hasDetails = true;
                        }
                        //else add to that button list of details
                        else if(previousFate.fateName == currentFate.fateName)
                        {
                            buttonScript.AddNewDetail(currentFate);
                        }
                        again = true;
                    }
                    else
                    {
                        buttonScript.UpdateFateReason(currentFate);
                        again = false;
                    }
                    i++;
                    previousFate = currentFate;
                } while (again);
                
                

                

                //if (autoSize == true)
                //{
                //    buttonObj.SetActive(true);
                //}
                //else
                //{
                //    buttonScript.enabled = true;
                //    buttonScript.UpdateFateReason(sourceList[i + (currentPage - 1) * btnObjList.Count]);
                //}
                

            }
            
            
        }
        if(autoSize == true)
        {
            UpdateMenuHeight(numActive);
        }
        else
        {
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, panelDefaultHeight);

            //panel_FateReasonMenu.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, panelDefaultHeight);
        }
    }

    private void PopulateDetailButtons(List<GameObject> btnObjList, List<FateReason> sourceList)
    {
        //pageCount = (int)Mathf.Ceil(sourceList.Count / btnObjList.Count);
        //pageCount = (int)Mathf.Ceil(fateReasons.Length / buttons.Count);
        //FateReason previousFate = new FateReason(),
        //    currentFate = new FateReason();

        FateReason currentFate = ScriptableObject.CreateInstance<FateReason>();


        int i = 0, numActive = 0;
        foreach (GameObject buttonObj in btnObjList)
        {
            //TMP_Text btnTMPobj = buttonObj.GetComponentInChildren<TMP_Text>();
            //btnTMPobj.text = fateReasons[i + (currentPage-1)*buttons.Count].name;

            FateReasonButton buttonScript = buttonObj.GetComponent<FateReasonButton>();

            // If there is no further information to populate list with, turn off button
            if ((i + (currentPage - 1) * btnObjList.Count) >= sourceList.Count)
            {
                buttonObj.SetActive(false);
            }
            // Else, populate button with correct information
            else
            {
                buttonObj.SetActive(true);
                buttonScript.enabled = true;
                buttonScript.showDetails = true;
                numActive++;

                currentFate = sourceList[i + (currentPage - 1) * btnObjList.Count];
                buttonScript.UpdateFateReason(currentFate);
            }
            i++;
        }
        UpdateMenuHeight(numActive);
    }

    private void UpdateMenuHeight(int numberBtnsActive)
    {
        //panel_Outermost
        int newHeight;

        newHeight = panelMinHeight + (numberBtnsActive - 1)*btnIncHeight;
        newHeight = Mathf.Clamp(newHeight, panelMinHeight, panelMaxHeight);

        //panel_FateReasonMenu.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, panelDefaultHeight);

    }

    public void SwitchFatePopup()
    {
        if(isDetailsOpen == true)
        {
            SwitchFatePopup(false);

        }
        else
        {
            SwitchFatePopup(true);
        }
    }

    public void LoadFateDetailPopup (FateReasonButton buttonInfo)
    {
        fateDetailsList.Clear();
        fateDetailsList = buttonInfo.detailsList;

        block_FateReaons.SetActive(false);
        block_FateDetails.SetActive(true);
        isDetailsOpen = true;
        tmpText_FateName.text = buttonInfo.fateReason.fateName;

        // Populate the buttons with info
        PopulateDetailButtons(btnObjList_Details, fateDetailsList);
    }

    public void SwitchFatePopup(bool changeToDetails)
    {
        // This is the Fate Reasons page, which should be the default
        if (changeToDetails == false)
        {
            block_FateReaons.SetActive(true);
            block_FateDetails.SetActive(false);
            isDetailsOpen = false;
            // Populate the buttons with info
            PopulateButtons(btnObjList_Reasons, fateReasonsList, false);
        }
        // This is the Fate Details page
        else
        {
            block_FateReaons.SetActive(false);
            block_FateDetails.SetActive(true);
            isDetailsOpen = true;
            // Populate the buttons with info
            PopulateButtons(btnObjList_Details, fateDetailsList, true);
        }
    }

    private void LoadCreatedFateReasons()
    {
        fateReasonsList.Clear();
        fateReasonsList = Resources.LoadAll("", typeof(FateReason)).Cast< FateReason>().ToList<FateReason>();
        //Debug.Log("Loaded " + fateReasons.Count + " fateReason scriptable objects from the folder: " + fatesDestinationFolder);
    }

    public void ReadFateReasonsFile()
    {
        fateReasonsList.Clear();
        //if (Directory.Exists(fatesDestinationFolder) == true)
        //{
        //    Directory.Delete(fatesDestinationFolder, true);
        //}
        //Directory.CreateDirectory(fatesDestinationFolder);

        List<FateReasonJson> fateReasonsJson = JsonConvert.DeserializeObject<List<FateReasonJson>>(File.ReadAllText(fateReasonsJsonFileName));

        foreach (FateReasonJson reasonJson in fateReasonsJson)
        {
            //Debug.Log(reasonJson.name + " has details?: " + reasonJson.hasDetails);
            if (reasonJson.hasDetails == true)
            {

                foreach (string detail in reasonJson.details)
                {
                    //Debug.Log(" option: " + detail);
                    FateReason newReason = ScriptableObject.CreateInstance<FateReason>();
                    newReason.fateName = reasonJson.name;
                    newReason.rawSentence = reasonJson.sentence;
                    newReason.hasDetails = reasonJson.hasDetails;
                    newReason.requiresAttacker = reasonJson.requiresAttacker;
                    newReason.detail = detail;
                    fateReasonsList.Add(newReason);
                }
            }
            else
            {
                FateReason newReason = ScriptableObject.CreateInstance<FateReason>();
                newReason.fateName = reasonJson.name;
                newReason.rawSentence = reasonJson.sentence;
                newReason.hasDetails = reasonJson.hasDetails;
                newReason.requiresAttacker = reasonJson.requiresAttacker;
                fateReasonsList.Add(newReason);
            }
        }

        Debug.Log("How many elements are in fateReasons? " + fateReasonsList.Count);

        foreach (var reason in fateReasonsList)
        {
            string assetName = "";
            if (reason.hasDetails)
            {
                assetName = reason.fateName + " (" + reason.detail + ").asset";
            }
            else
            {
                assetName = reason.fateName + ".asset";
            }
            Debug.Log("Creating FateReason asset '" + assetName + "' in " + fatesDestinationFolder);
            if(File.Exists(fatesDestinationFolder + "/" + assetName) == true)
            {
                //File.Delete(fatesDestinationFolder + "/" + assetName);
                Debug.Log("Skipping already existing FateReason '" + assetName + "'");
            }
            else
            {
                AssetDatabase.CreateAsset(reason, fatesDestinationFolder + "/" + assetName);
            }
            
        }
        LoadCreatedFateReasons();
    }
}

[System.Serializable]
public class FateReasonJson
{
    public string name,
        sentence;
    public bool hasDetails = false,
        requiresAttacker = false;
    public string[] details;
}