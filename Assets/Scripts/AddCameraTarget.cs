using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AddCameraTarget : MonoBehaviour
{
    public CinemachineTargetGroup _targetGroup;
    public Flag flag;
  //  public Checkpoint _lastCheckpoint;
    
    public Transform _lastCheckpointTransform;
    Transform _flagTransform;
   // Checkpoint[] _checkpoints;

   void Start()
    {
        _flagTransform = flag.transform;
    }

    void GetCheckpointTransform()
    {
        //find and set a local variable with the checkpoint manager
        var checkpointManager = FindObjectOfType<CheckpointManager>();
                
        //have the checkpoint manager get the last checkpoint passed
        var checkpoint = checkpointManager.GetLastCheckpointThatWasPassed();
        
        Debug.Log("we have exited the method above");
        
         _lastCheckpointTransform = checkpoint.transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("camera toggle"))
        {
            // Debug.Log(_lastCheckpoint);
             GetCheckpointTransform();
             _targetGroup.AddMember(_flagTransform, 1, 0.1f);
             _targetGroup.AddMember(_lastCheckpointTransform, 1f, 0.1f); 
             
        }



        if (Input.GetButtonDown("camera toggle off"))
        {
             _targetGroup.RemoveMember(_flagTransform);
             _targetGroup.RemoveMember(_lastCheckpointTransform);
            
        }
           
    }
}
