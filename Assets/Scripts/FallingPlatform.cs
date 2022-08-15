using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    //for the most part, public variables are for the developer to edit. private serialized fields creates some settings that a player can edit
    public bool PlayerInside;
    
    //a type of data collection used to distinguish between unique elements
    //the type of hash set is the object in between the less and greater than signs
    HashSet<Player> _playersInTrigger = new HashSet<Player>();
    Coroutine _coroutine;
    Vector3 _initialPosition;
    bool _falling;
    float _wiggleTimer;

    //the tooltip is a description of a configurable setting in the inspector that you can hover over to see
    [Tooltip("Resets the wiggle timer to 0 when no players are on the platform")]
    //this boolean allows us to toggle between two modes: the fall timer resets every time all the players leave the platform vs the fall timer never resets and builds up over time
    [SerializeField] bool _resetOnEmpty;
    [SerializeField] float _fallSpeed = 3;
    //slider from 0.1 to 5
    [Range(0.1f, 5)][SerializeField] float _fallAfterSeconds = 3;
    [Range(0.005f, 0.1f)][SerializeField] float _shakeY = 0.005f;
    [Range(0.005f, 0.1f)][SerializeField] float _shakeX = 0.005f;
    

    void Start()
    {
        _initialPosition = transform.position; 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>(); 
        if (player == null)
            return;
        
        //if the incoming collider is a player, then that player instance is added to the hash set
        _playersInTrigger.Add(player); 
        
        //PlayerInside is assigned true if a player collided with this collider
        PlayerInside = true;
        
        //as soon as 1 player enters the collider, the coroutine will start
        if(_playersInTrigger.Count == 1)
            
            //make a reference to the coroutine so that if you need to stop it, you can just use the reference
            //makes a difference when you have more than one coroutine to manage
            _coroutine = StartCoroutine(WiggleAndFall());
    }

    IEnumerator WiggleAndFall()
    {
        Debug.Log("Waiting to wiggle");
        yield return new WaitForSeconds(0.25f); 
        Debug.Log("Wiggling");
        
        //timer to keep track of how long the platform has been wiggling
        //_wiggleTimer = 0; 
        
        //while wiggleTimer is less than the set amount of time to wait to wiggle, the code in the braces will run
        while (_wiggleTimer < _fallAfterSeconds)
        {
            //random number is generated for the x and y values of the wiggle vector
            float randomX = UnityEngine.Random.Range(-_shakeX, _shakeX);
            float randomY = UnityEngine.Random.Range(-_shakeY, _shakeY);
            
            //the platform is wiggled. the initial position is altered by a vector with random values
            transform.position = _initialPosition + new Vector3(randomX, randomY);
            
            //random number that will be the random delay time in between wiggles
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f); 
            
            //the coroutine waits until the end of the random delay to execute
            yield return new WaitForSeconds(randomDelay); 
            
            //to eventually leave this while loop, the wiggle timer variable needs to be incremented by the randomDelay
            _wiggleTimer += randomDelay; 
        }
        

        Debug.Log("Falling");
        
        //falling flag to signify if the platform is falling or not
        _falling = true; 

        //Collider2D[] colliders = GetComponents<Collider2D>();
        //colliders can be put into a collection and then iterated over
        //
        //foreach loop that will iterate over each element in a collection such as an array or each component in the return of a GetComponents method
        foreach (var collider in GetComponents<Collider2D>())
        {
            //disabling each collider
            collider.enabled = false; 
        }
        
        //timer that will increment up to the amount of time the platform is supposed to fall for
        float fallTimer = 0;
        
        //while fallTimer is less than 3, run the code in the braces
        while (fallTimer < 3f)
        {
            //the platform will move down faster each frame
            transform.position += Vector3.down * (Time.deltaTime * _fallSpeed);
            
            //increment fallTimer by Time.deltaTime
            fallTimer += Time.deltaTime; 
            
            //wait until the next frame to iterate through the loop again
            yield return null;
        }
        
        //destroy the game object after it has fallen
        Destroy(gameObject); 
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //if the platform is falling, exit the method. this allows the coroutine to continue 
        if (_falling)
            return;
        
        var player = collision.GetComponent<Player>(); 
        if (player == null)
            return;
        
        //if there is a collider leaving this collider and that collider is a player, then that player instance is removed from the hash set
        _playersInTrigger.Remove(player);
        
        //if there are no players in the hash set, then make the players inside false
        if (_playersInTrigger.Count == 0)
        {
            PlayerInside = false;
            //stops the coroutine that is referenced
            StopCoroutine(_coroutine);
            
            //if resetOnEmpty is true, set wiggle timer to 0 
            if (_resetOnEmpty)
                _wiggleTimer = 0; 
        }
             
        
        
    }
}
