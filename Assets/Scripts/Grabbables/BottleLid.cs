using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleLid : GrabbableObject
{
    // Start is called before the first frame update
    private FixedJoint _joint;
    void Start()
    {
        _joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnJointBreak(float breakForce)
    {
        GameManager.Instance.OnLidBroken();
    }

    public override void OnGrab()
    {
        Debug.Log("lid grabbed");
        _joint.breakForce = 1;
    }
}
