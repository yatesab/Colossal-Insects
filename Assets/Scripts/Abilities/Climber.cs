using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Climber : MonoBehaviour
{
    [SerializeField] private Rigidbody playerBody;

    [Space]

    [SerializeField] private InputActionProperty velocityProperty;
    [SerializeField] private InputActionProperty grabProperty;

    [Space]

    [SerializeField] private float reachDistance = 0.1f;
    [SerializeField] private LayerMask grabbableLayer;

    public Vector3 velocity;
    public bool isGrabbing;

    private Rigidbody _body;
    private GameObject _heldObject;
    private FixedJoint _joint1, _joint2;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();

        grabProperty.action.performed += OnGrab;
        grabProperty.action.canceled += OnRelease;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbing || _heldObject)
        {
            playerBody.velocity = -velocityProperty.action.ReadValue<Vector3>() * 2f;
        }
    }

/*    void Climb()
    {
        velocity = velocityProperty.action.ReadValue<Vector3>();

        float drag = 1 / velocity.magnitude + 0.01f;

        drag = Mathf.Clamp(drag, 0.3f, 1f);

        playerBody.AddForce(-velocity * drag, ForceMode.VelocityChange);
    }*/

    // Action functions used for the button pulls
    private void OnGrab(InputAction.CallbackContext obj)
    {
        if (isGrabbing || _heldObject) return;
        
        Collider[] grabbableColliders = Physics.OverlapSphere(transform.position, reachDistance, grabbableLayer);
        if (grabbableColliders.Length < 1) return;

        var objectToGrab = grabbableColliders[0].transform.gameObject;

        var objectBody = objectToGrab.GetComponent<Rigidbody>();

        if(objectBody != null)
        {
            _heldObject = objectBody.gameObject;
        } else
        {
            objectBody = objectToGrab.GetComponentInParent<Rigidbody>();
            if (objectBody != null)
            {
                _heldObject = objectBody.gameObject;
            }
            else
            {
                return;
            }
        }

        isGrabbing = true;

        // Attach joints
        _joint1 = gameObject.AddComponent<FixedJoint>();
        _joint1.connectedBody = objectBody;
        _joint1.breakForce = float.PositiveInfinity;
        _joint1.breakTorque = float.PositiveInfinity;

        _joint1.connectedMassScale = 1;
        _joint1.massScale = 1;
        _joint1.enableCollision = false;
        _joint1.enablePreprocessing = false;

        _joint2 = _heldObject.AddComponent<FixedJoint>();
        _joint2.connectedBody = _body;
        _joint2.breakForce = float.PositiveInfinity;
        _joint2.breakTorque = float.PositiveInfinity;

        _joint2.connectedMassScale = 1;
        _joint2.massScale = 1;
        _joint2.enableCollision = false;
        _joint2.enablePreprocessing = false;
    }

    // Action functions used for the button pulls
    private void OnRelease(InputAction.CallbackContext obj)
    {
        _heldObject = null;
        isGrabbing = false;
        Destroy(_joint1);
        Destroy(_joint2);
    }
}
