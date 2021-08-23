using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurnProvider : MonoBehaviour
{
    [Header("Player Turning")]

    private Rigidbody _body;
    private float smooth = 5.0f;
    private Vector2 noMovementVector = new Vector2(0.0f, 0.0f);
    private InputAction turnAction;

    [SerializeField] private InputActionReference turnReference;
    [SerializeField] private float turnDegrees = 45f;
    [SerializeField] private float debounceTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponentInParent<Rigidbody>();

        //Enable Snap Turn
        turnReference.action.performed += onSnapTurn;
    }

    // Update is called once per frame
    void Update()
    {
        /**Vector2 turnDirection = turnAction.ReadValue<Vector2>();
        if (turnDirection.x >= Vector2.left.x && turnDirection != noMovementVector)
        {
            RotatePlayer(turnDegrees);
        } else if(turnDirection.x >= Vector2.right.x && turnDirection != noMovementVector)
        {
            RotatePlayer(-turnDegrees);
        }**/
    }

    private void onSnapTurn(InputAction.CallbackContext obj)
    {
        Vector2 turnDirection = obj.ReadValue<Vector2>();

        if (turnDirection.x < 0)
        {
            RotatePlayer(-turnDegrees);
        }
        else if (turnDirection.x > 0)
        {
            RotatePlayer(turnDegrees);
        }
    }

    private void RotatePlayer(float playerRotation)
    {
        _body.transform.Rotate(0f, playerRotation, 0f, Space.Self);
    }

}
