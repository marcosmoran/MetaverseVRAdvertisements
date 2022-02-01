using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PressableButton : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent onPressed, onReleased;
    [SerializeField] private float thresh = 0.1f;
    [SerializeField] private float deadzone = 0.025f;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;
    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPressed != true && GetValue() + thresh >= 1)
        {
            Pressed();
        }

        if (_isPressed && GetValue() + thresh <= 0)
        {
            Released();
        }
    }

    void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("released");
    }

    float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;

        if (Math.Abs(value) < deadzone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1, 1);
    }
}
