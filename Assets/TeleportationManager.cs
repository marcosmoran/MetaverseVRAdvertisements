using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private InputActionAsset _actionAsset;
    [SerializeField] private XRRayInteractor _rayInteractor;
    [SerializeField] private TeleportationProvider _teleportationProvider;
    private InputAction _thumbstick;
    private bool _isActive = false;
    void Start()
    {
        _rayInteractor.enabled = false;
        var activate = _actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;
        var cancel = _actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;
        _thumbstick = _actionAsset.FindActionMap("XRI LeftHand").FindAction("Move");
        _thumbstick.Enable();

    }

    private void OnTeleportActivate(InputAction.CallbackContext obj)
    {
        _rayInteractor.enabled = true;
        _isActive = true;
    }
    private void OnTeleportCancel(InputAction.CallbackContext obj)
    {
      DisactivateTeleport();
    }

    void DisactivateTeleport()
    {
        _rayInteractor.enabled = false;
        _isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive) return;
        if (_thumbstick.triggered) return;
        if (!_rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            DisactivateTeleport();
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point
        };
        _teleportationProvider.QueueTeleportRequest(request);
        DisactivateTeleport();
    }
}
