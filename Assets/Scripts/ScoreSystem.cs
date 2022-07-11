using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreSystem
{
   static int _score;

   public static event Action<int> OnScoreChanged;

   //static will allow other objects to use this method 
    public static void Add(int points)
    {
        _score += points; 
        //this invokes the event taking the score as input
        OnScoreChanged?.Invoke(_score);
        Debug.Log($"Score = {_score}");
    }
}
