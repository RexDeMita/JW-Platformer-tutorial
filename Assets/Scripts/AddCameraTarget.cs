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
    
   // Transform _lasCheckpointTransform;
    Transform _flagTransform;
   // Checkpoint[] _checkpoints;
    

    void Awake()
    {
        
    }

    void Start()
    {
       // var checkPointManager = FindObjectOfType<CheckpointManager>();
              //  _checkpoints = GetComponentsInChildren<Checkpoint>();
               // _lastCheckpoint = checkPointManager.GetLastCheckpointThatWasPassed();
                _flagTransform = flag.transform;
              // _lasCheckpointTransform = _lastCheckpoint.transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("camera toggle"))
        {
           // Debug.Log(_lastCheckpoint);
             _targetGroup.AddMember(_flagTransform, 1, 0.1f);
             //_targetGroup.AddMember(_lasCheckpointTransform, 1f, 0.1f); 
             
        }



        if (Input.GetButtonDown("camera toggle off"))
        {
             _targetGroup.RemoveMember(_flagTransform);
             //_targetGroup.RemoveMember(_lasCheckpointTransform);
            
        }
           
    }
}
