using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ThreadShooter : MonoBehaviour
{
    public enum hands
    {
        Left,
        Right
    };
    public hands handSide;
    private ActionBasedController _controller;
    private GameObject _playerRig;
    private GameObject silkwormModel;

    [Header("Joint Values")]
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private float startingTension = 0.9f;
    [SerializeField] private float spring = 10f;
    [SerializeField] private float damper = 6f;
    private RaycastHit controllerRaycastHit;
    private float distanceFromPoint;
    private Vector3 grapplePoint;
    private Rigidbody grappleBody;
    private Transform grappleTransform;
    private SpringJoint joint;

    [Header("Thread Values")]
    [SerializeField] private float lineWidth = 0.01f;
    private LineRenderer threadRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _playerRig = GameObject.Find("/PlayerRig");
        silkwormModel = GameObject.Find("Silkworm " + handSide);

        threadRenderer = GetComponent<LineRenderer>();

        _controller = GetComponent<ActionBasedController>();
        _controller.activateAction.action.performed += OnTriggerPull;
        _controller.activateAction.action.canceled += OnTriggerDeactivate;
    }

    void LateUpdate()
    {
        if (joint != null && threadRenderer != null)
        {
            DrawThread();
        }
    }

    // Action functions used for the button pulls
    private void OnTriggerPull(InputAction.CallbackContext obj)
    {
        CreateForwardRaycast();
        if (controllerRaycastHit.collider)
        {
            CreateThread();
        }
    }

    private void OnTriggerDeactivate(InputAction.CallbackContext obj)
    {
        if (joint != null)
        {
            DestroyThread();
        }
    }

    // Internal Functions used for making and destorying the thread
    private void CreateThread()
    {
        // Pull all raycastHit values
        grapplePoint = controllerRaycastHit.point;
        grappleBody = controllerRaycastHit.rigidbody;
        grappleTransform = controllerRaycastHit.transform;

        // Create Spring Joint on PlayerRig & Line Renderer on PlayerModel
        joint = _playerRig.AddComponent<SpringJoint>();
        threadRenderer = silkwormModel.AddComponent<LineRenderer>();

        // Configure Line Renderer
        threadRenderer.startWidth = lineWidth;
        threadRenderer.endWidth = lineWidth;

        // Set up player anchor and object anchor
        joint.autoConfigureConnectedAnchor = false;
        joint.enableCollision = true;
        joint.anchor = _playerRig.transform.InverseTransformPoint(silkwormModel.transform.position);

        if (grappleBody != null)
        {
            joint.connectedAnchor = grappleTransform.InverseTransformPoint(grapplePoint);
            joint.connectedBody = grappleBody;
        } else
        {
            joint.connectedAnchor = grapplePoint;

        }

        // Create initial maxDistance for joint and other spring settings
        float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint);
        joint.maxDistance = distanceFromPoint * startingTension;
        joint.massScale = _playerRig.GetComponent<Rigidbody>().mass;
        joint.spring = spring;
        joint.damper = damper;
    }

    private void DestroyThread()
    {
        // Destory Line Renderer
        Destroy(joint);
        Destroy(threadRenderer);
    }

    private void DrawThread()
    {
        // Set the start point at the controller position
        threadRenderer.SetPosition(0, silkwormModel.transform.position);

        // Set other position at rigidbody or grapple point depending
        if (joint.connectedBody != null)
        {
            threadRenderer.SetPosition(1, grappleTransform.TransformPoint(joint.connectedAnchor));
        }
        else
        {
            threadRenderer.SetPosition(1, grapplePoint);                                                          
        }
    }

    private void CreateForwardRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out controllerRaycastHit, maxDistance);
    }

    // Utility functions used by other scripts
    public void LowerJointMaxDistance(float distanceSubtract)
    {
        if (joint == null || joint.maxDistance < 0.5f) return;
        joint.maxDistance -= distanceSubtract;
    }


}
