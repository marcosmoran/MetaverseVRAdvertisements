using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rigidbody;
    public bool grabbed = false;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnGrab()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        grabbed = true;
    }
}
