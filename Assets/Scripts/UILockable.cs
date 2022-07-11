using UnityEngine;

public class UILockable : MonoBehaviour
{
    //this method will check to see if the enabled object has already been unlocked
    private void OnEnable()
    {
        var startButton = GetComponent<UIStartLevelButton>(); 
        //key is assigned the level name
        string key = startButton.LevelName + "Unlocked";

        //the string is stored in the player preferences between game sessions
        int unlocked = PlayerPrefs.GetInt(key, 0);
        
        //if the level has been unlocked, the value will not be zero
        //if the level has not been unlocked, unlocked equals 0,the game object is disabled
        if (unlocked == 0) 
            gameObject.SetActive(false);
    }
}