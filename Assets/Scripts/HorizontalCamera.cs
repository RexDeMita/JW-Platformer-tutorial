using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCamera : MonoBehaviour
{
    [SerializeField] Transform _target; 

    // Update is called once per frame
    void Update()
    {
        //the camera will follow a target 
        //the camera will keep its y and z values, but its x position will be the same as the target
        transform.position = new Vector3(_target.position.x, transform.position.y, transform.position.z);
    }
}
