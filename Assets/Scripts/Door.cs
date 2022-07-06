using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //these are fields for the sprites of the open door
    [SerializeField] Sprite _openMid;
    [SerializeField] Sprite _openTop;
    
    //these are the sprite renderers for each part of the door 
    [SerializeField] SpriteRenderer _rendererMid;
    [SerializeField] SpriteRenderer _rendererTop;
    
    //the required coins to open the door
    [SerializeField] int _requiredCoins = 3;
    
    //this current object which is a Door will be the entrance door and the field below is the exit door
    [SerializeField] Door _exit;

    [SerializeField] Canvas _canvas; 
    
    //boolean to check if the door is open or not
    bool _open; 

    //command in the inspector that will allow you to call a method from a script
    [ContextMenu("Open Door")]
    
    //this method will replace the sprites in each sprite renderer
    //this method is public because we want to call it from outside this object
    public void Open()
    {
        //if there happens to be a canvas object, then disable it.
        //this removes the necessity for a canvas instance to exist in the first place. 
        //having just canvas.enable = false; necessitates the prior existence of a canvas instance
        if (_canvas != null)
            _canvas.enabled = false; 
        
        //open is assigned to true
        _open = true; 
        
        //sprite renderers that render the two parts of the door
        _rendererMid.sprite = _openMid;
        _rendererTop.sprite = _openTop; 
        
    }
    
    
    void Update()
    {
        //checks every frame if open is false and if the coins collected is greater than or less than the required coins to open the door 
        if (_open == false && Coin.CoinsCollected >= _requiredCoins)
            Open();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //a check to see if the door is even open. if the door is not open, exit this method
        if (_open == false)
            return;
        
        var player = collision.GetComponent<Player>();
        if (player != null && _exit != null)
        {
            //the player is teleported to the position of the Door instance in the _exit variable
            player.TeleportTo(_exit.transform.position); 
        }
    }
}
