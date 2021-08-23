using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRColliderHelper : MonoBehaviour
{
    [SerializeField] private int minHeight = 1;
    [SerializeField] private int maxHeight = 2;

    private XRRig xrRig;
    private CapsuleCollider collider;
    private ActionBasedContinuousMoveProvider moveProvider;
    public bool canJump = true;
    public Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        xrRig = GetComponent<XRRig>();
        collider = GetComponent<CapsuleCollider>();
        moveProvider = GetComponentInChildren<ActionBasedContinuousMoveProvider>();
    }

    void FixedUpdate()
    {
        UpdateCollider();
    }

    /// <summary>
    /// Update the <see cref="body.height"/> and <see cref="body.center"/>
    /// based on the camera's position.
    /// </summary>
    protected virtual void UpdateCollider()
    {
        if (xrRig == null || collider == null)
            return;

        var height = Mathf.Clamp(xrRig.cameraInRigSpaceHeight, minHeight, maxHeight);

        center = xrRig.cameraInRigSpacePos;
        center.y = height / 2f + collider.radius;

        collider.height = height;
        collider.center = center;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            moveProvider.enabled = true;
            canJump = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            moveProvider.enabled = false;
            canJump = false;
        }
    }
}
