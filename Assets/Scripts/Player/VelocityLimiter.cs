using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityLimiter : MonoBehaviour
{

    public float fallMultiplier = 2.5f;

    private Rigidbody _playerBody;

    // Start is called before the first frame update
    void Start()
    {
        _playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerBody.velocity.y <= 0)
        {
            _playerBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void SetDrag(float dragAmount)
    {
        _playerBody.drag = dragAmount;
    }

    private float GetDrag()
    {
        return _playerBody.drag;
    }
}
