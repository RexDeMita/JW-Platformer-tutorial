using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
   [SerializeField] Sprite _toggleSwitchLeft;
   [SerializeField] Sprite _toggleSwitchMid;
   [SerializeField] Sprite _toggleSwitchRight;

   SpriteRenderer _spriteRenderer;

   void Start()
   {
      _spriteRenderer = GetComponent<SpriteRenderer>();
   }
   
   //runs every frame a collider is inside of this collider
   void OnTriggerStay2D(Collider2D collision)
   {
      var player = collision.GetComponent<Player>(); 
      if (player == null)
         return;

      //check for the existence of a rigidbody 
      var playerRigidbody = player.GetComponent<Rigidbody2D>(); 
      if(playerRigidbody == null)
         return;

      //is the position of the incoming collider further to the right on the x axis than the position the collider?
      bool wasOnRight = collision.transform.position.x > transform.position.x;
      
      //is the player walking to the right? positive x velocity is to the right
      var velocity = playerRigidbody.velocity;
      bool playerWalkingRight = velocity.x > 0;
      
      //is the player walking to the left?
      bool playerWalkingLeft = velocity.x < 0; 
      //set the sprite of the sprite renderer to the left or right switch sprite
      //if the player is on the right and they are walking right, use the right switch
      //if the player is on the left and walking left, use the left switch
      if (wasOnRight && playerWalkingRight)
         _spriteRenderer.sprite = _toggleSwitchRight; 
      else if (wasOnRight == false && playerWalkingLeft)
         _spriteRenderer.sprite = _toggleSwitchLeft; 
   } 
}
