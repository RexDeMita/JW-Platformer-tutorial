using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] string _sceneName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //we are checking to see if the incoming collider has a player component
        var player = collision.GetComponent<Player>();
        
        //if the player variable above is null, exit the method. 
        if (player == null)
            return;
        
        
        //this line gets the animator into a variable
        var animator = GetComponent<Animator>();
        
        //a trigger is used instead of a bool because a trigger is one way animation
        animator.SetTrigger("Raise");
        
        
        //the coroutine calls a method that will create a delay
        StartCoroutine(LoadAfterDelay());
    }
    //IEnumerator is necessary
    IEnumerator LoadAfterDelay()
    {
        //this line returns a sec of delay before the scene is loaded
        yield return new WaitForSeconds(1f); 
        
        //the scene manager loads the scene specified in the inspector
        SceneManager.LoadScene(_sceneName);
    }
    //test
}
