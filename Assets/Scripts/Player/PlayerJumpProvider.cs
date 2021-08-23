using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpProvider : MonoBehaviour
{
    [Header("Player Jump")]

    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float angledForce = 50f;
    private Rigidbody _body;
    private XRColliderHelper _colliderHelper;

    [SerializeField] private InputActionReference leftJumpReference;
    private InputAction leftJumpButton;

    [SerializeField] private InputActionReference rightJumpReference;
    private InputAction rightJumpButton;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponentInParent<Rigidbody>();
        _colliderHelper = GetComponentInParent<XRColliderHelper>();

        // Setup Left Leg Jump
        leftJumpButton = leftJumpReference.action;
        leftJumpButton.performed += OnLeftJump;

        // Setup Right Leg Jump
        rightJumpButton = rightJumpReference.action;
        rightJumpButton.performed += OnRightJump; 
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnLeftJump(InputAction.CallbackContext obj)
    {
        if (!_colliderHelper.canJump) return;
        _body.AddForce((Vector3.up * jumpForce) + (_body.transform.right * angledForce));
    }

    private void OnRightJump(InputAction.CallbackContext obj)
    {
        if (!_colliderHelper.canJump) return;
        _body.AddForce((Vector3.up * jumpForce) + (-_body.transform.right * angledForce));
    }
}
