﻿using System.Collections;
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

    AudioSource audioSource;
    float timer = 0;
   private void Awake()
    {
        standingQuaternion = Quaternion.Euler(standingRotation);
        layingQuaternion = Quaternion.Euler(lyingDownRotation);

        audioSource = this.GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.LogError("The target flipping script did not find an audio source component");
        }
    }

    public void Standup()
    {
        if (isTransitioningToStand == false && isTransitioningToLay == false)
        {
            isTransitioningToStand = true;
            isTransitioningToLay = false;
            isLayingDown = false;

            startLerpTime = Time.time;

            timer = Random.Range(minTime, maxTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    public void LayDown()
    {
        if (isTransitioningToLay == false && isTransitioningToStand == false)
        {
            isTransitioningToStand = false;
            isTransitioningToLay = true;
            isStanding = false;

            startLerpTime = Time.time;
            timer = 0;
            if (audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    public void OnShot()
    {
        if (isLayingDown == false && isTransitioningToLay == false && isTransitioningToStand == false)
        {
            LayDown();
        }
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
