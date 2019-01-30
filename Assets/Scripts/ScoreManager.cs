using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    uint score = 0;
    public string scoreKey = "HighScore";
    public string earnedScore = "EarnedScore";
    public Transform scoreUI;
    public uint maximumScore = 100;
    ScoreDisplayUI scoreUIScript;
    private void Start()
    {
        if(scoreUI == null)
        {
            Debug.LogError("The score manager did not have a reference to the UI object");
        }
        scoreUIScript = scoreUI.GetComponent<ScoreDisplayUI>();
        scoreUIScript.UpdateScoreDisplay(score);
    }
    public void AddScore(uint scoreIncrement)
    {
        score += scoreIncrement;
        scoreUIScript.UpdateScoreDisplay(score);
    }

    public void SaveHighScore()
    {
        int scoreAsInt = System.Convert.ToInt32(score);
        int highestScore = PlayerPrefs.GetInt(scoreKey);
        Debug.Log(scoreAsInt);
        Debug.Break();
        if (scoreAsInt > highestScore)
        {
            
            PlayerPrefs.SetInt(scoreKey, scoreAsInt);
        }
        PlayerPrefs.SetInt(earnedScore, scoreAsInt);
    }
}
