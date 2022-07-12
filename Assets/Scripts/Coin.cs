using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //this field can be edited by multiple objects using the same script
    //static variables are shared across all instances of the class
    //the public keyword signifies the ability for other classes to access this class variable
    public static int CoinsCollected;

    [SerializeField] List<AudioClip> _clips; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if there is no reference to the player component of the incoming collider exit this method
        var player = collision.GetComponent<Player>();
        //Debug.Log("Player");
        if (player == null)
            return;

        //increment the number of coins collected 
        CoinsCollected++;
        
        //print the value of coins collected to the log
        Debug.Log(CoinsCollected);
        
        //add 100 to the score
        ScoreSystem.Add(100);
        
        //Turn off the sprite renderer and collider
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false; 
        
        //if there is more than 0 clips, play a random clip
        if (_clips.Count > 0)
        {
            
            //randomize the audio clip from the list above that will be played
            int randomIndex = UnityEngine.Random.Range(0, _clips.Count); 
        
            //assign the clip from the list using the random index
            AudioClip clip = _clips[randomIndex]; 
        
            //getting a reference to the audio source and then using it right away to play the random clip
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        else
        {
            //getting a reference to the audio source and then playing the clip assigned in the component
            GetComponent<AudioSource>().Play();
        }
    }
}
