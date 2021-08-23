using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveProvider : MonoBehaviour
{
    [Header("Player Movement")]
    private Rigidbody _body;
    private XRColliderHelper _colliderHelper;
    private InputAction moveAction;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float maxVelocityChange = 4f;
    [SerializeField] private InputActionReference moveReference;
    [SerializeField] private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponentInParent<Rigidbody>();
        _colliderHelper = GetComponentInParent<XRColliderHelper>();

        //Enable Move
        moveAction = moveReference.action;
        moveAction.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 targetVelocity;
        if (moveInput != Vector2.zero && _colliderHelper.canJump)
        {
            // Calculate how fast we should be moving based on location
            targetVelocity = new Vector3(moveInput.x, 0, moveInput.y);
            targetVelocity = playerCamera.transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = _body.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            _body.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }
}
