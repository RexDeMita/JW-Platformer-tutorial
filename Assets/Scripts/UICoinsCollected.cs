using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICoinsCollected : MonoBehaviour
{
    
    TMP_Text _text;

    void Start()
    {
        //reference to the TMP component is cached to be used later
        _text = GetComponent<TMP_Text>(); 
    }

    void Update()
    {
        //the value of coins collected is converted to a string and rendered to the game view
        //setText only works on TMP
        _text.SetText(Coin.CoinsCollected.ToString()); 
    }
}