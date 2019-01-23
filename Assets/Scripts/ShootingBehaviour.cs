using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShootingBehaviour : MonoBehaviour
{

    public GameObject bulletHoleDecal;
    public GameObject scoreManager;

    Animation fireAnimation;
    LayerMask targetsMask;

    BulletCounter bulletCounter;
    ScoreManager scoreManagerScript;
    // Use this for initialization
    void Start()
    {
        fireAnimation = GetComponentInChildren<Animation>();
        targetsMask = LayerMask.GetMask("Target");
        if (fireAnimation == null)
        {
            Debug.LogError("Animations were not found attached to hte weapon. Check animations");
        }

        bulletCounter = GetComponent<BulletCounter>();
        if (bulletCounter == null)
        {
            Debug.LogError("The glock prefab must have the bullet counter script atttached");
        }
        if (bulletHoleDecal == null)
        {
            Debug.LogError("The shooting behaviour needs a prefab for it's bullet hole decal which was not assigned");
        }
        if(scoreManager == null)
        {
            Debug.LogError("The shooting behaviour of " + this.transform + " needs to have the score manager object assigned");
        }
        else
        {
            scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward);
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
        {

            //only take action if we are not in the anmimation state
            if (fireAnimation.IsPlaying("Fire") == false)
            {
                fireAnimation.Play("Fire");
                bulletCounter.SubtractBullet();
                RaycastHit hitInfo;

                if (Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, Mathf.Infinity, LayerMask.GetMask("BulletInteractable")))
                {
                    TargetFlipping targetScript = hitInfo.transform.GetComponent<TargetFlipping>();
                    if (targetScript != null)
                    {
                        targetScript.OnShot();
                        BoxCollider boxCollider = hitInfo.collider as BoxCollider;
                        if (boxCollider == null)
                        {
                            Debug.LogWarning("An interactable with the targetflipping script was hit whcih did not have abox collider. The script can not handle other collider types");
                        }
                        else {
                            CalculateAndAddScore(hitInfo.point, boxCollider);
                        }
                    }
                    GameObject decal = Instantiate(bulletHoleDecal, hitInfo.point, Quaternion.FromToRotation(-Vector3.forward, hitInfo.normal));
                    Vector3 decalScale = decal.transform.localScale;
                    decal.transform.parent = hitInfo.transform;
                    //did we scale larger than desired when attaching to parent
                    if (decal.transform.localScale.x > decal.transform.localScale.x * 4)
                    {
                        decal.transform.localScale = decalScale;
                    }
                }


            }
        }
    }

    void CalculateAndAddScore(Vector3 hitPoint, BoxCollider hitCollider)
    {
        //work out a percentage away from center of the target and scale the score based on this.
        float maximumDistance = hitCollider.size.x /2;
        Debug.Log("max distance " + maximumDistance);
        float distance = (hitPoint - hitCollider.center).magnitude;
        Debug.Log("distance " + distance);
        //too far away means a missed shot
        if(distance < maximumDistance)
        {
            Debug.Log("expected percentage " + ((maximumDistance -distance) / maximumDistance));
            Debug.Log(" score value expected " + scoreManagerScript.maximumScore * ((maximumDistance -distance) / maximumDistance));
            uint score = System.Convert.ToUInt32(Mathf.CeilToInt(scoreManagerScript.maximumScore * ((maximumDistance - distance) / maximumDistance)));
            Debug.Log("Score value is " + score);
            scoreManagerScript.AddScore(score);
        }
    }
}
