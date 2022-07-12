using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    //read from outside of this class, but only set in this class
    //singleton
    public static Music Instance { get; private set; }
     
    void Awake()
    {
        //if there is no current Instance, set the current Instance to this object, and dont destory it
        if (Instance == null)
        {
            Instance = this; 
            
            //tells unity to stop the destruction of the gameObject this script is a component of
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    
}
