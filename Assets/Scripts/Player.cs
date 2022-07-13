using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{   
    //a serializefield allows the variable to be edited in the inspector

    [SerializeField] int _playerNumber = 1; 
    [Header("Movement")]   
   //a serializefield allows the variable to be edited in the inspector
   //a serializefield allows the variable to be edited in the inspector
    
    [Header("Movement")]   
    [SerializeField] float _speed = 1;
    [SerializeField] float _slipFactor;
    [Header("Jump")]
    [SerializeField] int _jumpVelocity = 10;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;
    [SerializeField] Transform _leftSensor;
    [SerializeField] Transform _rightSensor;
    [SerializeField] float  _downPull = 1;
    [SerializeField] float _maxJumpDuration = 0.1f;
    [SerializeField] float _downForce;
    [SerializeField] float _wallSlideSpeed = 1f;
    
    Vector3 _startPosition;
    int _jumpsRemaining;
    float _fallTimer;
    float _jumpTimer;
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    float _horizontal;
    bool _isGrounded;
    bool _isOnSlipperySurface;
    string _jumpButton;
    string _horizontalAxis;
    int _layerMask;
    AudioSource _audioSource;
    


    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps; 
        
        //the lines below are caching the variables 
        
        //this line assigns a reference of the Rigidbody2D component of this object to rigidbody2D
        //assigning a reference of the component to a variable allows us to modify the component in the future
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        //assigns a reference of the Animator component of the player to var animator
        _animator = GetComponent<Animator>();
        
        //gets a reference of the SpriteRenderer component of this object 
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        //the string interpolation is cached once
        _jumpButton = $"P{_playerNumber}Jump";
        
        //cached value for horizontal input
        _horizontalAxis = $"P{_playerNumber}Horizontal"; 
        
        //cached value for the layermask
        _layerMask = LayerMask.GetMask("Default");
        
        //cached value for the audio source
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        //extracted methods to improve readability
        //rebuild as you go to make sure you don't have errors

        UpdateIsGrounded();

        ReadHorizontalInput();
        
        //if the surface is slippery, call the slipHorizontal method. otherwise, use moveHorizontal
        if (_isOnSlipperySurface)
            SlipHorizontal();
        else
            MoveHorizontal();

        UpdateAnimator();

        UpdateSpriteDirection();
        
        //check to see if player should slide
        if (ShouldSlide())
        {
            //if the input for a jump is pressed, wall jump
            if (ShouldStartJump())
                WallJump();
            //if you are sliding, return
            //this keeps the player at a steady downward slide without being affected by anything else
             Slide();
             return;
        }
        
        //beginning a jump functionality
        //if Fire1 is pressed during this frame and the player has jumps left
        if (ShouldStartJump())
            Jump();
        
        //if fire1 is held down this frame AND there is time left in the jumpTimer
        else if(ShouldContinueJump())
            ContinueJump();
        
        //jump timer is being incremented all the time, not just when the input is held 
        _jumpTimer += Time.deltaTime;
        
        //if isGrounded is true
        if (_isGrounded && _fallTimer > 0)
        { 
            //reset jumps
            _jumpsRemaining = _maxJumps;
            //set fall timer to 0
            _fallTimer = 0;
        }
        else
        {
            //increment falltimer with the change in time between frames
            _fallTimer += Time.deltaTime; 
            
            //variable that holds the value for downpull
            _downForce = _downPull * _fallTimer * _fallTimer; 
            
            //the player is being pulled down by a downpull that increases every frame
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y - _downForce);
        }
    }

    void WallJump()
    {
        //the player is pushed in the opposite direction of the horizontal away from the wall
        //the y velocity is changed by jump velocity and a multiplier so that the player goes up and out
        _rigidbody2D.velocity = new Vector2(-_horizontal * _jumpVelocity, _jumpVelocity * 1.5f);
    }

    void Slide()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -_wallSlideSpeed);

    }

    bool ShouldSlide()
    {
        //if player is on the ground, return false
        if (_isGrounded)
            return false; 
        
        //if the player is going in the upwards direction due to a jump, dont slide, return false
        if (_rigidbody2D.velocity.y > 0)
            return false; 
        
        //if player is going to the left
        if (_horizontal < 0)
        {
            //get the reference to the collision with the left sensor
            var hit = Physics2D.OverlapCircle(_leftSensor.position, 0.1f);

            //if the hit exists and the left sensor hits a wall
            if (hit != null && hit.CompareTag("Wall"))
                return true; 
        }
        
        //if player is going to the right
        if (_horizontal > 0)
        {
            //get the reference to the collision with the right sensor
            var hit = Physics2D.OverlapCircle(_rightSensor.position, 0.1f);

            //if the hit exists and the left sensor hits a wall
            if (hit != null && hit.CompareTag("Wall"))
                return true; 
        }
        
        //if we are not going left, return false. 
        return false;
    }

    //this is a get only property because we dont want any accidental setting of variables
    //the syntax below is a shortened version of the get only property
    public int PlayerNumber => _playerNumber;

    void ContinueJump()
    {
        //the y velocity is changed by jump velocity
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _fallTimer = 0;
    }
    
    bool ShouldContinueJump()
    {
        //the player number is being edited in the inspector
        //that number is interpolated to a string and used as input into the GetButton method of the Input 
        //this allows us to change the number and add players without having to make a new script
        return Input.GetButton(_jumpButton) && _jumpTimer <= _maxJumpDuration;
    }

    void Jump()
    {
        //the y velocity is changed by jump velocity
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);

        //decrement jumps remaining
        _jumpsRemaining--;

        //log entry to see if the jumps decrement
        Debug.Log($"Jumps remaining {_jumpsRemaining}");

        //every time we jump, we reset the time we have been jumping for
        _jumpTimer = 0;

        _fallTimer = 0;
         
        //play this sound if it exists
        if (_audioSource != null)
            _audioSource.Play();
    }

    bool ShouldStartJump()
    {
        //creating a string every time there is an input will create a lot of unnecessary memory allocation
        //cache strings when possible
        return Input.GetButtonDown(_jumpButton) && _jumpsRemaining > 0;
    }

    void MoveHorizontal()
    {
        //this method will return a floating point value that is a certain percentage from one value to another
        //Time.delta time with give us the percentage
        //the range of values is from the current velocity in the x direction to the horizontal value based on input
        float newHorizontal = Mathf.Lerp(_rigidbody2D.velocity.x, _horizontal * _speed, Time.deltaTime); 
        
        //sets the velocity of the player in the x direction only
            _rigidbody2D.velocity = new Vector2(newHorizontal, _rigidbody2D.velocity.y);

            //this line prints a message in the console
            //the dollar sign allows the inclusion of a variable inside the quotes and the variable is put inside the brackets
           // Debug.Log($"Velocity = {_rigidbody2D}");
        
    }

    void SlipHorizontal()
    {
        //setting the desired velocity that is the destination of the linear interpolation function
        var desiredVelocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);

        //this velocity is a velocity that is smoothly updated every frame using linear interpolation
        var smoothedVelocity = Vector2.Lerp(
            _rigidbody2D.velocity, 
            desiredVelocity, 
            Time.deltaTime * _slipFactor); 
        //sets the velocity of the player in the x direction only
        _rigidbody2D.velocity = smoothedVelocity; 

        //this line prints a message in the console
        //the dollar sign allows the inclusion of a variable inside the quotes and the variable is put inside the brackets
        // Debug.Log($"Velocity = {_rigidbody2D}");

    }
    void ReadHorizontalInput()
    {
        //assigns a value of -1 to 1 from a horizontal input to horizontal and the value is multiplied by speed
        _horizontal = Input.GetAxis(_horizontalAxis) * _speed;
    }

    void UpdateSpriteDirection()
    {
        if (_horizontal != 0)
        {
            //this line flips the sprite if horizontal is less than 0
            _spriteRenderer.flipX = _horizontal < 0;
        }
    }

    void UpdateAnimator()
    {
        //walking is true if horizontal is not 0
        bool walking = _horizontal != 0;

        //sets the walk to true or false
        _animator.SetBool("Walk", walking);
        
        //sets the jump to true or false
        _animator.SetBool("Jump", ShouldContinueJump());
        
        //sets the slide to true or false
        _animator.SetBool("Slide", ShouldSlide());

    }

    void UpdateIsGrounded()
    {
        //a circle at a specified size is created at the feet of the player to check for a layer associated with the object associated with the in incoming colliders
         var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, _layerMask);

        //a boolean variable that tells us whether the character is grounded (hit is not equal to null) or not (hit is equal to null)
        _isGrounded = hit != null;
        
        //check to see if hit is not null. if so, check the tag. 
        if (hit != null)
            _isOnSlipperySurface = hit.CompareTag("Slippery");
        else
            _isOnSlipperySurface = false; 
    }

    //when the collider from the ground enters the collider of the player, the remaining player jumps will be reset
    internal void ResetToStart()
    {
        _rigidbody2D.position = _startPosition; 
        
        //send the user to the main menu
        SceneManager.LoadScene("Main Menu"); 
    }

    //teleports the rigidbody to a position
    internal void TeleportTo(Vector3 transformPosition)
    {
        //the rigidbody is assigned a new position
        _rigidbody2D.position = transformPosition;
        
        //the velocity is set to zero for the rigidbody
        _rigidbody2D.velocity = Vector2.zero;
        
    }
    
    
}
//the home, end, and alt keys should be mapped 
//shift + end = selecting the whole line
//figure out block editing in rider