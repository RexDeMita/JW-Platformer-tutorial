using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 10;

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
            }
        }
    }
}
