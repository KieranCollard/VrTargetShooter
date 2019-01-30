using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayUI : MonoBehaviour {

    public string precedingText = "Score: ";
    Text text;
    // Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();
        if (text == null)
        {
            Debug.LogError("The prefab " + this.transform + "had the ScoreDisplayUI script attached but no GUIText component.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScoreDisplay(uint currentScore)
    {
        text.text = precedingText + System.Convert.ToString(currentScore);
    }
}
