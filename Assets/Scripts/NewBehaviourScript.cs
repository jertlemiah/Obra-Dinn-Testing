using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    GameObject mybuttonobject;

    // Setup the button to run this function 
    void OnClick()
    {
        mybuttonobject.SetActive(true);
    }
}
