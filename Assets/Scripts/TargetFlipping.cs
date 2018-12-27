using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cotnains our helper functions for making target stand up and llay  down
/// Contains the timer mechanism used to lie down again if not shot in time
/// </summary>
public class TargetFlipping : MonoBehaviour {

    public float minTime = 1;
    public float maxTime = 5;

    public float standTime = 0.5f;

    public bool isStanding = false;
    public bool isLayingDown = true;

    bool isTransitioningToStand = false;
    bool isTransitioningToLay = false;

    float startLerpTime = 0;


    public Vector3 standingRotation;
    public Vector3 lyingDownRotation;

    Quaternion standingQuaternion;
    Quaternion layingQuaternion;


    float timer = 0;
   private void Awake()
    {
        standingQuaternion = Quaternion.Euler(standingRotation);
        layingQuaternion = Quaternion.Euler(lyingDownRotation);
    }

    public void Standup()
    {
        if (isTransitioningToStand == false && isTransitioningToLay == false)
        {
            Debug.Log("standing up");
            isTransitioningToStand = true;
            isTransitioningToLay = false;
            isLayingDown = false;

            startLerpTime = Time.time;

            timer = Random.Range(minTime, maxTime);
        }
    }

    public void LayDown()
    {
        if (isTransitioningToLay == false && isTransitioningToStand == false)
        {
            Debug.Log("Laying down");
            isTransitioningToStand = false;
            isTransitioningToLay = true;
            isStanding = false;

            startLerpTime = Time.time;
            timer = 0;
        }
    }

    public void OnShot()
    {
        LayDown();
        ///TODO score system
    }

    private void Update()
    {
        float percentage = (Time.time - startLerpTime) / standTime;
        if (isTransitioningToStand)
        {           
            this.transform.rotation = Quaternion.Lerp(layingQuaternion, standingQuaternion, percentage);
        }
        else if(isTransitioningToLay)
        {

            ///TODO doesn't really account for if the target is shot during this transition period
            ///probably fine if the stand and lay transition is fast
            this.transform.rotation = Quaternion.Lerp(standingQuaternion, layingQuaternion, percentage);
        }


        if(this.transform.rotation == standingQuaternion)
        {
            isTransitioningToStand = false;
            isStanding = true;
            isLayingDown = false;
        }
        else if (this.transform.rotation == layingQuaternion)
        {
            isTransitioningToLay = false;
            isLayingDown = true;
            isStanding = false;
        }

        if(timer != 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                LayDown();
            }
        }
    }
}
