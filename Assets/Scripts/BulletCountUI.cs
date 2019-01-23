using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BulletCountUI : MonoBehaviour {

    public string precedingText = "Ammo: ";
    Text text;
	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
        if(text == null)
        {
            Debug.LogError("The prefab " + this.transform + "had the BulletCountUI script attached but no GUIText component.");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateCountDisplay(uint currentCount)
    {
        text.text = precedingText + System.Convert.ToString(currentCount);
    }
}
