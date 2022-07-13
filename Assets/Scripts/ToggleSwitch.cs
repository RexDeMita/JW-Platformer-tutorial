using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
   [SerializeField] ToggleDirection _startingDirection = ToggleDirection.Center;
   
   [SerializeField] UnityEvent _onLeft; 
   [SerializeField] UnityEvent _onCenter;
   [SerializeField] UnityEvent _onRight;
   [SerializeField] Sprite _toggleSwitchLeft;
   [SerializeField] Sprite _toggleSwitchCenter;
   [SerializeField] Sprite _toggleSwitchRight;

   [SerializeField] AudioClip _leftSound;
   [SerializeField] AudioClip _rightSound;
   
   SpriteRenderer _spriteRenderer;
   ToggleDirection _currentDirection;
   AudioSource _audioSource;

   //enum
   enum ToggleDirection
   {
      Left, 
      Center, 
      Right
   }
   
   void Start()
   {
      _audioSource = GetComponent<AudioSource>();
      _spriteRenderer = GetComponent<SpriteRenderer>();
      //sets the toggle switch to a default sprite
      //must be done after the sprite renderer
      SetToggleDirection(_startingDirection, true);
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
   
   //sets the sprite and invokes the event
   //based on direction and a boolean that tells this method whether or not to return
   //the boolean is an optional parameter: a default value that is given as input to any method that doesnt state otherwise
   void SetToggleDirection(ToggleDirection direction, bool force = false)
   {
      if(force == false && _currentDirection == direction)
         return;
      
      _currentDirection = direction; 
      switch (direction)
      {
         case ToggleDirection.Left:
            _spriteRenderer.sprite = _toggleSwitchLeft;
            _onLeft.Invoke();
            if(_audioSource != null)
               _audioSource.PlayOneShot(_leftSound);
            break;
         case ToggleDirection.Center:
            _spriteRenderer.sprite = _toggleSwitchCenter;
            _onCenter.Invoke();
            break;
         case ToggleDirection.Right:
            _spriteRenderer.sprite = _toggleSwitchRight;
            _onRight.Invoke();
            if(_audioSource != null)
               _audioSource.PlayOneShot(_rightSound);
            break;
         default:
            break;
      }
   }
   //this changes the sprite in the scene when i change the starting direction in the inspector
   void OnValidate()
   {
      _spriteRenderer = GetComponent<SpriteRenderer>();
      switch (_startingDirection)
      {
         case ToggleDirection.Left:
            _spriteRenderer.sprite = _toggleSwitchLeft;
            break;
         case ToggleDirection.Center:
            _spriteRenderer.sprite = _toggleSwitchCenter;
            break;
         case ToggleDirection.Right:
            _spriteRenderer.sprite = _toggleSwitchRight;
            break;
         default:
            break;
      }
   }
   
}
