using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float _launchForce = 5f;
    [SerializeField] float _bounceForce = 5f;
    [SerializeField] int _bouncesRemaining = 3;
    public float Direction { get; set; }
    Rigidbody2D _rigidbody;
   
    void Start()
    {
        //rigidbody reference so we can send the fireballs in the direction dictated by the player
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(_launchForce * Direction, _bounceForce); 
    } 

    void OnCollisionEnter2D(Collision2D collision)
    { 
        //getting a reference of the object that uses the ITakeDamage interface 
        ITakeDamage damageable = collision.collider.GetComponent<ITakeDamage>();
        if (damageable != null)
        {
            //the object will take damage in their way
            damageable.TakeDamage(); 
            
            //the object will be destroyed
            Destroy(gameObject);
            
            return;
        }
        
        //decrement bounces remaining when the fireball collides with anything
        _bouncesRemaining--;
        
        //if bounces remaining goes below zero, destroy the fireball
        //we want 3 bounces then die on the 4th bounce
        if(_bouncesRemaining < 0)
            Destroy(gameObject);
        else
        {
            //this maintains the velocity after each bounce
            _rigidbody.velocity = new Vector2(_launchForce * Direction, _bounceForce); 
        }
    }
}
