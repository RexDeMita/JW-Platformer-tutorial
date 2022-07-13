using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, ITakeDamage
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if the object that is colliding with this collider does not have a player component, exit this method. 
        if (collision.collider.GetComponent<Player>() == null)
            return;
        //if the y value of the normal vector of the contact point of the collision is positive (the contact is from below and object is traveling up to contact this collider)
        //then TakeHit
        if (collision.contacts[0].normal.y > 0)
            TakeHit(); 
    }
    
    void TakeHit()
    {
        //get a reference to the particle system
        var particleSystem = GetComponent<ParticleSystem>(); 
        
        //have the particle system play 
        particleSystem.Play();
        
        //disable the parent collider and sprite renderer
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TakeDamage()
    {
        TakeHit();
    }
}
