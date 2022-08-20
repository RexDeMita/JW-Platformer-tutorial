using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    //an array of checkpoints
   [SerializeField] Checkpoint[] _checkpoints;
    void Start()
    {
        //reference to an array of checkpoints 
        _checkpoints = GetComponentsInChildren<Checkpoint>(); 
        Debug.Log("in checkpoints manager, just created checkpoints " + _checkpoints);
    }

    public Checkpoint GetLastCheckpointThatWasPassed()
    {
        //linq statement 
        return _checkpoints.Last(t => t.Passed); 
    }
}
