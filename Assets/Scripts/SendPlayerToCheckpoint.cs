using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPlayerToCheckpoint : MonoBehaviour
{
    void SendPlayerToLastCheckPoint()
    {
        //make sure there is a default checkpoint for the level so that the player does not have to collide with a checkpoint for this to work

        //find and set a local variable with the checkpoint manager
        var checkpointManager = FindObjectOfType<CheckpointManager>();
        
        //have the checkpoint manager get the last checkpoint passed
        var checkpoint = checkpointManager.GetLastCheckpointThatWasPassed();
        
        //find and move the player
        var player = FindObjectOfType<Player>();
        player.transform.position = checkpoint.transform.position; 
    }
}
