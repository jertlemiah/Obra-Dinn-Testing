﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBook : MonoBehaviour
{
    public int currentLeftPage = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void OpenFateMenu()
    //{

    //}

    public void TurnPageForward()
    {
        currentLeftPage++;
    }

    public void TurnPageBackward()
    {
        currentLeftPage--;
    }

    public void GoToPageNumber(int pageNumber)
    {
        currentLeftPage = pageNumber;
    }
}

//[System.Serializable]
//public class FatePage
//{
//    public string currentName, currentReason, currentAttacker;

//    //public string curre
//}