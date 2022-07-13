using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] Fireball _fireballPrefab;
    [SerializeField] float _fireRate = 0.25f;
    
    string _fireButton;
    int _playerNumber;
    Player _player;
    float _nextFireTime;
    string _horizontalAxis;


    // Start is called before the first frame update
    void Awake()
    {
        //get the reference to the player
        _player = GetComponent<Player>();
        
        //cache the value for the player number
        _playerNumber = _player.PlayerNumber;
        
        //cache the string for fireButton
        _fireButton = $"P{_playerNumber}Fire"; 
        
        //cached value for horizontal input
        _horizontalAxis = $"P{_playerNumber}Horizontal"; 
    }

    // Update is called once per frame
    void Update()
    {
        //if the fireButton input was held during this frame and Time.time is greater than the delay we give between each shot
        if (Input.GetButtonDown(_fireButton) && Time.time >= _nextFireTime)
        {
            //reference to the horizontal input
            var horizontal = Input.GetAxis(_horizontalAxis);
            
            //instantiate the fireball prefab and get a reference to the prefab
            Fireball fireball = Instantiate(_fireballPrefab, transform.position, Quaternion.identity);
            
            //set the direction of the fireball object based on the input
            fireball.Direction = horizontal >= 0 ? 1f : -1f; 
            
            //this will allow for some time in between shots
            _nextFireTime += Time.time + _fireRate; 

        }
        
    }
}
