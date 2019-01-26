using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

//keep attached prefab in sync with the attached controller
//assumes that the controller is a child of the main OVR rig
public class ControllerTrack : MonoBehaviour {
    //object to use when converting from local headset space to worldspace
    public Transform referenceObject;
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
		if(referenceObject == null)
        {
            Debug.LogError("The Controller tracker script did not have an object set to use for word space conversions");
        }
	}
	
	// Update is called once per frame
	void Update () {

        
        if(Application.platform ==  RuntimePlatform.WindowsEditor) //assume dev computer
        {
            this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            if(Input.GetKeyDown(KeyCode.W))
            {
                this.transform.Rotate(new Vector3(1, 0, 0), 15);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                this.transform.Rotate(new Vector3(1, 0, 0), -15);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                this.transform.Rotate(new Vector3(0, 1, 0), 15);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                this.transform.Rotate(new Vector3(0, 1, 0), -15);
            }
        }
        else // no direct  check for oculus
        {
            //updte the controller
            OVRInput.Update();

            this.transform.position = referenceObject.TransformPoint(InputTracking.GetLocalPosition(XRNode.RightHand));
            this.transform.rotation = referenceObject.rotation * InputTracking.GetLocalRotation(XRNode.RightHand);
        }

    }

    private void FixedUpdate()
    {
        OVRInput.FixedUpdate();
    }
}
