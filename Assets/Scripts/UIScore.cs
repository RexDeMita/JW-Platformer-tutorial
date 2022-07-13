using System;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    TMP_Text _text;
    void Start()
    {
        //reference to the TMP component is cached to be used later
        _text = GetComponent<TMP_Text>();

        //when the score changes, update the score text
        ScoreSystem.OnScoreChanged += UpdateScoreText;
        
        //update the score from the score system on start so that the next level has the latest score
        UpdateScoreText(ScoreSystem.Score);
    }

    void OnDestroy()
    {
        //when this object is destroyed, the event will be deregistered 
        //this stops any text objects from past runs from calling any code
        ScoreSystem.OnScoreChanged -= UpdateScoreText;
    }

    void UpdateScoreText(int score)
    {
        //this will set the text of _text to the string of the score
        _text.SetText(score.ToString());
    }
}