using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerPrefsText : MonoBehaviour
{
    [SerializeField] private string _key; 
    void OnEnable()
    {
        //assigning the integer value from a corresponding key to a field
        int value = PlayerPrefs.GetInt(_key); 
        
        //sets the text based on the value field above
        GetComponent<TMP_Text>().SetText(value.ToString());
    }
}
