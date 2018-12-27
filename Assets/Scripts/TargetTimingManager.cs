using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//get all targets and randomize which ones will be activated at a time

public class TargetTimingManager : MonoBehaviour {

    List<TargetFlipping> targets;
    public float selectionTime = 2;
    public float minSelectionTime = 0.2f;

    public float speedIncreaseRate = 1.0f;
    public float speedIncreaseAmount = 0.1f;


    float selectionTimer; 
    float speedIncreaseTimer;
	// Use this for initialization
	void Start () {
       targets =  new List<TargetFlipping>(FindObjectsOfType<TargetFlipping>());

        if(targets.Count == 0)
        {
            Debug.Log("There were no target scripts found in the scene, Check that you have some targets");
        }

        selectionTimer = selectionTime;
        speedIncreaseTimer = speedIncreaseRate;

    }
	
	// Update is called once per frame
	void Update () {

        selectionTimer -= Time.deltaTime;
        speedIncreaseTimer -= Time.deltaTime;

        if(selectionTimer <=0 )
        {
            int index = Random.Range(0, targets.Count);
            if(targets[index].isLayingDown)
            {
                Debug.Log("used the random index");
                targets[index].Standup();
            }
            else
            {
                //randomly chose one which was busy
                //search for a not busy one

                for (int i = 0; i < targets.Count; ++i)
                {
                    if (targets[index].isLayingDown)
                    {
                        Debug.Log("searched the list");
                        targets[index].Standup();
                        break;
                    }
                }
            }
            selectionTimer = selectionTime;
        }

        if(selectionTime > minSelectionTime && speedIncreaseTimer <= 0)
        {
            selectionTime -= speedIncreaseAmount;
            speedIncreaseTimer = speedIncreaseRate;
            if(selectionTime < minSelectionTime)
            {
                selectionTime = minSelectionTime;
            }
        }
		
	}
}
