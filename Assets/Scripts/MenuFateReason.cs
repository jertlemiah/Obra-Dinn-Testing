using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

public class MenuFateReason : MonoBehaviour
{
    public string fileName;
    public List<FateReason> fateReasons = new List<FateReason>();
    
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
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