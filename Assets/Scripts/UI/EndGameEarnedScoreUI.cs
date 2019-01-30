using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameEarnedScoreUI : MonoBehaviour {

    public string prefixText = "The score you earned: ";
    public string earnedScoreKey = "EarnedScore";
	void Start () {
        Debug.Log(PlayerPrefs.GetInt(earnedScoreKey));
        this.GetComponent<Text>().text = prefixText + System.Convert.ToString(PlayerPrefs.GetInt(earnedScoreKey));
    }
	
}
