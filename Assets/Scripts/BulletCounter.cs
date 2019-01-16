using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//set and keep track of a number of 'bullets' for our game
//trigger ending state when the bullets have run out
public class BulletCounter : MonoBehaviour {

    public uint startingBulletCount = 17; //baseline with out an extension
    uint currentBulletCount = 0;

    public Transform gameStateManager;

	// Use this for initialization
	void Start () {
        currentBulletCount = startingBulletCount;

        if(gameStateManager == null)
        {
            Debug.Log("The game state manager is null. The game state manager needs to be assigned an object with the GameStateManager script\nThis object must be instantiated in scene\n" + this.transform.name);
        }
	}
	
	public void SubtractBullet()
    {
        --currentBulletCount;
        if(currentBulletCount<=0)
        {
            gameStateManager.GetComponent<GameStateManager>().Reload();
        }
    }
}
