using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AirManeuver : MonoBehaviour
{
    public enum hands
    {
        Left,
        Right
    };
    public hands handSide;

    private Rigidbody _playerbody;
    private Transform _playerCamera;

    [Header("Boost Values")]
    [SerializeField] private float forwardBoostAmount = 5f;
    [SerializeField] private float angledBoostAmount = 10f;
    [SerializeField] private float buttonCooldown = 2f;
    [Space]
    [SerializeField] private InputActionProperty boostProperty;
    private float coolDown = 0;

    [Header("Retract Values")]
    [SerializeField] private float retractSpeed = 0.2f;
    [Space]
    [SerializeField] private InputActionProperty retractProperty;
    private ThreadShooter _threadShooter;
    private InputAction retractAction;

    // Start is called before the first frame update
    void Start()
    {
        //Get Starting GameObjects
        _threadShooter = GetComponent<ThreadShooter>();
        _playerbody = GetComponentInParent<Rigidbody>();
        _playerCamera = GameObject.Find("Main Camera").transform;

        // Get Boost Action
        boostProperty.action.performed += OnButtonPress;
        boostProperty.action.canceled += OnButtonRelease;

        // Get Retract Action
        retractAction = retractProperty.action;
        retractAction.Enable();
    }

    void FixedUpdate()
    {
        if (retractAction.ReadValue<float>() == 1)
        {
            _threadShooter.LowerJointMaxDistance(retractSpeed);
        }
    }

    void Update()
    {
        if (coolDown > 0f)
        {
            coolDown -= Time.deltaTime;
        }
    }

    private void OnButtonPress(InputAction.CallbackContext obj)
    {
        if (coolDown > 0f) return;
        Vector3 boostDirection = handSide == hands.Left ? _playerCamera.right : -_playerCamera.right;
        _playerbody.AddForce((_playerCamera.forward * forwardBoostAmount) + (boostDirection * angledBoostAmount), ForceMode.VelocityChange);
    }

    private void OnButtonRelease(InputAction.CallbackContext obj)
    {
        if (coolDown > 0f) return;
        coolDown = buttonCooldown;
    }
}
