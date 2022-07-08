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
    
    //this property will override CanUse in the base class
    //if the item was used, CanUse will become true
    protected override bool CanUse => _used == false && _item != null;  
    
    //this method will override the Use method in the base class
    protected override void Use()
    {
        //backup check in case the CanUse check fails
        if (_item == null)
            return;
        
        //a boolean to check if the item was used
        _used = true; 
        
        //activate the item
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
