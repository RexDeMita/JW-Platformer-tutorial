using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public event Action OnPickedUp; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        //get a reference to the player and check if the player reference exists
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        //deactivate the sprite renderer and collider
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        //this line invokes events
        if (OnPickedUp != null)
        {
            OnPickedUp?.Invoke();
        }

        var audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.Play();

    }

    //the event is invoked each time the trigger on this object is collided with by a player
    //the event will 
}