using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//set and keep track of a number of 'bullets' for our game
//trigger ending state when the bullets have run out
public class BulletCounter : MonoBehaviour {

    public uint startingBulletCount = 17; //baseline with out an extension
    uint currentBulletCount = 0;
    public Transform ammoCounter;
    BulletCountUI uiDisplay;

    public Transform gameStateManager;
    public Transform scoreManager;
    ScoreManager scoreManagerScript;
	// Use this for initialization
	void Start () {
        currentBulletCount = startingBulletCount;

        if(gameStateManager == null)
        {
            Debug.Log("The game state manager is null. The game state manager needs to be assigned an object with the GameStateManager script\nThis object must be instantiated in scene\n" + this.transform.name);
        }
        if (ammoCounter == null)
        {
            Debug.LogError("The bulletcounter behaviour needs to know about a GUI text to be able to update it");
        }
        else
        {
            uiDisplay = ammoCounter.GetComponent<BulletCountUI>();
            if (uiDisplay == null)
            {
                Debug.LogError("The gui texture of the bulletcounter behavuor must have the BullletCountUI script attached");
            }
            uiDisplay.UpdateCountDisplay(currentBulletCount);
        }
        if(scoreManager == null)
        {
            Debug.LogError("The ammo counter object does not have areference to the score manager. This is required to allow saving of highscore");
        }
        else
        {
            scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
            if(scoreManagerScript == null)
            {
                Debug.LogError("The score manager script was not attached to the score manager object assigned to bullet  counter object");
            }
        }
    }
	
	public void SubtractBullet()
    {
        if (currentBulletCount > 0)
        {
            --currentBulletCount;
            uiDisplay.UpdateCountDisplay(currentBulletCount);
            if (currentBulletCount <= 0)
            {
                scoreManagerScript.SaveHighScore();
                gameStateManager.GetComponent<GameStateManager>().LoadEndGameScene();
            }
        }
    }
}
