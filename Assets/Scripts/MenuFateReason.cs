using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using TMPro;

public class MenuFateReason : MonoBehaviour
{
    public string fileName;
    public List<FateReason> fateReasons = new List<FateReason>();
    public GameObject buttonGroup,
        btn_pageRight, btn_pageLeft;
    public List<GameObject> buttons = new List<GameObject>();
    public int currentPage = 1,
        pageCount = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        GetButtonObjects();
        Debug.Log("Count in buttons List " + buttons.Count);
        ReadFateReasonsFile();
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
        int i = 0;
        foreach (GameObject button in buttons)
        {
            TMP_Text btnTMPobj = button.GetComponentInChildren<TMP_Text>();
            btnTMPobj.text = fateReasons[i + (currentPage-1)*buttons.Count].name;
            i++;
        }
    }

    private void ReadFateReasonsFile()
    {
        //JObject obj = JObject.Parse(File.ReadAllText(fileName));
        fateReasons = JsonConvert.DeserializeObject<List<FateReason>>(File.ReadAllText(fileName));
        Debug.Log(fateReasons.Count + " fateReasons found in json file");
        foreach (FateReason reason in fateReasons)
        {

            Debug.Log(reason.name + " has details?: " + reason.hasDetails);
            if (reason.hasDetails == true)
            {
                foreach (string detail in reason.details)
                {
                    Debug.Log(" option: " + detail);
                }
            }
        }
    }
}

[System.Serializable]
public class FateReason
{
    public string name,
        sentence;
    public bool hasDetails = false,
        hasKiller = false;
    public string[] details;
}