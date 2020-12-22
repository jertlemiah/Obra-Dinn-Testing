﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEditor;
using System.Linq;
using UnityEngine.UI;

public class MenuFateReason : MonoBehaviour
{
    public string fateReasonsJsonFileName = "Assets/Scripts/FateReasons.txt";
    public List<FateReason> fateReasonsList = new List<FateReason>(),
        fateDetailsList = new List<FateReason>();
    //public FateReason[] fateReasons;
    public GameObject block_FateReaons, block_FateDetails,
        panel_FateReasonMenu,
        btnGrp_Reasons,
        btn_pageRight, btn_pageLeft,
        lbl_PageNumber,
        btnGrp_Details, lbl_DetailsName;

    [SerializeField]
    public int panelDefaultHeight = 604,
        panelMinHeight = 264, 
        panelMaxHeight = 800, 
        btnIncHeight = 52; 

    public List<GameObject> btnObjList_Reasons = new List<GameObject>(),
        btnObjList_Details = new List<GameObject>();

    public int currentPage = 1,
        pageCount = 1;
    public string fatesDestinationFolder = "Assets/Game Objects/Scriptable Objects/Fate Reasons/Resources";
    
    // Start is called before the first frame update
    void Start()
    {
        GetButtonObjects(btnGrp_Reasons, btnObjList_Reasons);
        Debug.Log("Count in buttons List " + btnObjList_Reasons.Count);
        

        GetButtonObjects(btnGrp_Details, btnObjList_Details);

        //ReadFateReasonsFile();
        LoadCreatedFateReasons();
        pageCount = (int)Mathf.Ceil(fateReasonsList.Count / btnObjList_Reasons.Count);
        SwitchFatePopup(false);
        ChangePage(0);
    }

    public void ChangePage(int pagesTurned)
    {
        // Get the new page number, clamped from 1 to max page count
        currentPage = Mathf.Clamp(currentPage + pagesTurned, 1, pageCount);

        // Populate the buttons with info
        PopulateButtons(btnObjList_Reasons, fateReasonsList, false);

        // If first page, turn off left turn page button
        if(currentPage == 1) {
            btn_pageLeft.SetActive(false);
        }
        else {
            btn_pageLeft.SetActive(true);
        }      
        // If last page, turn off right turn off button
        if (currentPage == pageCount) {
            btn_pageRight.SetActive(false);
        }
        else {
            btn_pageRight.SetActive(true);
        }
        // Update the page number display
        lbl_PageNumber.GetComponent<TMP_Text>().text = currentPage + " / " + pageCount;
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
                    buttonScript.setButtonInactive();
                    buttonScript.enabled = false;
                }
            }
            // Else, populate button with correct information
            else
            {
                numActive++;
                if (autoSize == true)
                {
                    buttonObj.SetActive(true);
                }
                else
                {
                    buttonScript.enabled = true;
                    buttonScript.updateFateReason(sourceList[i + (currentPage - 1) * btnObjList.Count]);
                } 
            }
            i++;
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
        if(block_FateReaons.activeInHierarchy == true)
        {
            SwitchFatePopup(false);
        }
        else
        {
            SwitchFatePopup(true);
        }
    }

    public void SwitchFatePopup(bool changeToDetails)
    {
        // This is the Fate Reasons page, which should be the default
        if (changeToDetails == false)
        {
            block_FateReaons.SetActive(true);
            block_FateDetails.SetActive(false);
            // Populate the buttons with info
            PopulateButtons(btnObjList_Reasons, fateReasonsList, false);
        }
        // This is the Fate Details page
        else
        {
            block_FateReaons.SetActive(false);
            block_FateDetails.SetActive(true);
            // Populate the buttons with info
            PopulateButtons(btnObjList_Details, fateReasonsList, false);
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
                    newReason.name = reasonJson.name;
                    newReason.sentence = reasonJson.sentence;
                    newReason.hasDetails = reasonJson.hasDetails;
                    newReason.requiresAttacker = reasonJson.requiresAttacker;
                    newReason.detail = detail;
                    fateReasonsList.Add(newReason);
                }
            }
            else
            {
                FateReason newReason = ScriptableObject.CreateInstance<FateReason>();
                newReason.name = reasonJson.name;
                newReason.sentence = reasonJson.sentence;
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
                assetName = reason.name + " (" + reason.detail + ").asset";
            }
            else
            {
                assetName = reason.name + ".asset";
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