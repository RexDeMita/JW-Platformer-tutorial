using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] int _playerNumber = 1;
    [SerializeField] Sprite _pressedSprite;
    [SerializeField] UnityEvent _onPressed;
    [SerializeField] UnityEvent _onReleased;
    Sprite _upSprite;
    SpriteRenderer _spriteRenderer; 
    void Awake()
    {
        //the sprite renderer is cached
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        //the sprite of the cached sprite renderer is what is assigned to _upSprite
        _upSprite = _spriteRenderer.sprite;
        
        BecomeReleased();
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        //we are checking to see if the incoming collider has a player component
        var player = collision.GetComponent<Player>();
        
        //if the player variable above is null or the incorrect player number, exit the method. 
        if (player == null || player.PlayerNumber != _playerNumber)
            return;

        BecomePressed();
    }

    void BecomePressed()
    {
        //set the sprite to the downSprite
        _spriteRenderer.sprite = _pressedSprite;

        //this line triggers anything we've hooked up in the inspector
        _onPressed?.Invoke();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null || player.PlayerNumber != _playerNumber)
            return;
        
        BecomeReleased();
    }

    void BecomeReleased()
    {
        //if the unity event, onReleased has events registered, the code will run        
        if (_onReleased.GetPersistentEventCount() != 0)
        {
             _spriteRenderer.sprite = _upSprite;
             _onReleased.Invoke();
        }
    }
}
//make sure to assign the object or objects that the unity event affects in the inspector
//unity events can affect public methods in any of the components of the object(s) in question
//ctrl shift R to extract methods