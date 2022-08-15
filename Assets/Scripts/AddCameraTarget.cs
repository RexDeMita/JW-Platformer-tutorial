using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AddCameraTarget : MonoBehaviour
{
    public CinemachineTargetGroup _targetGroup;
    public Flag flag;
    Transform _flagTransform;

    void Awake()
    {
        _flagTransform = flag.transform; 
        
    }

    void Update()
    {
        if(Input.GetButtonDown("camera toggle"))
            _targetGroup.AddMember(_flagTransform, 1, 1); 
        
        if (Input.GetButtonDown("camera toggle off"))
            _targetGroup.RemoveMember(_flagTransform);
    }
}
