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

public class MenuFateReason : MonoBehaviour
{
    public string fileName;
    public List<FateReason> fateReasons = new List<FateReason>();
    //public FateReason[] fateReasons;
    public GameObject buttonGroup,
        btn_pageRight, btn_pageLeft;
    public List<GameObject> buttons = new List<GameObject>();
    public int currentPage = 1,
        pageCount = 1;
    public string fatesDestinationFolder = "Assets/Game Objects/Scriptable Objects/Fate Reasons/Resources";
    
    // Start is called before the first frame update
    void Start()
    {
        GetButtonObjects();
        Debug.Log("Count in buttons List " + buttons.Count);
        //ReadFateReasonsFile();
        LoadCreatedFateReasons();
        ChangePage(0);
    }

    public void ChangePage(int pagesTurned)
    {
        currentPage = Mathf.Clamp(currentPage + pagesTurned, 1, pageCount);
        PopulateButtons();
        if(currentPage == 1) {
            btn_pageLeft.SetActive(false);
        }
        else {
            btn_pageLeft.SetActive(true);
        }      

        if (currentPage == pageCount) {
            btn_pageRight.SetActive(false);
        }
        else {
            btn_pageRight.SetActive(true);
        }
    }

    private void GetButtonObjects()
    {
        if(buttonGroup == null)
        {
            Debug.Log("ButtonGroup GameObject is null, cannot get button GameObjects");
            return;
        }

        int childCount = 0;
        childCount = buttonGroup.transform.childCount;
        //Debug.Log("Child count of the ButtonGroup: " + childCount);
        foreach (Transform childTransform in buttonGroup.transform)
        {
            buttons.Add(childTransform.gameObject);
        }
    }

    private void PopulateButtons()
    {
        pageCount = (int)Mathf.Ceil(fateReasons.Count / buttons.Count);
        //pageCount = (int)Mathf.Ceil(fateReasons.Length / buttons.Count);
        int i = 0;
        foreach (GameObject buttonObj in buttons)
        {
            //TMP_Text btnTMPobj = buttonObj.GetComponentInChildren<TMP_Text>();
            //btnTMPobj.text = fateReasons[i + (currentPage-1)*buttons.Count].name;

            FateReasonButton buttonScript = buttonObj.GetComponent<FateReasonButton>();

            if ((i + (currentPage - 1) * buttons.Count) >= fateReasons.Count)
            {
                //buttonObj.SetActive(false);                
                buttonScript.setButtonInactive();
                buttonScript.enabled = false;
               
            }
            else
            {
                buttonScript.enabled = true;
                //buttonObj.SetActive(true);
                //buttonObj.GetComponent<Button>().enabled = true;
                buttonScript.updateFateReason(fateReasons[i + (currentPage - 1) * buttons.Count]);
            }

            i++;
        }
    }

    private void LoadCreatedFateReasons()
    {
        fateReasons.Clear();
        fateReasons = Resources.LoadAll("", typeof(FateReason)).Cast< FateReason>().ToList<FateReason>();
        //Debug.Log("Loaded " + fateReasons.Count + " fateReason scriptable objects from the folder: " + fatesDestinationFolder);
    }

    public void ReadFateReasonsFile()
    {
        fateReasons.Clear();
        //if (Directory.Exists(fatesDestinationFolder) == true)
        //{
        //    Directory.Delete(fatesDestinationFolder, true);
        //}
        //Directory.CreateDirectory(fatesDestinationFolder);

        List<FateReasonJson> fateReasonsJson = JsonConvert.DeserializeObject<List<FateReasonJson>>(File.ReadAllText(fileName));

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
                    fateReasons.Add(newReason);
                }
            }
            else
            {
                FateReason newReason = ScriptableObject.CreateInstance<FateReason>();
                newReason.name = reasonJson.name;
                newReason.sentence = reasonJson.sentence;
                newReason.hasDetails = reasonJson.hasDetails;
                newReason.requiresAttacker = reasonJson.requiresAttacker;
                fateReasons.Add(newReason);
            }
        }

        Debug.Log("How many elements are in fateReasons? " + fateReasons.Count);

        foreach (var reason in fateReasons)
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