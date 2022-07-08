using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : HittableFromBelow
{
    [SerializeField] int _totalCoins = 3;
    int _remainingCoins;

    void Start()
    {
        _remainingCoins = _totalCoins;
    }
    
    //this method will override the property in the base class
    //CanUse will be true if remaining coins is greater than 0
    //CanUse affects the method in the base class. if CanUse is false, the sprite changes to a disabled sprite
    //the value of CanUse is determined by the specific implementation of the child, but the effect of the value could be the same in the base class
    //this expression body format is preferred by some over the brackets for readability 
    protected override bool CanUse => _remainingCoins > 0;

    //this method will override the method in the base class
    protected override void Use()
    {
        _remainingCoins--;
        Coin.CoinsCollected++;
    }
}
