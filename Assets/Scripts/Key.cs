using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
   [SerializeField] KeyLock _keylock;
    AudioSource _audioSource;

    void Awake() => _audioSource = GetComponent<AudioSource>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            //this sets the transform of the player as the parent of this key
            transform.SetParent(player.transform);
            
            //the key will be positioned 1m above the local origin of the player 
            //localPosition is a local offset from the parent object
            transform.localPosition = Vector3.up;
            
            //play the sound if it exists
            if(_audioSource != null)
                _audioSource.Play();
        }
        
        
        //get a reference to the keyLock component from the incoming collider
        var keyLock = collision.GetComponent<KeyLock>();
        
        //if there is a keyLock to check AND the keylock collider encountered is the collider from the object in the class field above
        if (keyLock != null && keyLock == _keylock)
        {
            //unlock keylock
            keyLock.Unlock();
            
            //Destroy gameObject
            Destroy(gameObject);
        }
    }
}
