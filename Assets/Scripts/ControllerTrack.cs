﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

//keep attached prefab in sync with the attached controller
//assumes that the controller is a child of the main OVR rig
public class ControllerTrack : MonoBehaviour {

    OVRInput.Controller controller;
    // Use this for initialization
    void Start () {
        
        if(OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
        {
            controller = OVRInput.GetActiveController();
        }
        else
        {
            Debug.Log("The controller was not detected. Check controller is connected and restart");
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        
        if(Application.platform ==  RuntimePlatform.WindowsEditor) //assume dev computer
        {
            this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }
        else // no direct  check for oculus
        {
            //updte the controller
            OVRInput.Update();

            this.transform.position = this.transform.parent.TransformPoint(InputTracking.GetLocalPosition(XRNode.RightHand));
            this.transform.rotation = this.transform.parent.rotation * InputTracking.GetLocalRotation(XRNode.RightHand);
        }

    }

    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();
    }
}
