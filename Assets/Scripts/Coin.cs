using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //this field can be edited by multiple objects using the same script
    //static variables are shared across all instances of the class
    //the public keyword signifies the ability for other classes to access this class variable
    public static int CoinsCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if there is no reference to the player component of the incoming collider exit this method
        var player = collision.GetComponent<Player>();
        //Debug.Log("Player");
        if (player == null)
            return; 
        
        //deactivate the object
        gameObject.SetActive(false);

        //increment the number of coins collected 
        CoinsCollected++;
        
        //print the value of coinscollected to the log
        Debug.Log(CoinsCollected);
    }
}
