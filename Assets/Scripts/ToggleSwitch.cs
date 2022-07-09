using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
   [SerializeField] UnityEvent _onLeft; 
   [SerializeField] UnityEvent _onCenter;
   [SerializeField] UnityEvent _onRight;
   [SerializeField] Sprite _toggleSwitchLeft;
   [SerializeField] Sprite _toggleSwitchCenter;
   [SerializeField] Sprite _toggleSwitchRight;
   
   SpriteRenderer _spriteRenderer;
   ToggleDirection _currentDirection;
   
   //enum
   enum ToggleDirection
   {
      Left, 
      Center, 
      Right
   }
   
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
      {
         SetToggleDirection(ToggleDirection.Right);
      }
      else if (wasOnRight == false && playerWalkingLeft)
      {
         SetToggleDirection(ToggleDirection.Left);
      }
          
   }
   //sets the sprite and activates the object based on the toggle direction
   void SetToggleDirection(ToggleDirection direction)
   {
      if(_currentDirection == direction)
         return;
      
      _currentDirection = direction; 
      switch (direction)
      {
         case ToggleDirection.Left:
            _spriteRenderer.sprite = _toggleSwitchLeft;
            _onLeft.Invoke();
            break;
         case ToggleDirection.Center:
            _spriteRenderer.sprite = _toggleSwitchCenter;
            _onCenter.Invoke();
            break;
         case ToggleDirection.Right:
            _spriteRenderer.sprite = _toggleSwitchRight;
            _onRight.Invoke();
            break;
         default:
            break;
      }
    }
}
