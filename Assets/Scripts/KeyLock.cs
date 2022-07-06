using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    [SerializeField] UnityEvent _onUnlocked;

    public void Unlock()
    {
        //write Unlocked to the log when this method is called
        Debug.Log("Unlocked");

        //invocation of (a) unity event(s): turning off the keylock and the wall behind it to expose a door
        _onUnlocked.Invoke(); 
    }
}
