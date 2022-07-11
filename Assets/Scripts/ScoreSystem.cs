using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreSystem
{
   static int _score;
    //static will allow other objects to use this method 
    public static void Add(int points)
    {
        _score += points; 
        Debug.Log($"Score = {_score}");
    }
}
