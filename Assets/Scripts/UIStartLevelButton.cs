using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartLevelButton : MonoBehaviour
{
    [SerializeField] string _levelName;
    //property accessible to other scripts but it doesnt allow for reckless editing of a field
    public string LevelName => _levelName;

    public void LoadLevel()
    {
        //Loads the level given in the inspector
        SceneManager.LoadScene(_levelName);
    }
}

/*
 *  void OnValidate()
    {
        //allows us to see our input in the inspector become a change to the text in the scene
        //we now know what level corresponds to the level we are writing the level name inside of
        GetComponentInChildren<TMP_Text>()?.SetText(_levelName);
    }
 */
