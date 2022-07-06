using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    Vector2 _startPosition;
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _maxDistance = 2;
    [SerializeField] float _speed = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        //save the starting position
        _startPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        //move the fly upward per frame 1 m/s
        transform.Translate(_direction.normalized * Time.deltaTime * _speed);
        
        //distance from start position to current position
        var distance = Vector2.Distance(_startPosition, transform.position);
       
        //checks if the object has gone far enough to turn around
        //if the object has gone too far, its position will be the maxdistance
        if (distance >= _maxDistance)
        {
            //a normalized vector will give just a direction whose magnitude is 1, just direction, not movement amount
            transform.position = _startPosition + (_direction.normalized * _maxDistance); 
           
            //flips the direction
            _direction *= -1; 
        }
    }
    //End key goes to the end of the line
    //shift + home selects the entire line if the cursor is at the end of the line
    //shift delete deletes the entire line
}