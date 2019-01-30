using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndGameHighScoreUI : MonoBehaviour {

    public string prefixText = "Current High Score: ";
    public string scoreKey = "HighScore";

    // Use this for initialization
    void Start () {
        Debug.Log(PlayerPrefs.GetInt(scoreKey));
        this.GetComponent<Text>().text = prefixText + System.Convert.ToString(PlayerPrefs.GetInt(scoreKey));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
