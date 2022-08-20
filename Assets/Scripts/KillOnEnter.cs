using System;
using UnityEngine;

public class KillOnEnter : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        //the component being assigned to var player is the incoming script from the player that collided with the object this script belongs to
        var player = collision.collider.GetComponent<Player>();
        
        //if the player is found, reset the position of the player
        if (player != null)
        {
            //player.ResetToStart();
            var sendPlayerToCheckPoint = FindObjectOfType<SendPlayerToCheckpoint>();
            sendPlayerToCheckPoint.SendPlayerToLastCheckPoint(); 
        }
    }

    //resets the position of the player when the player collider enters the collider of the object this script belongs to
    //the player collider is being passed into this method
    void OnTriggerEnter2D(Collider2D collision)
    {
        //the component being assigned to var player is the incoming script from the player that collided with the object this script belongs to
        var player = collision.GetComponent<Player>();
        
        //if the player is found, reset the position of the player
        if (player != null)
        {
            //player.ResetToStart();
            var sendPlayerToCheckPoint = FindObjectOfType<SendPlayerToCheckpoint>();
            sendPlayerToCheckPoint.SendPlayerToLastCheckPoint();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        //the component being assigned to var player is the incoming script from the player that collided with the object this script belongs to
        var player = other.GetComponent<Player>();
        
        //if the player is found, reset the position of the player
        if (player != null)
        {
            //player.ResetToStart();
            var sendPlayerToCheckPoint = FindObjectOfType<SendPlayerToCheckpoint>();
            sendPlayerToCheckPoint.SendPlayerToLastCheckPoint();
        }
    }
}