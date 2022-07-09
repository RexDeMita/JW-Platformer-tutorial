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

   void OnTriggerEnter2D(Collider2D collision)
   {
      var player = collision.GetComponent<Player>(); 
      if (player == null)
         return;
      
      //is the position of the incoming collider further to the right on the x axis than the position the collider?
      bool wasOnRight = collision.transform.position.x > transform.position.x;
      
      //set the sprite of the sprite renderer to the left or right switch sprite
      //if wasOnRight is true, set the sprite to toggleSwitchRight, otherwise, set it to toggleSwitchLeft
      _spriteRenderer.sprite = wasOnRight ? _toggleSwitchLeft : _toggleSwitchRight; 
   } 
}
