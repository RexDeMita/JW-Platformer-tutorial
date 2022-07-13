using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
   //this can be read from outside, but only set from inside
   public static int Score { get; private set; }
   static int _highScore;
 

   void Start()
   {
       //getting and assigning the high score from player prefs that was saved from the last run
       _highScore = PlayerPrefs.GetInt("HighScore");
   }

   public static event Action<int> OnScoreChanged;

   //static will allow other objects to use this method 
    public static void Add(int points)
    {
        Score += points; 
        //this invokes the event taking the score as input
        OnScoreChanged?.Invoke(Score);
        Debug.Log($"Score = {Score}");
        
        //if score is greater than high score
        if (Score > _highScore)
        {
            //set high score to the current score
            _highScore = Score;
            Debug.Log($"High Score = {_highScore}");
            
            //set the integer for the preference based on the given key
            PlayerPrefs.SetInt("HighScore", _highScore);
        } 
    }
}
