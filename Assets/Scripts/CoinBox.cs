using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    [SerializeField] Sprite _usedSprite; 
    [SerializeField] int _totalCoins = 3;
    int _remainingCoins;

    void Start()
    {
        _remainingCoins = _totalCoins;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>(); 
        if(player == null)
            return;
        
        //if the collision is from the bottom going in the up direction and remaining coins is greater than 0
        //remaining coins will be decremented
        //, CoinsCollected will be incremented
        if (collision.contacts[0].normal.y > 0 && _remainingCoins > 0)
        {
            _remainingCoins--;
            Coin.CoinsCollected++;
            if (_remainingCoins <= 0)
            {
                GetComponent<SpriteRenderer>().sprite = _usedSprite; 
            }
        }
            

        
    }
}
