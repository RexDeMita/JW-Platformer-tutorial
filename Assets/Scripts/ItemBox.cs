using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject _item;
    [SerializeField] Vector2 _itemLaunchVelocity;
    bool _used;

    void Start()
    {
        //the item is inactive at the initialization of this script if the item exists
        if(_item != null)
            _item.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if the item was used already, return
        if (_used)
            return;

        var player = collision.collider.GetComponent<Player>(); 
        if(player == null)
            return;
        
        //if the collision is from the bottom going in the up direction and remaining coins is greater than 0
        //remaining coins will be decremented
        //, CoinsCollected will be incremented
        if (collision.contacts[0].normal.y > 0)
        {
            //this gets a reference to the sprite renderer component and sets it to a sprite of your choosing
            GetComponent<SpriteRenderer>().sprite = _usedSprite;
            
            //this activates the item if it exists
            if (_item != null)
            {
                //a boolean to check if the item was used
                _used = true; 
                _item.SetActive(true);
                //get the reference to the rigidbody
                var itemRigidbody = _item.GetComponent<Rigidbody2D>();
                //if the itemRigidbody exists
                if (itemRigidbody != null)
                {
                    //set the launch velocity
                    itemRigidbody.velocity = _itemLaunchVelocity; 
                }
            }
                
        }
             
            

        
    }
}
