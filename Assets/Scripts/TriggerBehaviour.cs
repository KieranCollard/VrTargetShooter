using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TriggerBehaviour : MonoBehaviour {
    Animation fireAnimation;

	// Use this for initialization
	void Start () {
       fireAnimation =  GetComponentInChildren<Animation>();

        if(fireAnimation == null)
        {
            Debug.Log("Animations were not found attached to hte weapon. Check animations");
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(this.transform.position, this.transform.forward);
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            //only take action if we are not in the anmimation state
            if (fireAnimation.IsPlaying("Fire") == false)
            {
                fireAnimation.Play("Fire");
                RaycastHit hitInfo;

                ///TODO layermask this properly so that we can only hit the targets
                if(Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo))
                {
                    hitInfo.transform.SendMessage("OnShot");
                }


            }
        }
    }
}
