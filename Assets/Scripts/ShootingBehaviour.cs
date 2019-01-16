﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShootingBehaviour : MonoBehaviour {

    public GameObject bulletHoleDecal;

    Animation fireAnimation;
    LayerMask targetsMask;

    BulletCounter bulletCounter;
    // Use this for initialization
    void Start()
    {
        fireAnimation = GetComponentInChildren<Animation>();
        targetsMask = LayerMask.GetMask("Target");
        if (fireAnimation == null)
        {
            Debug.Log("Animations were not found attached to hte weapon. Check animations");
        }

        bulletCounter = GetComponent<BulletCounter>();
        if(bulletCounter == null)
        {
            Debug.Log("The glock prefab must have the bullet counter script atttached");
        }

        if(bulletHoleDecal == null)
        {
            Debug.Log("The shooting behaviour needs a prefab for it's bullet hole decal which was not assigned");
        }
    }

    // Update is called once per frame
    void Update () {
        Debug.DrawRay(this.transform.position, this.transform.forward);
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
        {
            //only take action if we are not in the anmimation state
            if (fireAnimation.IsPlaying("Fire") == false)
            {
                fireAnimation.Play("Fire");
                bulletCounter.SubtractBullet();
                RaycastHit hitInfo;

                if(Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, Mathf.Infinity))
                {
                    hitInfo.transform.GetComponent<TargetFlipping>().OnShot();
                    //make our forward vector follow the normal of our hit location
                    Instantiate(bulletHoleDecal, hitInfo.point, Quaternion.FromToRotation(-Vector3.forward, hitInfo.normal), hitInfo.transform);
                }


            }
        }
    }
}