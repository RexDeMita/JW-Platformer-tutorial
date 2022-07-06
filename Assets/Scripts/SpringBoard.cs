using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{ 
    [SerializeField] float _bounceVelocity = 10;
    [SerializeField] Sprite _downSprite;
    SpriteRenderer _spriteRenderer;

    Sprite _upSprite;
    //by caching the sprite renderer, the downSprite became a sprite automatically?
    

    void Awake()
    {
        //caching the sprite renderer?
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
        
        //caching the upSprite
        _upSprite = _spriteRenderer.sprite; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //assigning the player component of the object with the incoming collider to a variable
        var player = collision.collider.GetComponent<Player>();

        if (player != null)
        {
            //assigning the rigidbody2d component of the player to a variable
            var rigidbody2D = player.GetComponent<Rigidbody2D>(); 
            
            //does the rigidbody2D component exist
            if (rigidbody2D != null)
            {
                //setting the velocity of the player as they jump on the mushroom
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _bounceVelocity);
                
                //setting the reference to the sprite in the sprite renderer component of spring board
                _spriteRenderer.sprite = _downSprite; 
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();

        if (player != null)
        {
            _spriteRenderer.sprite = _upSprite; 
        }
    }
}
