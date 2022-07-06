using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform _leftSensor;
    [SerializeField] Transform _rightSensor;
    [SerializeField] Sprite _deadSprite;
    Rigidbody2D _rigidbody2D;
    float _direction = -1;
    

    // Start is called before the first frame update
    void Start()
    {
        //caching the rigidbody2D component
        _rigidbody2D = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //the velocity is still -1 except now the y velocity is not overwritten as 0 every frame 
        _rigidbody2D.velocity = new Vector2(_direction, _rigidbody2D.velocity.y); 
        
        //if the direction is less than zero, pass the left sensor into the Scan sensor method
        if(_direction < 0)
            ScanSensor(_leftSensor);
        else
            ScanSensor(_rightSensor);
    }

    void ScanSensor(Transform sensor)
    {
        //a line to see the raycast in the down direction
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);
        
        //the raycast itself with a hit being returned and assigned to the variable result
        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);

        //if the hit does not exist, turn around
        if (result.collider == null)
            TurnAround();
        
        //a line to see the raycast in the direction the object is moving
        Debug.DrawRay(sensor.position, new Vector2(_direction, 0) * 0.1f, Color.red);
        
        //the raycast in the direction the object is moving
        var sideresult = Physics2D.Raycast(sensor.position, new Vector2(_direction, 0),0.1f);
        
        //if the hit exists, turn around
        if (sideresult.collider !=  null)
            TurnAround();

    }

    //change the direction of the object
    void TurnAround()
    {
        _direction *= -1;
        
        //getting a reference to the sprite renderer component and assigning it to the variable spriteRenderer
        var spriteRenderer = GetComponent<SpriteRenderer>();
        
        //the sprite Renderer will flip the sprite on the x axis when the direction is greater than 0
        spriteRenderer.flipX = _direction > 0; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //get the player component from the object that collided with this collider and assign the object that contains a player component to the player variable
        var player = collision.collider.GetComponent<Player>(); 
        
        //if player is null, just return
        if (player == null)
            return;
        
        //we will use the normal vector to determine the angle from where an incoming collider is coming from
        //if the incoming collider is coming from above or the right, the value is -1 for the y and x axes respectively 
        //vice a versa for positive 1
        
        //the contact point is contained as an index of an array. 
        var contact = collision.contacts[0]; 
        
        //assign the normal vector of the contact point to the variable normal
        Vector2 normal = contact.normal; 

        //if the y value of the normal vector is less than -0.5, then this object dies
        //if not, the player resets
        if (normal.y <= -0.5)
            //this line is necessary for coroutines to work
            StartCoroutine(Die()); 
        else
        {
            //if player is not null, reset the player to the starting position
            player.ResetToStart();
        }
    }
    //the return type for a method with a co routine is the IEnumerator
    IEnumerator Die()
    {
        //cache the spriteRenderer because the sprite color will be changed every frame  
        var spriteRenderer = GetComponent<SpriteRenderer>();
            
        //the dead Sprite is assigned to the sprite field of the sprite renderer component
            spriteRenderer.sprite = _deadSprite; 
        
        //turn the animator off so that the dead Sprite will appear rather than the animated walk sequence
        GetComponent<Animator>().enabled = false; 
        
        //turn off the parent collider, to account for any collider on this object, to stop this object from interacting with any colliders
        GetComponent<Collider2D>().enabled = false; 
        
        //turn this script off to stop the object from moving
        this.enabled = false; 
        
        //turn the rigidbody2D component off by deactivating the physics simulated on this object
        GetComponent<Rigidbody2D>().simulated = false;
        
        //set the alpha to 1
        float alpha = 1;
        
        //while the alpha is greater than 0, decrement it and change the color
        while (alpha > 0)
        {
            //to have this while loop decrement after each frame, you need a co routine
            yield return null;
            
            //decrement the alpha to 0 by time.deltaTime. this line is what is used to change the speed of fading by multiply or dividing those chunks of time
            alpha -= Time.deltaTime; 
        
            //set the color of the sprite 
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
        
        
    }
}
