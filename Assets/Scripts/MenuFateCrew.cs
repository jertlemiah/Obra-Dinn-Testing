using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;

public class MenuFateCrew : MonoBehaviour
{
    public GameObject btn_pageRight, btn_pageLeft,
        btnGrp_Crew;

    public TMP_Text tmpText_PageNum;

    MenuManager menuManager;
    private MenuFate menuFate;

    public List<GameObject> btnObjList_Crew = new List<GameObject>();
    public List<CrewMember> crewMemberList = new List<CrewMember>();
    public List<Quality> qualityList = new List<Quality>();
    public List<FateReason> fateReasonsList = new List<FateReason>();

    [SerializeField]
    private string pathToCrewMembers = "Assets/Game Objects/Scriptable Objects/Crew Members/Resources",
        fateCrewJsonFileName = "Assets/Scripts/CrewJson.txt";

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
        LoadQualityList();


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

            Debug.Log("sourceList.Count: " + sourceList.Count);
            Debug.Log("btnObjList.Count: " + btnObjList.Count);

            // If there is no further information to populate list with, turn off button
            if ((i + (currentPage - 1) * btnObjList.Count) >= sourceList.Count)
            {
                buttonScript.SetButtonInactive();
                //buttonScript.enabled = false;             
            }
            // Else populate button with info
            else
            {
                Debug.Log("Current Page" + currentPage);
                Debug.Log("About to try to access element: " + (i + (currentPage - 1) * btnObjList.Count));
                CrewMember newCrewMember = sourceList[i + (currentPage - 1) * btnObjList.Count];
                buttonScript.ChangeCrew(newCrewMember);
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
        pageCount = (int)Mathf.Clamp(Mathf.Ceil((float)crewMemberList.Count / (float)btnObjList_Crew.Count), 1, 10);
        //Debug.Log("crewMemberList.Count: " + crewMemberList.Count);
        //Debug.Log("btnObjList_Crew.Count: " + btnObjList_Crew.Count);
        // Debug.Log("Div: " + Mathf.Ceil((float)crewMemberList.Count / btnObjList_Crew.Count));

        // Debug.Log("15 / 12: " + Mathf.Ceil(15 / 12));

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

    private void LoadQualityList()
    {
        qualityList.Clear();
        qualityList = Resources.LoadAll("", typeof(Quality)).Cast<Quality>().ToList();
        Debug.Log("Loaded " + qualityList.Count + " qualties");
    }

    private void LoadCrewMemberList()
    {
        crewMemberList.Clear();
        crewMemberList = Resources.LoadAll(pathToCrewMembers, typeof(CrewMember)).Cast<CrewMember>().ToList();
        Debug.Log("Loaded " + crewMemberList.Count + " crewmembers");
    }

    private void LoadCreatedFateReasons()
    {
        fateReasonsList.Clear();
        fateReasonsList = Resources.LoadAll("", typeof(FateReason)).Cast<FateReason>().ToList<FateReason>();
        //Debug.Log("Loaded " + fateReasons.Count + " fateReason scriptable objects from the folder: " + fatesDestinationFolder);
    }

    public void ReadFateCrewFile()
    {
        List<CrewMemberJson> fateCrewJsonList = JsonConvert.DeserializeObject<List<CrewMemberJson>>(File.ReadAllText(fateCrewJsonFileName));
        LoadCreatedFateReasons();
        LoadQualityList();
        //FateReason fateReason;
        string crewFateReason, crewFateDetail, attacker;


        foreach (CrewMemberJson crewJson in fateCrewJsonList)
        {
            CrewMember newCrewMember = ScriptableObject.CreateInstance<CrewMember>();
            newCrewMember.crewName = crewJson.crewName;
            newCrewMember.internalID = crewJson.id;
            newCrewMember.crewOrigin = crewJson.crewOrigin;
            newCrewMember.usesMasculine = true;

            crewFateReason = crewJson.fateReason;
            crewFateDetail = crewJson.fateDetail;
            attacker = crewJson.attacker;

            foreach (FateReason fateReason in fateReasonsList)
            {
                if(fateReason.fateName == crewFateReason)
                {
                    if (fateReason.hasDetails == true)
                    {
                        if (fateReason.detail == crewFateDetail)
                        {
                            AcceptableFate newAcceptableFate = new AcceptableFate(fateReason, attacker);
                            newCrewMember.acceptableFates[0] = newAcceptableFate;
                            break;
                        }
                    }
                    else
                    {
                        AcceptableFate newAcceptableFate = new AcceptableFate(fateReason, attacker);
                        newCrewMember.acceptableFates[0] = newAcceptableFate;
                        break;
                    }

                }
            }

            foreach (Quality newQuality in qualityList)
            {
                if (newQuality.role == crewJson.crewQuality)
                {
                    newCrewMember.quality = newQuality;
                    break;
                }
            }

            crewMemberList.Add(newCrewMember);
        }

        Debug.Log("How many elements are in crewMemberList? " + crewMemberList.Count);

        foreach (var crewMember in crewMemberList)
        {
            //"1.1 Crew #4 Martin Perrott"
            string assetName = crewMember.quality.surroleOrder.ToString() 
                + ".1 Crew #" + crewMember.internalID 
                + " " + crewMember.crewName 
                + ".asset";

           
            if (File.Exists(pathToCrewMembers + "/" + assetName) == true)
            {
                //File.Delete(fatesDestinationFolder + "/" + assetName);
                Debug.Log("Skipping already existing crewMember '" + assetName + "'");
            }
            else
            {
                Debug.Log("Creating crewMember asset '" + assetName + "' in " + pathToCrewMembers);
                AssetDatabase.CreateAsset(crewMember, pathToCrewMembers + "/" + assetName);
            }

        }
    }
}

[System.Serializable]
public class CrewMemberJson
{
    /*{
        "id": 3,
        "crewName": "Edward Nichols",
        "crewQuality": "Second Mate",
        "crewOrigin": "England",
        "fateReason": "Shot",
        "fateDetail": "Gun",
        "attacker": "Chioh Tan",
        "fateRevealed": "Chapter 4, part 6",
        "appearances": 9
    }} */
    public int id,
        appearances;
    public string crewName,
        crewQuality,
        crewOrigin,
        fateReason,
        fateDetail,
        attacker,
        fateRevealed;
    public bool usesMasculine;
}



