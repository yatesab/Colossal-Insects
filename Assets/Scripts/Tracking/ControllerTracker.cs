using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerTracker : MonoBehaviour
{
    public enum hands
    {
        Left,
        Right
    };
    public hands handSide;

    [Space]
    [SerializeField] private float followSpeed = 50f;
    [SerializeField] private float rotationSpeed = 100f;
    
    [Space]
    
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    private Transform followTarget;
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        // Physics Movement
        followTarget = GameObject.Find("/PlayerRig/Camera Offset/Player_Controller_" + handSide).transform;

        body = GetComponentInChildren<Rigidbody>();

        if (body != null)
        {
            body.maxAngularVelocity = 20f;

            //Teleport Hands
            body.position = followTarget.position;
            body.rotation = followTarget.rotation;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (body != null)
        {
            PhysicsMove();
        }
    }

    private void PhysicsMove()
    {
        //Position
        var positionWithOffset = followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, body.transform.position);
        body.velocity = (positionWithOffset - body.transform.position).normalized * (followSpeed * distance);

        //Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotationSpeed);
    }
}
