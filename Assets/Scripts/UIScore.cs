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
    }
    void UpdateScoreText(int score)
    {
        //this will set the text of _text to the string of the score
        _text.SetText(score.ToString());
    }
}