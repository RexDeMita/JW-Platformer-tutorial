using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] Fireball _fireballPrefab;
    
    string _fireButton;
    int _playerNumber;
    private Player _player;


    // Start is called before the first frame update
    void Awake()
    {
        //get the reference to the player
        _player = GetComponent<Player>();
        
        //cache the value for the player number
        _playerNumber = _player.PlayerNumber;
        
        //cache the string for fireButton
        _fireButton = $"P{_playerNumber}Fire"; 
    }

    // Update is called once per frame
    void Update()
    {
        //if the fireButton input was held during this frame
        if (Input.GetButtonDown(_fireButton))
        {
            //instantiate the fireball prefab
            Instantiate(_fireballPrefab, transform.position, Quaternion.identity);    
        }
        
    }
}
